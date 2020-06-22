using NUnit.Framework;
using SillyButtons.Abstractions;
using SillyButtons.Hangman;
using SillyButtons.Views;
using System;

namespace SillyButtons.Modules.Tests
{
    public class GameRecordFormTest
    {
        private GameRecordForm form;

        [SetUp]
        public void Setup()
        {
            form = new GameRecordForm();
            form.Show();
        }

        [Test]
        public void TestContentForOneRecord()
        {
            var records = CreateRecordArray(1);
            form.ShowGameRecords(records);
            AssertRecordText(records[0]);
        }

        private GameRecord[] CreateRecordArray(int count)
        {
            GameRecord[] records = new GameRecord[count];
            for (int i = 0; i < count; i++)
            {
                records[i] = new GameRecord
                {
                    SecretWord = $"WORD_{i}",
                    Date = DateTime.Now,
                    GameResult = GameStatus.Lost,
                    GuessedCharacters = $"GUESSES_{i}",
                    WrongGuesses = i
                };
            }
            return records;
        }

        private void AssertRecordText(GameRecord record)
        {
            Assert.AreEqual(AppStrings.GameRecordString(record), form.gameRecordInfo.Text);
        }

        [Test]
        public void TestNavigationSingleRecord()
        {
            var records = CreateRecordArray(1);
            form.ShowGameRecords(records);
            Assert.IsFalse(form.previousButton.Enabled);
            Assert.IsFalse(form.nextButton.Enabled);
        }

        [Test]
        public void TestNavigationFirstOfMultipleRecords()
        {
            var records = CreateRecordArray(2);
            form.ShowGameRecords(records);
            Assert.IsFalse(form.previousButton.Enabled);
            Assert.IsTrue(form.nextButton.Enabled);
        }

        [Test]
        [TestCase(3)]
        [TestCase(5)]
        [TestCase(10)]
        [TestCase(25)]
        public void TestForwardAndBackwardNavigationSecondToLastButOneRecords(int recordCount)
        {
            var records = CreateRecordArray(recordCount);
            form.ShowGameRecords(records);
            for (int i = 1; i < recordCount - 1; i++)
            {
                form.nextButton.PerformClick();
                AssertRecordText(records[i]);
                Assert.IsTrue(form.previousButton.Enabled);
                Assert.IsTrue(form.nextButton.Enabled);
            }
            for (int i = recordCount - 2; i >= 1; i--)
            {
                AssertRecordText(records[i]);
                Assert.IsTrue(form.previousButton.Enabled);
                Assert.IsTrue(form.nextButton.Enabled);
                form.previousButton.PerformClick();
            }
        }

        [Test]
        public void TestNavigationLastfMultipleRecords()
        {
            var records = CreateRecordArray(2);
            form.ShowGameRecords(records);
            form.nextButton.PerformClick();
            Assert.IsTrue(form.previousButton.Enabled);
            Assert.IsFalse(form.nextButton.Enabled);
        }

        [Test]
        public void TestSetTitle()
        {
            form.SetTitle("title");
            Assert.AreEqual("title", form.Text);
        }
    }
}
