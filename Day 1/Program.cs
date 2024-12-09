var inputNumbers = await File.ReadAllLinesAsync("input.txt");
var leftNumbers = new List<int>();
var rightNumbers = new List<int>();

foreach (var inputNumber in inputNumbers)
{
    var lineValues = inputNumber.Split("   ");
    leftNumbers.Add(int.Parse(lineValues[0]));
    rightNumbers.Add(int.Parse(lineValues[1]));
}

leftNumbers.Sort();
rightNumbers.Sort();

var totalDistance = 0d;
for (int i = 0; i < inputNumbers.Length; i++)
{
    totalDistance += Math.Abs(leftNumbers[i] - rightNumbers[i]);
}

Console.WriteLine(totalDistance);


// Part 2
inputNumbers = await File.ReadAllLinesAsync("input.txt");
leftNumbers = new List<int>();
rightNumbers = new List<int>();

foreach (var inputNumber in inputNumbers)
{
    var lineValues = inputNumber.Split("   ");
    leftNumbers.Add(int.Parse(lineValues[0]));
    rightNumbers.Add(int.Parse(lineValues[1]));
}

leftNumbers.Sort();
rightNumbers.Sort();

totalDistance = 0d;
for (int i = 0; i < inputNumbers.Length; i++)
{
    var leftNumber = leftNumbers[i] ;
    var rightListCount = rightNumbers.Count(x => x == leftNumber);
    totalDistance += leftNumber * rightListCount;
}

Console.WriteLine(totalDistance);