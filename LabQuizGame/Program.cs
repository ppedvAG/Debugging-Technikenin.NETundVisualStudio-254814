﻿using QuizGame.Enums;
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

            // Zu Testzwecken verwenden wir nur die ersten 3 Fragen
            // Precompiler Anweisung: Wenn wir uns im DEBUG Modus befinden
#if DEBUG
            quizItems = quizItems.Take(3);
#endif
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
                    PropertyNameCaseInsensitive = true,
                    Converters = { /*new JsonStringEnumConverter()*/ }
                };

                var json = File.ReadAllText(filePath);
                var result = JsonSerializer.Deserialize<T[]>(json, options);
                if (result != null)
                {
                    return result;
                }
            }
            else if (!filePath.EndsWith(".json"))
            {
                return ReadJsonFile<T>($"{filePath}.json");
            }

            // default gibt den Standartwert des Object-Typs zurueck
            // int = 0, float = 0, bool = false, object = null (arrays, strings usw.)
            //return default;

            // Besser ist es aber ein leeres Array zurueck zu geben
            return [];
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
