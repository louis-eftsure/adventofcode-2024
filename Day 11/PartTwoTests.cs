using Shouldly;

namespace Day_11;

public class PartTwoTests
{
   
    [Fact]
    public async Task PuzzleInput()
    {
        var input = await File.ReadAllTextAsync("Inputs/input.txt");
        var stones = new Stones(input, "Outputs/puzzle-output.txt");
        stones.Blink(75);
        stones.Count().ShouldBe(55312);
    }
}