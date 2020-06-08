﻿using NUnit.Framework;
using SillyButtons.Game;

namespace SillyButtons.Modules.Tests
{
    public class HangmanGameTest
    {
        private HangmanGame game;

        [SetUp]
        public void Setup()
        {
            game = new HangmanGame();
        }

        [Test]
        public void TestCorrectGuess()
        {
            game.SetSecretWord("WORD");
            game.MakeGuess('W');
            Assert.AreEqual(Constants.MaximumGuesses, game.RemainingGuesses);
            Assert.AreEqual("W", game.GuessedCharacters);
            Assert.AreEqual("W   ", game.DisplayWord);
            Assert.AreEqual(GameStatus.Playing, game.Status);

        }

        [Test]
        public void TestIncorrectGuess()
        {
            game.SetSecretWord("WORD");
            game.MakeGuess('A');
            Assert.AreEqual(Constants.MaximumGuesses - 1, game.RemainingGuesses);
            Assert.AreEqual("A", game.GuessedCharacters);
            Assert.AreEqual("    ", game.DisplayWord);
            Assert.AreEqual(GameStatus.Playing, game.Status);
        }

        [Test]
        public void TestDuplicateWrongGuess()
        {
            game.SetSecretWord("WORD");
            game.MakeGuess('A');
            game.MakeGuess('A');
            Assert.AreEqual(Constants.MaximumGuesses - 1, game.RemainingGuesses);
            Assert.AreEqual("A", game.GuessedCharacters);
            Assert.AreEqual("    ", game.DisplayWord);
            Assert.AreEqual(GameStatus.Playing, game.Status);
        }

        [Test]
        public void TestDuplicateCorrectGuess()
        {
            game.SetSecretWord("WORD");
            game.MakeGuess('R');
            game.MakeGuess('R');
            Assert.AreEqual(Constants.MaximumGuesses, game.RemainingGuesses);
            Assert.AreEqual("R", game.GuessedCharacters);
            Assert.AreEqual("  R ", game.DisplayWord);
            Assert.AreEqual(GameStatus.Playing, game.Status);
        }

        [Test]
        public void TestTwoGuessesOnWordWithDuplicateChars()
        {
            game.SetSecretWord("HANGMAN");
            game.MakeGuess('N');
            game.MakeGuess('A');
            Assert.AreEqual(Constants.MaximumGuesses, game.RemainingGuesses);
            Assert.AreEqual("NA", game.GuessedCharacters);
            Assert.AreEqual(" AN  AN", game.DisplayWord);
            Assert.AreEqual(GameStatus.Playing, game.Status);
        }

        [Test]
        public void TestResetSecretWord()
        {
            game.SetSecretWord("HANGMAN");
            game.MakeGuess('Z');
            game.SetSecretWord("WORD");
            game.MakeGuess('D');
            Assert.AreEqual(Constants.MaximumGuesses, game.RemainingGuesses);
            Assert.AreEqual("D", game.GuessedCharacters);
            Assert.AreEqual("   D", game.DisplayWord);
            Assert.AreEqual(GameStatus.Playing, game.Status);
        }

        [Test]
        public void TestLosingGame()
        {
            game.SetSecretWord("WORD");
            game.MakeGuess("ABCDEFGHIJ");
            Assert.AreEqual(0, game.RemainingGuesses);
            Assert.AreEqual("ABCDEFG", game.GuessedCharacters);
            Assert.AreEqual("   D", game.DisplayWord);
            Assert.AreEqual(GameStatus.Lost, game.Status);
        }

        [Test]
        public void TestWinningGame()
        {
            game.SetSecretWord("JOHNNY");
            game.MakeGuess("ABJOHNCDYIKLJMN");
            Assert.AreEqual(2, game.RemainingGuesses);
            Assert.AreEqual("ABJOHNCDY", game.GuessedCharacters);
            Assert.AreEqual("JOHNNY", game.DisplayWord);
            Assert.AreEqual(GameStatus.Won, game.Status);
        }


    }
}