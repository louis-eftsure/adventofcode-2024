namespace Day_9;

public class PartTwo
{
    public async Task<double> GetResult(string inputFileName)
    {
        var inputString = await File.ReadAllTextAsync(inputFileName);


        return inputString.Length;
    }
}