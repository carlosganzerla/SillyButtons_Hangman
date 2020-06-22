using SillyButtons.Abstractions;

namespace SillyButtons.Hangman
{
    public static class AppStrings
    {
        public static readonly string GameLostMessage = "You lost :(";
        public static readonly string GameWonMessage = "You won, congratulations! :)";
        public static string GamePlayingMessage(IHangmanGame game)
        {
            return $"You have already guessed the characters '{game.GuessedCharacters}'. You have {game.RemainingGuesses} guesses left.";
        }

        public static string GameRecordWindowTitle(string playerName)
        {
            return $"Game Record of {playerName}";
        }
        public static string GameRecordString(GameRecord record)
        {
            return $"**** Game Record ****\r\n**** {record.Date:yyyy-MM-dd HH:mm:ss} ****\r\nGame Result: {record.GameResult}\r\n" +
                $"Secret Word: {record.SecretWord}\r\nGuesses: {record.GuessedCharacters} - {record.WrongGuesses} wrong";
        }
    }

}
