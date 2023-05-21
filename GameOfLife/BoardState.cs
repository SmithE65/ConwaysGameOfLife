namespace GameOfLife;

internal class BoardState : IBoardState
{
    private readonly int _depth;
    private readonly bool[][] _state;
    private int current = 0;

    public int HistoryDepth { get; init; }
    public int BoardSize { get; init; }

    public BoardState(int height, int width, int depth)
    {
        BoardSize = height * width;
        _depth = depth;
        _state = new bool[depth][];
        for (int i = 0; i < depth; i++)
        {
            _state[i] = new bool[BoardSize];
        }
    }

    public bool[] GetState(int delta = 0)
    {
        if (delta < 0 || delta >= _depth)
        {
            throw new InvalidOperationException($"{nameof(delta)} exceeds max history depth ({_depth})");
        }

        return _state[(_depth + current - delta) % _depth];
    }

    public bool[] Next()
    {
        current++;
        current %= _depth;
        return _state[current];
    }
}
