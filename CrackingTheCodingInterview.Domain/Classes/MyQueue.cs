namespace CrackingTheCodingInterview.Domain.Classes
{
    public class MyQueue<T>
    {
        private class QueueNode
        {
            public T Data;
            public QueueNode Next;
            
            public QueueNode(T data) => 
                this.Data = data;
        }

        private QueueNode first;
        private QueueNode last;
        
        public void Enqueue(T item)
        {
            var node = new QueueNode(item);
            if (first == null)
                first = node;
            if (last != null)
                last.Next = node;
            last = node;
        }

        public T Dequeue()
        {
            var res = first.Data;
            first = first.Next;
            return res;
        }

        public T Peek() => first.Data;

        public bool IsEmpty() => first == null;
    }
}