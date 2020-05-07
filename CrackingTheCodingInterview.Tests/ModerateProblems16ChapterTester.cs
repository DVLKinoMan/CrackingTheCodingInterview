using NUnit.Framework;
using static CrackingTheCodingInterview.Domain.ModerateProblems16Chapter;

namespace CrackingTheCodingInterview.Tests
{
    [TestFixture]
    public class ModerateProblems16ChapterTester
    {
        [Test]
        public void TestIntegerOnEnglish()
        {
            Assert.Multiple(() =>
                {
                    Assert.That(IntegerOnEnglish(1456302402),
                        Is.EqualTo(
                            "One Billion Four Hundred Fifty Six Million Three Hundred Two Thousand Four Hundred Two"));
                    Assert.That(IntegerOnEnglish(1000400000),
                        Is.EqualTo(
                            "One Billion Four Hundred Thousand"));
                    Assert.That(IntegerOnEnglish(430),
                        Is.EqualTo(
                            "Four Hundred Thirty"));
                });
        }

        [Test]
        public void TestSubSort()
        {
            Assert.That(SubSort(new int[]{1, 2, 4, 7, 10, 11, 7, 12, 6, 7, 16, 18, 19}), Is.EqualTo(6));
        }

        [Test]
        public void TestContiguousSum()
        {
            Assert.That(ContiguousLargestSum(new int[] {-8, 3, -2, 4, -10}), Is.EqualTo(5));
        }
    }
}