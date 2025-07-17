using QuizGame.Enums;
using QuizGame.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace QuizGame
{
    public class Program
    {
        private const string HighScoreFilePath = "Highscore.json";
        private const string DefaultFilePath = "Grundlagen.json";

        static void Main(string[] args)
        {
            string fileName = args.FirstOrDefault() ?? DefaultFilePath;
            var highscore = new HighScore(ReadJsonFile<User>(HighScoreFilePath));

            var currentUser = new User
            {
                Name = GetUserName(),
                Game = Path.GetFileNameWithoutExtension(fileName),
            };

            currentUser.ShowGreeting();

            IEnumerable<QuizItem> quizItems = ReadJsonFile<QuizItem>(fileName)
                .OrderBy(x => Random.Shared.Next());

            var difficulty = SelectDifficulty();
            if (difficulty != null)
            {
                quizItems = quizItems.Where(x => x.Difficulty == difficulty);
            }

            int i = 0;
            foreach (QuizItem item in quizItems)
            {
                // Alternative waere eine if-Abfrage fuer die Schwierigkeit zu schreiben
                // if (item.Difficulty == difficulty.ToString())

                var points = item.AskQuestion(++i, quizItems.Count());
                currentUser.IncrementScore(points);
            }

            currentUser.ShowScore();

            highscore.Add(currentUser);
            highscore.Show();
            WriteJsonFile(HighScoreFilePath, highscore.Entries);

            Console.WriteLine("Beliebige Taste zum Beenden druecken...");
            Console.ReadKey();
        }

        private static string GetUserName()
        {
            Console.WriteLine("Wie lautet dein Name?");
            string? name = Console.ReadLine();
            if (string.IsNullOrEmpty(name))
            {
                name = "Gast";
            }

            return name;
        }

        private static Difficulty? SelectDifficulty()
        {
            Console.WriteLine("\nBitte waehle eine Schwierigkeit:");

            foreach (Difficulty difficulty in Enum.GetValues(typeof(Difficulty)))
            {
                Console.WriteLine($"{(int)difficulty}. {difficulty}");
            }

            if (int.TryParse(Console.ReadLine(), out int selection) 
                && Enum.IsDefined(typeof(Difficulty), selection))
            {
                Console.WriteLine($"Du hast die Schwierigkeit {Enum.GetName(typeof(Difficulty), selection)} gewaehlt.");
                return (Difficulty)selection;
            }

            Console.WriteLine("Alle Fragen werden gewaehlt.");
            return null;
        }

        private static T[] ReadJsonFile<T>(string filePath)
        {
            if (File.Exists(filePath))
            {
                var options = new JsonSerializerOptions
                {
                    Converters = { }
                };

                var json = File.ReadAllText(filePath + ".json");
                var result = JsonSerializer.Deserialize<T[]>(json, options);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        private static void WriteJsonFile(string filePath, List<User> entries)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var json = JsonSerializer.Serialize(entries, options);
            File.WriteAllText(filePath, json);
        }
    }
}
