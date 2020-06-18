using Moq;
using NUnit.Framework;
using SillyButtons.Abstractions;
using SillyButtons.Presenters;

namespace SillyButtons.Modules.Tests
{
    public class PlayerNamePresenterTest
    {
        private PlayerNamePresenter presenter;
        private Mock<IViewLoader> loaderMock;
        private Mock<ILoginView> viewMock;
        private Mock<IPlayerContext> contextMock;
        private static readonly string[] players = new[] { "first", "second", "third" };

        [SetUp]
        public void Setup()
        {
            loaderMock = new Mock<IViewLoader>();
            viewMock = new Mock<ILoginView>();
            contextMock = new Mock<IPlayerContext>();
            contextMock.Setup(m => m.GetPlayerList()).Returns(players);
            presenter = new PlayerNamePresenter(viewMock.Object, contextMock.Object, loaderMock.Object);
        }

        [Test]
        public void TestStartGame()
        {
            viewMock.Setup(m => m.UserName).Returns("name");
            viewMock.Raise(m => m.StartGame += null, null, null);
            contextMock.Verify(m => m.SetPlayerName("name"), Times.Once());
            loaderMock.Verify(m => m.LoadGameView(), Times.Once());
            loaderMock.VerifyNoOtherCalls();
        }
        [Test]
        public void TestLoadNames()
        {
            viewMock.Verify(m => m.SetNames(players), Times.Once());
            viewMock.VerifyNoOtherCalls();
        }
    }
}
