using CrackingTheCodingInterview.Domain;
using NUnit.Framework;

namespace CrackingTheCodingInterview.Tests
{
    [TestFixture]
    public class SortingAndSearchingTester
    {
        [Test]
        public void TestGroupAnagrams()
        {
            var arr = new string[] {"aba", "kaka", "baa", "aaba", "aaka", "kaak", "aab"};
            SortingAndSearching.GroupAnagrams(arr);
        }

        [Test]
        public void SearchInSortedRotatedArray()
        {
            var arr = new int[] {9, 11, 13, 19, 20, 21, 1, 3, 4, 5, 6, 7, 8};
            Assert.That(SortingAndSearching.SearchInRotatedArray(arr, 11), Is.EqualTo(1));
        }
    }
}