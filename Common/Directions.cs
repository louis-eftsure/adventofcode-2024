namespace Common;

public static class Directions
{
    public static Coordinate North => new(0, -1);
    public static Coordinate South => new(0, 1);
    public static Coordinate East => new(1, 0);
    public static Coordinate West => new(-1, 0);
    
    public static Coordinate NorthEast => new(1, -1);
    public static Coordinate SouthEast => new(1, 1);
    public static Coordinate SouthWest => new(-1, 1);
    public static Coordinate NorthWest => new(-1, -1);
    
    public static Coordinate[] AllDirections => new[]
    {
        North, NorthEast, East, SouthEast, South, SouthWest, West, NorthWest
    };
    
    public static Coordinate[] CardinalDirections => new[]
    {
        North, East, South, West
    };
}