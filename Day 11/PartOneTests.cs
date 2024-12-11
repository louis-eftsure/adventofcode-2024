using Shouldly;

namespace Day_11;

public class PartOneTests
{
    [Fact]
    public async Task ExampleInput()
    {
        var input = await File.ReadAllTextAsync("Inputs/example-input.txt");
        var stones = new Stones(input, "Outputs/example-output.txt");
        stones.Blink(25);
        stones.Count().ShouldBe(55312);
    }
    
    [Fact]
    public async Task PuzzleInput()
    {
        var input = await File.ReadAllTextAsync("Inputs/input.txt");
        var stones = new Stones(input, "Outputs/puzzle-output.txt");
        stones.Blink(25);
        stones.Count().ShouldBe(183248);
    }
}