using Shouldly;
using Xunit;

namespace Day_10;

public class PartTwoTests
{
    [Fact]
    public async Task CalculateTrailPaths_When_ExampleInput()
    {
        var input = await File.ReadAllLinesAsync("example-input.txt");
        var result = 0;
        var trailMap = new TrailMap(input);
        result = trailMap.CountTrailHeads();
        
        result.ShouldBe(81);
    }
    
    [Fact]
    public async Task CalculateTrailPaths_When_PuzzleInput()
    {
        var input = await File.ReadAllLinesAsync("input.txt");
        var result = 0;
        var trailMap = new TrailMap(input);
        result = trailMap.CountTrailHeads();
        
        result.ShouldBe(1794);
    }
}