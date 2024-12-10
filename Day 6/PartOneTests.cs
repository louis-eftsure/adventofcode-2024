using Shouldly;
using Xunit;

namespace Day_6;

public class PartOneTests
{
    [Fact]
    public async Task ExampleInput()
    {
        var inputStrings = await File.ReadAllLinesAsync("example.txt");
        var lab = new Lab(inputStrings.Select(x => x.ToCharArray()).ToArray());
        var guard = new Guard();
        var guardRoute = guard.CalculateExitPath(lab.GetGuardPosition(), lab.Tiles!, out _);
        var result = guardRoute.Select(Move.ToVector2).Distinct().Count();
        result.ShouldBe(41);
    }
    
    [Fact]
    public async Task PuzzleInput()
    {
        var inputStrings = await File.ReadAllLinesAsync("input.txt");
        var lab = new Lab(inputStrings.Select(x => x.ToCharArray()).ToArray());
        var guard = new Guard();
        var guardRoute = guard.CalculateExitPath(lab.GetGuardPosition(), lab.Tiles!, out _);
        var result = guardRoute.Select(Move.ToVector2).Distinct().Count();
        result.ShouldBe(4982);
    }
}