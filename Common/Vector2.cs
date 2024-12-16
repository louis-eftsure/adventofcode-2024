using System.Numerics;

namespace Common;

public static class Vector2Extensions
{
    public static readonly Vector2 Zero = new(0, 0);
    
    public static bool IsInBounds(Vector2 size, Vector2 vector2)
    {
        return vector2.X >= 0 && vector2.X < size.X && vector2.Y >= 0 && vector2.Y < size.Y;
    }
}