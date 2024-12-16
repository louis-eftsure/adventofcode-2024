namespace Day_16.AStar;

public class PathFinder
{
    private readonly char[,] grid;
    private readonly int rows;
    private readonly int cols;
    private const int MOVEMENT_COST = 1;
    private const int ROTATION_COST = 1000;
    private bool enableDebug = false;

    public PathFinder(char[,] grid, bool debug = false)
    {
        this.grid = grid;
        rows = grid.GetLength(0);
        cols = grid.GetLength(1);
        enableDebug = debug;
    }

    public static char[,] ParseGrid(string gridStr)
    {
        var lines = gridStr.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        var grid = new char[lines.Length, lines[0].Length];
        
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                grid[i, j] = lines[i][j];
            }
        }
        
        return grid;
    }

    public (Position start, Position end) FindStartAndEnd()
    {
        Position start = null;
        Position end = null;
        
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (grid[i, j] == 'S')
                    start = new Position(i, j, Direction.East); // Default facing direction
                else if (grid[i, j] == 'E')
                    end = new Position(i, j, Direction.East); // Default facing direction
            }
        }
        
        return (start, end);
    }

    private void DebugLog(string message)
    {
        if (enableDebug)
            Console.WriteLine($"DEBUG: {message}");
    }

    public List<Route> FindEqualCostPaths(Position start, Position target)
    {
        var openSet = new PriorityQueue<Node, int>();
        var processedStates = new Dictionary<string, Node>();
        var startNode = new Node(start);
        startNode.G = 0;
        startNode.H = CalculateHeuristic(start, target);
        openSet.Enqueue(startNode, startNode.F);
        
        var endNodes = new List<Node>();
        int? bestCost = null;

        while (openSet.Count > 0)
        {
            var currentNode = openSet.Dequeue();
            var stateKey = GetStateKey(currentNode.Position);
            
            if (currentNode.Position.X == target.X && currentNode.Position.Y == target.Y)
            {
                DebugLog($"Found path to target with cost {currentNode.G}");
                if (bestCost == null || currentNode.G == bestCost)
                {
                    bestCost = currentNode.G;
                    endNodes.Add(currentNode);
                    DebugLog($"Added to endNodes. Total end nodes: {endNodes.Count}");
                }
                else if (currentNode.G > bestCost)
                {
                    DebugLog($"Found worse path with cost {currentNode.G}, stopping search");
                    break;
                }
                continue;
            }

            foreach (var neighbor in GetNeighbors(currentNode, target))
            {
                var tentativeG = currentNode.G + CalculateMovementCost(currentNode.Position, neighbor.Position);
                var neighborKey = GetStateKey(neighbor.Position);

                if (processedStates.TryGetValue(neighborKey, out var existingNode))
                {
                    if (tentativeG < existingNode.G)
                    {
                        DebugLog($"Found better path to {neighborKey} with cost {tentativeG}");
                        existingNode.EqualCostParents.Clear();
                        existingNode.Parent = currentNode;
                        existingNode.G = tentativeG;
                        openSet.Enqueue(existingNode, existingNode.F);
                    }
                    else if (tentativeG == existingNode.G)
                    {
                        DebugLog($"Found equal cost path to {neighborKey} with cost {tentativeG}");
                        existingNode.EqualCostParents.Add(currentNode);
                    }
                }
                else
                {
                    neighbor.G = tentativeG;
                    neighbor.H = CalculateHeuristic(neighbor.Position, target);
                    neighbor.Parent = currentNode;
                    processedStates[neighborKey] = neighbor;
                    openSet.Enqueue(neighbor, neighbor.F);
                    DebugLog($"Added new node at {neighborKey} with cost {tentativeG}");
                }
            }
        }

        var routes = ReconstructPaths(endNodes);
        DebugLog($"Final number of routes: {routes.Count}");
        return routes;
    }

    private string GetStateKey(Position pos)
    {
        return $"{pos.X},{pos.Y},{pos.Facing}";
    }

    private List<Route> ReconstructPaths(List<Node> endNodes)
    {
        var routes = new List<Route>();
        var processedPaths = new HashSet<string>();

        foreach (var endNode in endNodes)
        {
            var paths = GenerateAllPaths(endNode);
            
            foreach (var path in paths)
            {
                var pathKey = string.Join(";", path.Select(p => $"{p.X},{p.Y},{p.Facing}"));
                if (!processedPaths.Contains(pathKey))
                {
                    var cost = CalculatePathCost(path);
                    routes.Add(new Route(path, cost));
                    processedPaths.Add(pathKey);
                }
            }
        }

        return routes;
    }

    private List<List<Position>> GenerateAllPaths(Node endNode)
    {
        var paths = new List<List<Position>>();
        GeneratePathsRecursive(endNode, new List<Position> { endNode.Position }, paths);
        return paths;
    }

    private void GeneratePathsRecursive(Node node, List<Position> currentPath, List<List<Position>> allPaths)
    {
        if (node.Parent == null)
        {
            allPaths.Add(new List<Position>(currentPath));
            return;
        }

        // Add main parent path
        currentPath.Insert(0, node.Parent.Position);
        GeneratePathsRecursive(node.Parent, currentPath, allPaths);
        currentPath.RemoveAt(0);

        // Add equal cost parent paths
        foreach (var equalCostParent in node.EqualCostParents)
        {
            currentPath.Insert(0, equalCostParent.Position);
            GeneratePathsRecursive(equalCostParent, currentPath, allPaths);
            currentPath.RemoveAt(0);
        }
    }

    private int CalculatePathCost(List<Position> path)
    {
        int totalCost = 0;
        for (int i = 1; i < path.Count; i++)
        {
            totalCost += CalculateMovementCost(path[i - 1], path[i]);
        }
        return totalCost;
    }

    private List<Node> GetNeighbors(Node currentNode, Position target)
    {
        var neighbors = new List<Node>();

        // Add possible rotations
        foreach (Direction direction in Enum.GetValues(typeof(Direction)))
        {
            if (direction != currentNode.Position.Facing)
            {
                neighbors.Add(new Node(new Position(
                    currentNode.Position.X,
                    currentNode.Position.Y,
                    direction
                )));
            }
        }

        // Add possible movements in current facing direction
        var (dx, dy) = GetDirectionOffset(currentNode.Position.Facing);
        var newX = currentNode.Position.X + dx;
        var newY = currentNode.Position.Y + dy;

        if (IsValidPosition(newX, newY) && grid[newX, newY] != '#')
        {
            neighbors.Add(new Node(new Position(
                newX,
                newY,
                currentNode.Position.Facing
            )));
        }

        return neighbors;
    }

    private (int dx, int dy) GetDirectionOffset(Direction direction)
    {
        return direction switch
        {
            Direction.North => (-1, 0),
            Direction.South => (1, 0),
            Direction.West => (0, -1),
            Direction.East => (0, 1),
            _ => throw new ArgumentException("Invalid direction")
        };
    }

    private bool IsValidPosition(int x, int y)
    {
        return x >= 0 && x < rows && y >= 0 && y < cols;
    }

    private int CalculateHeuristic(Position current, Position target)
    {
        return Math.Abs(current.X - target.X) + Math.Abs(current.Y - target.Y);
    }

    private int CalculateMovementCost(Position from, Position to)
    {
        if (from.X == to.X && from.Y == to.Y)
        {
            // This is a rotation
            return ROTATION_COST;
        }
        return MOVEMENT_COST;
    }
}