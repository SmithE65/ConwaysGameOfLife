namespace GameOfLife;

public interface IBoardState
{
    int BoardSize { get; init; }

    bool[] State(int delta = 0);
    bool[] Next();
}
