using System.Collections.Concurrent;
using Common;
using Day_6.Tiles;

namespace Day_6;

public class Guard : ITile
{
    private Vector2 CurrentFacingDirection { get; set; } = Directions.North;

    public List<Move> CalculateExitPath(Vector2 guardPosition, ITile[,] labLayout, out bool isLoop)
    {
        var labSize = new Vector2(labLayout.GetLength(1), labLayout.GetLength(0));
        var visited = new ConcurrentBag<Move>(){new (guardPosition, Vector2.Zero)};
        var visitedPositionsCount = new ConcurrentDictionary<Move, int>();
        isLoop = false;
        
        var currentMove = new Move(guardPosition, CurrentFacingDirection);
        
        while (Vector2.IsInBounds(labSize, currentMove))
        {
            var count = visitedPositionsCount.AddOrUpdate(currentMove, 1, (_, c) => c + 1);
            
            if (count > 25)
            {
                isLoop = true;
                break;
            }
            
            if (labLayout[currentMove.Y, currentMove.X] is Obstacle or LoopObstacle)
            {
                currentMove = new Move(currentMove.Position, GetNewTargetDirection(currentMove.Direction));
                continue;
            }
            
            visited.Add(currentMove);
            currentMove = new Move(currentMove, currentMove.Direction);
        }
        
        return visited.ToList();
    }

    private static Vector2 GetNewTargetDirection(Vector2 targetDirection)
    {
        if (targetDirection.Equals(Directions.North))
            return Directions.East;
        if (targetDirection.Equals(Directions.East))
            return Directions.South;
        if (targetDirection.Equals(Directions.South))
            return Directions.West;

        return Directions.North;
    }
}