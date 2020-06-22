using Moq;
using NUnit.Framework;
using SillyButtons.Abstractions;
using SillyButtons.Hangman;
using System;
using System.IO;
using System.Linq;

namespace SillyButtons.Modules.Tests
{
    public class PlayerContextTest
    {
        private PlayerContext context;
        private static readonly string recordsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppConstants.GameRecordsRelativePath);

        [SetUp]
        public void Setup()
        {
            context = new PlayerContext();
        }

        [Test]
        public void TestSavingWithoutSettingCurrentPlayer()
        {
            Assert.Throws<InvalidOperationException>(() => context.SaveCurrentPlayerRecord(It.IsAny<GameRecord>()));
        }

        [Test]
        public void TestSettingPlayerAndSavingInitialRecord()
        {
            context.SetCurrentPlayer("Name");
            FileAssert.Exists(GetPlayerRecordFileName("Name"));
        }

        private string GetPlayerRecordFileName(string playerName)
        {
            return Path.Combine(recordsFilePath, $"{playerName}.hgr");
        }

        [Test]
        public void TestSaveAndGetRecordForTheFirstTime()
        {
            var date = DateTime.Now;
            var gameRecord = new GameRecord
            {
                Date = date,
                SecretWord = "WORD",
                GameResult = GameStatus.Playing,
                GuessedCharacters = "ABC",
                WrongGuesses = 3
            };
            context.SetCurrentPlayer("Name");
            context.SaveCurrentPlayerRecord(gameRecord);
            FileAssert.Exists(GetPlayerRecordFileName("Name"));
            var savedRecords = context.GetCurrentPlayerRecords().ToList();
            Assert.AreEqual(1, savedRecords.Count);
            var savedRecord = savedRecords[0];
            AssertRecordsAreEqual(gameRecord, savedRecord);
        }

        [Test]
        public void TestSaveAndGetMultipleRecords()
        {
            context.SetCurrentPlayer("Name");
            var recordZero = new GameRecord() { SecretWord = "A" };
            var recordOne = new GameRecord() { SecretWord = "B" };
            var recordTwo = new GameRecord() { SecretWord = "C" };
            context.SaveCurrentPlayerRecord(recordZero);
            context.SaveCurrentPlayerRecord(recordOne);
            context.SaveCurrentPlayerRecord(recordTwo);
            var savedRecords = context.GetCurrentPlayerRecords().ToList();
            Assert.AreEqual(3, savedRecords.Count);
            AssertRecordsAreEqual(recordZero, savedRecords[0]);
            AssertRecordsAreEqual(recordOne, savedRecords[1]);
            AssertRecordsAreEqual(recordTwo, savedRecords[2]);
        }


        private void AssertRecordsAreEqual(GameRecord expected, GameRecord actual)
        {
            Assert.AreEqual(expected.Date, actual.Date);
            Assert.AreEqual(expected.SecretWord, actual.SecretWord);
            Assert.AreEqual(expected.GameResult, actual.GameResult);
            Assert.AreEqual(expected.GuessedCharacters, actual.GuessedCharacters);
            Assert.AreEqual(expected.WrongGuesses, actual.WrongGuesses);
        }

        [Test]
        public void TestGetCurrentPlayer()
        {
            context.SetCurrentPlayer("Name");
            Assert.AreEqual("Name", context.CurrentPlayer);
        }


        [Test]
        public void TestGetPlayerNames_DirectoryExists()
        {
            Directory.CreateDirectory(recordsFilePath);
            File.WriteAllText(GetPlayerRecordFileName("Bar"), "bsdasdsd"); // Alphabetic order cuz the windows FS does this way
            File.WriteAllText(GetPlayerRecordFileName("Foo"), "asdsaasd");
            File.WriteAllText(GetPlayerRecordFileName("Joe"), "dsasddas");
            var players = context.GetAllPlayers();
            Assert.AreEqual(3, players.Count());
            Assert.AreEqual("Bar", players.ElementAt(0));
            Assert.AreEqual("Foo", players.ElementAt(1));
            Assert.AreEqual("Joe", players.ElementAt(2));
        }

        [Test]
        public void TestGetPlayerNames_DirectoryDoesNotExist()
        {
            var players = context.GetAllPlayers();
            Assert.AreEqual(0, players.Count());
        }

        [Test]
        public void TestGetRecordsWithoutSettingCurrentPlayer()
        {
            Assert.Throws<InvalidOperationException>(() => context.GetCurrentPlayerRecords());
        }

        [Test]
        public void TestGetRecordsForAbsolutelyNewPlayer()
        {
            context.SetCurrentPlayer("Player");
            var records = context.GetCurrentPlayerRecords();
            Assert.AreEqual(0, records.Count());
        }


        [TearDown]
        public void Teardown()
        {
            TestExtensions.CleanUpDirectory(recordsFilePath);
        }

    }
}
