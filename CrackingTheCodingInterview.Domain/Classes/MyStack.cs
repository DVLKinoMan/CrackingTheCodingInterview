namespace CrackingTheCodingInterview.Domain.Classes
{
    public class MyStack<T>
    {
        private class StackNode
        {
            public T Data;
            public StackNode Next;

            public StackNode(T data)
                => this.Data = data;
        }

        private StackNode top;
        
        public T Peek() => top.Data;

        public void Push(T item)
        {
            var node = new MyStack<T>.StackNode(item);
            node.Next = top;
            top = node;
        }

        public bool IsEmpty() => top == null;

        public T Pop()
        {
            var res = top.Data;
            top = top.Next;
            return res;
        }
    }
}