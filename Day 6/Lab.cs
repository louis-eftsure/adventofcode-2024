using Common;
using Day_6.Tiles;

namespace Day_6;

public class Lab
{
    public ITile?[,]? Tiles { get; }
    public Vector2 Size { get; }
    private Vector2 _guardPosition { get; }
    public Lab(char[][] lab)
    {
        Tiles = new ITile[lab.Length, lab[0].Length];
        for (var y = 0; y < lab.Length; y++)
        {
            for (var x = 0; x < lab[y].Length; x++)
            {
                if(lab[y][x] == '^')
                {
                    _guardPosition = new Vector2(x, y);
                    Tiles[y, x] = new Guard();
                    continue;
                }
                Tiles[y,x] = lab[y][x] switch
                {
                    '#' => new Obstacle(),
                    _ => null
                };
            }
        }
        
        Size = new Vector2(lab[0].Length, lab.Length);
    }
    
    public Vector2 GetGuardPosition()
    {
        return _guardPosition;
    }

    public List<Vector2> GetPathLoopOptions(Guard guard)
    {
        var options = new List<Move>();
        var routeSteps = guard.CalculateExitPath(GetGuardPosition(), Tiles, out _);
        
        foreach(var move in routeSteps)
        {
            var tempTile = Tiles[move.Y, move.X];
            
            if(tempTile is Guard)
                continue;
            
            Tiles[move.Y, move.X] = new LoopObstacle();
            if (HasInfiniteLoop(guard, Tiles) && !options.Exists(timeObstacle => timeObstacle == move))
                options.Add(move);
            Tiles[move.Y, move.X] = tempTile;
        }
        
        return options.Select(Move.ToVector2).Distinct().ToList();
    }
    
    public bool HasInfiniteLoop(Guard guard, ITile[,] testLayout)
    {
        var route = guard.CalculateExitPath(_guardPosition, testLayout, out var isLoop);
        // MapUtilities.VisualiseRoute(route, testLayout);
        return isLoop;
    }
}