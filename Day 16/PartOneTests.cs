using Shouldly;

namespace Day_16;

public class PartOneTests
{
    [Fact]
    public async Task Example_Test()
    {
        var input = await File.ReadAllTextAsync("Inputs/example.txt");
        var maze = new Maze(input);
        var result = maze.FindMazePathScore();
        result.ShouldBe(7036);
    }
    
    [Fact]
    public async Task Test()
    {
        var input = await File.ReadAllTextAsync("Inputs/input.txt");
        var maze = new Maze(input);
        var result = maze.FindMazePathScore();
        result.ShouldBe(95476);
    }
}