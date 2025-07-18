namespace TermCalculator.Refactored;

public enum CalcOperation { Addition = 1, Subtraction, Multiplication, Division }

public class Program
{
    public static void Main(string[] args)
    {
        var input = GetInput();
        var result = new Term(input)
            .Parse()
            .Calculate();

        Console.WriteLine($"\t={result}");
    }

    public static string? GetInput()
    {
        Console.WriteLine("Bitte gib einen Term mit zwei Zahlen und einem Grundrechenoperator (+ - * /) ein (z.B.: 25+13):");
        return Console.ReadLine();
    }
}
