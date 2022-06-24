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

    public static bool ConwayRules(ICell cell)
    {
        var liveNeighbors = cell.AliveNeighbors;
        if (cell.IsAlive && liveNeighbors == 2)
        {
            return true;
        }
        else if (liveNeighbors == 3)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
