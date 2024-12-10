using Common;
using Day_6.Tiles;

namespace Day_6;

public class Lab
{
    public ITile?[,]? Tiles { get; }
    public Coordinate Size { get; }
    private Coordinate _guardPosition { get; }
    public Lab(char[][] lab)
    {
        Tiles = new ITile[lab.Length, lab[0].Length];
        for (var y = 0; y < lab.Length; y++)
        {
            for (var x = 0; x < lab[y].Length; x++)
            {
                if(lab[y][x] == '^')
                {
                    _guardPosition = new Coordinate(x, y);
                    continue;
                }
                Tiles[y,x] = lab[y][x] switch
                {
                    '#' => new Obstacle(),
                    _ => null
                };
            }
        }
        
        Size = new Coordinate(lab[0].Length, lab.Length);
    }
    
    public Coordinate GetGuardPosition()
    {
        return _guardPosition;
    }

    public int GetPathLoopOptions(Guard guard)
    {
        var options = 0;
        var routeSteps = guard.CalculateExitPath(GetGuardPosition(), Tiles, out _);
        
        // foreach(var move in routeSteps)
        // {
        //     var tempTile = Tiles[move.Coordinate.Y, move.Coordinate.X];
        //     Tiles[move.Coordinate.Y, move.Coordinate.X] = new LoopObstacle();
        //     if (HasInfiniteLoop(guard, Tiles))
        //         options++;
        //     Tiles[move.Coordinate.Y, move.Coordinate.X] = tempTile;
        // }
        Parallel.ForEach(routeSteps, move =>
        {
            var tilesClone = Tiles.Clone() as ITile[,];
            tilesClone[move.Y, move.X] = new LoopObstacle();
            if (HasInfiniteLoop(guard, tilesClone))
                Interlocked.Increment(ref options);
        });
        
        return options;
    }
    
    public bool HasInfiniteLoop(Guard guard, ITile[,] testLayout)
    {
        var route = guard.CalculateExitPath(_guardPosition, testLayout, out var isLoop);
        // MapUtilities.VisualiseRoute(route, testLayout);
        return isLoop;
    }
}