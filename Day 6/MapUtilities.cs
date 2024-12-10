using Common;
using Day_6.Tiles;

namespace Day_6;

public static class MapUtilities
{
    public static void  VisualiseRoute(List<Move> route, ITile[,] labLayout)
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
                
                var matchingMoves = route.Where(routeMove => routeMove.X == x && routeMove.Y == y).ToList();
                var matchingMovesCount = matchingMoves.Count;
                if (matchingMovesCount > 0)
                {
                    //set blue

                    if (matchingMovesCount == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    else if(matchingMovesCount == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if(matchingMovesCount == 3)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else if(matchingMovesCount == 4)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                    }

                    var directionsOnTile = matchingMoves.GroupBy(x => x.Direction).ToList();
                    switch (directionsOnTile.Count)
                    {
                        case 1 when directionsOnTile[0].Key == Directions.North || directionsOnTile[0].Key == Directions.South:
                            Console.Write('|');
                            break;
                        case 1 when directionsOnTile[0].Key == Directions.West || directionsOnTile[0].Key == Directions.East:
                            Console.Write('-');
                            break;
                        case >1:
                            Console.Write('+');
                            break;
                    }
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