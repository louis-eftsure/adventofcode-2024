namespace Day_16.AStar;

public class Route
{
    public List<Position> Path { get; set; }
    public int Cost { get; set; }

    public Route(List<Position> path, int cost)
    {
        Path = path;
        Cost = cost;
    }
}