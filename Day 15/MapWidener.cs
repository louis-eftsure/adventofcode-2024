using System.Text;

namespace Day_15;

public static class MapWidener
{
    public static string WidenMap(string originalMap)
    {
        var lines = originalMap.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
        var widenedLines = new List<string>();

        foreach (var line in lines)
        {
            var widenedLine = new StringBuilder();
            foreach (char c in line)
            {
                switch (c)
                {
                    case '#': widenedLine.Append("##"); break;
                    case 'O': widenedLine.Append("[]"); break;
                    case '.': widenedLine.Append(".."); break;
                    case '@': widenedLine.Append("@."); break;
                    default: widenedLine.Append(c).Append(c); break;
                }
            }
            widenedLines.Add(widenedLine.ToString());
        }

        return string.Join("\r\n", widenedLines);
    }
}