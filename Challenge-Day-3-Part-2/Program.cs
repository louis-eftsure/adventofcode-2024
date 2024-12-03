// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;

var challengeInputString = await File.ReadAllTextAsync("input.txt");
var validOperations = challengeInputString;
while (validOperations.Contains("don't()"))
{
    var dontIndex = validOperations.IndexOf("don't()", StringComparison.Ordinal);
    var nextDoIndex = validOperations.IndexOf("do()", dontIndex, StringComparison.Ordinal);
    validOperations = validOperations.Remove(dontIndex, nextDoIndex - dontIndex + 4);
}

var totalSum = 0d;
var regex = new Regex(@"mul\(\d+,\d+\)");
var matches = regex.Matches(validOperations);

foreach (Match match in matches)
{
    totalSum += int.Parse(match.Value.Split(",")[0].Replace("mul(", "")) *
                int.Parse(match.Value.Split(",")[1].Replace(")", ""));
}

Console.WriteLine(totalSum);