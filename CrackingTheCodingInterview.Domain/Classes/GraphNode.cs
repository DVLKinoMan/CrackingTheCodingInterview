namespace CrackingTheCodingInterview.Domain.Classes
{
    public class GraphNode
    {
        public int Val { get; set; }
        public GraphNode(int val) => this.Val = val; 
        public GraphNode[] Children = new GraphNode[0];
    }
}