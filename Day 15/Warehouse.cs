using Day_15;
using Spectre.Console;

public class Warehouse
{
    private readonly char[,] grid;
    private (int Row, int Col) robotPosition;
    private readonly bool isWideMode;
    private int moveCount = 0;
    private int totalMoves = 0;
    private readonly IMovementStrategy moveStrategy;

    public Warehouse(string map, bool isWideMode = false)
    {
        this.isWideMode = isWideMode;
        var lines = map.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
        grid = new char[lines.Length, lines[0].Length];

        for (int row = 0; row < lines.Length; row++)
        {
            for (int col = 0; col < lines[0].Length; col++)
            {
                grid[row, col] = lines[row][col];
                if (grid[row, col] == '@')
                {
                    robotPosition = (row, col);
                }
            }
        }
        moveStrategy = isWideMode ? new WideMovementStrategy() : new NormalMovementStrategy();
    }

    public void ProcessMoves(string moves)
    {
        foreach (char move in moves.Where(c => "^v<>".Contains(c)))
        {
            moveStrategy.TryMove(grid, move, ref robotPosition);
        }
    }

    

    public async Task ProcessMovesWithVisualization(string moves)
    {
        var validMoves = moves.Where(c => "^v<>".Contains(c)).ToList();
        totalMoves = validMoves.Count;

        foreach (char move in validMoves)
        {
            moveCount++;
            moveStrategy.TryMove(grid, move, ref robotPosition);
            await DisplayCurrentState(move, moves);
            await Task.Delay(100);
        }
    }

    private async Task DisplayCurrentState(char direction, string moves)
    {
        AnsiConsole.Cursor.MoveUp(grid.GetLength(0) + 3);
        AnsiConsole.MarkupLine($"Move {moveCount}/{totalMoves} - Direction: {direction}");

        var table = new Table()
            .Border(TableBorder.None)
            .HideHeaders();

        for (int i = 0; i < grid.GetLength(1); i++)
            table.AddColumn(new TableColumn(string.Empty));

        for (int row = 0; row < grid.GetLength(0); row++)
        {
            var cells = new List<string>();
            for (int col = 0; col < grid.GetLength(1); col++)
            {
                string cell = grid[row, col] switch
                {
                    '#' => "[grey]#[/]",
                    '@' => "[red]@[/]",
                    '[' => "[green][[[/]",
                    ']' => "[green]]][/]",
                    'O' => "[green]O[/]",
                    '.' => "[blue].[/]",
                    _ => " "
                };
                cells.Add(cell);
            }

            table.AddRow(cells.ToArray());
        }
        
        // Adds the next series of moves to the table
        var nextMoves = moves.Skip(moveCount).Take(grid.GetLength(1)).ToList();
        table.AddRow(nextMoves.Select(m => m switch
        {
            '^' => "[bold]^[/]",
            'v' => "[bold]v[/]",
            '<' => "[bold]<[/]",
            '>' => "[bold]>[/]",
            _ => "?"
        }).ToArray());

        AnsiConsole.Write(table);
    }

    public int CalculateGPSSum()
    {
        int sum = 0;
        var processedPositions = new HashSet<(int row, int col)>();

        for (int row = 0; row < grid.GetLength(0); row++)
        {
            for (int col = 0; col < grid.GetLength(1); col++)
            {
                if (isWideMode)
                {
                    // Only process box if we haven't counted it yet
                    if ((grid[row, col] == '[' || grid[row, col] == ']') && 
                        !processedPositions.Contains((row, col)))
                    {
                        // Find both parts of the box
                        int leftCol = col;
                        int rightCol = col;

                        if (grid[row, col] == '[' && col + 1 < grid.GetLength(1))
                            rightCol = col + 1;
                        else if (grid[row, col] == ']' && col - 1 >= 0)
                            leftCol = col - 1;

                        // Mark both positions as processed
                        processedPositions.Add((row, leftCol));
                        processedPositions.Add((row, rightCol));

                        // Calculate GPS using leftmost column (since it's always the closest to left edge)
                        sum += (100 * row) + leftCol;
                    }
                }
                else if (grid[row, col] == 'O')
                {
                    sum += (100 * row) + col;
                }
            }
        }
        return sum;
    }
}