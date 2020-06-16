using Moq;
using NUnit.Framework;
using SillyButtons.Abstractions;
using SillyButtons.Hangman;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace SillyButtons.Modules.Tests
{
    public class PlayerContextTest
    {
        private PlayerContext context;
        private static readonly string recordsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Constants.GameRecordsRelativePath);

        [SetUp]
        public void Setup()
        {
            context = new PlayerContext();
        }

        [Test]
        public void TestSavingWithoutPlayerName()
        {
            Assert.Throws<InvalidOperationException>(() => context.SaveGameRecord(It.IsAny<GameRecord>()));
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
            context.SetPlayerName("Name");
            context.SaveGameRecord(gameRecord);
            FileAssert.Exists(GetPlayerRecordFileName("Name"));
            var savedRecords = GetPlayerRecords("Name");
            Assert.AreEqual(1, savedRecords.Count);
            var savedRecord = savedRecords[0];
            AssertRecordsAreEqual(gameRecord, savedRecord);
        }

        [Test]
        public void TestSaveAndGetMultipleRecords()
        {
            context.SetPlayerName("Name");
            var recordZero = new GameRecord() { SecretWord = "A" };
            var recordOne = new GameRecord() { SecretWord = "B" };
            var recordTwo = new GameRecord() { SecretWord = "C" };
            context.SaveGameRecord(recordZero);
            context.SaveGameRecord(recordOne);
            context.SaveGameRecord(recordTwo);
            var savedRecords = GetPlayerRecords("Name");
            Assert.AreEqual(3, savedRecords.Count);
            AssertRecordsAreEqual(recordZero, savedRecords[0]);
            AssertRecordsAreEqual(recordOne, savedRecords[1]);
            AssertRecordsAreEqual(recordTwo, savedRecords[2]);

        }

        private string GetPlayerRecordFileName(string playerName)
        {
            return Path.Combine(recordsFilePath, $"{playerName}.hgr");
        }

        private void AssertRecordsAreEqual(GameRecord expected, GameRecord actual)
        {
            Assert.AreEqual(expected.Date, actual.Date);
            Assert.AreEqual(expected.SecretWord, actual.SecretWord);
            Assert.AreEqual(expected.GameResult, actual.GameResult);
            Assert.AreEqual(expected.GuessedCharacters, actual.GuessedCharacters);
            Assert.AreEqual(expected.WrongGuesses, actual.WrongGuesses);
        }

        private List<GameRecord> GetPlayerRecords(string playerName)
        {
            return JsonSerializer.Deserialize<List<GameRecord>>(File.ReadAllText(GetPlayerRecordFileName(playerName)));
        }

        [TearDown]
        public void Teardown()
        {
            TestExtensions.CleanUpDirectory(recordsFilePath);
        }

    }
}
