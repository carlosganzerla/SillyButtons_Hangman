using Moq;
using NUnit.Framework;
using SillyButtons.Abstractions;
using SillyButtons.Hangman;
using System;

namespace SillyButtons.Modules.Tests
{
    public class RecordableGameTest
    {
        private RecordableGame game;
        private Mock<IHangmanGame> actualGame;
        private Mock<IPlayerContext> playerContext;

        [SetUp]
        public void Setup()
        {
            playerContext = new Mock<IPlayerContext>();
            actualGame = new Mock<IHangmanGame>();
            game = new RecordableGame(actualGame.Object, playerContext.Object);
            actualGame.Setup(m => m.Status).Returns(GameStatus.Playing);
            actualGame.Setup(m => m.GuessedCharacters).Returns("");
            actualGame.Setup(m => m.RemainingGuesses).Returns(AppConstants.MaximumGuesses);
            game.SetSecretWord("WORD");
        }

        [Test]
        public void TestResetSecretWordBeforeEndgame()
        {
            var previousRecord = game.CurrentRecord;
            game.SetSecretWord("OTHER");
            Assert.AreEqual("OTHER", game.CurrentRecord.SecretWord);
            Assert.AreNotSame(previousRecord, game.CurrentRecord);
        }

        [Test]
        public void TestSetSecretWordForRecordBegin()
        {
            Assert.IsNotNull(game.CurrentRecord);
            Assert.AreEqual("WORD", game.CurrentRecord.SecretWord);
            Assert.AreEqual(game.CurrentRecord.GameResult, GameStatus.Playing);
            Assert.AreEqual(game.CurrentRecord.GuessedCharacters, "");
            Assert.AreEqual(0, game.CurrentRecord.WrongGuesses);
            Assert.AreNotEqual(DateTime.MinValue, game.CurrentRecord.Date);
            actualGame.Verify(m => m.SetSecretWord("WORD"));
        }

        [Test]
        public void TestMakeGuessDelegation()
        {
            game.MakeGuess('A');
            actualGame.Verify(m => m.MakeGuess('A'));
        }

        [Test]
        public void TestMakeGuessesThenGameIsLost()
        {
            string guessedCharacters = "ABC";
            actualGame.Setup(m => m.Status).Returns(GameStatus.Lost);
            actualGame.Setup(m => m.GuessedCharacters).Returns(guessedCharacters);
            actualGame.Setup(m => m.RemainingGuesses).Returns(0);
            game.MakeGuess(guessedCharacters);
            Assert.AreEqual(game.CurrentRecord.GameResult, GameStatus.Lost);
            Assert.AreEqual(game.CurrentRecord.GuessedCharacters, "ABC");
            Assert.AreEqual(AppConstants.MaximumGuesses, game.CurrentRecord.WrongGuesses);
            playerContext.Verify(m => m.SaveCurrentPlayerRecord(game.CurrentRecord), Times.Once());
            playerContext.VerifyNoOtherCalls();
        }

        [Test]
        public void TestMakeGuessesThenGameContinues()
        {
            int remainingGuesses = 3;
            string guessedCharacters = "ABC";
            actualGame.Setup(m => m.Status).Returns(GameStatus.Playing);
            actualGame.Setup(m => m.GuessedCharacters).Returns(guessedCharacters);
            actualGame.Setup(m => m.RemainingGuesses).Returns(remainingGuesses);
            game.MakeGuess(guessedCharacters);
            Assert.AreEqual(game.CurrentRecord.GameResult, GameStatus.Playing);
            Assert.AreEqual(game.CurrentRecord.GuessedCharacters, "ABC");
            Assert.AreEqual(AppConstants.MaximumGuesses - remainingGuesses, game.CurrentRecord.WrongGuesses);
            playerContext.Verify(m => m.SaveCurrentPlayerRecord(It.IsAny<GameRecord>()), Times.Never());
        }

        [Test]
        public void TestMakeGuessesThenGameIsWon()
        {
            int remainingGuesses = 4;
            string guessedCharacters = "ABC";
            actualGame.Setup(m => m.Status).Returns(GameStatus.Won);
            actualGame.Setup(m => m.GuessedCharacters).Returns(guessedCharacters);
            actualGame.Setup(m => m.RemainingGuesses).Returns(remainingGuesses);
            game.MakeGuess(guessedCharacters);
            Assert.AreEqual(game.CurrentRecord.GameResult, GameStatus.Won);
            Assert.AreEqual(game.CurrentRecord.GuessedCharacters, "ABC");
            Assert.AreEqual(AppConstants.MaximumGuesses - remainingGuesses, game.CurrentRecord.WrongGuesses);
            playerContext.Verify(m => m.SaveCurrentPlayerRecord(game.CurrentRecord), Times.Once());
            playerContext.VerifyNoOtherCalls();
        }

        [Test]
        public void TestMakeGuessesAfterGameIsLost()
        {
            actualGame.Setup(m => m.Status).Returns(GameStatus.Lost);
            game.MakeGuess('A');
            game.MakeGuess('B');
            playerContext.Verify(m => m.SaveCurrentPlayerRecord(game.CurrentRecord), Times.Once());
            playerContext.VerifyNoOtherCalls();
        }


        [Test]
        public void TestResetAndLoseGameAfterPreviousGameIsWon()
        {
            var previousRecord = game.CurrentRecord;
            actualGame.Setup(m => m.Status).Returns(GameStatus.Won);
            game.MakeGuess('A');
            game.SetSecretWord("DROW");
            var currentRecord = game.CurrentRecord;
            actualGame.Setup(m => m.Status).Returns(GameStatus.Lost);
            game.MakeGuess('A');
            playerContext.Verify(m => m.SaveCurrentPlayerRecord(previousRecord), Times.Once());
            playerContext.Verify(m => m.SaveCurrentPlayerRecord(currentRecord), Times.Once());
            playerContext.VerifyNoOtherCalls();
        }

        [Test]
        public void TestConcedingGame()
        {
            actualGame.Setup(m => m.Concede()).Callback(() =>
            {
                actualGame.Setup(m => m.Status).Returns(GameStatus.Lost);
            });
            game.Concede();
            actualGame.Verify(m => m.Concede(), Times.Once());
            Assert.AreEqual(GameStatus.Lost, game.CurrentRecord.GameResult);
            playerContext.Verify(m => m.SaveCurrentPlayerRecord(game.CurrentRecord), Times.Once());
            playerContext.VerifyNoOtherCalls();
        }

        [Test]
        public void TestConcedingGameAfterGameIsWon()
        {
            actualGame.Setup(m => m.Status).Returns(GameStatus.Won);
            game.MakeGuess('A');
            game.Concede();
            actualGame.Verify(m => m.Concede(), Times.Once());
            playerContext.Verify(m => m.SaveCurrentPlayerRecord(game.CurrentRecord), Times.Once());
            playerContext.VerifyNoOtherCalls();
        }
    }
}
