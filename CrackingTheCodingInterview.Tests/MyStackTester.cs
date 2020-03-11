using CrackingTheCodingInterview.Domain.Classes;
using NUnit.Framework;

namespace CrackingTheCodingInterview.Tests
{
    [TestFixture]
    public class MyStackTester
    {
        [Test]
        public void TestMethod1()
        {
            var myStack = new MyStack<int>();
            myStack.Push(1);
            Assert.That(myStack.Peek(), Is.EqualTo(1));
        }

        [Test]
        public void TestMethod2()
        {
            var myStack = new MyStack<int>();
            Assert.That(myStack.IsEmpty(), Is.EqualTo(true));
            
            myStack.Push(1);
            myStack.Push(2);
            Assert.That(myStack.Pop(), Is.EqualTo(2));
            Assert.That(myStack.IsEmpty(), Is.EqualTo(false));
            Assert.That(myStack.Pop(), Is.EqualTo(1));
            
            Assert.That(myStack.IsEmpty(), Is.EqualTo(true));
        }
    }
}