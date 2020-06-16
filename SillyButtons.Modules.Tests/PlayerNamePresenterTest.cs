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
        private Mock<IPlayerStore> storeMock;
        private static readonly string[] players = new[] { "first", "second", "third" };

        [SetUp]
        public void Setup()
        {
            loaderMock = new Mock<IViewLoader>();
            viewMock = new Mock<ILoginView>();
            storeMock = new Mock<IPlayerStore>();
            storeMock.Setup(m => m.GetPlayerList()).Returns(players);
            presenter = new PlayerNamePresenter(viewMock.Object, storeMock.Object, loaderMock.Object);
        }

        [Test]
        public void TestStartGame()
        {
            viewMock.Setup(m => m.UserName).Returns("name");
            viewMock.Raise(m => m.StartGame += null, null, null);
            storeMock.Verify(m => m.StorePlayer("name"), Times.Once());
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
