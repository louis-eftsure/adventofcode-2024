namespace Day_15;

public class NormalMovementStrategy : IMovementStrategy
{
    private (int dRow, int dCol) GetDirectionOffset(char direction) => direction switch
    {
        '^' => (-1, 0),
        'v' => (1, 0),
        '<' => (0, -1),
        '>' => (0, 1),
        _ => throw new ArgumentException("Invalid direction")
    };

    private bool IsValidPosition(char[,] grid, int row, int col) =>
        row >= 0 && row < grid.GetLength(0) && col >= 0 && col < grid.GetLength(1);

    public bool TryMove(char[,] grid, char direction, ref (int Row, int Col) robotPosition)
    {
        var (dRow, dCol) = GetDirectionOffset(direction);
        var newRobotPos = (Row: robotPosition.Row + dRow,Col: robotPosition.Col + dCol);
        
        if (!IsValidPosition(grid, newRobotPos.Row, newRobotPos.Col) || 
            grid[newRobotPos.Row, newRobotPos.Col] == '#')
            return false;

        if (grid[newRobotPos.Row, newRobotPos.Col] == 'O')
        {
            var boxPositions = new List<(int Row, int Col)>();
            var currentPos = newRobotPos;
            
            while (IsValidPosition(grid, currentPos.Row, currentPos.Col) && 
                   grid[currentPos.Row, currentPos.Col] == 'O')
            {
                boxPositions.Add(currentPos);
                currentPos = (currentPos.Row + dRow, currentPos.Col + dCol);
            }

            if (!IsValidPosition(grid, currentPos.Row, currentPos.Col) || 
                grid[currentPos.Row, currentPos.Col] != '.')
                return false;

            // Move boxes from back to front
            for (int i = boxPositions.Count - 1; i >= 0; i--)
            {
                var boxPos = boxPositions[i];
                var nextPos = (Row: boxPos.Row + dRow,Col: boxPos.Col + dCol);
                grid[nextPos.Row, nextPos.Col] = 'O';
                grid[boxPos.Row, boxPos.Col] = '.';
            }
        }

        grid[robotPosition.Row, robotPosition.Col] = '.';
        grid[newRobotPos.Row, newRobotPos.Col] = '@';
        robotPosition = newRobotPos;
        return true;
    }
}
