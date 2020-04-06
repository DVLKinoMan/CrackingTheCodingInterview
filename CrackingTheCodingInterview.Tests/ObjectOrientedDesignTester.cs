using System.Linq;
using CrackingTheCodingInterview.Domain;
using NUnit.Framework;

namespace CrackingTheCodingInterview.Tests
{
    [TestFixture]
    public class ObjectOrientedDesignTester
    {
        [Test]
        public void TestCircularArray()
        {
            var actualArray = Enumerable.Range(0, 10).ToArray();
            var arr = new ObjectOrientedDesign.CircularArray<int>(actualArray);

            Assert.Multiple(() =>
            {
                Assert.That(arr, Is.EquivalentTo(actualArray));
                arr.Rotate(2);
                Assert.That(arr, Is.EquivalentTo(new int[] {2, 3, 4, 5, 6, 7, 8, 9, 0, 1}));
            });
        }

        [Test]
        public void TestMinesweeper()
        {
            var game = new ObjectOrientedDesign.Minesweeper(3, 7);
            game.Shoot(0,0);
            game.Shoot(3,6);
            game.Shoot(4,5);
        }
    }
}