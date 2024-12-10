using Common;

namespace Day_6;

public class Move : Coordinate
{
    public Move(Coordinate coordinate, Coordinate direction): base(coordinate.X + direction.X, coordinate.Y+ direction.Y)
    {
        Coordinate = coordinate;
        Direction = direction;
    }
    
    public Move(Move coordinate, Coordinate direction): base(coordinate.X + direction.X, coordinate.Y+ direction.Y)
    {
        Coordinate = new Coordinate(coordinate.X, coordinate.Y);
        Direction = direction;
    }

    public Coordinate Coordinate { get; }
    public Coordinate Direction { get; }

    public override string ToString()
    {
        return $"Coordinate: {Coordinate}, Direction: {Direction}";
    }
    
    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        var move = (Move) obj;
        return Coordinate.Equals(move.Coordinate) && Direction.Equals(move.Direction);
    }

    public override int GetHashCode()
    {
        return Coordinate.GetHashCode() + Direction.GetHashCode();
    }
}