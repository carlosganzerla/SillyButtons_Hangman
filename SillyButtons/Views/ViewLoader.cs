using SillyButtons.Abstractions;
using SillyButtons.Hangman;
using SillyButtons.Presenters;
using System.Windows.Forms;

namespace SillyButtons.Views
{
    public class ViewLoader : IViewLoader
    {
        private readonly IHangmanGame game;
        private readonly IWordGenerator generator;
        private readonly IPlayerContext context;

        public Form LastLoadedForm { get; private set; }

        public ViewLoader(IHangmanGame game, IWordGenerator generator, IPlayerContext context)
        {
            this.game = game;
            this.generator = generator;
            this.context = context;
        }

        public void LoadGameRecordView()
        {
            var form = new GameRecordForm();
            LastLoadedForm = form;
            form.SetTitle(AppStrings.GameRecordWindowTitle(context.CurrentPlayer));
            form.ShowGameRecords(context.GetCurrentPlayerRecords());
            form.Show();
        }

        public void LoadGameView()
        {
            var form = new GameForm();
            LastLoadedForm = form;
            var presenter = new GamePresenter(form, game, generator);
            form.Show();
        }
    }
}
