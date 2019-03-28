using System.Collections.Immutable;

namespace JavaNet
{
    /// <summary>
    /// Immutable state of the JVM during method execution (stack and locals)
    /// </summary>
    class JavaState<T>
    {
        private readonly ImmutableStack<T> _stack;
        private readonly ImmutableList<T> _locals;

        public JavaState(int length = 0)
        {
            _stack = ImmutableStack<T>.Empty;
            _locals = new T[length].ToImmutableList();
        }

        public JavaState(ImmutableStack<T> stack, ImmutableList<T> locals)
        {
            _stack = stack;
            _locals = locals;
        }

        public JavaState<T> Push(T o)
        {
            return new JavaState<T>(_stack.Push(o), _locals);
        }

        public JavaState<T> Pop()
        {
            return Pop(out _);
        }

        public JavaState<T> Pop(out T top)
        {
            return new JavaState<T>(_stack.Pop(out top), _locals);
        }

        public T StackTop => _stack.Peek();

        public JavaState<T> Store(int index, T value)
        {
            return new JavaState<T>(_stack, _locals.SetItem(index, value));
        }

        public T Load(int index) => _locals[index];
    }
}