using SillyButtons.Abstractions;
using SillyButtons.Presenters;

namespace SillyButtons.Views
{
    public class ViewLoader : IViewLoader
    {
        private readonly IHangmanGame game;
        private readonly IWordGenerator generator;
        public ViewLoader(IHangmanGame game, IWordGenerator generator)
        {
            this.game = game;
            this.generator = generator;
        }

        public void LoadGameView()
        {
            var form = new GameForm();
            var presenter = new GamePresenter(form, game, generator);
            form.Show();
        }
    }
}
