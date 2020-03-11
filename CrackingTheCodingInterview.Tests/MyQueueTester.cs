using CrackingTheCodingInterview.Domain.Classes;
using NUnit.Framework;

namespace CrackingTheCodingInterview.Tests
{
    [TestFixture]
    public class MyQueueTester
    {
        [Test]
        public void TestMethod1()
        {
            var queue=new MyQueue<int>();
            Assert.That(queue.IsEmpty, Is.EqualTo(true));
            queue.Enqueue(1);
            Assert.That(queue.Peek(), Is.EqualTo(1));
            queue.Enqueue(2);
            
            Assert.That(queue.Peek(), Is.EqualTo(1));

            Assert.That(queue.IsEmpty, Is.EqualTo(false));
            Assert.That(queue.Dequeue(), Is.EqualTo(1));
 
            Assert.That(queue.IsEmpty, Is.EqualTo(false));
            Assert.That(queue.Dequeue(), Is.EqualTo(2));
            
            Assert.That(queue.IsEmpty, Is.EqualTo(true));
        }
    }
}