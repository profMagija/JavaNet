using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using Mono.Cecil;

namespace JavaNet
{
    /// <summary>
    /// Immutable state of the JVM during method execution (stack and locals)
    /// </summary>
    class JavaState
    {
        private readonly ImmutableStack<JavaValue> _stack;
        private readonly ImmutableDictionary<int, JavaValue> _locals;

        public JavaState(int length = 0)
        {
            _stack = ImmutableStack<JavaValue>.Empty;
            _locals = ImmutableDictionary<int, JavaValue>.Empty;
        }

        public JavaState(ImmutableStack<JavaValue> stack, ImmutableDictionary<int, JavaValue> locals)
        {
            _stack = stack;
            _locals = locals;
        }

        public ImmutableDictionary<int, JavaValue> Locals => _locals;
        public ImmutableStack<JavaValue> Stack => _stack;

        public JavaState Push(JavaValue o)
        {
            return new JavaState(_stack.Push(o), _locals);
        }

        public JavaState PushIf(bool cond, JavaValue val = null)
        {
            return cond ? Push(val) : this;
        }

        public JavaState Pop()
        {
            var ns = Pop(out var i);
            Debug.Assert(i == null);
            return ns;
        }

        public JavaState PopIf(bool cond)
        {
            return cond ? Pop() : this;
        }

        public JavaState Pop(out JavaValue top)
        {
            return new JavaState(_stack.Pop(out top), _locals);
        }

        public JavaState Store(int index, JavaValue value)
        {
            return new JavaState(_stack, _locals.SetItem(index, value));
        }

        public JavaValue Load(int index)
        {
            return _locals[index];
            //if (_locals.TryGetValue(index, out var value))
            //    return value;
            //return new InputLocalValue(null, index);
        }

        public (List<MethodAction>, JavaState) Unconst()
        {
            var actions = new List<MethodAction>();
            var newStack = new List<JavaValue>();
            var newLocals = new Dictionary<int, JavaValue>();

            foreach (var st in _stack.Reverse())
            {
                if (st?.IsConst == true)
                {
                    var v = new CalculatedValue(st.ActualType);
                    actions.Add(new ConstantSetAction(v, st));
                    newStack.Add(v);
                }
                else
                {
                    newStack.Add(st);
                }
            }

            foreach (var (key, value) in _locals)
            {
                if (value?.IsConst == true)
                {
                    var v = new CalculatedValue(value.ActualType);
                    actions.Add(new ConstantSetAction(v, value));
                    newLocals.Add(key, v);
                }
                else
                {
                    newLocals.Add(key, value);
                }
            }

            return (actions,
                new JavaState(ImmutableStack.Create(newStack.ToArray()), newLocals.ToImmutableDictionary()));
        }

        public (IEnumerable<MethodAction> acts, JavaState curState, JavaState newTarget) MapTo(JavaState targetState)
        {
            // next we map target into the current stack
            var target = targetState._stack.ToArray();
            var cur = _stack.ToArray();

            var mappings = new List<(JavaValue, JavaValue)>();

            for (int i = 0; i < Math.Min(target.Length, cur.Length); i++)
            {
                if (target[i] == cur[i]) continue;
                mappings.Add((target[i], cur[i]));
                cur[i] = target[i];
            }

            // now we do the locals
            var resDict = _locals;
            var newTarget = targetState;
            foreach (var (index, value) in _locals)
            {
                if (!targetState._locals.TryGetValue(index, out var targetValue))
                {
                    newTarget = newTarget.Store(index, value);
                    continue;
                }
                if (targetValue == value) continue;

                mappings.Add((targetValue, value));
                resDict = resDict.SetItem(index, targetValue);
            }

            //foreach (var (index, _) in targetState._locals)
            //{
            //    Debug.Assert(_locals.ContainsKey(index)); // just to be sure
            //}

            return (mappings.Any() ? new[] {new MappingAction(mappings)} : new MethodAction[0], new JavaState(ImmutableStack.Create(cur.Reverse().ToArray()), resDict), newTarget);
        }

        public override string ToString()
        {
            return string.Join(", ", _stack.Select(x => x?.ToString() ?? "<none>"));
        }

        public JavaValue TryLoad(int index)
        {
            _locals.TryGetValue(index, out var value);
            return value;
        }
    }
}