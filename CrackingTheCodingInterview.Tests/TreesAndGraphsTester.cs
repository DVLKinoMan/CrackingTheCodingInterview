using CrackingTheCodingInterview.Domain.Classes;
using NUnit.Framework;
using static CrackingTheCodingInterview.Domain.TreesAndGraphs;

namespace CrackingTheCodingInterview.Tests
{
    [TestFixture]
    public class TreesAndGraphsTester
    {
        [Test]
        public void IsRouteTest()
        {
            var node2 = new GraphNode(1)
            {
                Children = new[] {new GraphNode(2), new GraphNode(3)}
            };

            var node1 = new GraphNode(10)
            {
                Children = new[]
                {
                    new GraphNode(9)
                    {
                        Children = new[] {new GraphNode(8)}
                    },
                    new GraphNode(7)
                    {
                        Children = new[]
                        {
                            new GraphNode(6)
                            {
                                Children = new[]
                                {
                                    new GraphNode(5)
                                    {
                                        Children = new[] {node2}
                                    },
                                }
                            },
                        }
                    },
                }
            };

            Assert.That(IsRoute(node1, node2), Is.EqualTo(true));
        }

        [Test]
        public void MinimalTreeTest()
        {
            var expectedResult = new TreeNode(5)
            {
                Left = new TreeNode(2)
                {
                    Left = new TreeNode(1),
                    Right = new TreeNode(3)
                    {
                        Right = new TreeNode(4)
                    }
                },
                Right = new TreeNode(7)
                {
                    Left = new TreeNode(6),
                    Right = new TreeNode(8)
                    {
                        Right = new TreeNode(9)
                    }
                }
            };
            var actual = MinimalTree(new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9});
            Assert.That(actual, Is.EqualTo(expectedResult));
        }
        
        [Test]
        public void ConstructLinkListNodesTest()
        {
            var treenode = new TreeNode(5)
            {
                Left = new TreeNode(2)
                {
                    Left = new TreeNode(1),
                    Right = new TreeNode(3)
                    {
                        Right = new TreeNode(4)
                    }
                },
                Right = new TreeNode(7)
                {
                    Left = new TreeNode(6),
                    Right = new TreeNode(8)
                    {
                        Right = new TreeNode(9)
                    }
                }
            };
            var actual = ConstructLinkListNodes(treenode);
        }
    }
}