using HelloDebug.Data;
using System.Diagnostics;
using System.Threading.Channels;

namespace HelloDebug
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Probleme vorbeugen: Positive Zahlen eingeben.");
            _ = ParsePositiveNumber(Console.ReadLine());

            Console.WriteLine("\nProbleme vorbeugen: Dateizugriff");
            var (content, error) = ReadFileContent(Console.ReadLine());
            Console.WriteLine(error ?? content);

            Console.WriteLine("\nProbleme vorbeugen: Ungueltige Parameter nicht erlauben mittels Constraints");
            PrintCreature(new Bird() { Name = "Duffy 🦆" });


            Console.WriteLine("\nPre- & Post-Conditions");
            CalculateVelocity(0, 0, 0);

            Console.WriteLine("\nFail-Fast vs. Fail-Safe");
            Divide(0, 0);
            DivideOrDefault(0, 0);
        }

        #region Eingaben validieren
        // Beispiel 1: Input-Validierung einer Benutzer-Eingabe
        private static int ParsePositiveNumber(string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException("Eingabe darf nicht leer sein.");
            }

            if (!int.TryParse(input, out int number))
            {
                throw new FormatException("Eingabe muss eine Zahl sein.");
            }

            if (number <= 0)
            {
                throw new ArgumentOutOfRangeException("Eingabe muss eine positive Zahl sein.");
            }

            return number;
        }

        // Beispiel 2: Fehlerbehandlung bei Dateizugriff
        private static (string result, string? error) ReadFileContent(string? filePath)
        {
            try
            {
                // Proaktive Validierung, um Overhead zu vermeiden
                if (string.IsNullOrWhiteSpace(filePath))
                {
                    return (string.Empty, "Dateipfad darf nicht leer sein.");
                }

                if (!File.Exists(filePath))
                {
                    return (string.Empty, "Datei existiert nicht.");
                }

                var content = File.ReadAllText(filePath);
                return (content, null);
            }
            catch (FileNotFoundException)
            {
                return (string.Empty, "Datei nicht gefunden.");
            }
            catch (UnauthorizedAccessException)
            {
                return (string.Empty, "Keine Berechtigung zum Lesen der Datei.");
            }
            catch (Exception ex)
            {
                return (string.Empty, $"Fehler beim Lesen der Datei: {ex.Message}");
            }
        }

        // Beispiel 3: Input Parameter mit Contraints einschraenken
        // Der uebergebene Parameter muss sowohl ICreature als auch IVolatile implementieren
        private static void PrintCreature_BadSample(object creature) => Console.WriteLine(creature);
        private static void PrintCreature<T>(T creature)
            where T : ICreature, IVolatile
        {
            creature.Talk();
            creature.Fly();
        }
        #endregion

        #region Assertion
        public static double CalculateVelocity(double phi, double theta, double radius)
        {
            // Preconditions (Entwicklungszeit)
            Debug.Assert(phi is > 0 and < 2 * Math.PI);
            Debug.Assert(radius is > 0 and < 100);

            // Sanitäre Validierung (Laufzeit)
            var p = Math.Clamp(phi, 0, 2 * Math.PI);
            var r = Math.Max(0, Math.Min(radius, 100));

            // ... Berechnung ...
            var result = Math.Sqrt(r * r * Math.Cos(p) * Math.Sin(theta));

            // Postcondition (Entwicklungszeit)
            Debug.Assert(Math.Abs(result) < 1000);
            return result;
        }
        #endregion

        #region Fail-Fast vs. Fail-Safe
        public static double Divide(double numerator, double denominator)
        {
            // Fail-Fast: Sofort stoppen, wenn Bedingung nicht erfüllt
            if (denominator == 0)
                throw new ArgumentException("Denominator must not be zero.");

            return numerator / denominator;
        }

        public static double DivideOrDefault(double numerator, double denominator)
        {
            // Fail-Safe: Fehler abfangen, Standardwert zurückgeben
            if (denominator == 0)
            {
                // Loggen, Benutzer informieren, Defaultwert zurückgeben
                Console.WriteLine("Warnung: Division durch Null. Ergebnis auf 0 gesetzt.");
                return default;
            }

            return numerator / denominator;
        }
        #endregion
    }
}
