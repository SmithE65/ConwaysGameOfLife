using GameOfLife;
using GameOfLife.Rules;

namespace TestProject;

public class Cell : ICell
{
    private readonly bool _isAlive;

    public Cell(bool alive, List<bool> neighbors)
    {
        _isAlive = alive;
    }

    public int AliveNeighbors { get; init; }
    public bool IsAlive => _isAlive;
}

public class RulesTests
{
    [Fact]
    public void SurviveRule_TwoNeighbors_ReturnsTrue()
    {
        // Arrange
        var cell = new Cell(true, new List<bool> { false, false, true, true });
        var sut = new SurvivesRule();

        // Act
        var result = sut.Execute(cell);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void SurviveRule_OneNeighbor_ReturnsFalse()
    {
        // Arrange
        var cell = new Cell(true, new List<bool> { true, false, false, false });
        var sut = new SurvivesRule();

        // Act
        var result = sut.Execute(cell);

        // Assert
        Assert.False(result);
    }
}