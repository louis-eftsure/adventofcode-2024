
using System.Numerics;
using Day_16.AStar;

namespace Day_16;

public class Maze
{
    public char[,] Map { get; set; }
    public Maze(string input)
    {
        var inputStr = input.Split("\n").Select(x => x.ToCharArray()).ToArray();
        Map = new char[inputStr.Length, inputStr[0].Length];
        for (int y = 0; y < inputStr.Length; y++)
        {
            for (int x = 0; x < inputStr[y].Length; x++)
            {
                Map[y, x] = inputStr[y][x];
            }
        }
    }
    
    public Position FindStartingPoint()
    {
        for (int y = 0; y < Map.GetLength(0); y++)
        {
            for (int x = 0; x < Map.GetLength(1); x++)
            {
                if (Map[y, x] == 'S')
                {
                    return new Position(x, y, Direction.East);
                }
            }
        }

        return new Position(-1, -1, Direction.East);
    }
    
    public Position FindEndPoint()
    {
        for (int y = 0; y < Map.GetLength(0); y++)
        {
            for (int x = 0; x < Map.GetLength(1); x++)
            {
                if (Map[y, x] == 'E')
                {
                    return new Position(x, y, Direction.North);
                }
            }
        }

        return new Position(-1, -1, Direction.North);
    }
   

    public double FindMazePathScore()
    {
        var start = FindStartingPoint();
        var end = FindEndPoint();
        var pathFinder = new PathFinder(Map);
        var path = pathFinder.FindEqualCostPaths(start, end);
        VisualizePath(path.First().Path);
        return path.First().Cost;
    }
    
    public double FindOptimalSeats()
    {
        var start = FindStartingPoint();
        var end = FindEndPoint();
        var pathFinder = new PathFinder(Map);
        var paths = pathFinder.FindEqualCostPaths(start, end);
        var seats = CountUniqueSeatsOnPaths(paths);
        
        return seats;
    }

    private double CountUniqueSeatsOnPaths(List<Route> paths)
    {
        var seats = new HashSet<Vector2>();
        foreach (var path in paths)
        {
            VisualizePath(path.Path);
            foreach (var step in path.Path)
            {
                seats.Add(new Vector2(step.X, step.Y));
            }
        }

        return seats.Count;
    }

    private void VisualizePath(List<Position> path)
    {
        var pathMap = (char[,])Map.Clone();
        
        foreach (var step in path)
        {
            pathMap[step.Y, step.X] = GetDirectionChar(step.Facing);
        }
        
        for (var y = 0; y < pathMap.GetLength(0); y++)
        {
            for (var x = 0; x < pathMap.GetLength(1); x++)
            {
                Console.Write(pathMap[y, x]);
            }
            Console.WriteLine();
        }
    }

    private char GetDirectionChar(Direction direction)
    {
        return direction switch
        {
            Direction.North => '^',
            Direction.East => '>',
            Direction.South => 'v',
            Direction.West => '<',
            _ => ' '
        };
    }

    private int CountCorners(List<Vector2> path)
    {
        // Count all 90 degree turns made
        var corners = 0;
        for (int i = 1; i < path.Count - 1; i++)
        {
            var previous = path[i - 1];
            var current = path[i];
            var next = path[i + 1];
            if (previous.X != current.X && current.X != next.X || previous.Y != current.Y && current.Y != next.Y)
            {
                corners++;
            }
        }
        
        return corners;
    }
}