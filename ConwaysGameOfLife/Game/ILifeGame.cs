namespace ConwaysGameOfLife.Game;

public interface ILifeGame
{
    void Tick();
}

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
        _gameboard.UpdateCells(x => UpdateIsAlive(x));
    }

    private bool UpdateIsAlive(ICell cell) => _rules.All(r => r.Execute(cell));
}

public interface IGameboard
{
    void UpdateCells(Func<ICell, bool> func);
}

internal class Gameboard : IGameboard
{
    private readonly int _height;
    private readonly int _width;
    private bool[] _grid;

    public Gameboard(int height, int width)
    {
        _height = height;
        _width = width;
        _grid = CreateBoard();
    }

    public void SetAlive(int x, int y, bool alive = true) => _grid[y * _width + x] = alive;

    public bool[] Snapshot() => _grid;

    public void UpdateCells(Func<ICell, bool> func)
    {
        _grid = CreateBoard();
        var gv = new GridView(_grid, _width, _height);
        for (int i = 0; i < _grid.Length; i++)
        {
            var cell = new CellView(i % _width, i / _width, gv);
            _grid[i] = func(cell);
        }
    }

    private bool[] CreateBoard(bool[]? current = null)
    {
        if (current is null)
        {
            return new bool[_width * _height];
        }

        return _grid.Clone() as bool[] ?? throw new Exception("Failed cloning board state.");
    }

    private class GridView
    {
        private readonly bool[] _grid;
        private readonly int _width;
        private readonly int _height;

        public GridView(bool[] grid, int width, int height)
        {
            _grid = grid;
            _width = width;
            _height = height;
        }

        public bool IsInBounds(int x, int y) => x >= 0 && x < _width && y >= 0 && y < _height;

        public bool ValueAt(int x, int y) => IsInBounds(x, y) ? _grid[y * _width + x] : throw new Exception();
    }

    private class CellView : ICell
    {
        private readonly bool _isAlive;
        private bool _nextAlive;

        public CellView(int x, int y, GridView gv)
        {
            Neighbors = new List<bool?>
            {
                GetCell(x-1,y),
                GetCell(x+1, y),
                GetCell(x,y-1),
                GetCell(x,y+1)
            }.Where(x => x is not null)
            .OfType<bool>().ToList();

            bool? GetCell(int x, int y) => gv.IsInBounds(x, y) ? gv.ValueAt(x, y) : null;
        }

        public IReadOnlyCollection<bool> Neighbors { get; init; }

        public bool GetCurrent() => _isAlive;

        public bool GetNext() => _nextAlive;

        public void SetNext(bool isAlive) => _nextAlive = isAlive;
    }
}

public interface IRule
{
    bool Execute(ICell cell);
}

public interface ICell
{
    IReadOnlyCollection<bool> Neighbors { get; }
    bool GetCurrent();
    void SetNext(bool isAlive);
}
