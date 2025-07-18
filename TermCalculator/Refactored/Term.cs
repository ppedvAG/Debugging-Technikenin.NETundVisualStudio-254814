namespace TermCalculator.Refactored;

public class Term
{
    public string Input { get; set; }
    public double Number1 { get; set; }
    public double Number2 { get; set; }
    public CalcOperation Operation { get; set; }

    public Term(string? input)
    {
        Input = input ?? throw new ArgumentNullException(nameof(input));
        if (string.IsNullOrEmpty(Input.Trim()))
        {
            throw new ArgumentException(nameof(input), "Input cannot be empty");
        }
    }

    public Term Parse()
    {
        Operation = GetCalcOperation();

        string[] numbers = SplitTerm();

        Number1 = double.Parse(numbers[0]);
        Number2 = double.Parse(numbers[1]);

        return this;
    }

    public double Calculate() => Operation switch
    {
        CalcOperation.Addition => Number1 + Number2,
        CalcOperation.Subtraction => Number1 - Number2,
        CalcOperation.Multiplication => Number1 * Number2,
        CalcOperation.Division => Number1 / Number2,
        _ => throw new InvalidOperationException("Invalid operation"),
    };

    private CalcOperation GetCalcOperation()
    {
        if (Input.Contains('+'))
            return CalcOperation.Addition;
        else if (Input.Contains('-'))
            return CalcOperation.Subtraction;
        else if (Input.Contains('*'))
            return CalcOperation.Multiplication;
        else if (Input.Contains('/'))
            return CalcOperation.Division;
        else
            throw new InvalidOperationException("Invalid operation");
    }

    private string[] SplitTerm()
    {
        char separator = Operation switch
        {
            CalcOperation.Addition => '+',
            CalcOperation.Subtraction => '-',
            CalcOperation.Multiplication => '*',
            CalcOperation.Division => '/',
            _ => throw new InvalidOperationException("Invalid operation"),
        };

        var result = Input.Split(separator);
        return result.Length == 2 
            ? result 
            : throw new FormatException("Invalid term format");
    }
}
