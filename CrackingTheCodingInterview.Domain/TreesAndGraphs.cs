using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using CrackingTheCodingInterview.Domain.Classes;

namespace CrackingTheCodingInterview.Domain
{
    public static class TreesAndGraphs
    {
        // 4.1 Given a directed graph, design an algorithm to find out whether there is a
        // route between two nodes. 
        public static bool IsRoute(GraphNode node1, GraphNode node2)
        {
            var visited = new HashSet<GraphNode>();
            return Dfs(node1);

            bool Dfs(GraphNode node)
            {
                if (node == node2)
                    return true;
                if (visited.Contains(node))
                    return false;
                visited.Add(node);
                foreach (var child in node.Children)
                    if (Dfs(child))
                        return true;

                return false;
            }
        }

        // 4.2 Given a sorted (increasing order) array with unique integer elements, write an
        // algorithm to create a binary search tree with minimal height.
        public static TreeNode MinimalTree(int[] SortedArray)
        {
            return ConstructTreeNode(0, SortedArray.Length - 1);

            TreeNode ConstructTreeNode(int stIndex, int endIndex)
            {
                if (stIndex > endIndex)
                    return null;

                int index = (stIndex + endIndex) / 2;
                return new TreeNode(SortedArray[index])
                {
                    Left = ConstructTreeNode(stIndex, index - 1),
                    Right = ConstructTreeNode(index + 1, endIndex)
                };
            }
        }

        // 4.3 Given a binary tree, design an algorithm which creates a linked list of all the nodes
        // at each depth (e.g., if you have a tree with depth D, you'll have D linked lists). 
        public static IEnumerable<LinkListNode> ConstructLinkListNodes(TreeNode root)
        {
            var list = new List<LinkListNode>();
            var queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            while (queue.Count != 0)
            {
                var children = new Queue<TreeNode>();
                LinkListNode rootListNode = null;
                var current = rootListNode;
                while (queue.Count != 0)
                {
                    var node = queue.Dequeue();
                    if (rootListNode == null)
                    {
                        rootListNode = new LinkListNode(node.Val);
                        current = rootListNode;
                    }
                    else
                    {
                        current.Next = new LinkListNode(node.Val);
                        current = current.Next;
                    }

                    if (node.Left != null)
                        children.Enqueue(node.Left);
                    if (node.Right != null)
                        children.Enqueue(node.Right);
                }

                list.Add(rootListNode);
                queue = children;
            }

            return list;
        }

        // 4.4 Implement a function to check if a binary tree is balanced. For the purposes of
        // this question, a balanced tree is defined to be a tree such that the heights of the two subtrees of any
        // node never differ by more than one. 
        public static bool IsBalancedTree(TreeNode root)
        {
            int minHeight = int.MaxValue,
                maxHeight = 0;
            Dfs(root);
            return maxHeight - minHeight <= 1;

            void Dfs(TreeNode node, int current = 0)
            {
                if (node == null)
                {
                    minHeight = Math.Min(minHeight, current);
                    maxHeight = Math.Max(maxHeight, current);
                    return;
                }

                Dfs(node.Left, current + 1);
                Dfs(node.Right, current + 1);
            }
        }

        // 4.5 Implement a function to check if a binary tree is a binary search tree. 
        public static bool IsBinarySearchTree(TreeNode root, int min = int.MinValue, int max = int.MaxValue)
        {
            if (root == null)
                return true;

            return root.Val < max &&
                   root.Val > min &&
                   IsBinarySearchTree(root.Left, min, root.Val) &&
                   IsBinarySearchTree(root.Right, root.Val, max);
        }

        // 4.6 Write an algorithm to find the "next" node (i.e., in-order successor) of a given node in a
        //     binary search tree. You may assume that each node has a link to its parent. 
        public static TreeNode NextNode(TreeNode root, TreeNode node)
        {
            var curr = node;
            if (node.Right != null)
            {
                curr = curr.Right;
                if (curr == null)
                    return null;

                while (curr.Left != null)
                    curr = curr.Left;

                return curr;
            }

            SetupParentsOfTreeNode(root);
            while (curr != null && curr == curr.Parent.Right)
                curr = curr.Parent;

            if (curr == null)
                return null;

            curr = curr.Parent.Right;
            if (curr == null)
                return null;

            while (curr.Left != null)
                curr = curr.Left;

            return curr;
        }

        private static void SetupParentsOfTreeNode(TreeNode curr, TreeNode par = null)
        {
            if (curr == null)
                return;
            curr.Parent = par;
            SetupParentsOfTreeNode(curr.Left, curr);
            SetupParentsOfTreeNode(curr.Right, curr);
        }

        // 4.7 You are given a list of projects and a list of dependencies (which is a list of pairs of
        // projects, where the second project is dependent on the first project). All of a project's dependencies
        //     must be built before the project is. Find a build order that will allow the projects to be built. If there
        // is no valid build order, return an error.
        //     EXAMPLE
        // Input:
        // projects: a, b, c, d, e, f
        //     dependencies: (a, d), (f, b), (b, d), (f, a), (d, c)
        //     Output: f, e, a, b, d, c
        public static IList<char> BuildOrder(IList<char> projects, IList<(char x, char y)> dependencies)
        {
            var beforeDict = new Dictionary<char, HashSet<char>>();
            var afterDict = new Dictionary<char, HashSet<char>>();
            var result = new List<char>();

            foreach (var proj in projects)
            {
                beforeDict.Add(proj, new HashSet<char>());
                afterDict.Add(proj, new HashSet<char>());
            }

            foreach (var dependency in dependencies)
            {
                beforeDict[dependency.y].Add(dependency.x);
                afterDict[dependency.x].Add(dependency.y);
            }

            var keys = beforeDict.Where(bf => bf.Value.Count == 0)
                .Select(bf => bf.Key).ToList();

            while (keys.Count != 0)
            {
                result.AddRange(keys);
                var newKeys = new List<char>();
                foreach (var key in keys)
                foreach (var secondKey in afterDict[key])
                {
                    beforeDict[secondKey].Remove(key);
                    if (beforeDict[secondKey].Count == 0)
                        newKeys.Add(secondKey);
                }

                keys = newKeys;
            }

            return result.Count == projects.Count ? result : new List<char>();
        }

        //todo: BuildOrder with Dfs

        // 4.8 First Common Ancestor: Design an algorithm and write code to find the first common ancestor
        // of two nodes in a binary tree. Avoid storing additional nodes in a data structure. NOTE: This is not
        //     necessarily a binary search tree. 
        public static TreeNode FirstCommonAncestor(TreeNode root, TreeNode node1, TreeNode node2)
        {
            TreeNode answer = null;
            Dfs(root);
            return answer;

            int Dfs(TreeNode node)
            {
                if (node == null)
                    return 0;

                int count = 0;
                if (node == node1 || node == node2)
                    count += 1;
                count += Dfs(node.Left);
                count += Dfs(node.Right);
                if (count == 2 && answer == null)
                    answer = node;
                return count;
            }
        }

        // 4.9 BST Sequences: A binary search tree was created by traversing through an array from left to right
        // and inserting each element. Given a binary search tree with distinct elements, print all possible
        //     arrays that could have led to this tree.
        //     EXAMPLE
        //     Input:
        // Output: {2, 1, 3}, {2, 3, 1} 
        /// <summary>
        /// todo: do not understand
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static IList<IList<int>> BstSequences(TreeNode root)
        {
            var result = new List<IList<int>>();
            if (root == null)
            {
                result.Add(new List<int>());
                return result;
            }

            var prefix = new List<int>();
            prefix.Add(root.Val);

            /* Recurse on left and right subtrees. */
            var leftSeq = BstSequences(root.Left);
            var rightSeq = BstSequences(root.Right);

            /* Weave together each list from the left and right sides. */
            for (var i = 0; i < leftSeq.Count; i++)
            {
                var left = leftSeq[i];
                foreach (var right in rightSeq)
                {
                    IList<IList<int>> weaved =
                        new List<IList<int>>();
                    WeaveLists(left, right, weaved, prefix);
                    result.AddRange(weaved);
                }
            }

            return result;

            static void WeaveLists(IList<int> first, IList<int> second,
                IList<IList<int>> results, List<int> prefix)
            {
                /* One list is empty. Add remainder to [a cloned] prefix and store result. */
                if (first.Count == 0 || second.Count == 0)
                {
                    var result2 = prefix.ToList();
                    result2.AddRange(first);
                    result2.AddRange(second);
                    results.Add(result2);
                    return;
                }

                /* Recurse with head of first added to the prefix. Removing the head will damage
 43 * first, so ll need to put it back where we found it afterwards. */
                int headFirst = first[0];
                first.RemoveAt(0);
                prefix.Add(headFirst);
                WeaveLists(first, second, results, prefix);
                prefix.RemoveAt(prefix.Count - 1);
                first.Insert(0, headFirst);

                /* Do the same thing with second, damaging and then restoring the list.*/
                int headSecond = second[0];
                second.RemoveAt(0);
                prefix.Add(headSecond);
                WeaveLists(first, second, results, prefix);
                prefix.RemoveAt(prefix.Count - 1);
                second.Insert(0, headSecond);
            }
        }
        
        // 4.10 Tl and T2 are two very large binary trees, with Tl much bigger than T2. Create an
        //     algorithm to determine if T2 is a subtree of Tl.
        //     A tree T2 is a subtree of Tl if there exists a node n in Tl such that the subtree of n is identical to T2.
        //     That is, if you cut off the tree at node n, the two trees would be identical. 
        public static bool IsSubtree(TreeNode root1, TreeNode root2)
        {
            if (root1 == null && root2 == null)
                return true;
            if (root1 == null || root2 == null)
                return false;

            if( root1.Val == root2.Val &&
                   IsSubtree(root1.Left, root2.Left) &&
                   IsSubtree(root1.Right, root2.Right))
                return true;

            return IsSubtree(root1.Left, root2) || IsSubtree(root2.Right, root2);
        }  
        
        // 4.11 Random Node: You are implementing a binary tree class from scratch which, in addition to
        // insert, find, and delete, has a method getRandomNode() which returns a random node
        //     from the tree. All nodes should be equally likely to be chosen. Design and implement an algorithm
        // for getRandomNode, and explain how you would implement the rest of the methods.
        public class BinarySearchTree
        {
            private readonly List<TreeNode> _allNodes;
            private TreeNode _root;

            public BinarySearchTree()
            {
                _allNodes = new List<TreeNode>();
            }

            public void Insert(int val)
            {
                var node = new TreeNode(val);
                _allNodes.Add(node);
                var curr = _root;
                TreeNode prev = null;
                while (curr != null)
                {
                    prev = curr;
                    curr = curr.Val > val ? curr.Right : curr.Left;
                }

                if (prev == null)
                    _root = node;
                else
                {
                    if (prev.Val > val)
                        prev.Left = node;
                    else prev.Right = node;
                }
            }

            public void Delete(int val)
            {
                var curr = _root;
                TreeNode parent = null;
                while (curr!=null && curr.Val!=val)
                {
                    parent = curr;
                    curr = curr.Val > val ? curr.Right : curr.Left;
                }

                if (curr == null)
                    return;

                if (parent == null)
                {
                    var root = _root;
                    _root = curr.Right;
                    var mostLeft = _root;
                    while (mostLeft.Left != null)
                        mostLeft = mostLeft.Left;
                    mostLeft.Left = root.Left;
                }
                else
                {
                    if (curr.Right == null)
                        parent.Right = curr.Left;
                    else
                    {
                        parent.Right = curr.Right;
                        var mostLeft = curr.Right;
                        while (mostLeft.Left != null)
                            mostLeft = mostLeft.Left;
                        mostLeft.Left = curr.Left;
                    }
                }
                _allNodes.Remove(curr);
            }

            public TreeNode Find(int val)
            {
                var curr = _root;
                while (true)
                {
                    if (curr == null)
                        return null;
                    if (curr.Val == val)
                        return curr;
                    curr = curr.Val > val ? curr.Right : curr.Left;
                }
            }

            public TreeNode GetRandomNode()
            {
                var rand = new Random();
                return _allNodes[rand.Next(0, _allNodes.Count - 1)];
            }
        }
        
        // 4.12 You are given a binary tree in which each node contains an integer value (which
        // might be positive or negative). Design an algorithm to count the number of paths that sum to a
        //     given value. The path does not need to start or end at the root or a leaf, but it must go downwards
        //     (traveling only from parent nodes to child nodes).
        public static int PathsWithSumCount(TreeNode root, int targetSum)
        {
            var dict = new Dictionary<int, int>();
            int count = 0;
            Dfs(root);
            return count;
            
            void Dfs(TreeNode node, int currSum = 0)
            {
                if (node == null)
                    return;

                currSum += node.Val;
                if (dict.ContainsKey(targetSum - currSum))
                    count += dict[targetSum - currSum];
                dict[currSum] = dict.ContainsKey(currSum) ? dict[currSum] + 1 : 1;
                Dfs(node.Left, currSum);
                Dfs(node.Right, currSum);
                dict[currSum]--;
            }
        }
    }
}