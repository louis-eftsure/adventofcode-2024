using System.Numerics;
using Common;

namespace Day_10;

public class TrailMap
{
    public List<Trail> Trails { get; set; } = new();
    int[,] CharMap { get; set; }
    public TrailMap(string[] input)
    {
        CharMap = new int[input.Length, input[0].Length];
        for (int y = 0; y < input.Length; y++)
        {
            for (int x = 0; x < input[y].Length; x++)
            {
                var trailStepValue = int.Parse(input[y][x].ToString());
                CharMap[y, x] = trailStepValue;

                if (trailStepValue == 0)
                {
                    Trails.Add(new Trail(new TrailStep(new Vector2(x, y), trailStepValue)));
                }
            }
        }

        CompleteTrails();
    }

    public void CompleteTrails()
    {
        foreach (var trail in Trails)
        {
            
            CompleteTrail(trail);
        }
    }

    private void CompleteTrail(Trail trail)
    {
        var currentStep = trail.Path[0];
        while(true)
        {
            var surroundingSteps = GetSurroundingSteps(currentStep);
            var possibleNextSteps = surroundingSteps.Where(surroundingStep => surroundingStep.Step == currentStep.Step + 1).ToList();
            
            if(possibleNextSteps.Count > 1)
            {
                // Split trail
                foreach(var step in possibleNextSteps)
                {
                    var newTrail = new Trail(step);
                    
                    CompleteTrail(newTrail);
                    if(newTrail.CanReachSummit())
                        trail.AddTrailHead(newTrail);
                }
                break;
            }
            
            if(possibleNextSteps.Count == 1)
            {
                // Continue trail
                trail.Add(possibleNextSteps[0]);
                currentStep = possibleNextSteps[0];
            }
            else
            {
                // End trail
                break;
            }
        }
    }

    private List<TrailStep> GetSurroundingSteps(TrailStep currentStep)
    {
        var result = new List<TrailStep>();
        foreach(var direction in Directions.CardinalDirections)
        {
            var x = currentStep.Vector2.X + direction.X;
            var y = currentStep.Vector2.Y + direction.Y;
            if (x >= 0 && x < CharMap.GetLength(1) && y >= 0 && y < CharMap.GetLength(0))
            {
                result.Add(new TrailStep(new Vector2(x, y), CharMap[y, x]));
            }
        }
        
        return result;
    }

    public int CountTrailHeads()
    {
        var result = Trails.Sum(trail => trail.GetTrailScore());
        return result;
    }
    
    public int CountUniqueTrailHeads()
    {
        var result = Trails.Sum(trail => trail.GetUniqueTrailScore());
        return result;
    }

    // public int GetTrailScores()
    // {
    //     var result = Trails.Sum(trail => trail.GetUpdatedTrailScore());
    //     return result;
    // }
}