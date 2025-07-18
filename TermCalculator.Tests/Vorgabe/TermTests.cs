using TermCalculator.Vorgabe;

namespace TermCalculator.Tests.Vorgabe;

public class TermTests
{
    // Good Cases
    [Fact]
    public void Term_ValidInputs_ShouldParseCorrectly()
    {
        // Arrange -- Ausgangszustand des Tests vorbereiten

        // Act -- Test ausführen
        var term = new Term("25+13");

        // Assert -- Testergebnis überprüfen
        Assert.Equal(25, term.Number1);
        Assert.Equal(13, term.Number2);
        Assert.Equal(CalcOperation.Addition, term.Operation);
    }

    // Bad Cases
    [Fact]
    public void Term_NullInput_ShouldThrowNullReferenceException()
    {
        Assert.Throws<NullReferenceException>(() => new Term(null));
    }

    [Fact]
    public void Term_EmptyInput_ShouldThrowNullReferenceException()
    {
        Assert.Throws<NullReferenceException>(() => new Term(""));
    }

    [Fact]
    public void Term_InvalidOperation_ShouldThrowNullReferenceException()
    {
        Assert.Throws<NullReferenceException>(() => new Term("1x1"));
    }

    [Fact]
    public void Term_InvalidNumber_ShouldThrowFormatException()
    {
        Assert.Throws<FormatException>(() => new Term("abc+123"));
    }
}