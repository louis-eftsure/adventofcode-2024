namespace Common;

public class Vector2
{
    public static readonly Vector2 Zero = new(0, 0);
    public int X { get; }
    public int Y { get; }

    public Vector2(int x, int y)
    {
        X = x;
        Y = y;
    }
    
    public static Vector2 operator +(Vector2 a, Vector2 b)
    {
        return new Vector2(a.X + b.X, a.Y + b.Y);
    }
    
    public static Vector2 operator *(Vector2 a, Vector2 b)
    {
        return new Vector2(a.X * b.X, a.Y * b.Y);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        var other = (Vector2) obj;
        return X == other.X && Y == other.Y;
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
    
    public static bool IsInBounds(Vector2 size, Vector2 vector2)
    {
        return vector2.X >= 0 && vector2.X < size.X && vector2.Y >= 0 && vector2.Y < size.Y;
    }
    
    public override string ToString()
    {
        return $"X: {X}, Y: {Y}";
    }
    
    public static bool operator ==(Vector2 a, Vector2 b)
    {
        return a.Equals(b);
    }
    
    public static bool operator !=(Vector2 a, Vector2 b)
    {
        return !a.Equals(b);
    }
}