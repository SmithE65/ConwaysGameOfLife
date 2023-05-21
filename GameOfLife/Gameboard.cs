namespace GameOfLife;

public class Gameboard : IGameboard
{
    private readonly int _height;
    private readonly int _width;
    private readonly int _historyDepth = 10;
    private readonly IBoardState _grid;

    public Gameboard(int height, int width)
    {
        _height = height;
        _width = width;
        _grid = new BoardState(height, width, _historyDepth);
    }

    public void SetAlive(int x, int y, bool alive = true) => _grid.GetState()[y * _width + x] = alive;

    public bool[] Snapshot(int delta = 0) => _grid.GetState(delta);

    public void UpdateCells(Func<ICell, bool> func)
    {
        _grid.Next();
        var current = _grid.GetState();
        var gv = new GridView(_grid.GetState(1), _width, _height);
        var tasks = Enumerable.Range(0, _height)
            .Select(row => Task.Run(() =>
            {
                var cv = new CellView();
                var start = row * _width;
                for (int i = 0; i < _width; i++)
                {
                    cv.Set(i, row, gv);
                    current[start+i] = func(cv);
                }
            })).ToArray();

        Task.WaitAll(tasks);
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
        public void Set(int x, int y, GridView gv)
        {
            IsAlive = gv.ValueAt(x, y);
            int n1 = GetCell(x - 1, y - 1) == true ? 1 : 0;
            int n2 = GetCell(x - 1, y) == true ? 1 : 0;
            int n3 = GetCell(x - 1, y + 1) == true ? 1 : 0;
            int n4 = GetCell(x, y - 1) == true ? 1 : 0;
            int n5 = GetCell(x, y + 1) == true ? 1 : 0;
            int n6 = GetCell(x + 1, y - 1) == true ? 1 : 0;
            int n7 = GetCell(x + 1, y) == true ? 1 : 0;
            int n8 = GetCell(x + 1, y + 1) == true ? 1 : 0;
            AliveNeighbors = n1 + n2 + n3 + n4 + n5 + n6 + n7 + n8;

            bool? GetCell(int x, int y) => gv.IsInBounds(x, y) ? gv.ValueAt(x, y) : null;
        }

        public int AliveNeighbors { get; private set; }

        public bool IsAlive { get; private set; }
    }
}
