namespace GameOfLife;

public interface IRule
{
    bool? Execute(ICell cell);
}
