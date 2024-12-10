using Common;

namespace Day_10;

public class Trail
{
    public List<TrailStep> Path { get; set; } = new();
    public List<Trail> TrailHeads { get; set; } = new();

    public Trail(TrailStep start)
    {
        Path.Add(start);
    }

    public void Add(TrailStep coordinate)
    {
        Path.Add(coordinate);
    }

    public void AddTrailHead(Trail trail)
    {
        TrailHeads.Add(trail);
    }

    public int GetTrailScore()
    {
        return GetReachableSummits().Count;
    }
    
    public int GetUniqueTrailScore()
    {
        return GetReachableSummits().Distinct().Count();
    }

    public List<TrailStep> GetReachableSummits()
    {
        var reachableSummits = new List<TrailStep>();

        if (IsSummit())
        {
            reachableSummits.Add(Path[^1]);
        }

        foreach (var trail in TrailHeads)
        {
            reachableSummits.AddRange(trail.GetReachableSummits());
        }

        return reachableSummits;
    }

    public bool CanReachSummit()
    {
        return IsSummit() || TrailHeads.Exists(trail => trail.CanReachSummit());
    }

    public bool IsSummit()
    {
        return Path.Exists(x => x.Step == 9);
    }
}