using System.Text.RegularExpressions;

namespace Day_8;

public class AntennaMap
{
    private const string AntennaChannelRegex = "(\\d{0})\\w+";

    private AntennaMap(Coordinate size, List<Antenna> antennas)
    {
        Size = size;
        Antennas = antennas;
    }

    public Coordinate Size { get; }
    public List<Antenna> Antennas { get; }
    public List<AntiNode> AntiNodes { get; } = new();

    private bool IsInBounds(Coordinate coordinate)
    {
        return coordinate.X >= 0 && coordinate.X < Size.X && coordinate.Y >= 0 && coordinate.Y < Size.Y;
    }

    private void AddAntiNode(AntiNode antiNode)
    {
        if(!IsInBounds(antiNode.Position))
        {
            return;
        }
        
        if(AntiNodes.Find(x => Equals(x.Position, antiNode.Position)) != null)
        {
            return;
        }
        
        AntiNodes.Add(antiNode);
    }
    
    public void CalculateAntiNodes(bool printResults)
    {
        if(printResults)
        {
            Print();
        }
        
        foreach (var channel in Antennas.Select(x => x.Channel).Distinct())
        {
            CalculateChannelAntiNodes(channel, printResults);
        }
    }

    private void CalculateChannelAntiNodes(char channel, bool printResults)
    {
        var channelAntennas = Antennas
            .Where(x => x.Channel == channel)
            .Select(x => x.Position)
            .ToList();

        foreach (var antennaA in channelAntennas)
        {
            foreach (var antennaB in channelAntennas)
            {
                if (Equals(antennaA, antennaB))
                {
                    continue;
                }
                var distance = CalculateDistance(antennaA, antennaB);
                var direction = GetDirection(antennaA, antennaB);
                var antiNode = new AntiNode(antennaB + distance * direction, channel);
                AddAntiNode(antiNode);
                
                if (!printResults) continue;
                Console.Clear();
                Print();
            }
        }

    }
    
    private static Coordinate CalculateDistance(Coordinate a, Coordinate b)
    {
        return new Coordinate(Math.Abs(a.X - b.X), Math.Abs(a.Y - b.Y));
    }
    
    private static Coordinate GetDirection(Coordinate a, Coordinate b)
    {
        return new Coordinate(Math.Sign(b.X - a.X), Math.Sign(b.Y - a.Y));
    }

    public static AntennaMap Parse(string inputString)
    {
        var lines = inputString.Split("\r\n");
        var size = new Coordinate(lines[0].Length, lines.Length);
        var antennas = new List<Antenna>();
        var antennaChannelRegex = new Regex(AntennaChannelRegex);

        for (var y = 0; y < size.Y; y++)
        {
            for (var x = 0; x < size.X; x++)
            {
                var channel = antennaChannelRegex.Match(lines[y][x].ToString()).Value;
                if (channel != "")
                {
                    antennas.Add(new Antenna(new Coordinate(x, y), channel[0]));
                }
            }
        }

        return new AntennaMap(size, antennas);
    }
    
    public void Print()
    {
        for (var y = 0; y < Size.Y; y++)
        {
            for (var x = 0; x < Size.X; x++)
            {
                var antenna = Antennas.Find(a => Equals(a.Position, new Coordinate(x, y)));
                var antiNode = AntiNodes.Find(a => Equals(a.Position, new Coordinate(x, y)));
                if (antiNode != null)
                {
                    // Set console color to red
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(antiNode.Channel);
                    Console.ResetColor();
                }
                else if (antenna != null)
                {
                    // Set console color to green
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(antenna.Channel);
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(".");
                }
            }
            Console.WriteLine();
        }
    }

    public void CalculateResonantHarmonicsAntiNodes(bool printResults)
    {
        if (printResults)
        {
            Print();
        }
        
        foreach (var channel in Antennas.Select(x => x.Channel).Distinct())
        {
            CalculateChannelResonantHarmonicsAntiNodes(channel, printResults);
        }
    }
    
    private void CalculateChannelResonantHarmonicsAntiNodes(char channel, bool printResults)
    {
        var channelAntennas = Antennas
            .Where(x => x.Channel == channel)
            .Select(x => x.Position)
            .ToList();

        foreach (var antennaA in channelAntennas)
        {
            foreach (var antennaB in channelAntennas)
            {
                if (Equals(antennaA, antennaB))
                {
                    continue;
                }
                var distance = CalculateDistance(antennaA, antennaB);
                var direction = GetDirection(antennaA, antennaB);
                var antiNodePos = antennaA + distance * direction;
                while(IsInBounds(antiNodePos))
                {
                    var antiNode = new AntiNode(antiNodePos, channel);
                    AddAntiNode(antiNode);
                    antiNodePos += distance * direction;
                }
                
                if (!printResults) continue;
                Console.Clear();
                Print();
            }
        }

        if (printResults)
        {
            Console.WriteLine();
        }

    }
}