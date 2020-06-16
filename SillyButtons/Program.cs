using SillyButtons.Hangman;
using SillyButtons.Hangman.Stores;
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
            var store = new FilePlayerStore("players.store");
            var loader = new ViewLoader(new HangmanGame(), new RandomWordApiGenerator(Constants.RandomWordApiAddress));
            var loginPresenter = new PlayerNamePresenter(loginView, store, loader);
            Application.Run(loginView);
        }
    }
}
