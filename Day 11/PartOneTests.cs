using Shouldly;

namespace Day_11;

public class PartOneTests
{
    [Fact]
    public async Task ExampleInput()
    {
        var input = await File.ReadAllTextAsync("Inputs/example-input.txt");
        var stones = new Stones(input);
        stones.Blink(25).ShouldBe(55312);
    }
    
    [Fact]
    public async Task PuzzleInput()
    {
        var input = await File.ReadAllTextAsync("Inputs/input.txt");
        var stones = new Stones(input);
        stones.Blink(25).ShouldBe(183248);
    }
}