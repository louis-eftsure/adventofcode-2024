namespace Common;

public class Coordinate
{
    public int X { get; }
    public int Y { get; }

    public Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }
    
    public static Coordinate operator +(Coordinate a, Coordinate b)
    {
        return new Coordinate(a.X + b.X, a.Y + b.Y);
    }
    
    public static Coordinate operator *(Coordinate a, Coordinate b)
    {
        return new Coordinate(a.X * b.X, a.Y * b.Y);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        var other = (Coordinate) obj;
        return X == other.X && Y == other.Y;
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
    
    public static bool IsInBounds(Coordinate size, Coordinate coordinate)
    {
        return coordinate.X >= 0 && coordinate.X < size.X && coordinate.Y >= 0 && coordinate.Y < size.Y;
    }
}