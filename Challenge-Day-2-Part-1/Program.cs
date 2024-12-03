var challengeInput = await File.ReadAllLinesAsync("input.txt");

var reports =
    challengeInput.Select(inputLine => inputLine.Split(" ").Select(int.Parse).ToArray())
        .ToList(); // Create a list of arrays to store the reports

var safeReports = 0;

foreach (var report in reports)
{
    var isAscending = report[0] < report[^1];
    var safe = true;

    for (var i = 1; i < report.Length; i++)
    {
        var currentLevel = report[i];
        var previousLevel = report[i - 1];
        var difference = Math.Abs(currentLevel - previousLevel);
        if (isAscending && previousLevel > currentLevel || !isAscending && previousLevel < currentLevel ||
            difference == 0 || difference > 3)
        {
            safe = false;
            break;
        }
    }

    if (safe)
    {
        safeReports++;
    }
}

Console.WriteLine(safeReports);