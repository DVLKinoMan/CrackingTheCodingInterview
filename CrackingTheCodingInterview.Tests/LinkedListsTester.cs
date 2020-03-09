using CrackingTheCodingInterview.Domain;
using NUnit.Framework;
using static CrackingTheCodingInterview.Domain.LinkedLists;

namespace CrackingTheCodingInterview.Tests
{
    [TestFixture]
    public class LinkedListsTester
    {
        [Test]
        public void RemoveDuplicatesUsingSetTest()
        {
            var root = new LinkListNode(1)
            {
                Next = new LinkListNode(2)
                {
                    Next = new LinkListNode(3)
                    {
                        Next = new LinkListNode(1)
                        {
                            Next = new LinkListNode(2)
                            {
                                Next = new LinkListNode(4)
                            }
                        }
                    }
                }
            };

            RemoveDuplicatesUsingSet(root);
            var expected = new LinkListNode(1)
            {
                Next = new LinkListNode(2)
                {
                    Next = new LinkListNode(3)
                    {
                        Next = new LinkListNode(4)
                    }
                }
            };
            Assert.That(root, Is.EqualTo(expected));
        }

        [Test]
        public void RemoveDuplicatesWithoutTemporaryBufferTest()
        {
            var root = new LinkListNode(1)
            {
                Next = new LinkListNode(2)
                {
                    Next = new LinkListNode(3)
                    {
                        Next = new LinkListNode(1)
                        {
                            Next = new LinkListNode(2)
                            {
                                Next = new LinkListNode(4)
                            }
                        }
                    }
                }
            };

            RemoveDuplicatesWithoutTemporaryBuffer(root);
            var expected = new LinkListNode(1)
            {
                Next = new LinkListNode(2)
                {
                    Next = new LinkListNode(3)
                    {
                        Next = new LinkListNode(4)
                    }
                }
            };
            Assert.That(root, Is.EqualTo(expected));
        }

        [Test]
        public void ReturnLastTest()
        {
            var root = new LinkListNode(1)
            {
                Next = new LinkListNode(2)
                {
                    Next = new LinkListNode(3)
                    {
                        Next = new LinkListNode(1)
                        {
                            Next = new LinkListNode(2)
                            {
                                Next = new LinkListNode(4)
                            }
                        }
                    }
                }
            };

            Assert.That(ReturnLast(root, 4), Is.EqualTo(3));
        }

        [Test]
        public void ReturnLastWithRecursionTest()
        {
            var root = new LinkListNode(1)
            {
                Next = new LinkListNode(2)
                {
                    Next = new LinkListNode(3)
                    {
                        Next = new LinkListNode(1)
                        {
                            Next = new LinkListNode(2)
                            {
                                Next = new LinkListNode(4)
                            }
                        }
                    }
                }
            };

            Assert.That(ReturnLastWithRecursion(root, 4), Is.EqualTo(3));
        }

        [Test]
        public void DeleteTheMiddleTest()
        {
            var root = new LinkListNode(1)
            {
                Next = new LinkListNode(2)
                {
                    Next = new LinkListNode(3)
                    {
                        Next = new LinkListNode(4)
                        {
                            Next = new LinkListNode(5)
                            {
                                Next = new LinkListNode(6)
                            }
                        }
                    }
                }
            };

            var expected = new LinkListNode(1)
            {
                Next = new LinkListNode(2)
                {
                    Next = new LinkListNode(3)
                    {
                        Next = new LinkListNode(5)
                        {
                            Next = new LinkListNode(6)
                        }
                    }
                }
            };

            DeleteTheMiddle(root);

            Assert.That(root, Is.EqualTo(expected));
        }


        [Test]
        public void PartitionTest()
        {
            var root = new LinkListNode(4)
            {
                Next = new LinkListNode(6)
                {
                    Next = new LinkListNode(1)
                    {
                        Next = new LinkListNode(3)
                        {
                            Next = new LinkListNode(2)
                            {
                                Next = new LinkListNode(5)
                            }
                        }
                    }
                }
            };

            var expected = new LinkListNode(1)
            {
                Next = new LinkListNode(2)
                {
                    Next = new LinkListNode(4)
                    {
                        Next = new LinkListNode(6)
                        {
                            Next = new LinkListNode(3)
                            {
                                Next = new LinkListNode(5)
                            }
                        }
                    }
                }
            };

            var actual = Partition(root, 3);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void SumListsTest()
        {
            var root1 = new LinkListNode(7)
            {
                Next = new LinkListNode(1)
                {
                    Next = new LinkListNode(6)
                }
            };

            var root2 = new LinkListNode(5)
            {
                Next = new LinkListNode(9)
                {
                    Next = new LinkListNode(2)
                }
            };

            Assert.That(SumLists(root1, root2), Is.EqualTo(912));
        }

        [Test]
        public void IsPalindromeTest()
        {
            var root = new LinkListNode(1)
            {
                Next = new LinkListNode(2)
                {
                    Next = new LinkListNode(3)
                    {
                        Next = new LinkListNode(2)
                        {
                            Next = new LinkListNode(1)
                        }
                    }
                }
            };

            Assert.That(IsPalindrome(root), Is.EqualTo(true));
        }

        [Test]
        public void IsPalindromeTest2()
        {
            var root = new LinkListNode(1)
            {
                Next = new LinkListNode(2)
                {
                    Next = new LinkListNode(3)
                    {
                        Next = new LinkListNode(3)
                        {
                            Next = new LinkListNode(2)
                            {
                                Next = new LinkListNode(1)
                            }
                        }
                    }
                }
            };

            Assert.That(IsPalindrome(root), Is.EqualTo(true));
        }

        [Test]
        public void IsPalindromeTest3()
        {
            var root = new LinkListNode(1)
            {
                Next = new LinkListNode(2)
                {
                    Next = new LinkListNode(4)
                    {
                        Next = new LinkListNode(3)
                        {
                            Next = new LinkListNode(2)
                            {
                                Next = new LinkListNode(1)
                            }
                        }
                    }
                }
            };

            Assert.That(IsPalindrome(root), Is.EqualTo(false));
        }

        [Test]
        public void IsPalindromeTest4()
        {
            var root = new LinkListNode(1)
            {
                Next = new LinkListNode(2)
                {
                    Next = new LinkListNode(3)
                    {
                        Next = new LinkListNode(3)
                        {
                            Next = new LinkListNode(4)
                            {
                                Next = new LinkListNode(2)
                                {
                                    Next = new LinkListNode(1)
                                }
                            }
                        }
                    }
                }
            };

            Assert.That(IsPalindrome(root), Is.EqualTo(false));
        }

        [Test]
        public void IntersectionNodeTest()
        {
            var root1 = new LinkListNode(7)
            {
                Next = new LinkListNode(1)
                {
                    Next = new LinkListNode(6)
                }
            };

            var root2 = new LinkListNode(5)
            {
                Next = new LinkListNode(9)
                {
                    Next = new LinkListNode(2)
                    {
                        Next = root1
                    }
                }
            };

            Assert.That(IntersectionNode(root1, root2), Is.EqualTo(root1));
        }

        [Test]
        public void LoopDetectionTest()
        {
            var root1 = new LinkListNode(1)
            {
                Next = new LinkListNode(2)
                {
                    Next = new LinkListNode(3)
                    {
                        Next = new LinkListNode(4)
                        {
                            Next = new LinkListNode(3)
                            {
                                Next = new LinkListNode(4)
                            }
                        }
                    }
                }
            };
    
            Assert.That(LoopDetection(root1), Is.EqualTo(3));
        }
    }
}