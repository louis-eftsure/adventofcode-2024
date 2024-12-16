using Day_15;

public class WideMovementStrategy : IMovementStrategy
{
    private (int dRow, int dCol) GetDirectionOffset(char direction) => direction switch
    {
        '^' => (-1, 0),
        'v' => (1, 0),
        '<' => (0, -2),
        '>' => (0, 2),
        _ => throw new ArgumentException("Invalid direction")
    };

    public bool TryMove(char[,] grid, char direction, ref (int Row, int Col) robotPosition)
    {
        var (dRow, dCol) = GetDirectionOffset(direction);
        var newRobotPos = (Row: robotPosition.Row + dRow, Col: robotPosition.Col + dCol);

        if (!IsValidPosition(grid, newRobotPos.Row, newRobotPos.Col))
            return false;

        // Moving into a box
        if (IsBoxPart(grid, newRobotPos.Row, newRobotPos.Col))
        {
            // Check the entire chain of boxes before moving any
            var boxPositions = GetBoxChain(grid, newRobotPos.Row, newRobotPos.Col, direction);
            if (boxPositions.Count == 0) return false;

            // Check if the last box in the chain can move
            var lastBox = boxPositions[^1];
            var finalTargetPos = (Row: lastBox.Row + dRow, Col: lastBox.Col + dCol);

            if (IsValidPosition(grid, finalTargetPos.Row, finalTargetPos.Col + 1))
            {
                // Target space must be empty (not a wall or another box)
                if (grid[finalTargetPos.Row, finalTargetPos.Col] == '.' && 
                    grid[finalTargetPos.Row, finalTargetPos.Col + 1] == '.')
                {
                    // Move all boxes from back to front
                    foreach (var boxPos in boxPositions.AsEnumerable().Reverse())
                    {
                        var targetPos = (Row: boxPos.Row + dRow, Col: boxPos.Col + dCol);
                        // Move box
                        grid[boxPos.Row, boxPos.Col] = '.';
                        grid[boxPos.Row, boxPos.Col + 1] = '.';
                        grid[targetPos.Row, targetPos.Col] = '[';
                        grid[targetPos.Row, targetPos.Col + 1] = ']';
                    }

                    // Move robot
                    grid[robotPosition.Row, robotPosition.Col] = '.';
                    grid[robotPosition.Row, robotPosition.Col + 1] = '.';
                    grid[newRobotPos.Row, newRobotPos.Col] = '@';
                    grid[newRobotPos.Row, newRobotPos.Col + 1] = '.';
                    robotPosition = newRobotPos;
                    return true;
                }
            }
            return false;
        }

        // Moving into empty space
        if (grid[newRobotPos.Row, newRobotPos.Col] == '.' && 
            grid[newRobotPos.Row, newRobotPos.Col + 1] == '.')
        {
            grid[robotPosition.Row, robotPosition.Col] = '.';
            grid[robotPosition.Row, robotPosition.Col + 1] = '.';
            grid[newRobotPos.Row, newRobotPos.Col] = '@';
            grid[newRobotPos.Row, newRobotPos.Col + 1] = '.';
            robotPosition = newRobotPos;
            return true;
        }

        return false;
    }

    private List<(int Row, int Col)> GetBoxChain(char[,] grid, int row, int col, char direction)
    {
        var boxes = new List<(int Row, int Col)>();
        var current = (Row: row, Col: GetBoxStartColumn(grid, row, col));
        var (dRow, dCol) = GetDirectionOffset(direction);

        // Find all connected boxes in the chain
        while (IsValidPosition(grid, current.Row, current.Col + 1) &&
               grid[current.Row, current.Col] == '[' &&
               grid[current.Row, current.Col + 1] == ']')
        {
            boxes.Add(current);
            
            // Check next position
            current = (Row: current.Row + dRow, Col: current.Col + dCol);
            
            // Check if we've hit a wall or the edge
            if (!IsValidPosition(grid, current.Row, current.Col + 1) ||
                grid[current.Row, current.Col] == '#')
                break;
            
            // If we're not at a box, stop scanning
            if (grid[current.Row, current.Col] != '[')
                break;
        }

        return boxes;
    }

    private int GetBoxStartColumn(char[,] grid, int row, int col)
    {
        return grid[row, col] == ']' && col > 0 && grid[row, col - 1] == '[' 
            ? col - 1 
            : col;
    }

    private bool IsBoxPart(char[,] grid, int row, int col) =>
        grid[row, col] == '[' || grid[row, col] == ']';

    private bool IsValidPosition(char[,] grid, int row, int col) =>
        row >= 0 && row < grid.GetLength(0) && col >= 0 && col < grid.GetLength(1);
}