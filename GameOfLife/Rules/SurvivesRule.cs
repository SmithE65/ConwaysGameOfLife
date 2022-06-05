namespace GameOfLife.Rules;

public class SurvivesRule : IRule
{
    public bool? Execute(ICell cell) => cell.IsAlive ?
        (cell.AliveNeighbors == 2 || cell.AliveNeighbors == 3)
        : null;
}

public class RevivesRule : IRule
{
    public bool? Execute(ICell cell) =>cell.IsAlive && cell.AliveNeighbors == 3 ? true : null;
}

public class OverPopulationRule : IRule
{
    public bool? Execute(ICell cell) => cell.AliveNeighbors > 3 ? false : null;
}

public class UnderPopulationRule : IRule
{
    public bool? Execute(ICell cell) => cell.AliveNeighbors < 2 ? false : null;
}
