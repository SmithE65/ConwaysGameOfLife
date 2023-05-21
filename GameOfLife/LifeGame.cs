namespace GameOfLife;

public class LifeGame : ILifeGame
{
    private readonly IGameboard _gameboard;
    private readonly Func<ICell, bool> _rules;

    public LifeGame(IGameboard gameboard, Func<ICell, bool> rules)
    {
        _gameboard = gameboard;
        _rules = rules;
    }

    public void Tick()
    {
        _gameboard.UpdateCells(x => _rules(x));
    }

    public static bool IsAlive(ICell cell)
    {
        var liveNeighbors = cell.AliveNeighbors;

        return cell.IsAlive && liveNeighbors == 2 || liveNeighbors == 3;
    }
}
