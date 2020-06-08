using SillyButtons.Interfaces;
using System.Text;

namespace SillyButtons.Game
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

        private void ResetScore(int secretWordLength)
        {
            Status = GameStatus.Playing;
            RemainingGuesses = Constants.MaximumGuesses;
            guessedCharacters.Clear();
            displayWord.Clear();
            for (int i = 0; i < secretWordLength; i++)
            {
                displayWord.Append(" ");
            }
        }


        public void SetSecretWord(string word)
        {
            secretWord = word;
            ResetScore(word.Length);
        }

        public int RemainingGuesses { get; private set; }

        private void UpdateRemainingGuesses()
        {
            RemainingGuesses--;
            if (RemainingGuesses == 0)
            {
                Status = GameStatus.Lost;
            }
        }

        private void UpdateScore(char guess)
        {
            guessedCharacters.Append(guess);
            if (!secretWord.Contains(guess))
            {
                UpdateRemainingGuesses();
            }
            else
            {
                UpdateDisplayWord(guess);
            }
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
            return Status == GameStatus.Playing && !GuessedCharacters.Contains(guess);
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
            if (DisplayWord == secretWord)
            {
                Status = GameStatus.Won;
            }
        }
    }
}
