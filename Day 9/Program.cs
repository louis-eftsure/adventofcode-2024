using Day_9;

var inputFileName = "input.txt";
var partOne = new PartOne();
var partOneResult = await partOne.GetResult(inputFileName);

var partTwo = new PartTwo();
var partTwoResult = await partTwo.GetResult(inputFileName);

Console.WriteLine("Part 1: " + partOneResult);
Console.WriteLine("Part 2: " + partTwoResult);