namespace Common;

public static class Directions
{
    public static Vector2 North => new(0, -1);
    public static Vector2 South => new(0, 1);
    public static Vector2 East => new(1, 0);
    public static Vector2 West => new(-1, 0);
    
    public static Vector2 NorthEast => new(1, -1);
    public static Vector2 SouthEast => new(1, 1);
    public static Vector2 SouthWest => new(-1, 1);
    public static Vector2 NorthWest => new(-1, -1);
    
    public static Vector2[] AllDirections => new[]
    {
        North, NorthEast, East, SouthEast, South, SouthWest, West, NorthWest
    };
    
    public static Vector2[] CardinalDirections => new[]
    {
        North, East, South, West
    };
}