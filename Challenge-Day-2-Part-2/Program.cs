var challengeInput = await File.ReadAllLinesAsync("input.txt");

var reports =
    challengeInput.Select(inputLine => inputLine.Split(" ").Select(int.Parse).ToArray())
        .ToList(); // Create a list of arrays to store the reports

var safeReports = 0;

foreach (var report in reports)
{
    var isAscending = report[0] < report[^1];
    Console.Write($"Levels: {string.Join($", ", report)} , isAscending: {isAscending}" );
    var safe = false;
 
    var levels = report.ToList();

    if (IsReportSafe(levels))
    {
        safe = true;
    }
    else
    {
        var currentIndex = 0;

        while (currentIndex <= levels.Count)
        {
            levels = report.ToList();
            levels.RemoveAt(currentIndex);
            if (IsReportSafe(levels))
            {
                Console.Write(" - Extra life used on level " + report[currentIndex]);
                safe = true;
                break;
            }

            currentIndex++;
        }
    }

    if (safe)
    {
        Console.Write(" - Safe");
        safeReports++;
    }
    Console.Write("\n");
}

Console.WriteLine(safeReports);

bool IsReportSafe(List<int> report)
{
    var isAscending = report[0] < report[^1];
    var safe = true;

    for (var i = 1; i <= report.Count-1; i++)
    {
        var currentLevel = report[i];
        var previousLevel = report[i - 1];
        var difference = Math.Abs(currentLevel - previousLevel);
        if (isAscending && previousLevel > currentLevel || !isAscending && previousLevel < currentLevel || difference == 0 || difference > 3)
        {
            safe = false;
            break;
        }
    }
    
    return safe;
}