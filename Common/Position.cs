
using System.Numerics;

namespace Common;

public class Position
{
    public Vector2 Coordinate { get; set; }
    public Vector2 Facing { get; set; }

    public Position(int x, int y, Vector2 facing)
    {
        Coordinate = new Vector2(x, y);
        Facing = facing;
    }
    
    public Position(Vector2 coordinate, Vector2 facing)
    {
        Coordinate = coordinate;
        Facing = facing;
    }

    public override bool Equals(object obj)
    {
        if (obj is Position other)
        {
            return Coordinate.X == other.Coordinate.X && Coordinate.Y == other.Coordinate.Y && Facing == other.Facing;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Coordinate.X, Coordinate.Y, Facing);
    }
}