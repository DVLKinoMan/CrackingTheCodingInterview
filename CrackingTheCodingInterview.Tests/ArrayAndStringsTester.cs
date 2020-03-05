using NUnit.Framework;
using static CrackingTheCodingInterview.Domain.ArraysAndStrings;

namespace CrackingTheCodingInterview.Tests
{
    [TestFixture]
    public class ArrayAndStringsTester
    {
        [Test]
        [TestCase("Dato", true)]
        [TestCase("Common", false)]
        [TestCase("Dato123_+", true)]
        [TestCase("Dato123_+cd", true)]
        public void HasAllUniqueCharacters1Test(string str, bool expected)
        {
            Assert.That(HasAllUniqueCharacters1(str), Is.EqualTo(expected));
        }
        
        //It doesn't work on uppercase letters
        [Test]
        [TestCase("dato", true)]
        [TestCase("common", false)]
        public void HasAllUniqueCharacters2Test(string str, bool expected)
        {
            Assert.That(HasAllUniqueCharacters2(str), Is.EqualTo(expected));
        }
        
        [Test]
        [TestCase("dato", "adto", true)]
        [TestCase("common", "moonmcc", false)]
        public void CheckPermutationTest(string a, string b, bool expected)
        {
            Assert.That(CheckPermutation(a,b), Is.EqualTo(expected));
        }

        [Test]
        [TestCase("tact coa", true)]
        [TestCase("common", false)]
        public void IsPalindromePermutationTest(string str, bool expected)
        {
            Assert.That(IsPalindromePermutation1(str), Is.EqualTo(expected));
        }
        
        [Test]
        [TestCase("tactcoa", true)]
        [TestCase("common", false)]
        [TestCase("comomc", true)]
        [TestCase("tactoa", false)]
        public void IsPalindromePermutation2Test(string str, bool expected)
        {
            Assert.That(IsPalindromePermutation2(str), Is.EqualTo(expected));
        }

        [Test]
        [TestCase("abcd","abcd",  true)]
        [TestCase("abad", "abcd", true)]
        [TestCase("abd", "abcd", true)]
        [TestCase("abd", "avde", false)]
        [TestCase("abd", "abdee", false)]
        public void IsOneEditAwayTest(string a, string b, bool expected)
        {
            Assert.That(OneEditAway(a,b), Is.EqualTo(expected));
        }

        [Test]
        [TestCase("aaabbddsskddddd", "a3b2d2s2k1d5")]
        public void StringCompressionTest(string str, string expected)
        {
            Assert.That(StringCompression(str), Is.EqualTo(expected));
        }

        [Test]
        public void ZeroMatrixTest()
        {
            var m = new int[][]
            {
                new int[] {1, 2, 3, 4, 0},
                new int[] {5, 6, 7, 8, 10},
                new int[] {1, 1, 0, 1, 2},
                new int[] {1, 1, 1, 1, 2},
                new int[] {0, 1, 2, 2, 2}
            };
            Assert.That(ZeroMatrix(m), Is.EquivalentTo(new int[][]
            {
                new int[] {0, 0, 0, 0, 0},
                new int[] {0, 6, 0, 8, 0},
                new int[] {0, 0, 0, 0, 0},
                new int[] {0, 1, 0, 1, 0},
                new int[] {0, 0, 0, 0, 0}
            }));
        }
    }
}