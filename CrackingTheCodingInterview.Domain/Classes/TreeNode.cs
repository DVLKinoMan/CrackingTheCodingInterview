namespace CrackingTheCodingInterview.Domain.Classes
{
    public class TreeNode
    {
        public TreeNode(int val) => this.Val = val;

        public int Val { get; }
        public TreeNode Left { get; set; }
        public TreeNode Right { get; set; }
        
        public override bool Equals(object? obj)
        {
            var treeNode = obj as TreeNode;
            return this.Val == treeNode.Val &&
                   ((treeNode.Left == null && Left == null) || treeNode.Left.Equals(Left)) &&
                   ((treeNode.Right == null && Right == null) || treeNode.Right.Equals(Right));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}