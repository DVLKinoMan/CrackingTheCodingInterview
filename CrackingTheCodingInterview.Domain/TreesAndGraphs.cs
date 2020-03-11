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
    }
}