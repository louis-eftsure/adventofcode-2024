namespace Day_16.AStar;

public class Node
{
    public Position Position { get; set; }
    public Node Parent { get; set; }
    public int G { get; set; }  // Cost from start to current node
    public int H { get; set; }  // Estimated cost from current node to target
    public int F => G + H;      // Total cost
    public List<Node> EqualCostParents { get; set; } = new List<Node>();

    public Node(Position position)
    {
        Position = position;
    }
}