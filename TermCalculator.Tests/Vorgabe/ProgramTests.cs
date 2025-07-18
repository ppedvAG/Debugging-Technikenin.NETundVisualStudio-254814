using TermCalculator.Vorgabe;

namespace TermCalculator.Tests.Vorgabe;

public class ProgramTests
{
    [Theory]
    [InlineData("25+13", 38)]
    [InlineData("25-13", 12)]
    [InlineData("25*13", 325)]
    [InlineData("26/13", 2)]
    public void CalculateTerm_FundamentalOperations_ShouldReturnCorrectResult(string input, int expectedOutput)
    {
        // Arrange
        var term = new Term(input);

        // Act
        var result = Program.CalculateTerm(term);

        // Assert
        Assert.Equal(expectedOutput, result);
    }
}
