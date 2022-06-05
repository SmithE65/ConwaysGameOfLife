namespace GameOfLife;

public interface IGameboard
{
    void UpdateCells(Func<ICell, bool> func);
}
