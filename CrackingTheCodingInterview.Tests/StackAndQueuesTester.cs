using CrackingTheCodingInterview.Domain;
using NUnit.Framework;

namespace CrackingTheCodingInterview.Tests
{
    [TestFixture]
    public class StackAndQueuesTester
    {
        [Test]
        public void MyStackWithMinTest()
        {
            var myStackWithMin = new StackAndQueues.MyStackWithMin<int>();
            myStackWithMin.Push(1);
            myStackWithMin.Push(2);
            Assert.That(myStackWithMin.Min(), Is.EqualTo(1));
            myStackWithMin.Push(0);
            Assert.That(myStackWithMin.Min(), Is.EqualTo(0));
            myStackWithMin.Pop();
            Assert.That(myStackWithMin.Min(), Is.EqualTo(1));
        }

        [Test]
        public void SetOfStacksTest()
        {
            var stack = new StackAndQueues.SetOfStacks<int>(2);
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            Assert.That(stack.Pop(), Is.EqualTo(3));
            Assert.That(stack.Peek(), Is.EqualTo(2));
            Assert.That(stack.Pop(), Is.EqualTo(2));
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);
            stack.Push(5);
            Assert.That(stack.PopAt(1), Is.EqualTo(4));
        }

        [Test]
        public void SortStackTest()
        {
            var stack = new StackAndQueues.SortStack<int>();
            Assert.That(stack.IsEmpty, Is.EqualTo(true));
            stack.Push(4);
            stack.Push(7);
            stack.Push(5);
            stack.Push(6);
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            Assert.That(stack.Pop(), Is.EqualTo(7));
            Assert.That(stack.Pop(), Is.EqualTo(6));
            Assert.That(stack.Pop(), Is.EqualTo(5));
            Assert.That(stack.Pop(), Is.EqualTo(4));
            Assert.That(stack.Pop(), Is.EqualTo(3));
            Assert.That(stack.Pop(), Is.EqualTo(2));
            Assert.That(stack.Peek(), Is.EqualTo(1));
            Assert.That(stack.IsEmpty(), Is.EqualTo(false));
            Assert.That(stack.Pop(), Is.EqualTo(1));
            Assert.That(stack.IsEmpty(), Is.EqualTo(true));
        }
    }
}