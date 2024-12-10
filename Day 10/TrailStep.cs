using Common;

namespace Day_10;

public class TrailStep
{
    public TrailStep(Coordinate coordinate, int step)
    {
        Coordinate = coordinate;
        Step = step;
    }

    public Coordinate Coordinate { get; }
    public int Step { get; }
    
    public override string ToString()
    {
        return $"Coordinate: {Coordinate}, Step: {Step}";
    }
    
    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        var step = (TrailStep) obj;
        return Coordinate.Equals(step.Coordinate) && Step == step.Step;
    }
    
    public override int GetHashCode()
    {
        return Coordinate.GetHashCode() + Step.GetHashCode();
    }
}