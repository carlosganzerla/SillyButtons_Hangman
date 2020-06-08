using SillyButtons.Interfaces;
using System;

namespace SillyButtons.Presenters
{
    public class LoginPresenter
    {
        private readonly ILoginView view;
        private readonly IPlayerStore playerStore;
        private readonly IViewLoader loader;

        public LoginPresenter(ILoginView view, IPlayerStore playerStore, IViewLoader loader)
        {
            this.view = view;
            this.playerStore = playerStore;
            this.loader = loader;
            RefreshNames();
            view.StartGame += OnStartGame;
        }


        public void RefreshNames()
        {
            view.SetNames(playerStore.GetPlayerList());
        }

        private void OnStartGame(object sender, EventArgs e)
        {
            playerStore.StorePlayer(view.UserName);
            loader.LoadGameView();
        }
    }
}
