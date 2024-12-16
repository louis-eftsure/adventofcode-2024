using Shouldly;

namespace Day_12;

public class UnitTest1
{
    [Fact]
    public async Task Test1()
    {
        var input = await File.ReadAllLinesAsync("Inputs/input.txt");
        var result = 0d;
        var regionMap = new RegionMap(input);
        // var regions = regionMap.GetRegions();
        // result = regions.Sum(x => x.GetFenceCost());
        regionMap.Print();
        result.ShouldBe(1930);
    }
}