using System.Numerics;

namespace Day_12;

public class RegionMap
{
    private char[,] _map;
    private HashSet<(char plantType, int x, int y)> _plants = new();
    public RegionMap(string[] input)
    {
        _map = new char[input[0].Length, input.Length];
        for(var y = 0; y < input.Length; y++)
        {
            for(var x = 0; x < input[y].Length; x++)
            {
               _map[x, y] = input[y][x];
               _plants.Add((input[y][x], x, y));
            }
        }
    }
    
    public List<Region> GetRegions()
    {
        var regions = new List<Region>();
        foreach (var plant in _plants)
        {
            var region = CalculateRegion(plant.plantType, plant.x, plant.y);
            if(!regions.Any(r => r.Plants.Intersect(region.Plants).Any()))
                regions.Add(region);
        }
        return regions;
    }

    private Region CalculateRegion(char plantType, int x, int y)
    {
        var region = new Region(plantType, new Vector2(x, y));
        var visited = new HashSet<Vector2>();
        ExploreRegion(plantType, x, y, region, visited);
        return region;
    }

    private void ExploreRegion(char plantType, int x, int y, Region region, HashSet<Vector2> visited)
    {
        if (visited.Contains(new Vector2(x, y)))
            return;

        visited.Add(new Vector2(x, y));
        var neighbours = GetNeighbours(x, y);

        foreach (var neighbour in neighbours)
        {
            if (neighbour.plantType == plantType)
            {
                region.Plants.Add(neighbour.pos);
                ExploreRegion(plantType, (int)neighbour.pos.X, (int)neighbour.pos.Y, region, visited);
            }
            else
            {
                region.Parameters.Add(neighbour.pos);
            }
        }
    }

    private List<(char plantType, Vector2 pos)> GetNeighbours(int x, int y)
    {
        var neighbours = new List<(char, Vector2)>();
        for(var i = -1; i <= 1; i++)
        {
            for(var j = -1; j <= 1; j++)
            {
                if(i == 0 && j == 0)
                    continue;
                if(x + i >= 0 && x + i < _map.GetLength(0) && y + j >= 0 && y + j < _map.GetLength(1))
                {
                    neighbours.Add((_map[x + i, y + j], new Vector2(x + i, y + j)));
                }
            }
        }
        return neighbours;
    }
    
    public void Print()
    {
        // Print by region and highlight with color
        var regions = GetRegions();
        for(var y = 0; y < _map.GetLength(1); y++)
        {
            for(var x = 0; x < _map.GetLength(0); x++)
            {
                var plant = _plants.FirstOrDefault(p => p.x == x && p.y == y);
                if(plant != default)
                {
                    var region = regions.FirstOrDefault(r => r.Plants.Contains(new Vector2(x, y)));
                    if(region != default)
                    {
                        Console.ForegroundColor = (ConsoleColor)new Random().Next(1, 15);
                        Console.Write(plant.plantType);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(plant.plantType);
                    }
                }
                else
                {
                    Console.Write(_map[x, y]);
                }
            }
            Console.WriteLine();
        }
    }
}