using Moq;
using NUnit.Framework;
using SillyButtons.Abstractions;
using SillyButtons.Hangman;
using SillyButtons.Views;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SillyButtons.Modules.Tests
{
    public class ViewLoaderTest
    {
        private ViewLoader loader;
        private Mock<IWordGenerator> generatorMock;
        private Mock<IHangmanGame> gameMock;
        private Mock<IPlayerContext> contextMock;

        [SetUp]
        public void Setup()
        {
            generatorMock = new Mock<IWordGenerator>();
            gameMock = new Mock<IHangmanGame>();
            contextMock = new Mock<IPlayerContext>();
            loader = new ViewLoader(gameMock.Object, generatorMock.Object, contextMock.Object);
        }

        [Test]
        public void TestGameRecordViewLoad()
        {
            var record = new GameRecord() { SecretWord = "WORD " };
            contextMock.Setup(m => m.GetCurrentPlayerRecords()).Returns(new[] { record });
            contextMock.Setup(m => m.CurrentPlayer).Returns("name");
            loader.LoadGameRecordView();
            var window = loader.LastLoadedForm;
            Assert.AreEqual(AppStrings.GameRecordWindowTitle("name"), window.Text);
            Assert.AreEqual(AppStrings.GameRecordString(record), window.Controls.OfType<Label>().First().Text);
        }

        [Test]
        public void TestGameViewLoad()
        {
            gameMock.Setup(m => m.DisplayWord).Returns("   ");
            generatorMock.Setup(m => m.GenerateWord()).Returns("WORD");
            loader.LoadGameView();
            gameMock.Verify(m => m.SetSecretWord("WORD"));
        }

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
    }
}
