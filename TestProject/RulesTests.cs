using GameOfLife;
using TestProject.Fakes;

namespace TestProject;

public class RulesTests
{
    [Theory]
    [InlineData(1, true, false)]
    [InlineData(1, false, false)]
    [InlineData(2, true, true)]
    [InlineData(2, false, false)]
    [InlineData(3, true, true)]
    [InlineData(3, false, true)]
    [InlineData(4, true, false)]
    public void ConwayRules_MatchExpected(int liveNeighbors, bool initialState, bool expectedAlive)
    {
        // Arrange
        var cell = new Cell(initialState, liveNeighbors);

        // Act
        var result = LifeGame.ConwayRules(cell);

        // Assert
        Assert.Equal(expectedAlive, result);
    }
}