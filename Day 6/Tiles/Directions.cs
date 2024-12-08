namespace Day_6.Tiles;

public static class Directions
{
    public static Coordinate North => new(0, -1);
    public static Coordinate South => new(0, 1);
    public static Coordinate East => new(1, 0);
    public static Coordinate West => new(-1, 0);
}