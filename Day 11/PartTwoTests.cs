using Shouldly;

namespace Day_11;

public class PartTwoTests
{
   
    [Fact]
    public async Task PuzzleInput()
    {
        var input = await File.ReadAllTextAsync("Inputs/input.txt");
        var stones = new Stones(input);
        stones.Blink(75).ShouldBe(218811774248729d);
    }
}