namespace Day_8;

public class PartTwo
{
    public async Task<double> GetResult(bool printResults, string inputFileName)
    {
        var inputString = await File.ReadAllTextAsync(inputFileName);
        var antennaMap = AntennaMap.Parse(inputString);
        antennaMap.Print();
        antennaMap.CalculateResonantHarmonicsAntiNodes(printResults);
        
        return antennaMap.AntiNodes.Count;
    }
}