using GameOfLife.Rules;
using TestProject.Fakes;

namespace TestProject;

public class RulesTests
{
    [Fact]
    public void SurviveRule_TwoNeighbors_ReturnsTrue()
    {
        // Arrange
        var cell = new Cell(true, 2);
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
        var cell = new Cell(true, 1);
        var sut = new SurvivesRule();

        // Act
        var result = sut.Execute(cell);

        // Assert
        Assert.False(result);
    }
}