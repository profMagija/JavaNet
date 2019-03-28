using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace JavaNet
{
    class MethodGenerator
    {
        public static MethodBody GenerateMethod(MethodDefinition md, JavaMethodInfo mi, CodeAttribute ca, CpInfo[] cp)
        {
            var mg = new MethodGenerator(md, mi, ca, cp);
            mg.DivideIntoOps();

            foreach (var op in mg._ops)
            {
                if (op != null)
                    Console.WriteLine(op);
            }

            mg.BlockAnalyzer();

            foreach (var block in mg._blocks.Values.OrderBy(x => x.JavaOffset))
            {
                Console.WriteLine(block);
            }

            return null;
        }

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

        private MethodGenerator(MethodDefinition md, JavaMethodInfo mi, CodeAttribute ca, CpInfo[] cp)
        {
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
                        _ops[start] = new JavaOp(start, instr);
                        break;

                    case JavaInstrArguments.LocalVarIndex:
                    case JavaInstrArguments.AType:
                        _ops[start] = new JavaOp(start, instr, code[pc++]);
                        break;
                    case JavaInstrArguments.Byte:
                        _ops[start] = new JavaOp(start, instr, (sbyte)code[pc++]);
                        break;
                    case JavaInstrArguments.BranchOffset:
                    {
                        var jumpTarget = I2(code, ref pc) + start;
                        _jumpTarget[jumpTarget] = true;
                        _ops[start] = new JavaOp(start, instr, jumpTarget);
                        break;
                    }
                    case JavaInstrArguments.Short:
                        _ops[start] = new JavaOp(start, instr, I2(code, ref pc));
                        break;
                    case JavaInstrArguments.Iinc:
                        _ops[start] = new JavaOp(start, instr, code[pc++], (sbyte) code[pc++]);
                        break;
                    case JavaInstrArguments.WidePrefix:
                        var widenedInstr = (JavaInstruction) code[pc++];
                        if (widenedInstr == JavaInstruction.iinc)
                            _ops[start] = new JavaOp(start, instr, widenedInstr, I2(code, ref pc), I2(code, ref pc));
                        else 
                            _ops[start] = new JavaOp(start, instr, widenedInstr, I2(code, ref pc));
                        break;
                    case JavaInstrArguments.CpIndex:
                        _ops[start] = new JavaOp(start, instr, _cp[I2(code, ref pc)]);
                        break;

                    case JavaInstrArguments.ByteCpIndex:
                        _ops[start] = new JavaOp(start, instr, _cp[code[pc++]]);
                        break;
                    case JavaInstrArguments.MultiNewArray:
                        _ops[start] = new JavaOp(start, instr, _cp[I2(code, ref pc)], code[pc++]);
                        break;
                    case JavaInstrArguments.WideBranchOffset:
                        _ops[start] = new JavaOp(start, instr, I4(code, ref pc));
                        break;
                    case JavaInstrArguments.InvokeDynamic:
                        _ops[start] = new JavaOp(start, instr, _cp[I2(code, ref pc)]);
                        pc += 2;
                        break;
                    case JavaInstrArguments.InvokeInterface:
                        _ops[start] = new JavaOp(start, instr, _cp[I2(code, ref pc)], code[pc++]);
                        pc++;
                        break;
                    case JavaInstrArguments.LookupSwitch:
                    {
                        pc = ((pc - 1) | 3) + 1;
                        var defOffset = I4(code, ref pc);
                        var nPairs = I4(code, ref pc);

                        var mtp = new KeyValuePair<int, int>[nPairs];
                        for (var i = 0; i < nPairs; i++)
                        {
                            mtp[i] = new KeyValuePair<int, int>(I4(code, ref pc), I4(code, ref pc) + start);
                            _jumpTarget[mtp[i].Value] = true;
                        }

                        _ops[start] = new JavaOp(start, instr, defOffset, mtp);

                        break;
                    }
                    case JavaInstrArguments.TableSwitch:
                    {
                        pc = ((pc - 1) | 3) + 1;
                        var defOffset = I4(code, ref pc);
                        var low = I4(code, ref pc);
                        var high = I4(code, ref pc);

                        var targets = new int[high - low + 1];
                        for (var i = 0; i < targets.Length; i++)
                        {
                            targets[i] = I4(code, ref pc) + start;
                            _jumpTarget[targets[i]] = true;
                        }

                        _ops[start] = new JavaOp(start, instr, defOffset, low, high, targets);

                        break;
                    }
                    default:
                        throw new ArgumentOutOfRangeException();
                }
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
                    curBlock = new ActionBlock(i);
                    _blocks.Add(i, curBlock);
                }
                curBlock.JavaOps.Add(_ops[i]);
                
            }
        }

        private void 

        private static short I2(byte[] code, ref int pc)
        {
            return (short)((code[pc++] << 8) | code[pc++]);
        }

        private static int I4(byte[] code, ref int pc)
        {
            return (code[pc++] << 24) | (code[pc++] << 16) | (code[pc++] << 8) | code[pc++];
        }

    }

    class JavaOp
    {
        public int Offset { get; }
        public JavaInstruction Instr { get; }
        public object[] Args { get; }

        public JavaOp(int offset, JavaInstruction instr, params object[] args)
        {
            Offset = offset;
            Instr = instr;
            Args = args;
        }

        public override string ToString()
        {
            return $"0x{Offset:X4} {Instr} " +
                   string.Join(' ', Args.Select(x =>
                   {
                       switch (x)
                       {
                           case CpInfo info:
                               return info.Represent();
                           case IFormattable form:
                               return "0x" + form.ToString("X4", null);
                           default:
                               return x.ToString();
                       }
                   }));
        }
    }

    class ActionBlock
    {
        public int JavaOffset { get; }
        public List<JavaOp> JavaOps { get; } = new List<JavaOp>();
        public int NetOffset { get; set; }

        public JavaState<object> StartingState { get; set; }

        public ActionBlock(int javaOffset)
        {
            JavaOffset = javaOffset;
        }

        public override string ToString()
        {
            var s = $"actionBlock at 0x{JavaOffset:X4}\n";
            foreach (var op in JavaOps)
            {
                s += $"  {op}\n";
            }

            return s;
        }
    }
}
