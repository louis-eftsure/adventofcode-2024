namespace Day_16.AStar;

public class Position
{
    public int X { get; set; }
    public int Y { get; set; }
    public Direction Facing { get; set; }

    public Position(int x, int y, Direction facing)
    {
        X = x;
        Y = y;
        Facing = facing;
    }

    public override bool Equals(object obj)
    {
        if (obj is Position other)
        {
            return X == other.X && Y == other.Y && Facing == other.Facing;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Facing);
    }
}