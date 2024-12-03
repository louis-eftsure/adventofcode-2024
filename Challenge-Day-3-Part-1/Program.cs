// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;

var challengeInputString = await File.ReadAllTextAsync("input.txt");

var totalSum = 0d;
var regex = new Regex(@"mul\(\d+,\d+\)");
var matches = regex.Matches(challengeInputString);

foreach (Match match in matches)
{
    totalSum += int.Parse(match.Value.Split(",")[0].Replace("mul(", "")) *
                int.Parse(match.Value.Split(",")[1].Replace(")", ""));
}

Console.WriteLine(totalSum);