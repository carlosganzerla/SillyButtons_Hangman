using SillyButtons.Abstractions;
using System;

namespace SillyButtons.Presenters
{
    public class PlayerNamePresenter
    {
        private readonly ILoginView view;
        private readonly IPlayerContext context;
        private readonly IViewLoader loader;

        public PlayerNamePresenter(ILoginView view, IPlayerContext context, IViewLoader loader)
        {
            this.view = view;
            this.context = context;
            this.loader = loader;
            RefreshNames();
            view.StartGame += OnStartGame;
        }

        public void RefreshNames()
        {
            view.SetNames(context.GetPlayerList());
        }

        private void OnStartGame(object sender, EventArgs e)
        {
            context.SetPlayerName(view.UserName);
            loader.LoadGameView();
        }
    }
}
