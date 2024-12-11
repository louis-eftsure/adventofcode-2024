using System.IO;

namespace Day_11;

public class Stones
{
    private readonly string _filePath;

    public Stones(string input, string filePath)
    {
        _filePath = filePath;
        File.WriteAllText(_filePath, input);
    }

    public void Blink()
    {
        var input = File.ReadAllText(_filePath);
        var stones = input.Split(' ').Select(double.Parse).ToList();
        var newStones = new List<double>(stones.Count + stones.Count(x => x.ToString().Length % 2 == 0));
        
        foreach (var stoneEngraving in stones)
        {
            if (stoneEngraving == 0)
            {
                newStones.Add(1);
            }
            else if (stoneEngraving.ToString().Length % 2 == 0)
            {
                var stoneStr = stoneEngraving.ToString();
                var leftStone = int.Parse(stoneStr[..(stoneStr.Length / 2)]);
                var rightStone = int.Parse(stoneStr[(stoneStr.Length / 2)..]);
                newStones.Add(leftStone);
                newStones.Add(rightStone);
            }
            else
            {
                newStones.Add(stoneEngraving * 2024);
            }
        }

        File.WriteAllText(_filePath, string.Join(' ', newStones));
    }

    public void Blink(int times)
    {
        for (var i = 0; i < times; i++)
        {
            Blink();
        }
    }

    public int Count()
    {
        var input = File.ReadAllText(_filePath);
        return input.Split(' ').Length;
    }
}