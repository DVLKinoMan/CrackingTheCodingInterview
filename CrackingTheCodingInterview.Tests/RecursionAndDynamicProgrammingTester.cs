using CrackingTheCodingInterview.Domain;
using NUnit.Framework;

namespace CrackingTheCodingInterview.Tests
{
    [TestFixture]
    public class RecursionAndDynamicProgrammingTester
    {
        [Test]
        [TestCase(6)]
        public void HanoiTester(int howManyElements)
        {
            RecursionAndDynamicProgramming.TowersOfHanoi(howManyElements);
        }

        [Test]
        public void ParensTester()
        {
            var actual = RecursionAndDynamicProgramming.Parens(3);
            Assert.That(actual, Is.EquivalentTo(new []{"((()))", "(()())", "(())()", "()(())", "()()()"}));
        }

        [Test]
        public void BooleanEvaluationTester()
        {
            var actual = RecursionAndDynamicProgramming.BooleanEvaluation("1^0|0|1", false);
            Assert.That(actual, Is.EqualTo(2));
        }
    }
}