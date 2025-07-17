﻿namespace QuizGame.Models
{
    public class User
    {
        public string Name { get; set; }

        public string Game { get; set; }

        public int Score { get; set; }

        public void IncrementScore(int points)
        {
            Score += points;
        }

        public void ShowGreeting()
        {

            Console.WriteLine($"Hallo, {Name}!");
        }

        public void ShowScore()
        {
            Console.WriteLine($"Du hast {Score} Punkte erreicht.");
        }
    }
}
