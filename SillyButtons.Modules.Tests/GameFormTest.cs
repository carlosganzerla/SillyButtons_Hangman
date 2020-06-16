using NUnit.Framework;
using SillyButtons.Hangman;
using SillyButtons.Properties;
using SillyButtons.Views;
using System.Drawing;
using System.IO;
using System.Text;

namespace SillyButtons.Modules.Tests
{
    public class GameFormTest
    {
        private GameForm form;

        [SetUp]
        public void Setup()
        {
            bool gameStarted = false;
            form = new GameForm();
            form.StartGame += (s, e) => gameStarted = true;
            form.Show();
            Assert.IsTrue(gameStarted);
        }

        [Test]
        public void TestAlphabetButtons()
        {
            form.EnableCharacters(true);
            StringBuilder guessesMade = new StringBuilder();
            form.GuessMade += (s, e) => guessesMade.Append(e);
            Assert.AreEqual(Constants.Alphabet.Length, form.charButtonsPanel.Controls.Count);
            for (int i = 0; i < form.charButtonsPanel.Controls.Count; i++)
            {
                var button = form.charButtonsPanel.Controls[i] as System.Windows.Forms.Button;
                Assert.IsNotNull(button);
                button.PerformClick();
                Assert.AreEqual(Constants.Alphabet[i], button.Tag);
                Assert.AreEqual(Constants.Alphabet[i].ToString(), button.Text);
            }
            Assert.AreEqual(Constants.Alphabet, guessesMade.ToString());
        }

        [Test]
        public void TestRestartButton_CharacterButtonsEnabled()
        {
            form.StartGame += (s, e) => Assert.Fail();
            form.EnableCharacters(true);
            form.restartButton.PerformClick();
            Assert.Pass();
        }

        [Test]
        public void TestRestartButton_CharacterButtonsDisabled()
        {
            form.StartGame += (s, e) => Assert.Pass();
            form.EnableCharacters(false);
            form.restartButton.PerformClick();
            Assert.Fail();
        }


        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void TestGameOver(bool enabled)
        {
            form.EnableCharacters(enabled);
            for (int i = 0; i < form.charButtonsPanel.Controls.Count; i++)
            {
                Assert.AreEqual(enabled, form.charButtonsPanel.Controls[i].Enabled);
            }
            Assert.AreEqual(!enabled, form.restartButton.Enabled);
        }

        [Test]
        public void TestSetScoreWord()
        {
            form.SetScoreWord("W RD");
            Assert.AreEqual(4, form.scoreCharsPanel.Controls.Count);
            Assert.AreEqual("W", form.scoreCharsPanel.Controls[0].Text);
            Assert.AreEqual("_", form.scoreCharsPanel.Controls[1].Text);
            Assert.AreEqual("R", form.scoreCharsPanel.Controls[2].Text);
            Assert.AreEqual("D", form.scoreCharsPanel.Controls[3].Text);
        }

        [Test]
        public void TestResetScoreWord()
        {
            form.SetScoreWord("W RD");
            form.SetScoreWord("HI");
            Assert.AreEqual(2, form.scoreCharsPanel.Controls.Count);
            Assert.AreEqual("H", form.scoreCharsPanel.Controls[0].Text);
            Assert.AreEqual("I", form.scoreCharsPanel.Controls[1].Text);
        }

        [Test]
        public void TestSetStatusMessage()
        {
            form.SetStatusMessage("message");
            Assert.AreEqual("message", form.gameMessage.Text);
        }

        [Test]
        public void TestShowHangman_SixGuessesRemaining()
        {
            form.ShowHangman(6);
            Assert.IsTrue(Resources.hangman_remaining_6.BitmapEquals((Bitmap)form.hangmanImage.Image));
        }

        [Test]
        public void TestShowHangman_FiveGuessesRemaining()
        {
            form.ShowHangman(5);
            Assert.IsTrue(Resources.hangman_remaining_5.BitmapEquals((Bitmap)form.hangmanImage.Image));
        }

        [Test]
        public void TestShowHangman_FourGuessesRemaining()
        {
            form.ShowHangman(4);
            Assert.IsTrue(Resources.hangman_remaining_4.BitmapEquals((Bitmap)form.hangmanImage.Image));
        }

        [Test]
        public void TestShowHangman_ThreeGuessesRemaining()
        {
            form.ShowHangman(3);
            Assert.IsTrue(Resources.hangman_remaining_3.BitmapEquals((Bitmap)form.hangmanImage.Image));
        }

        [Test]
        public void TestShowHangman_TwoGuessesRemaining()
        {
            form.ShowHangman(2);
            Assert.IsTrue(Resources.hangman_remaining_2.BitmapEquals((Bitmap)form.hangmanImage.Image));
        }

        [Test]
        public void TestShowHangman_OneGuessesRemaining()
        {
            form.ShowHangman(1);
            Assert.IsTrue(Resources.hangman_remaining_1.BitmapEquals((Bitmap)form.hangmanImage.Image));
        }

        [Test]
        public void TestShowHangman_ZeroGuessesRemaining()
        {
            form.ShowHangman(0);
            Assert.IsTrue(Resources.hangman_remaining_0.BitmapEquals((Bitmap)form.hangmanImage.Image));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(-2)]
        [TestCase(7)]
        [TestCase(10)]
        public void TestShowHangman_InvalidNumberOfGuessesRemaining(int remaining)
        {
            Assert.Throws<FileNotFoundException>(() => form.ShowHangman(remaining));
        }

        [Test]
        public void TestBlockButton()
        {
            form.EnableCharacters(true);
            form.BlockButton('A');
            Assert.IsFalse(form.charButtonsPanel.Controls[0].Enabled);
            form.BlockButton('B');
            Assert.IsFalse(form.charButtonsPanel.Controls[1].Enabled);
            form.BlockButton('c');
            Assert.IsTrue(form.charButtonsPanel.Controls[2].Enabled);
        }
    }
}
