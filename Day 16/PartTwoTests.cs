
using Shouldly;

namespace Day_16;

public class PartTwoTests
{
    [Fact]
    public async Task Example_Test()
    {
        var input = await File.ReadAllTextAsync("Inputs/example.txt");
        var maze = new Maze(input);
        var result = maze.FindOptimalSeats();
        result.ShouldBe(45);
    }
    
    [Fact]
    public async Task Test()
    {
        var input = await File.ReadAllTextAsync("Inputs/input.txt");
        var maze = new Maze(input);
        var result = maze.FindOptimalSeats();
        result.ShouldBe(511);
    }
}