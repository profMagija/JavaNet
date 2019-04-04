using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;

namespace JavaNet
{
    class MethodGenerator
    {
        public static MethodBody GenerateMethod(MethodDefinition md, JavaMethodInfo mi, CodeAttribute ca, CpInfo[] cp)
        {

            var mg = new MethodGenerator(JavaAssemblyBuilder.Instance, md, mi, ca, cp);


            //Console.WriteLine(md.FullName);

            //foreach (var entry in ca.ExceptionTable)
            //{

            //    Console.WriteLine(" try {{ {0:X4} .. {1:X4} }} catch ({3}) {{ {2:X4} }}", entry.StartPc, entry.EndPc, entry.HandlerPc, entry.CatchType != 0 ? cp[entry.CatchType].Represent() : "*");

            //}

            mg.DivideIntoOps();

            if (mg._ops.Any(x => x?.Instr == JavaInstruction.invokedynamic))
                throw new JavaNetException(JavaNetException.ReasonType.ClassLoad, "invokedynamic");

            //foreach (var op in mg._ops)
            //{
            //    if (op != null)
            //        Console.WriteLine(op);
            //}

            mg.BlockAnalyzer();

            //foreach (var block in mg._blocks.Values.OrderBy(x => x.JavaOffset))
            //{
            //    Console.WriteLine("    {0}: [{1} ops]", block, block.JavaOps.Count);

            //    foreach (var op in block.JavaOps)
            //    {
            //        Console.WriteLine("        {0}", op);
            //    }
            //}

            mg._body = new MethodBody(mg._md);

            mg.ValueAnalyzer();

            //foreach (var block in mg._blocks.Values.OrderBy(x => x.JavaOffset))
            //{
            //    Console.WriteLine("    {0}: [{1} actions]", block, block.Actions.Count);
            //    if (!block.Generated)
            //    {
            //        Console.WriteLine("        [NOT GENERATED]");
            //    }
            //    foreach (var action in block.Actions)
            //    {
            //        Console.WriteLine("        {0}", action);
            //    }
            //}

            var values = mg._blocks.Values.SelectMany(x => x.Actions.SelectMany(a => a.RequiredValues)).Distinct().Count();

            mg.LocalAnalyzer();

            foreach (var local in mg._locals)
            {
                mg._body.Variables.Add(local);
            }

            mg.HandlerAnalyzer();

            mg.ActionGenerator();


            //foreach (var entry in mg._ca.ExceptionTable)
            //{
            //    //var (handlerIndex, handlerBlocks) = mg._handlers.First(x => x.Value[0].JavaOffset == entry.HandlerPc);
            //    //body.ExceptionHandlers.Add(new ExceptionHandler(ExceptionHandlerType.Catch)
            //    //{
            //    //    TryStart = mg._blocks[entry.StartPc].FirstNetOp,
            //    //    TryEnd = mg._blocks[entry.EndPc].FirstNetOp,
            //    //    HandlerStart = handlerBlocks[0].FirstNetOp,
            //    //    HandlerEnd = mg._blocks[handlerBlocks.Last().JavaOffset + handlerBlocks.Last().JavaLength].FirstNetOp,
            //    //    CatchType = handlerBlocks[0].ExceptionValue.ActualType
            //    //});
            //    Debug.Assert(mg._blocks[entry.EndPc].JavaOps[0].Instr == JavaInstruction.@goto);
            //}

            mg._body.Optimize();

            return mg._body;
        }

        private void HandlerAnalyzer()
        {
            var handlerNum = 0;
            foreach (var entry in _ca.ExceptionTable.OrderBy(x => x.StartPc).ThenBy(x => x.EndPc))
            {
                handlerNum++;
                foreach (var (_, block) in _blocks.Where(x => x.Key >= entry.StartPc && x.Key < entry.EndPc))
                {
                    block.HandlerNum = handlerNum;
                }
            }
        }

        private readonly JavaAssemblyBuilder _ab;
        private readonly MethodDefinition _md;
        private readonly JavaMethodInfo _mi;
        private readonly CodeAttribute _ca;
        private readonly CpInfo[] _cp;

        /// <summary>
        /// Array of Java instructions. Each instruction is in it's original offset, and
        /// some locations between the instructions are null.
        /// </summary>
        private JavaOp[] _ops;

        /// <summary>
        /// Offsets that are targets of jumps
        /// </summary>
        private bool[] _jumpTarget;

        /// <summary>
        /// Dictionary of methods ActionBlocks, keyed by their Java Offset
        /// </summary>
        private Dictionary<int, ActionBlock> _blocks;

        /// <summary>
        /// Index of the calculated value used
        /// </summary>
        private int _valueIndex = 0;

        private MethodGenerator(JavaAssemblyBuilder ab, MethodDefinition md, JavaMethodInfo mi, CodeAttribute ca,
            CpInfo[] cp)
        {
            _ab = ab;
            _md = md;
            _mi = mi;
            _ca = ca;
            _cp = cp;
        }
        /// <summary>
        /// Divides the Java bytecode into individual instructions, with their arguments, and places
        /// the result into <see cref="_ops"/>
        /// </summary>
        private void DivideIntoOps()
        {
            var code = _ca.Code;
            _ops = new JavaOp[code.Length];
            _jumpTarget = new bool[code.Length];
            var pc = 0;

            while (pc < code.Length)
            {
                var start = pc;
                var instr = (JavaInstruction)code[pc++];
                switch (instr.ArgsType())
                {
                    // instructions without arguments
                    case JavaInstrArguments.Niladic:
                    {
                        _ops[start] = new JavaOp.Niladic(start, instr);
                        break;
                    }

                    case JavaInstrArguments.LocalVarIndex:
                    {
                        var localIndex = code[pc++];
                        _ops[start] = new JavaOp.LocalVarIndex(start, instr, localIndex);
                        break;
                    }
                    case JavaInstrArguments.AType:
                        _ops[start] = new JavaOp.AType(start, instr, (ArrayType) code[pc++]);
                        break;
                    case JavaInstrArguments.Byte:
                        _ops[start] = new JavaOp.Numeric(start, instr, (sbyte)code[pc++]);
                        break;
                    case JavaInstrArguments.BranchOffset:
                    {
                        var jumpTarget = I2(code, ref pc) + start;
                        _jumpTarget[jumpTarget] = true;
                        _ops[start] = new JavaOp.Branch(start, instr, jumpTarget);
                        break;
                    }
                    case JavaInstrArguments.Short:
                        _ops[start] = new JavaOp.Numeric(start, instr, I2(code, ref pc));
                        break;
                    case JavaInstrArguments.Iinc:
                        _ops[start] = new JavaOp.Iinc(start, instr, code[pc++], (sbyte) code[pc++]);
                        break;
                    case JavaInstrArguments.WidePrefix:
                        var widenedInstr = (JavaInstruction) code[pc++];
                        if (widenedInstr == JavaInstruction.iinc)
                            _ops[start] = new JavaOp.Iinc(start, widenedInstr, I2(code, ref pc), I2(code, ref pc));
                        else 
                            _ops[start] = new JavaOp.LocalVarIndex(start, widenedInstr, I2(code, ref pc));
                        break;
                    case JavaInstrArguments.CpIndex:
                        _ops[start] = new JavaOp.ConstPool(start, instr, _cp[I2(code, ref pc)]);
                        break;

                    case JavaInstrArguments.ByteCpIndex:
                        _ops[start] = new JavaOp.ConstPool(start, instr, _cp[code[pc++]]);
                        break;
                    case JavaInstrArguments.MultiNewArray:
                        _ops[start] = new JavaOp.MultiNewArray(start, instr, _cp[I2(code, ref pc)], code[pc++]);
                        break;
                    case JavaInstrArguments.WideBranchOffset:
                        _ops[start] = new JavaOp.Branch(start, instr, I4(code, ref pc));
                        break;
                    case JavaInstrArguments.InvokeDynamic:
                        _ops[start] = new JavaOp.ConstPool(start, instr, _cp[I2(code, ref pc)]);
                        pc += 2;
                        break;
                    case JavaInstrArguments.InvokeInterface:
                        _ops[start] = new JavaOp.ConstPool(start, instr, _cp[I2(code, ref pc)]);
                        pc += 2;
                        break;
                    case JavaInstrArguments.LookupSwitch:
                    {
                        pc = ((pc - 1) | 3) + 1;
                        var defOffset = I4(code, ref pc) + start;
                        _jumpTarget[defOffset] = true;
                        var nPairs = I4(code, ref pc);

                        var mtp = new KeyValuePair<int, int>[nPairs];
                        for (var i = 0; i < nPairs; i++)
                        {
                            mtp[i] = new KeyValuePair<int, int>(I4(code, ref pc), I4(code, ref pc) + start);
                            _jumpTarget[mtp[i].Value] = true;
                        }

                        _ops[start] = new JavaOp.LookupSwitch(start, instr, defOffset, mtp);

                        break;
                    }
                    case JavaInstrArguments.TableSwitch:
                    {
                        pc = ((pc - 1) | 3) + 1;
                        var defOffset = I4(code, ref pc) + start;
                        _jumpTarget[defOffset] = true;
                        var low = I4(code, ref pc);
                        var high = I4(code, ref pc);

                        var targets = new int[high - low + 1];
                        for (var i = 0; i < targets.Length; i++)
                        {
                            targets[i] = I4(code, ref pc) + start;
                            _jumpTarget[targets[i]] = true;
                        }

                        _ops[start] = new JavaOp.TableSwitch(start, instr, defOffset, low, high, targets);

                        break;
                    }
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            foreach (var entry in _ca.ExceptionTable)
            {
                _jumpTarget[entry.StartPc] = true;
                _jumpTarget[entry.EndPc] = true;
                _jumpTarget[entry.HandlerPc] = true;
            }
        }
        
        /// <summary>
        /// Divides the method into ActionBlocks
        /// </summary>
        private void BlockAnalyzer()
        {
            _blocks = new Dictionary<int, ActionBlock>();

            ActionBlock curBlock = null;

            for (var i = 0; i < _ops.Length; i++)
            {
                if (_ops[i] == null)
                    continue;
                if (curBlock == null || _jumpTarget[i])
                {
                    if (curBlock != null)
                        curBlock.JavaLength = i - curBlock.JavaOffset;
                    curBlock = new ActionBlock(i);
                    _blocks.Add(i, curBlock);
                }
                curBlock.JavaOps.Add(_ops[i]);
                
            }
        }

        private List<ArgumentValue> _argumentValues = new List<ArgumentValue>();

        private void ValueAnalyzer()
        {
            var startingBlock = _blocks[0];
            var paramTypes = _md.Parameters.SelectMany(x =>
            {
                if (x.ParameterType.FullName == typeof(long).FullName || x.ParameterType.FullName == typeof(double).FullName)
                    return new[] {x, null};
                return new[] {x};
            });
            //Debug.Assert(_md.Name != "getOrDefault_defaultImpl");
            if (_body.ThisParameter != null)
                paramTypes = new[] {_body.ThisParameter}.Concat(paramTypes);

            var paramTypesArray = paramTypes.ToArray();

            _argumentValues = paramTypesArray.Select(pt => pt == null ? null : new ArgumentValue(pt.ParameterType, pt)).ToList();

            startingBlock.StartingState = new JavaState(
                ImmutableStack<JavaValue>.Empty,
                _argumentValues.Select((tr, i) => (tr, i)).ToImmutableDictionary(
                    xi => xi.i,
                    xi => (JavaValue) xi.tr
                )
            );

            if (_md.Name == "forOutputStreamWriter")
                ;

            foreach (var entry in _ca.ExceptionTable)
            {
                _blocks[entry.EndPc].HandlersBeforeMe.Add(entry);
            }

            var curBlock = _blocks.FirstOrDefault(x => !x.Value.Generated && x.Value.StartingState != null).Value;
            var states = new List<(JavaOp, JavaState)>();
            while (curBlock != null)
            {

                var curState = curBlock.StartingState;

                Debug.Assert(curState.Locals.Values.All(x => x?.IsConst != true));
                Debug.Assert(curState.Stack.All(x => x?.IsConst != true));

                foreach (var entry in _ca.ExceptionTable)
                {
                    if (entry.StartPc != curBlock.JavaOffset)
                        continue;
                    var handlerBlock = _blocks[entry.HandlerPc];
                    var catchType = entry.CatchType != null ? JavaAssemblyBuilder.Instance.ResolveTypeReference(entry.CatchType.Name) : null;
                    handlerBlock.ExceptionValue = new CalculatedValue(catchType ?? JavaAssemblyBuilder.Instance.Import(typeof(Exception)));
                    IEnumerable<MethodAction> acts;
                    (acts, curState) = curState.Unconst();
                    curBlock.Actions.AddRange(acts);
                    if (handlerBlock.StartingState == null)
                    {
                        handlerBlock.StartingState = new JavaState(ImmutableStack.Create<JavaValue>(handlerBlock.ExceptionValue), curState.Locals);
                    }
                    else
                    {
                        // we need to map our state to that of the jump block
                        if (handlerBlock.Generated)
                            handlerBlock.Generated = false; // regenerate 
                        JavaState newState;
                        (acts, newState, handlerBlock.StartingState) = new JavaState(ImmutableStack.Create<JavaValue>(handlerBlock.ExceptionValue), curState.Locals).MapTo(handlerBlock.StartingState);
                        curState = new JavaState(curState.Stack, newState.Locals);
                        curBlock.Actions.AddRange(acts);
                    }
                }

                foreach (var op in curBlock.JavaOps)
                {
                    states.Add((op, curState));
                    if (curState == null)
                    {
                        //Console.WriteLine("Unreachable instruction: {0}", op);
                        break;
                    }

                    var jumpTargets = op.JumpTargets.ToArray();
                    IEnumerable<MethodAction> acts;

                    var oldState = curState;

                    (curState, acts) = op.ActUpon(curState, _blocks);

                    if (jumpTargets.Length != 0)
                    {
                        // this is some form of a jump instruction
                        // we need to un-const all of the values
                        // so that no constants are carried cross-block
                        var targetState = curState ?? oldState;
                        IEnumerable<MethodAction> jumpActs;
                        (jumpActs, targetState) = targetState.Unconst();
                        curBlock.Actions.AddRange(jumpActs);

                        foreach (var target in jumpTargets)
                        {
                            if (_blocks[target].StartingState == null)
                            {
                                // block is not yet 'discovered', so we just set it's starting state
                                _blocks[target].StartingState = targetState;
                            }
                            else
                            {
                                // we need to map our state to that of the jump block
                                (jumpActs, targetState, _blocks[target].StartingState) = targetState.MapTo(_blocks[target].StartingState);
                                curBlock.Actions.AddRange(jumpActs);
                            }
                        }
                    }

                    curBlock.Actions.AddRange(acts);
                }

                if (curState != null && _blocks.TryGetValue(curBlock.JavaOffset + curBlock.JavaLength, out var nextBlock))
                {
                    curBlock.ProceedsToNext = true;

                    IEnumerable<MethodAction> acts;
                    (acts, curState) = curState.Unconst();
                    curBlock.Actions.AddRange(acts);

                    if (nextBlock.StartingState == null)
                        nextBlock.StartingState = curState;
                    else
                    {
                        (acts, curState, nextBlock.StartingState) = curState.MapTo(nextBlock.StartingState);
                        curBlock.Actions.AddRange(acts);
                    }
                }

                curBlock.Generated = true;
                curBlock = _blocks.FirstOrDefault(x => !x.Value.Generated && x.Value.StartingState != null).Value;
            }

            if (_md.Name == "defaultCharset")
                ;

            if (_blocks.Any(x => !x.Value.Generated))
            {
                //throw new Exception("Orphaned block: " + _blocks.FirstOrDefault(x => !x.Value.Generated));
            }
        }

        private List<VariableDefinition> _locals;
        private Dictionary<int, List<ActionBlock>> _handlers;
        private MethodBody _body;

        private void LocalAnalyzer()
        {
            _locals = new List<VariableDefinition>();

            foreach (var argumentValue in _argumentValues)
            {
                if (argumentValue?.Backing == null) continue;
                argumentValue.VarDef = new VariableDefinition(argumentValue.ActualType);
                _locals.Add(argumentValue.VarDef);
            }

            foreach (var value in _blocks.Where(x => x.Value.Generated).SelectMany(x => x.Value.Actions).SelectMany(x => x.RequiredValues))
            {
                if (value is ArgumentValue av)
                {
                }
                else if (value is CalculatedValue cv && cv.VarDef == null)
                {
                    cv.VarDef = new VariableDefinition(cv.ActualType);
                    _locals.Add(cv.VarDef);
                }
            }

            foreach (var value in _blocks.Values.Select(x => x.ExceptionValue).Where(v => v != null))
            {
                if (value.VarDef == null)
                {
                    value.VarDef = new VariableDefinition(value.ActualType);
                    _locals.Add(value.VarDef);
                }
            }

            if (_md.Name == "defaultCharset")
                ;
        }

        private void ActionGenerator()
        {

            foreach (var (_, block) in _blocks)
            {
                foreach (var action in block.Actions)
                {
                    block.NetOps.AddRange(action.Generate(block));
                }
            }

            var ilp = _body.GetILProcessor();

            ilp.Append(Instruction.Create(OpCodes.Ldstr, _md.FullName));
            ilp.Append(Instruction.Create(OpCodes.Call, JavaAssemblyBuilder.Instance.Import(typeof(Console).GetMethod("WriteLine", new[] {typeof(object)}))));

            foreach (var av in _argumentValues)
            {
                if (av?.Backing == null) continue;
                ilp.Append(Instruction.Create(OpCodes.Ldarg, av.Param));
                ilp.Append(Instruction.Create(OpCodes.Stloc, av.VarDef));
            }

            foreach (var (_, block) in _blocks.OrderBy(x => x.Key))
            {
                ExceptionHandler lastHandler = null;
                foreach (var entry in block.HandlersBeforeMe)
                {
                    if (lastHandler == null)
                        ilp.Append(Instruction.Create(OpCodes.Leave, block.GetFirstNetOp()));

                    var handlerBlock = _blocks[entry.HandlerPc];
                    if (!handlerBlock.Generated)
                        continue;

                    Debug.Assert(_blocks[entry.EndPc] == block);

                    var storeValue = handlerBlock.ExceptionValue.StoreValue();
                    var start = Instruction.Create(OpCodes.Leave, handlerBlock.GetFirstNetOp());


                    foreach (var instruction in storeValue)
                    {
                        ilp.Append(instruction);
                    }
                    ilp.Append(start);

                    var exceptionHandler = new ExceptionHandler(ExceptionHandlerType.Catch)
                    {
                        TryStart = _blocks[entry.StartPc].GetFirstNetOp(),
                        TryEnd = lastHandler?.TryEnd ?? storeValue[0],
                        HandlerStart = storeValue[0],
                        HandlerEnd = block.GetFirstNetOp(),
                        CatchType = JavaAssemblyBuilder.Instance.ResolveTypeReference(entry.CatchType?.Name ?? "java/lang/Throwable")
                    };
                    _body.ExceptionHandlers.Add(exceptionHandler);
                    if (lastHandler != null)
                        lastHandler.HandlerEnd = storeValue[0];
                    lastHandler = exceptionHandler;
                }

                //if (block.ExceptionValue != null)
                //{
                //    foreach (var instruction in block.ExceptionValue.StoreValue())
                //    {
                //        ilp.Append(instruction);
                //    }
                //}

                foreach (var instruction in block.NetOps)
                {
                    ilp.Append(instruction);
                }
            }
        }

        private static short I2(byte[] code, ref int pc)
        {
            return (short)((code[pc++] << 8) | code[pc++]);
        }

        private static int I4(byte[] code, ref int pc)
        {
            return (code[pc++] << 24) | (code[pc++] << 16) | (code[pc++] << 8) | code[pc++];
        }

    }

    class ActionBlock
    {
        public int JavaOffset { get; }
        public int JavaLength { get; set; }
        public List<JavaOp> JavaOps { get; } = new List<JavaOp>();

        public List<Instruction> NetOps { get; } = new List<Instruction>();

        public List<ExceptionTableEntry> HandlersBeforeMe { get; } = new List<ExceptionTableEntry>();
        public bool Generated { get; set; } = false;
        public JavaState StartingState { get; set; }

        public List<MethodAction> Actions { get; } = new List<MethodAction>();
        public bool ProceedsToNext { get; set; }

        public int HandlerNum { get; set; }

        public Instruction GetFirstNetOp()
        {
            if (NetOps.Count == 0)
                NetOps.Add(Instruction.Create(OpCodes.Nop));
            return NetOps[0];
        }

        public CalculatedValue ExceptionValue { get; set; }

        public ActionBlock(int javaOffset)
        {
            JavaOffset = javaOffset;
        }

        public override string ToString()
        {
           return $"actionBlock at 0x{JavaOffset:X4}";
        }
    }
}
