using SillyButtons.Game;
using SillyButtons.Presenters;
using SillyButtons.Stores;
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
            var loginView = new LoginForm();
            var store = new FilePlayerStore("players.store");
            var loader = new ViewLoader(new HangmanGame(), new RandomWordApiGenerator(Constants.RandomWordApiAddress));
            var loginPresenter = new LoginPresenter(loginView, store, loader);
            Application.Run(loginView);
        }
    }
}
