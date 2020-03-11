using System;
using System.Collections.Generic;

namespace CrackingTheCodingInterview.Domain
{
    public static class StackAndQueues
    {
        // 3.1 Describe how you could use a single array to implement three stacks. 

        // 3.2 How would you design a stack which, in addition to push and pop, has a function min
        // which returns the minimum element? Push, pop and min should all operate in 0(1) time. 
        public class MyStackWithMin<T> where T : IComparable
        {
            private class StackNode
            {
                public T Data;
                public StackNode Next;
                public readonly T Min;

                public StackNode(T data, T min)
                {
                    this.Data = data;
                    this.Min = min;
                }
            }

            private StackNode top;

            public T Peek() => top.Data;

            public T Min() => top.Min;

            public void Push(T item)
            {
                var node = new MyStackWithMin<T>.StackNode(item,
                    top != null && top.Data.CompareTo(item) < 0 ? top.Data : item);
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

        // 3.3 Imagine a (literal) stack of plates. If the stack gets too high, it might topple.
        //     Therefore, in real life, we would likely start a new stack when the previous stack exceeds some
        //     threshold. Implement a data structure SetOfStacks that mimics this. SetO-fStacks should be
        //     composed of several stacks and should create a new stack once the previous one exceeds capacity.
        //     SetOfStacks. push() and SetOfStacks. pop() should behave identically to a single stack
        //     (that is, pop () should return the same values as it would if there were just a single stack).
        // FOLLOW UP
        // Implement a function popAt (int index) which performs a pop operation on a specific sub-stack.         
        public class SetOfStacks<T>
        {
            private int _count = 0;
            private readonly int _sizeOfChunks;
            private StackNode _lastStackNode;

            private class StackNode
            {
                public Stack<T> Data = new Stack<T>();
                public StackNode Previous;

                public void Push(T item) => Data.Push(item);

                public T Pop() => Data.Pop();

                public T Peek() => Data.Peek();
            }

            public SetOfStacks(int sizeOfChunks)
                => _sizeOfChunks = sizeOfChunks;

            public void Push(T item)
            {
                if (_count % _sizeOfChunks == 0)
                {
                    var node = new StackNode();
                    if (_lastStackNode == null)
                        _lastStackNode = node;
                    else
                    {
                        node.Previous = _lastStackNode;
                        _lastStackNode = node;
                    }
                }

                _lastStackNode.Push(item);
                _count++;
            }

            public T Pop()
            {
                if (_count % _sizeOfChunks == 0)
                    _lastStackNode = _lastStackNode.Previous;
                _count--;
                return _lastStackNode.Pop();
            }

            //After this operation other methods may not work
            public T PopAt(int index)
            {
                int c = _count / _sizeOfChunks + (_count % _sizeOfChunks == 0 ? 0 : 1) - 1;
                var curr = _lastStackNode;
                while (c != index)
                {
                    curr = curr.Previous;
                    c--;
                }

                _count--;
                return curr.Pop();
            }

            public T Peek() => _count % _sizeOfChunks == 0 ? _lastStackNode.Previous.Peek() : _lastStackNode.Pop();
        }

        // 3.4 Implement a MyQueue class which implements a queue using two stacks
        public class MyQueueWithStacks<T>
        {
            private readonly Stack<T> _stackNewest;
            private readonly Stack<T> _stackOldest;

            public MyQueueWithStacks()
            {
                this._stackNewest = new Stack<T>();
                this._stackOldest = new Stack<T>();
            }

            public void Enqueue(T item) => _stackNewest.Push(item);

            private void ShiftStacks()
            {
                if (_stackOldest.Count == 0)
                    while (_stackNewest.Count != 0)
                        _stackOldest.Push(_stackNewest.Pop());
            }

            public T Dequeue()
            {
                ShiftStacks();
                return _stackOldest.Pop();
            }

            public T Peek()
            {
                ShiftStacks();
                return _stackOldest.Peek();
            }
        }

        // 3.5 Sort Stack: Write a program to sort a stack such that the smallest items are on the top. You can use
        // an additional temporary stack, but you may not copy the elements into any other data structure
        //     (such as an array). The stack supports the following operations: push, pop, peek, and is Empty. 
        public class SortStack<T> where T : IComparable<T>
        {
            private Stack<T> _innerStack;

            public SortStack()
            {
                _innerStack = new Stack<T>();
            }


            public void Push(T item)
            {
                var tempStack = new Stack<T>();
                while (_innerStack.Count > 0 && _innerStack.Peek().CompareTo(item) > 0)
                    tempStack.Push(_innerStack.Pop());
                _innerStack.Push(item);
                while (tempStack.Count > 0)
                    _innerStack.Push(tempStack.Pop());
            }

            public T Peek() => _innerStack.Peek();

            public T Pop() => _innerStack.Pop();

            public bool IsEmpty() => _innerStack.Count == 0;
        }        
        
        // 3.6 An animal shelter, which holds only dogs and cats, operates on a strictly"first in, first
        // out" basis. People must adopt either the "oldest" (based on arrival time) of all animals at the shelter,
        // or they can select whether they would prefer a dog or a cat (and will receive the oldest animal of
        //     that type). They cannot select which specific animal they would like. Create the data structures to
        //     maintain this system and implement operations such as enqueue, dequeueAny, dequeueDog,
        // and dequeueCat. You may use the built-in Linked list data structure. 
        public abstract class Animal
        {
            public int Age { get; set; }
        }

        public class Dog : Animal
        {
            public Dog(int age) => Age = age;
        }

        public class Cat : Animal
        {
            public Cat(int age) => Age = age;
        }
        
        public class AnimalShelterQueue
        {
            private class QueueNode
            {
                public Animal Animal;
                public QueueNode Next;

                public QueueNode(Animal animal)
                {
                    Animal = animal;
                }
            }

            private QueueNode first;
            private QueueNode last;
            
            public void Enqueue(Animal animal)
            {
                var node = new QueueNode(animal) {Next = last};
                last = node;
                if (first == null)
                    first = last;
            }

            public Animal DequeueAny()
            {
                var res = first;
                first = first.Next;
                return res.Animal;
            }

            public Dog DequeueDog()
            {
                var curr = first;
                QueueNode prev = null;
                while (curr != null && !(curr.Animal is Dog))
                {
                    prev = curr;
                    curr = curr.Next;
                }
                if (curr == null)
                    throw new Exception("there was no any dog");
                if (prev != null)
                    prev.Next = curr.Next;
                return curr.Animal as Dog;
            }

            public Cat DequeueCat()
            {
                var curr = first;
                QueueNode prev = null;
                while (curr != null && !(curr.Animal is Cat))
                {
                    prev = curr;
                    curr = curr.Next;
                }
                if (curr == null)
                    throw new Exception("there was no any cat");
                if (prev != null)
                    prev.Next = curr.Next;
                return curr.Animal as Cat;
            }
        }
    }
}