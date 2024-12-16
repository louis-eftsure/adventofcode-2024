namespace Day_15;

public interface IMovementStrategy
{
    bool TryMove(char[,] grid, char direction, ref (int Row, int Col) robotPosition);
}