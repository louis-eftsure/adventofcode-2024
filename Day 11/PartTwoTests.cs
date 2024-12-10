using Shouldly;

namespace Day_11;

public class PartTwoTests
{
    [Fact]
    public async Task ExampleInput()
    {
        var input = await File.ReadAllLinesAsync("Inputs/example-input.txt");
        var result = input.Length;
        
        result.ShouldBe(0);
    }
    
    [Fact]
    public async Task PuzzleInput()
    {
        var input = await File.ReadAllLinesAsync("Inputs/input.txt");
        var result = input.Length;
        
        result.ShouldBe(0);
    }
}