namespace GameOfLife;

public interface IBoardState
{
    int BoardSize { get; init; }

    bool[] GetState(int delta = 0);
    bool[] Next();
}
