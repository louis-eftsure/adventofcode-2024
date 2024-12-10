using Shouldly;
using Xunit;

namespace Day_10;

public class PartOneTests
{
    [Fact]
    public async Task ExampleInput()
    {
        var input = await File.ReadAllLinesAsync("example-input.txt");
        var result = 0;
        var trailMap = new TrailMap(input);
        result = trailMap.CountUniqueTrailHeads();
        
        result.ShouldBe(36);
    }
    
    [Fact]
    public async Task PuzzleInput()
    {
        var input = await File.ReadAllLinesAsync("input.txt");
        var result = 0;
        var trailMap = new TrailMap(input);
        result = trailMap.CountUniqueTrailHeads();
        
        result.ShouldBe(811);
    }
    
}