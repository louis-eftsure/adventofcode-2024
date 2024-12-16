using System.Numerics;
using Common;

namespace Day_10;

public class TrailStep
{
    public TrailStep(Vector2 vector2, int step)
    {
        Vector2 = vector2;
        Step = step;
    }

    public Vector2 Vector2 { get; }
    public int Step { get; }
    
    public override string ToString()
    {
        return $"Coordinate: {Vector2}, Step: {Step}";
    }
    
    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        var step = (TrailStep) obj;
        return Vector2.Equals(step.Vector2) && Step == step.Step;
    }
    
    public override int GetHashCode()
    {
        return Vector2.GetHashCode() + Step.GetHashCode();
    }
}