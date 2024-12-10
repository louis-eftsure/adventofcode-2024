using Common;

namespace Day_6;

public class Move : Vector2
{
    public Move(Vector2 position, Vector2 direction): base(position.X + direction.X, position.Y+ direction.Y)
    {
        Position = position;
        Direction = direction;
    }
    
    public Move(Move coordinate, Vector2 direction): base(coordinate.X + direction.X, coordinate.Y+ direction.Y)
    {
        Position = new Vector2(coordinate.X, coordinate.Y);
        Direction = direction;
    }

    public Vector2 Position { get; }
    public Vector2 Direction { get; }

    public override string ToString()
    {
        return $"Coordinate: {Position}, Direction: {Direction}";
    }
    
    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        var move = (Move) obj;
        return Position.Equals(move.Position) && Direction.Equals(move.Direction);
    }

    public override int GetHashCode()
    {
        return Position.GetHashCode() + Direction.GetHashCode();
    }
    
    public static Vector2 ToVector2(Move move) => new(move.X, move.Y);
}