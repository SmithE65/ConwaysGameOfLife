namespace GameOfLife;

public interface ICell
{
    int AliveNeighbors { get; }
    bool IsAlive { get; }
}
