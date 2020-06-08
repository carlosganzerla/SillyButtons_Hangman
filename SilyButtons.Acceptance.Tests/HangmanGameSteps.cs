using Moq;
using NUnit.Framework;
using SillyButtons;
using SillyButtons.Game;
using SillyButtons.Interfaces;
using SillyButtons.Modules.Tests;
using SillyButtons.Presenters;
using SillyButtons.Properties;
using SillyButtons.Views;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TechTalk.SpecFlow;

namespace SilyButtons.Acceptance.Tests
{
    [Binding]
    public class HangmanGameSteps
    {
        private HangmanGame game;
        private GameForm form;
        private GamePresenter presenter;
        private Mock<IWordGenerator> generator;

        public HangmanGameSteps()
        {
            generator = new Mock<IWordGenerator>();
            game = new HangmanGame();
            form = new GameForm();
            presenter = new GamePresenter(form, game, generator.Object);
        }
        [Given(@"The word is ""(.*)""")]
        public void GivenTheWordIs(string p0)
        {
            generator.Setup(m => m.GenerateWord()).Returns(p0);
        }


        [Given(@"the game has started")]
        public void GivenTheGameHasStarted()
        {
            form.Show();
        }

        [When(@"I guess the character '(.*)'")]
        public void WhenIGuessTheCharacter(string p0)
        {
            WhenIGuessTheCharacters(p0);
        }

        [When(@"I guess the characters ""(.*)""")]
        public void WhenIGuessTheCharacters(string p0)
        {
            for (int i = 0; i < p0.Length; i++)
            {
                ClickCharacterButton(p0[i]);
            }
        }

        [When(@"I restart the game")]
        public void WhenIRestartTheGame()
        {
            form.restartButton.PerformClick();
        }


        [Then(@"(.*) appearances of the character '(.*)' are shown")]
        public void ThenAppearancesOfTheCharacterAreShown(int p0, string p1)
        {
            Assert.AreEqual(p0, GetDisplayedCharacters().Count(x => p1[0] == x));
        }

        [Then(@"the character '(.*)' is blocked")]
        public void ThenTheCharacterIsBlocked(string p0)
        {
            Assert.IsFalse(form.charButtonsPanel.GetCharacterButton(p0[0]).Enabled);
        }

        [Then(@"I have (.*) guesses left")]
        public void ThenIHaveGuessesLeft(int p0)
        {
            Assert.AreEqual(p0, game.RemainingGuesses);
            Assert.IsTrue(GuiHelper.GetHangmanImage(p0).BitmapEquals((Bitmap)form.hangmanImage.Image));
        }


        [Then(@"the game is lost")]
        public void ThenTheGameIsLost()
        {
            Assert.AreEqual(GameStatus.Lost, game.Status);
            Assert.AreEqual(Constants.GameLostMessage, form.gameMessage.Text);
        }

        [Then(@"all characters are blocked")]
        public void ThenAllCharactersAreBlocked()
        {
            foreach (Button b in form.charButtonsPanel.Controls)
            {
                Assert.IsFalse(b.Enabled);
            }
        }

        [Then(@"the characters ""(.*)"" are counted as wrong guesses")]
        public void ThenTheCharactersAreCountedAsWrongGuesses(string p0)
        {
            for (int i = 0; i < p0.Length; i++)
            {
                Assert.IsTrue(game.GuessedCharacters.Contains(p0[i]));
                Assert.IsFalse(game.DisplayWord.Contains(p0[i]));
            }
        }

        [Then(@"the charcters ""(.*)"" are not counted as guesses")]
        public void ThenTheCharctersAreNotCountedAsGuesses(string p0)
        {
            Assert.IsFalse(game.GuessedCharacters.Contains(p0));
        }

        [Then(@"the game is won")]
        public void ThenTheGameIsWon()
        {
            Assert.AreEqual(GameStatus.Won, game.Status);
            Assert.AreEqual(Constants.GameWonMessage, form.gameMessage.Text);
        }

        [Then(@"the game is restarted")]
        public void ThenTheGameIsRestarted()
        {
            foreach (Button b in form.charButtonsPanel.Controls)
            {
                Assert.IsTrue(b.Enabled);
            }
            Assert.IsFalse(form.restartButton.Enabled);
            Assert.AreEqual(Constants.MaximumGuesses, game.RemainingGuesses);
            Assert.AreEqual("", game.GuessedCharacters);
            Assert.AreEqual(GameStatus.Playing, game.Status);
            Assert.AreEqual("", game.DisplayWord.Trim());
            Assert.IsTrue(Resources.hangman_remaining_6.BitmapEquals((Bitmap)form.hangmanImage.Image));
            Assert.AreEqual(string.Format(Constants.GamePlayingMessage, "", Constants.MaximumGuesses), form.gameMessage.Text);
        }

        private string GetDisplayedCharacters()
        {
            return string.Join("", form.scoreCharsPanel.Controls.OfType<Label>().Select(x => x.Text));
        }

        private void ClickCharacterButton(char character)
        {
            form.charButtonsPanel.GetCharacterButton(character).PerformClick();
        }

    }
}
