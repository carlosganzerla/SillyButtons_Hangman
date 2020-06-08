using Moq;
using NUnit.Framework;
using SillyButtons.Game;
using SillyButtons.Interfaces;
using SillyButtons.Presenters;

namespace SillyButtons.Modules.Tests
{
    public class GamePresenterTest
    {
        private GamePresenter presenter;
        private Mock<IGameView> viewMock;
        private Mock<IHangmanGame> gameMock;
        private Mock<IWordGenerator> generatorMock;

        [SetUp]
        public void Setup()
        {
            viewMock = new Mock<IGameView>();
            gameMock = new Mock<IHangmanGame>();
            generatorMock = new Mock<IWordGenerator>();
            presenter = new GamePresenter(viewMock.Object, gameMock.Object, generatorMock.Object);
        }

        [Test]
        public void TestGameStart()
        {
            generatorMock.Setup(m => m.GenerateWord()).Returns("word");
            gameMock.Setup(m => m.Status).Returns(GameStatus.Playing);
            gameMock.Setup(m => m.DisplayWord).Returns("    ");
            gameMock.Setup(m => m.RemainingGuesses).Returns(6);
            gameMock.Setup(m => m.GuessedCharacters).Returns("");
            viewMock.Raise(m => m.StartGame += null, null, null);
            gameMock.Verify(m => m.SetSecretWord("WORD"));
            viewMock.Verify(m => m.SetScoreWord("    "));
            viewMock.Verify(m => m.SetStatusMessage(string.Format(Constants.GamePlayingMessage, gameMock.Object.GuessedCharacters,
                gameMock.Object.RemainingGuesses)));
        }

        [Test]
        public void TestGuessMadeAndGameContinues()
        {
            string guessesMade = "WA";
            int remainingGuesses = 5;
            gameMock.Setup(m => m.GuessedCharacters).Returns("WA");
            gameMock.Setup(m => m.Status).Returns(GameStatus.Playing);
            gameMock.Setup(m => m.RemainingGuesses).Returns(remainingGuesses);
            gameMock.Setup(m => m.DisplayWord).Returns("W   ");
            viewMock.Raise(m => m.GuessMade += null, null, 'A');
            viewMock.Verify(m => m.SetStatusMessage(string.Format(Constants.GamePlayingMessage, guessesMade, remainingGuesses)));
            gameMock.Verify(m => m.MakeGuess('A'));
            viewMock.Verify(m => m.BlockButton('A'));
            viewMock.Verify(m => m.SetScoreWord("W   "));
            viewMock.Verify(m => m.ShowHangman(remainingGuesses));
            viewMock.Verify(m => m.EnableCharacters(It.IsAny<bool>()), Times.Never);
        }

        [Test]
        public void TestGuessMadeAndGameIsLost()
        {
            gameMock.Setup(m => m.Status).Returns(GameStatus.Lost);
            gameMock.Setup(m => m.RemainingGuesses).Returns(0);
            gameMock.Setup(m => m.DisplayWord).Returns("W   ");
            viewMock.Raise(m => m.GuessMade += null, null, 'A');
            viewMock.Verify(m => m.SetStatusMessage(Constants.GameLostMessage));
            gameMock.Verify(m => m.MakeGuess('A'));
            viewMock.Verify(m => m.BlockButton('A'));
            viewMock.Verify(m => m.SetScoreWord("W   "));
            viewMock.Verify(m => m.ShowHangman(0));
            viewMock.Verify(m => m.EnableCharacters(false), Times.Once);
        }

        [Test]
        public void TestGuessMadeAndGameIsWon()
        {
            gameMock.Setup(m => m.Status).Returns(GameStatus.Won);
            gameMock.Setup(m => m.RemainingGuesses).Returns(2);
            gameMock.Setup(m => m.DisplayWord).Returns("WORD");
            viewMock.Raise(m => m.GuessMade += null, null, 'A');
            viewMock.Verify(m => m.SetStatusMessage(Constants.GameWonMessage));
            gameMock.Verify(m => m.MakeGuess('A'));
            viewMock.Verify(m => m.BlockButton('A'));
            viewMock.Verify(m => m.SetScoreWord("WORD"));
            viewMock.Verify(m => m.ShowHangman(2));
            viewMock.Verify(m => m.EnableCharacters(false), Times.Once);
        }
    }
}
