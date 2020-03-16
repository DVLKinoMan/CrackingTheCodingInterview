using System;
using System.Collections;
using System.Collections.Generic;
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
        // public static IList<char> BuildOrder(IList<char> projects, IList<(char, char)> dependencies)
        // {
        // }
    }
}