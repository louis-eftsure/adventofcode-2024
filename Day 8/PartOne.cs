namespace Day_8;

public class PartOne
{
    public async Task<double> GetResult(bool printResults, string inputFileName)
    {
        var inputString = await File.ReadAllTextAsync(inputFileName);
        var antennaMap = AntennaMap.Parse(inputString);
        
        antennaMap.CalculateAntiNodes(printResults);
        
        return antennaMap.AntiNodes.Count;
    }
}