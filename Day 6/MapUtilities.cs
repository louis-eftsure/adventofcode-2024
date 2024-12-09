using Day_6.Tiles;

namespace Day_6;

public static class MapUtilities
{
    public static void VisualiseRoute(List<Move> route, ITile[,] labLayout)
    {
        Console.Clear();
        Console.WriteLine();

        for (var y = 0; y < labLayout.GetLength(0); y++)
        {
            for (var x = 0; x < labLayout.GetLength(1); x++)
            {
                var tile = labLayout[y, x];
                if(tile is Guard)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write('^');
                    Console.ResetColor();
                    continue;
                }
                
                var matchingMoves = route.Count(routeMove => routeMove.X == x && routeMove.Y == y);
                if (matchingMoves > 0)
                {
                    //set blue

                    if (matchingMoves == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    else if(matchingMoves == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if(matchingMoves == 3)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else if(matchingMoves == 4)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                    }
                    
                    Console.Write('X');
                    Console.ResetColor();
                }
                else
                {
                    if (labLayout[y, x] is LoopObstacle)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write('O');
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(labLayout[y, x] switch
                        {
                            Obstacle => '#',
                            Guard => '^',
                            _ => '.'
                        });
                    }
                }
            }
            Console.WriteLine();
        }
    }
}