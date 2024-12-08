using Day_6;
using Day_6.Tiles;

var inputStrings = await File.ReadAllLinesAsync("input.txt");
var lab = new Lab(inputStrings.Select(x => x.ToCharArray()).ToArray());
var guard = new Guard();
var guardRoute = guard.CalculateExitPath(lab.GetGuardPosition(), lab.Tiles!).Distinct();
var result = guardRoute.Count();
var partTwoResult = lab.GetPathLoopOptions(guard);
Console.WriteLine("Part 1: " + result);
Console.WriteLine("Part 2: " + partTwoResult);