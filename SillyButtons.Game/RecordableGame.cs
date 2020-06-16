using SillyButtons.Abstractions;
using System;

namespace SillyButtons.Hangman
{
    public class RecordableGame : IHangmanGame
    {
        private readonly IHangmanGame actualGame;
        private readonly IPlayerContext context;
        private bool currentRecordSaved;

        public RecordableGame(IHangmanGame actualGame, IPlayerContext context)
        {
            this.actualGame = actualGame;
            this.context = context;
        }

        public GameRecord CurrentRecord { get; private set; }

        public string DisplayWord => actualGame.DisplayWord;

        public string GuessedCharacters => actualGame.GuessedCharacters;

        public GameStatus Status => actualGame.Status;

        public int RemainingGuesses => actualGame.RemainingGuesses;

        public void SetSecretWord(string word)
        {
            actualGame.SetSecretWord(word);
            CreateGameRecord(word);
            UpdateGameRecord();
        }
        private void CreateGameRecord(string word)
        {
            CurrentRecord = new GameRecord
            {
                Date = DateTime.Now,
                SecretWord = word,
            };
            currentRecordSaved = false;
        }

        private void UpdateGameRecord()
        {
            CurrentRecord.GuessedCharacters = actualGame.GuessedCharacters;
            CurrentRecord.GameResult = actualGame.Status;
            CurrentRecord.WrongGuesses = Constants.MaximumGuesses - actualGame.RemainingGuesses;
        }

        public void MakeGuess(char guess)
        {
            actualGame.MakeGuess(guess);
            UpdateGameRecord();
            if (CanSaveRecord())
            {
                SaveGameRecord(); 
            }
        }

        private void SaveGameRecord()
        {
            context.SaveGameRecord(CurrentRecord);
            currentRecordSaved = true;
        }

        private bool CanSaveRecord()
        {
            return actualGame.Status != GameStatus.Playing && !currentRecordSaved;
        }

    }
}
