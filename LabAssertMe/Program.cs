using System.Diagnostics;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Starte Debugging-Lab...");

        double[] werte = { 2.0, 4.0, 8.0, 16.0 };
        double mittelwert = BerechneMittelwert(werte);
        Console.WriteLine($"Mittelwert: {mittelwert}");

        double ergebnis = BerechneFormel(5, 0); // Achtung: 0 als zweiter Parameter!
        Console.WriteLine($"Formelergebnis: {ergebnis}");

        string text = "12345";
        int summe = Quersumme(text);
        Console.WriteLine($"Ziffernsumme: {summe}");

        Console.WriteLine("Lab beendet.");
    }

    // Fehler 1: Division durch Null bei leerem Array (tritt nur auf, wenn werte.Length == 0)
    static double BerechneMittelwert(double[] werte)
    {
        Debug.Assert(werte != null && werte.Length > 0, "Array muss Werte enthalten!");

        if (werte == null || werte.Length == 0)
        {
            // Fail-Fast
            throw new ArgumentException("Das Array darf nicht leer sein.");
        }

        double summe = 0;
        foreach (var w in werte)
            summe += w;

        double mittelwert = summe / werte.Length;

        Debug.Assert(mittelwert >= 0, "Der Mittelwert sollte nicht negativ sein.");
        return mittelwert;
    }


    // Fehler 2: Falsche Bereichsprüfung, logischer Fehler
    static double BerechneFormel(double x, double y)
    {
        if (y <= 0)
        {
            // Fail-Fast
            throw new ArgumentOutOfRangeException(nameof(y), "y muss größer als 0 sein.");
        }

        double resultat = (x * x) / y;

        Debug.Assert(!double.IsNaN(resultat), "Ergebnis darf nicht NaN sein.");
        return resultat;
    }


    // Fehler 3: Verdeckter Fehler bei ungültigem Input (kein Fehler bei "12345", aber z.B. bei "12a45")
    static int Quersumme(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("Eingabe darf nicht leer sein.");

        int summe = 0;
        foreach (var c in input)
        {
            if (char.IsDigit(c))
            {
                summe += int.Parse(c.ToString());
            }
            else
            {
                Console.WriteLine($"Hinweis: Ungültiges Zeichen '{c}' übersprungen.");
            }
        }

        Debug.Assert(summe >= 0, "Ziffernsumme sollte nicht negativ sein.");
        return summe;
    }
}
