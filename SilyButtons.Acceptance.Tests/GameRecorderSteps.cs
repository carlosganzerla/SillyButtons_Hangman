using NUnit.Framework;
using SillyButtons.Abstractions;
using SillyButtons.Hangman;
using SillyButtons.Modules.Tests;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using TechTalk.SpecFlow;

namespace SilyButtons.Acceptance.Tests
{
    [Binding]
    public class GameRecorderSteps
    {
        private RecordableGame recordableGame;
        private HangmanGame actualGame;
        private PlayerContext context;
        private GameRecord currentRecord;
        private DateTime gameDate;
        private static readonly string recordsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppConstants.GameRecordsRelativePath);

        public GameRecorderSteps()
        {
            context = new PlayerContext();
            actualGame = new HangmanGame();
            recordableGame = new RecordableGame(actualGame, context);
        }

        [Given(@"My names is ""(.*)""")]
        public void GivenMyNamesIs(string p0)
        {
            context.SetCurrentPlayer(p0);

        }
        [Given(@"the word is ""(.*)""")]
        public void GivenTheWordIs(string p0)
        {
            recordableGame.SetSecretWord(p0);
            gameDate = DateTime.Now;
        }

        [When(@"I guess ""(.*)""")]
        public void WhenIGuess(string p0)
        {
            recordableGame.MakeGuess(p0);
        }

        [When(@"I quit the game")]
        public void WhenIQuitTheGame()
        {
            recordableGame.Concede();
        }

        [Then(@"the a game is added to ""(.*)"" history")]
        public void ThenTheAGameIsAddedToHistory(string p0)
        {
            string filePath = Path.Combine(recordsFilePath, $"{p0}.hgr");
            FileAssert.Exists(filePath);
            var savedRecords = JsonSerializer.Deserialize<List<GameRecord>>(File.ReadAllText(filePath));
            currentRecord = savedRecords[^1];
        }

        [Then(@"the history records (.*) wrong guesses")]
        public void ThenTheHistoryRecordsWrongGuesses(int p0)
        {
            Assert.AreEqual(p0, currentRecord.WrongGuesses);
        }

        [Then(@"the hisotry records the ""(.*)"" guessed characters")]
        public void ThenTheHisotryRecordsTheGuessedCharacters(string p0)
        {
            Assert.AreEqual(p0, currentRecord.GuessedCharacters);
        }

        [Then(@"the history records ""(.*)"" as the secret word")]
        public void ThenTheHistoryRecordsAsTheSecretWord(string p0)
        {
            Assert.AreEqual(p0, currentRecord.SecretWord);
        }

        [Then(@"the history records the result as a win")]
        public void ThenTheHistoryRecordsTheResultAsAWin()
        {
            Assert.AreEqual(GameStatus.Won, currentRecord.GameResult);
        }

        [Then(@"the history records the result as a loss")]
        public void ThenTheHistoryRecordsTheResultAsALoss()
        {
            Assert.AreEqual(GameStatus.Lost, currentRecord.GameResult);
        }

        [Then(@"the history records the game start date")]
        public void ThenTheHistoryRecordsTheGameStartDate()
        {
            Assert.AreEqual(0, gameDate.Subtract(currentRecord.Date).TotalMilliseconds, 100);
        }


        [AfterFeature]
        public static void Teardown()
        {
            TestExtensions.CleanUpDirectory(recordsFilePath);
        }
    }
}
