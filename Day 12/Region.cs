using System.Numerics;

namespace Day_12;

public class Region
{
    public char Plant { get; set; }
    public Vector2 StartPos { get; }
    public List<Vector2> Plants { get; set; } = [];
    public List<Vector2> Parameters { get; set; } = [];

    public Region(char plant, Vector2 startPos)
    {
        Plant = plant;
        StartPos = startPos;
    }

    public double GetFenceCost()
    {
        return Plants.Count * Parameters.Count;
    }
}