var challengeInputs = await File.ReadAllLinesAsync("input.txt");
var result = 0;
for (int yIndex = 0; yIndex < challengeInputs.Length; yIndex++)
{
    for (var xIndex = 0; xIndex < challengeInputs[yIndex].Length; xIndex++)
    {
        if (challengeInputs[yIndex][xIndex] == 'X')
        {
            result += GetXmasOccurances(challengeInputs, xIndex, yIndex);
        }
    }
}

Console.WriteLine(result);
static int GetXmasOccurances(string[] input, int xIndex, int yIndex)
{
    var directions = new (int, int)[]
    {
        (0, 1),  // Right
        (0, -1), // Left
        (1, 0),  // Down
        (-1, 0), // Up
        (1, 1),  // Down-Right
        (1, -1), // Down-Left
        (-1, 1), // Up-Right
        (-1, -1) // Up-Left
    };

    int count = 0;
    foreach (var (dx, dy) in directions)
    {
        if (IsXmasWord(input, xIndex, yIndex, dx, dy))
        {
            count++;
        }
    }

    return count;
}

static bool IsXmasWord(string[] lines, int startX, int startY, int dx, int dy)
{
    string word = "";
    for (int i = 0; i < 4; i++)
    {
        int x = startX + i * dx;
        int y = startY + i * dy;
        if (y < 0 || y >= lines.Length || x < 0 || x >= lines[y].Length)
        {
            return false;
        }
        word += lines[y][x];
    }
    return word.Equals("xmas", StringComparison.OrdinalIgnoreCase);
}