using System.Numerics;
using Common;

namespace Day_6;

public class Move
{
    public Vector2 MoveVector2 { get; set; }
    public Move(Vector2 position, Vector2 direction)
    {
        MoveVector2 = new Vector2(position.X + direction.X, position.Y + direction.Y);
        Position = position;
        Direction = direction;
    }
    
    public Move(Move move, Vector2 direction)
    {
        MoveVector2 = new Vector2(move.MoveVector2.X + direction.X, move.MoveVector2.Y + direction.Y);
        Position = new Vector2(move.MoveVector2.X, move.MoveVector2.Y);
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
    
    public static Vector2 ToVector2(Move move) => new(move.MoveVector2.X, move.MoveVector2.Y);
}