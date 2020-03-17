using System.Collections.Generic;
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

        [Test]
        public void BalancedTreeTest()
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
            var actual = IsBalancedTree(treenode);

            Assert.That(actual, Is.EqualTo(true));
        }

        [Test]
        public void BalancedTreeTest2()
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
                        {
                            Left = new TreeNode(11)
                        }
                    }
                }
            };
            var actual = IsBalancedTree(treenode);

            Assert.That(actual, Is.EqualTo(false));
        }

        [Test]
        public void IsBinarySearchTreeTest()
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
                        {
                            Left = new TreeNode(11)
                        }
                    }
                }
            };
            Assert.That(IsBinarySearchTree(treenode), Is.EqualTo(false));
        }

        [Test]
        public void IsBinarySearchTreeTest2()
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
                    Left = new TreeNode(4),
                    Right = new TreeNode(8)
                    {
                        Right = new TreeNode(9)
                    }
                }
            };
            Assert.That(IsBinarySearchTree(treenode), Is.EqualTo(false));
        }

        [Test]
        public void IsBinarySearchTreeTest3()
        {
            var treeNode = new TreeNode(5)
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
            Assert.That(IsBinarySearchTree(treeNode), Is.EqualTo(true));
        }

        [Test]
        public void NextNodeTest4()
        {
            var node = new TreeNode(5);
            var root = new TreeNode(6)
            {
                Left = new TreeNode(2)
                {
                    Left = new TreeNode(1),
                    Right = new TreeNode(3)
                    {
                        Right = new TreeNode(4)
                        {
                            Right = node
                        }
                    }
                },
                Right = new TreeNode(7)
                {
                    Right = new TreeNode(8)
                    {
                        Right = new TreeNode(9)
                    }
                }
            };
            Assert.That(NextNode(root, node).Val, Is.EqualTo(7));
        }

        [Test]
        public void BuildOrderTest()
        {
            var actual = BuildOrder(new List<char>() {'a', 'b', 'c', 'd', 'e', 'f'}, new List<(char x, char y)>()
            {
                ('a', 'd'),
                ('f', 'b'),
                ('b', 'd'),
                ('f', 'a'),
                ('d', 'c')
            });

            Assert.That(actual, Is.EquivalentTo(new List<char>() {'f', 'e', 'a', 'b', 'd', 'c'}));
        }

        [Test]
        public void FirstCommonAncestorTest()
        {
            var node1 = new TreeNode(3)
            {
                Right = new TreeNode(4)
                {
                    Right = new TreeNode(78)
                }
            };
            var node2 = new TreeNode(8)
            {
                Right = new TreeNode(9)
            };
            var root = new TreeNode(6)
            {
                Left = new TreeNode(2)
                {
                    Left = new TreeNode(1),
                    Right = node1
                },
                Right = new TreeNode(7)
                {
                    Right = node2
                }
            };

            Assert.That(FirstCommonAncestor(root, node1, node2), Is.EqualTo(root));
        }

        [Test]
        public void BstSequencesTest()
        {
            var root = new TreeNode(2)
            {
                Left = new TreeNode(1),
                Right = new TreeNode(3)
            };
            var actual = BstSequences(root);
        }
    }
}