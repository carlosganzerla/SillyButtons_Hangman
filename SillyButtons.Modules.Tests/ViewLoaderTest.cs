using Moq;
using NUnit.Framework;
using SillyButtons.Interfaces;
using SillyButtons.Views;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace SillyButtons.Modules.Tests
{
    public class ViewLoaderTest
    {
        private ViewLoader loader;
        private Mock<IWordGenerator> generatorMock;
        private Mock<IHangmanGame> gameMock;

        [SetUp]
        public void Setup()
        {
            generatorMock = new Mock<IWordGenerator>();
            gameMock = new Mock<IHangmanGame>();
            loader = new ViewLoader(gameMock.Object, generatorMock.Object);
        }

        [Test]
        public void TestGameViewLoad()
        {
            gameMock.Setup(m => m.DisplayWord).Returns("   ");
            generatorMock.Setup(m => m.GenerateWord()).Returns("WORD");
            Task.Delay(1000).ContinueWith((t) => CloseWindow());
            loader.LoadGameView();
            gameMock.Verify(m => m.SetSecretWord("WORD"));
        }

        private void CloseWindow()
        {
            var hwnd = GetForegroundWindow();
            SendMessage(hwnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        }

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        private const uint WM_CLOSE = 0x0010;


    }
}
