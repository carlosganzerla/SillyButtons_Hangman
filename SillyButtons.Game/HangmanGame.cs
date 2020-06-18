using SillyButtons.Abstractions;
using System.Text;

namespace SillyButtons.Hangman
{
    public class HangmanGame : IHangmanGame
    {
        private readonly StringBuilder guessedCharacters = new StringBuilder();
        private readonly StringBuilder displayWord = new StringBuilder();
        private string secretWord;
        public string DisplayWord
        {
            get
            {
                return displayWord.ToString();
            }
        }
        public string GuessedCharacters
        {
            get
            {
                return guessedCharacters.ToString();
            }
        }
        public GameStatus Status { get; private set; }
        public int RemainingGuesses { get; private set; }
        private void ResetScore(int secretWordLength)
        {
            ResetGameVariables();
            ResetDisplayWord(secretWordLength);
        }

        private void ResetGameVariables()
        {
            Status = GameStatus.Playing;
            RemainingGuesses = Constants.MaximumGuesses;
            guessedCharacters.Clear();
        }

        private void ResetDisplayWord(int length)
        {
            displayWord.Clear();
            for (int i = 0; i < length; i++)
            {
                displayWord.Append(" ");
            }
        }

        public void SetSecretWord(string word)
        {
            secretWord = word;
            ResetScore(word.Length);
        }

        public void MakeGuess(char guess)
        {
            if (IsValidGuess(guess))
            {
                UpdateScore(guess);
            }
        }

        private bool IsValidGuess(char guess)
        {
            return IsGameBeingPlayed() && !GuessedCharacters.Contains(guess);
        }

        private bool IsGameBeingPlayed()
        {
            return Status == GameStatus.Playing;
        }

        private void UpdateScore(char guess)
        {
            guessedCharacters.Append(guess);
            if (!secretWord.Contains(guess))
            {
                RemainingGuesses--;
                CheckDefeat();
            }
            else
            {
                UpdateDisplayWord(guess);
                CheckVictory();
            }
        }

        private void UpdateDisplayWord(char guess)
        {
            for (int i = 0; i < secretWord.Length; i++)
            {
                if (secretWord[i] == guess)
                {
                    displayWord[i] = guess;
                }
            }
        }

        private void CheckVictory()
        {
            if (DisplayWord == secretWord)
            {
                Status = GameStatus.Won;
            }
        }

        private void CheckDefeat()
        {
            if (RemainingGuesses == 0)
            {
                Status = GameStatus.Lost;
            }
        }

        public void Concede()
        {
            if (IsGameBeingPlayed())
            {
                Status = GameStatus.Lost;
            }
        }
    }
}
