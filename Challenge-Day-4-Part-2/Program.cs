var challengeInputs = await File.ReadAllLinesAsync("input.txt");
var result = 0;
for (int yIndex = 0; yIndex < challengeInputs.Length; yIndex++)
{
    for (var xIndex = 0; xIndex < challengeInputs[yIndex].Length; xIndex++)
    {
        var wordBlock = GetWordBlock(challengeInputs, xIndex, yIndex);
        if(IsXMas(wordBlock))
        {
            result++;
        }
    }
}

Console.WriteLine(result);
bool IsMasWord(string x, string y, string z) => x == "M" && y == "A" && z == "S" || x == "S" && y == "A" && z == "M";
bool IsXMas(string[][] wordBlock)
{
    if (!IsMasWord(wordBlock[0][0], wordBlock[1][1], wordBlock[2][2]) || !IsMasWord(wordBlock[0][2], wordBlock[1][1], wordBlock[2][0]))
    {
        return false;
    }

    return true;
}

string[][] GetWordBlock(string[] inputs, int xIndex, int yIndex)
{
    var wordBlock = new string[3][];
    for (var i = 0; i < 3; i++)
    {
        wordBlock[i] = new string[3];
        for (var j = 0; j < 3; j++)
        {
            var x = xIndex - 1 + j;
            var y = yIndex - 1 + i;
            if (y < 0 || y >= inputs.Length || x < 0 || x >= inputs[y].Length)
            {
                wordBlock[i][j] = "";
            }
            else
            {
                wordBlock[i][j] = inputs[y][x].ToString();
            }
        }
    }

    return wordBlock;
}