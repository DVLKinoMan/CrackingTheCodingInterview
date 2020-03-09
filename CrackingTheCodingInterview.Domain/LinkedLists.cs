using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrackingTheCodingInterview.Domain
{
    public static class LinkedLists
    {
        // 2.1 Write code to remove duplicates from an unsorted linked list.
        //     FOLLOW UP
        //     How would you solve this problem if a temporary buffer is not allowed? 
        public static void RemoveDuplicatesUsingSet(LinkListNode root)
        {
            var set = new HashSet<int>();
            var node = root;
            LinkListNode prev = null;
            while (node != null)
            {
                if (!set.Add(node.Value))
                    prev.Next = node.Next;
                else prev = node;
                node = node.Next;
            }
        }

        public static void RemoveDuplicatesWithoutTemporaryBuffer(LinkListNode root)
        {
            var current = root;
            while (current != null)
            {
                var runner = current.Next;
                var prev = current;
                while (runner != null)
                {
                    if (runner.Value == current.Value)
                        prev.Next = runner.Next;
                    else prev = runner;
                    runner = runner.Next;
                }

                current = current.Next;
            }
        }

        // 2.2 Implement an algorithm to find the kth to last element of a singly linked list. 
        public static int ReturnLast(LinkListNode root, int k)
        {
            var stack = new Stack<int>();
            var current = root;
            while (current != null)
            {
                stack.Push(current.Value);
                current = current.Next;
            }

            int count = 1;
            while (count != k)
            {
                stack.Pop();
                count++;
            }

            return stack.Pop();
        }

        public static int ReturnLastWithRecursion(LinkListNode root, int k)
        {
            var res = 0;
            Dfs(root);
            return res;

            int Dfs(LinkListNode node)
            {
                if (node == null)
                    return 0;
                int depth = Dfs(node.Next) + 1;
                if (depth == k)
                    res = node.Value;
                return depth;
            }
        }

        // 2.3 Implement an algorithm to delete a node in the middle (i.e., any node but
        //     the first and last node, not necessarily the exact middle) of a singly linked list, given only access to
        // that node.
        //     EXAMPLE
        // Input: the node c from the linked list a->b->c->d->e->f
        //     Result: nothing is returned, but the new linked list looks like a->b->d->e->f 
        public static void DeleteTheMiddle(LinkListNode root)
        {
            var current = root;
            var runner = root;
            LinkListNode prev = null;
            while (runner != null)
            {
                runner = runner.Next?.Next;
                prev = current;
                current = current.Next;
            }

            //Remove Current
            prev.Next = current.Next;
        }

        // 2.4 Write code to partition a linked list around a value x, such that all nodes less than x come
        // before all nodes greater than or equal to x. If x is contained within the list, the values of x only need
        //     to be after the elements less than x (see below). The partition element x can appear anywhere in the
        // "right partition"; it does not need to appear between the left and right partitions.
        //     EXAMPLE
        // Input: 3 -> 5 -> 8 -> 5 -> 10 -> 2 -> 1 [partition= 5]
        // Output: 3 -> 1 -> 2 -> 10 -> 5 -> 5 -> 8 
        public static LinkListNode Partition(LinkListNode root, int partition)
        {
            LinkListNode leftPartRoot = null,
                rightPartRoot = null,
                leftPartPointer = null,
                rightPartPointer = null;

            var current = root;
            while (current != null)
            {
                var next = current.Next;

                if (current.Value < partition)
                {
                    if (leftPartRoot == null)
                    {
                        leftPartRoot = current;
                        leftPartPointer = current;
                    }
                    else
                    {
                        leftPartPointer.Next = current;
                        leftPartPointer = leftPartPointer.Next;
                    }
                }
                else
                {
                    if (rightPartRoot == null)
                    {
                        rightPartRoot = current;
                        rightPartPointer = current;
                    }
                    else
                    {
                        rightPartPointer.Next = current;
                        rightPartPointer = rightPartPointer.Next;
                    }
                }

                current = next;
            }

            leftPartPointer.Next = rightPartRoot;
            rightPartPointer.Next = null;

            return leftPartRoot;
        }

        // 2.5 You have two numbers represented by a linked list, where each node contains a single
        // digit. The digits are stored in reverse order, such that the 1 's digit is at the head of the list. Write a
        //     function that adds the two numbers and returns the sum as a linked list.
        //     EXAMPLE
        //     Input: (7-> 1 -> 6) + (5 -> 9 -> 2).That is,617 + 295.
        // Output: 2 -> 1 -> 9. That is, 912.
        // FOLLOW UP
        // Suppose the digits are stored in forward order. Repeat the above problem.
        //     EXAMPLE
        //     lnput:(6 -> 1 -> 7) + (2 -> 9 -> 5).That is,617 + 295.
        // Output: 9 -> 1 -> 2. That is, 912. 
        public static int SumLists(LinkListNode root1, LinkListNode root2)
        {
            var builder1 = new StringBuilder();
            var builder2 = new StringBuilder();

            var current = root1;
            while (current != null)
            {
                builder1.Append(current.Value);
                current = current.Next;
            }

            current = root2;
            while (current != null)
            {
                builder2.Append(current.Value);
                current = current.Next;
            }

            return int.Parse(string.Join("", builder1.ToString().Reverse())) +
                   int.Parse(string.Join("", builder2.ToString().Reverse()));
        }

        // 2.6 Implement a function to check if a linked list is a palindrome. 
        public static bool IsPalindrome(LinkListNode root)
        {
            var fromStart = root;
            int? depth = null;

            return Dfs(root);

            bool Dfs(LinkListNode current, int dep = 0)
            {
                if (current.Next == null)
                {
                    depth = dep;
                    if (fromStart.Value != current.Value)
                        return false;
                    fromStart = fromStart.Next;
                    return true;
                }

                if (!Dfs(current.Next, dep + 1))
                    return false;

                if (dep < depth / 2)
                    return true;
                if (fromStart.Value != current.Value)
                    return false;
                fromStart = fromStart.Next;

                return true;
            }
        }

        // 2.7  Given two (singly) linked lists, determine if the two lists intersect. Return the intersecting node. Note that the intersection is defined based on reference, not value.
        // That is, if the kth
        // node of the first linked list is the exact same node (by reference) as the jth node of the second
        // linked list, then they are intersecting. 
        public static LinkListNode IntersectionNode(LinkListNode root1, LinkListNode root2)
        {
            LinkListNode res = null;
            var stack1 = new Stack<LinkListNode>();
            var stack2 = new Stack<LinkListNode>();

            var current = root1;
            while (current != null)
            {
                stack1.Push(current);
                current = current.Next;
            }

            current = root2;
            while (current != null)
            {
                stack2.Push(current);
                current = current.Next;
            }

            if (stack1.Count == 0 || stack2.Count == 0 || stack1.Peek() != stack2.Peek())
                return res;

            while (stack1.Count != 0 && stack2.Count != 0 && stack1.Peek() == stack2.Peek())
            {
                res = stack1.Pop();
                stack2.Pop();
            }

            return res;
        }

        // 2.8 Given a circular linked list, implement an algorithm that returns the node at the
        // beginning of the loop.
        //     DEFINITION
        // Circular linked list: A (corrupt) linked list in which a node's next pointer points to an earlier node, so
        // as to make a loop in the linked list.
        //     EXAMPLE
        //     Input: A -> B -> C -> D -> E -> C [the same C as earlier]
        // Output: C 
        public static int LoopDetection(LinkListNode root)
        {
            var set = new HashSet<int>();
            var current = root;
            while (current != null && set.Add(current.Value))
                current = current.Next;

            return current?.Value ?? -1;
        }

        public static LinkListNode FindBeginning(LinkListNode head)
        {
            var slow = head;
            var fast = head;

            /* Find meeting point. This will be LOOP_SIZE - k steps into the linked list. */
            while (fast?.Next != null)
            {
                slow = slow.Next;
                fast = fast.Next?.Next;
                if (slow == fast)
                    //Collision
                    break;
            }

            /* Error check - no meeting point, and therefore no loop*/
            if (fast?.Next == null)
                return null;

            /* Move slow to Head. Keep fast at Meeting Point. Each are k steps from the
20 * Loop Start. If they move at the same pace, they must meet at Loop Start. */
            slow = head;
            while (slow != fast)
            {
                slow = slow.Next;
                fast = fast.Next;
            }

            /* Both now point to the start of the loop. */
            return fast;
        }
    }

    public class LinkListNode
    {
        public int Value { get; }

        public LinkListNode(int val) => Value = val;
        public LinkListNode Next { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is LinkListNode node &&
                   node.Value == Value &&
                   ((Next == null && node.Next == null) || node.Next.Equals(Next));
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode() + Next.GetHashCode();
        }
    }
}