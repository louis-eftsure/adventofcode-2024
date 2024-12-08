namespace Day_8;

public class Antenna
{
    public Antenna(Coordinate position, char channel)
    {
        Position = position;
        Channel = channel;
    }

    public Coordinate Position { get; }
    public char Channel { get; }
}