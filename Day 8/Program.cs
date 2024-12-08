using Day_8;

var inputFileName = "input.txt";
var partOne = new PartOne();
var partOneResult = await partOne.GetResult(false, inputFileName);

var partTwo = new PartTwo();
var partTwoResult = await partTwo.GetResult(false, inputFileName);

Console.WriteLine("Part 1: " + partOneResult);
Console.WriteLine("Part 2: " + partTwoResult);