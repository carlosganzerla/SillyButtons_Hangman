using SillyButtons.Hangman;
using SillyButtons.Presenters;
using SillyButtons.Views;
using System;
using System.Windows.Forms;

namespace SillyButtons
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var loginView = new PlayerNameForm();
            var context = new PlayerContext();
            var actualGame = new HangmanGame();
            var recordableGame = new RecordableGame(actualGame, context);
            var loader = new ViewLoader(recordableGame, new RandomWordApiGenerator(AppConstants.RandomWordApiAddress), context);
            var loginPresenter = new PlayerNamePresenter(loginView, context, loader);
            Application.Run(loginView);
        }
    }
}
