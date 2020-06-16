using System;

namespace SillyButtons.Abstractions
{
    public class GameRecord
    {
        public string SecretWord { get; set; }
        public GameStatus GameResult { get; set; }
        public string GuessedCharacters { get; set; }
        public int WrongGuesses { get; set; }
        public DateTime Date { get; set; }
    }
}
