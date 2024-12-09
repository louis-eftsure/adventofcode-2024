namespace Day_6.Tiles;

public class Guard : ITile
{
    private Coordinate CurrentFacingDirection { get; set; } = Directions.North;

    public List<Move> CalculateExitPath(Coordinate guardPosition, ITile[,] labLayout, out bool isLoop)
    {
        var labSize = new Coordinate(labLayout.GetLength(1), labLayout.GetLength(0));
        var testMove = new Move(guardPosition, CurrentFacingDirection);
        var visited = new List<Move>();
        isLoop = false;

        while (Coordinate.IsInBounds(labSize, testMove))
        {
            if (visited.Count > labSize.X * labSize.Y)
            {
                isLoop = true;
                return visited;
            }
            
            switch (Coordinate.IsInBounds(labSize, testMove))
            {
                case true when labLayout[testMove.Y, testMove.X] is Obstacle || labLayout[testMove.Y, testMove.X] is LoopObstacle:
                    testMove = new Move(testMove.Coordinate, GetNewTargetDirection(testMove.Direction));
                    continue;
                case true:
                    visited.Add(testMove);
                    testMove = new Move(testMove, testMove.Direction);
                    break;
            }
        }

        return visited;
    }

    private static Coordinate GetNewTargetDirection(Coordinate targetDirection)
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