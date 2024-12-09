namespace Day_9;

public class PartOne
{
    public async Task<double> GetResult(string inputFileName)
    {
        var inputString = await File.ReadAllTextAsync(inputFileName);


        return inputString.Length;
    }
}