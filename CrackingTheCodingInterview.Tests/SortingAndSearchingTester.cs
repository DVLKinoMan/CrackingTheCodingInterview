using CrackingTheCodingInterview.Domain;
using NUnit.Framework;

namespace CrackingTheCodingInterview.Tests
{
    [TestFixture]
    public class SortingAndSearchingTester
    {
        [Test]
        public void TestMethod1()
        {
            var arr = new string[] {"aba", "kaka", "baa", "aaba", "aaka", "kaak", "aab"};
            SortingAndSearching.GroupAnagrams(arr);
        }
    }
}