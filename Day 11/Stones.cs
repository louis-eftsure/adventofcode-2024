namespace Day_11;

public class Stones
{
    private Dictionary<double, double> _stonesDict = new();

    public Stones(string input)
    {
        var stones = input.Split(' ').Select(double.Parse).ToList();
        foreach (var stone in stones)
        {
            _stonesDict.Add(stone, 1);
        }
    }
    
    public double Blink(int times)
    {
        for (var i = 0; i < times; i++)
        {
            foreach (var (stoneEngraving, n) in _stonesDict.ToList())
            {
                Remove(stoneEngraving, n);
                if (stoneEngraving == 0)
                {
                    Add(1d, n);
                }
                else if (stoneEngraving.ToString().Length % 2 == 0)
                {
                    var stoneStr = stoneEngraving.ToString();
                    var leftStone = int.Parse(stoneStr[..(stoneStr.Length / 2)]);
                    var rightStone = int.Parse(stoneStr[(stoneStr.Length / 2)..]);
                    Add(leftStone, n);
                    Add(rightStone, n);
                }
                else
                {
                    Add(stoneEngraving * 2024, n);
                }
            }
        }

        return _stonesDict.Values.Sum();
    }
    
    void Add(double key, double value)
    {
        _stonesDict.TryAdd(key, 0L);
        _stonesDict[key] += value;
    }
    
    void Remove(double key, double value) => _stonesDict[key] -= value;
}