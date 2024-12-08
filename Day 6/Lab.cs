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
        var routeSteps = guard.CalculateExitPath(GetGuardPosition(), Tiles);

        Parallel.For(1, routeSteps.Count, i =>
        {
            var step = routeSteps[i];
            var testLayout = Tiles.Clone() as ITile[,];
            testLayout[step.Y, step.X] = new LoopObstacle();
            if (HasInfiniteLoop(guard, testLayout))
                options++;
        });
        
        return options;
    }
    
    public bool HasInfiniteLoop(Guard guard, ITile?[,]? testLayout)
    {
        var routeSteps = guard.CalculateExitPath(GetGuardPosition(), testLayout);
        return routeSteps.Count == 0;
    }
    
}