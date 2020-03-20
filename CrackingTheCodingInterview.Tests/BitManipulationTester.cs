using NUnit.Framework;
using static CrackingTheCodingInterview.Domain.BitManipulation;

namespace CrackingTheCodingInterview.Tests
{
    [TestFixture]
    public class BitManipulationTester
    {
        [Test]
        public void TestMethod1()
        {
            Assert.That(Insertion(1024, 19, 2, 6), Is.EqualTo(1100));
        }
    }
}