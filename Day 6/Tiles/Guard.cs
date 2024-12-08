namespace Day_6.Tiles;

public class Guard : ITile
{
    private Coordinate CurrentFacingDirection { get; set; } = Directions.North;

    public List<Coordinate> CalculateExitPath(Coordinate guardPosition, ITile[,] labLayout)
    {
        var plannedRoute = new List<Coordinate>() { guardPosition };
        var labSize = new Coordinate(labLayout.GetLength(1), labLayout.GetLength(0));
        var targetDirection = CurrentFacingDirection;
        var targetPosition = guardPosition;
        Coordinate testPosition;
        var visited = new HashSet<Coordinate> { guardPosition };
        var moveSequence = new List<Coordinate>();

        while (Coordinate.IsInBounds(labSize, targetPosition))
        {
            testPosition = targetPosition + targetDirection;
            
            if (visited.Contains(testPosition) && IsRepeatingSequence(moveSequence))
            {
                // If the position has already been visited, check if the move sequence is repeating
                return new List<Coordinate>(); // Return an empty list to identify loop
            }
            
            switch (Coordinate.IsInBounds(labSize, testPosition))
            {
                case true when labLayout[testPosition.Y, testPosition.X] is Obstacle:
                    targetDirection = GetNewTargetDirection(targetDirection);
                    continue;
                case true when
                    plannedRoute.Find(coord => Equals(coord, testPosition)) == null:
                    plannedRoute.Add(testPosition);
                    visited.Add(testPosition);
                    moveSequence.Add(testPosition);
                    break;
            }

            targetPosition = testPosition;
        }

        // Console.Clear();
        // MapUtilities.VisualiseRoute(plannedRoute, labLayout);

        return plannedRoute;
    }
    
    private static bool IsRepeatingSequence(List<Coordinate> moveSequence)
    {
        // Check if the current move sequence is repeating
        var sequenceLength = moveSequence.Count;
        for (var i = 0; i < sequenceLength / 2; i++)
        {
            if (!moveSequence[i].Equals(moveSequence[sequenceLength / 2 + i]))
            {
                return false;
            }
        }
        return true;
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