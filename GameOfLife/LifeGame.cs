namespace GameOfLife;

public class LifeGame : ILifeGame
{
    private readonly IGameboard _gameboard;
    private readonly IReadOnlyCollection<IRule> _rules;

    public LifeGame(IGameboard gameboard, IReadOnlyCollection<IRule> rules)
    {
        _gameboard = gameboard;
        _rules = rules;
    }

    public void Tick()
    {
        _gameboard.UpdateCells(x => Test(x));
    }

    private bool UpdateIsAlive(ICell cell) =>
        _rules.Select(x => x.Execute(cell))
            .Where(x => x is not null)
            .All(x => x == true);

    private static bool Test(ICell cell)
    {
        var liveNeighbors = cell.AliveNeighbors;
        if (cell.IsAlive && (liveNeighbors == 2 || liveNeighbors == 3))
        {
            return true;
        }
        else if (!cell.IsAlive && liveNeighbors == 3)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
