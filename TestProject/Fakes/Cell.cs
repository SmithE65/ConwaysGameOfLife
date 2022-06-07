using GameOfLife;

namespace TestProject.Fakes;

public class Cell : ICell
{
    private readonly bool _isAlive;

    public Cell(bool alive, int liveNeighbors)
    {
        _isAlive = alive;
        AliveNeighbors = liveNeighbors;
    }

    public int AliveNeighbors { get; init; }
    public bool IsAlive => _isAlive;
}
