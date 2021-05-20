using System;
using System.Linq;
using System.Collections.Generic;

namespace Hangman_workshop_Bot.Models
{
    public class Game
    {
        private static List<Game> Games { get; set; }
        internal static List<string> Images { get; set; }
        
        private long ChatId { get; set; }
        private int AttemptCount { get; set; }
        private string Word { get; set; }
        internal string ProcessedWord { get; set; }
        internal List<char> NotGuessedCharacters { get; set; }


        private Game()
        {
            Word                    = "developer";
            ProcessedWord           = new string('-', Word.Length);
            AttemptCount            = 6;
            NotGuessedCharacters    = new List<char>();
            Images                  = new List<string>()
                { 
                    @"D:/Downloads/Hang/75px-Hangman-0.png",
                    @"D:/Downloads/Hang/75px-Hangman-1.png",
                    @"D:/Downloads/Hang/75px-Hangman-2.png",
                    @"D:/Downloads/Hang/75px-Hangman-3.png",
                    @"D:/Downloads/Hang/75px-Hangman-4.png",
                    @"D:/Downloads/Hang/75px-Hangman-5.png",
                    @"D:/Downloads/Hang/75px-Hangman-6.png"
                };
        }

        public static Game GetGame(long chatId)
        {
            if (Games != null && Games.Count != 0)
            {
                return Games.Where(g => g.ChatId == chatId).FirstOrDefault();
            }

            Game game = new Game();
            game.ChatId = chatId;

            Games = new List<Game>();
            Games.Add(game);

            return game;
        }

        internal void Attempt(char character)
        {
            if (Word.Contains(character))
            {
                ProcessedWord = Guessed(character);
            }
            else
            {
                DidNotGuess(character);
            }
        }

        private string Guessed(char character)
        {
            var pWord = ProcessedWord.ToCharArray();
            for (int i = 0; i < Word.Length; i++)
            {
                if (Word[i] == character)
                    pWord[i] = character;
            }
            return new String(pWord);
        }
        private void DidNotGuess(char character)
        {
            AttemptCount -= 1;
            NotGuessedCharacters.Add(character);
        }

        internal bool GuessWholeWord(string message)
        {
            string[] temp = message.Split(" ");

            return temp[1].ToLower() == Word;
        }

        internal bool IsMistaken()
        {
            return AttemptCount < 6;
        }

        internal bool IsLose()
        {
            return AttemptCount == 0;
        }

        internal bool IsWin()
        {
            return !ProcessedWord.Contains('-');
        }

        internal string Lose(string answer)
        {
            EndGame();
            return $"{answer}\nYou lost, the man was hanged";
        }

        internal string Win(string answer)
        {
            EndGame();
            return $"{answer}\nYou win, the man is still alive!";
        }

        internal void EndGame()
        {
            Games.Remove(this);
        }
    }
}
