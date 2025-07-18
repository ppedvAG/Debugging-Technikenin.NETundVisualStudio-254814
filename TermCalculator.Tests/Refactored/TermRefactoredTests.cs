using TermCalculator.Refactored;

namespace TermCalculator.Tests.Refactored;

public class TermRefactoredTests
{
    // Good Cases
    [Fact]
    public void Term_ValidInputs_ShouldParseCorrectly()
    {
        // Arrange -- Ausgangszustand des Tests vorbereiten
        var term = new Term("25+13");

        // Act -- Test ausführen
        term.Parse();

        // Assert -- Testergebnis überprüfen
        Assert.Equal(25, term.Number1);
        Assert.Equal(13, term.Number2);
        Assert.Equal(CalcOperation.Addition, term.Operation);
    }

    [Theory]
    [InlineData("25+13", 38)]
    [InlineData("25-13", 12)]
    [InlineData("25*13", 325)]
    [InlineData("26/13", 2)]
    public void Calculate_FundamentalOperations_ShouldReturnCorrectResult(string input, int expectedOutput)
    {
        // Arrange
        var term = new Term(input).Parse();

        // Act
        var result = term.Calculate();

        // Assert
        Assert.Equal(expectedOutput, result);
    }

    // Bad Cases
    [Fact]
    public void Term_NullInput_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new Term(null));
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Term_EmptyInput_ShouldThrowArgumentException(string input)
    {
        Assert.Throws<ArgumentException>(() => new Term(input));
    }

    [Theory]
    [InlineData("12x34")]
    public void Term_InvalidOperation_ShouldThrowInvalidOperationException(string input)
    {
        // Arrange
        var term = new Term(input);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(term.Parse);
    }

    [Theory]
    [InlineData("abc+123")]
    public void Term_InvalidInput_ShouldThrowFormatException(string input)
    {
        // Arrange
        var term = new Term(input);

        // Act & Assert
        Assert.Throws<FormatException>(term.Parse);
    }
}