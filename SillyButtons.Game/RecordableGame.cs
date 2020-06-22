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
            CreateRecord(word);
            UpdateRecord();
        }
        private void CreateRecord(string word)
        {
            CurrentRecord = new GameRecord
            {
                Date = DateTime.Now,
                SecretWord = word,
            };
            currentRecordSaved = false;
        }
        private void UpdateRecord()
        {
            CurrentRecord.GuessedCharacters = actualGame.GuessedCharacters;
            CurrentRecord.GameResult = actualGame.Status;
            CurrentRecord.WrongGuesses = AppConstants.MaximumGuesses - actualGame.RemainingGuesses;
        }

        public void MakeGuess(char guess)
        {
            actualGame.MakeGuess(guess);
            UpdateRecord();
            SaveRecordIfNeeded();
        }

        private void SaveRecordIfNeeded()
        {
            if (CanSaveRecord())
            {
                context.SaveCurrentPlayerRecord(CurrentRecord);
                currentRecordSaved = true;
            }
        }

        private bool CanSaveRecord()
        {
            return actualGame.Status != GameStatus.Playing && !currentRecordSaved;
        }

        public void Concede()
        {
            actualGame.Concede();
            UpdateRecord();
            SaveRecordIfNeeded();
        }
    }
}
