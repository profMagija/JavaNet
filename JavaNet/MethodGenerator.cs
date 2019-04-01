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

            _body = new MethodBody(mg._md);

            foreach (var local in mg._locals)
            {
                _body.Variables.Add(local);
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

            _body.Optimize();

            return _body;
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
                        _ops[start] = new JavaOp.Niladic(start, instr);
                        break;

                    case JavaInstrArguments.LocalVarIndex:
                        _ops[start] = new JavaOp.LocalVarIndex(start, instr, code[pc++]);
                        break;
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
                        _ops[start] = new JavaOp.InvokeInterface(start, instr, _cp[I2(code, ref pc)], code[pc++]);
                        pc++;
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

        private void ValueAnalyzer()
        {
            var startingBlock = _blocks[0];
            var paramTypes = _md.Parameters.SelectMany(x =>
            {
                if (x.ParameterType.FullName == typeof(long).FullName || x.ParameterType.FullName == typeof(double).FullName)
                    return new[] {x, null};
                return new[] {x};
            });
            if (!_md.IsStatic)
                paramTypes = new[] {_md.Body.ThisParameter}.Concat(paramTypes);

            startingBlock.StartingState = new JavaState(
                ImmutableStack<JavaValue>.Empty,
                paramTypes.Select((tr, i) => (tr, i)).ToImmutableDictionary(
                    xi => xi.i,
                    xi => xi.tr != null ? (JavaValue) new ArgumentValue(xi.tr.ParameterType, xi.tr) : null
                )
            );

            foreach (var entry in _ca.ExceptionTable)
            {
                _blocks[entry.StartPc].HandlerBlock.Add((
                    _blocks[entry.HandlerPc],
                    entry.CatchType == 0 ? null : JavaAssemblyBuilder.Instance.ResolveTypeReference(((ClassInfo) _cp[entry.CatchType]).Name)
                ));
            }

            var curBlock = _blocks.FirstOrDefault(x => !x.Value.Generated && x.Value.StartingState != null).Value;
            var states = new List<(JavaOp, JavaState)>();
            while (curBlock != null)
            {
                var curState = curBlock.StartingState;

                if (curBlock.ExceptionValue != null)
                {
                    curBlock.NetOps.AddRange(curBlock.ExceptionValue.StoreValue());
                }

                foreach (var (handlerBlock, catchType) in curBlock.HandlerBlock)
                {
                    handlerBlock.ExceptionValue = new CalculatedValue(catchType ?? JavaAssemblyBuilder.Instance.TypeSystem.Object);
                    handlerBlock.StartingState = new JavaState(ImmutableStack.Create<JavaValue>(handlerBlock.ExceptionValue), curState.Locals);
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
                                _blocks[target].StartingState = curState;
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
                    if (nextBlock.StartingState == null)
                        nextBlock.StartingState = curState;
                    else
                    {
                        IEnumerable<MethodAction> acts;
                        (acts, curState, nextBlock.StartingState) = curState.MapTo(nextBlock.StartingState);
                        curBlock.Actions.AddRange(acts);
                    }
                }

                curBlock.Generated = true;
                curBlock = _blocks.FirstOrDefault(x => !x.Value.Generated && x.Value.StartingState != null).Value;
            }

            if (_blocks.Any(x => !x.Value.Generated))
            {
                //throw new Exception("Orphaned block: " + _blocks.FirstOrDefault(x => !x.Value.Generated));
            }
        }

        private List<VariableDefinition> _locals;
        private Dictionary<int, List<ActionBlock>> _handlers;
        private static MethodBody _body;

        private void LocalAnalyzer()
        {
            _locals = new List<VariableDefinition>();

            foreach (var value in _blocks.Where(x => x.Value.Generated).SelectMany(x => x.Value.Actions).SelectMany(x => x.RequiredValues))
            {
                if (value is CalculatedValue cv && cv.VarDef == null)
                {
                    cv.VarDef = new VariableDefinition(cv.ActualType);
                    _locals.Add(cv.VarDef);
                }
            }


        }

        private void ActionGenerator()
        {
            foreach (var (_,  block )in _blocks)
            {
                block.NetOps.Add(Instruction.Create(OpCodes.Nop));
            }

            foreach (var (_, block) in _blocks)
            {
                foreach (var action in block.Actions)
                {
                    block.NetOps.AddRange(action.Generate(block));
                }
            }

            var ilp = _body.GetILProcessor();

            foreach (var (_, block) in _blocks.OrderBy(x => x.Key))
            {
                if (block.ExceptionValue != null)
                {
                    foreach (var instruction in block.ExceptionValue.StoreValue())
                    {
                        ilp.Append(instruction);
                    }
                }

                foreach (var instruction in block.NetOps)
                {
                    ilp.Append(instruction);
                }
            }

            foreach (var entry in _ca.ExceptionTable)
            {
                var start = Instruction.Create(OpCodes.Leave, _blocks[entry.HandlerPc].FirstNetOp);
                var end = Instruction.Create(OpCodes.Nop);

                ilp.Append(start);
                ilp.Append(end);

                _md.Body.ExceptionHandlers.Add(new ExceptionHandler(ExceptionHandlerType.Catch)
                {
                    TryStart = _blocks[entry.StartPc].FirstNetOp,
                    TryEnd = _blocks[entry.EndPc].FirstNetOp,
                    HandlerStart = start,
                    HandlerEnd = end
                });
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

        public List<(ActionBlock, TypeReference)> HandlerBlock { get; set; } = new List<(ActionBlock, TypeReference)>();

        public bool Generated { get; set; } = false;
        public JavaState StartingState { get; set; }

        public List<MethodAction> Actions { get; } = new List<MethodAction>();
        public bool ProceedsToNext { get; set; }

        public int HandlerNum { get; set; }

        public Instruction FirstNetOp
        {
            get
            {
                if (NetOps.Count == 0)
                    NetOps.Add(Instruction.Create(OpCodes.Nop));
                return NetOps[0];
            }
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
