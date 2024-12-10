using Shouldly;
using Xunit;

namespace Day_6;

public class PartTwoTests
{
    [Fact]
    public async Task ExampleInput()
    {
        var inputStrings = await File.ReadAllLinesAsync("example.txt");
        var lab = new Lab(inputStrings.Select(x => x.ToCharArray()).ToArray());
        var guard = new Guard();
        var partTwoResult = lab.GetPathLoopOptions(guard).Count;
        
        partTwoResult.ShouldBe(6);
    }
    
    [Fact]
    public async Task PuzzleInput()
    {
        var inputStrings = await File.ReadAllLinesAsync("input.txt");
        var lab = new Lab(inputStrings.Select(x => x.ToCharArray()).ToArray());
        var guard = new Guard();
        var partTwoResult = lab.GetPathLoopOptions(guard).Count;
        
        partTwoResult.ShouldBe(1663);
    }
}