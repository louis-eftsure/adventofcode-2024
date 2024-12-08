using Day_6.Tiles;

namespace Day_6;

public static class MapUtilities
{
    public static void VisualiseRoute(List<Coordinate> route, ITile[,] labLayout)
    {
        Console.WriteLine();

        for (var y = 0; y < labLayout.GetLength(0); y++)
        {
            for (var x = 0; x < labLayout.GetLength(1); x++)
            {
                if (route.Find(coord => coord.X == x && coord.Y == y) != null)
                {
                    //set blue
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write('X');
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(labLayout[y, x] switch
                    {
                        Obstacle => '#',
                        Guard => '^',
                        LoopObstacle => 'O',
                        _ => '.'
                    });
                }
            }
            Console.WriteLine();
        }
    }
}