using SillyButtons.Abstractions;
using System;

namespace SillyButtons.Presenters
{
    public class PlayerNamePresenter
    {
        private readonly IPlayerNameView view;
        private readonly IPlayerContext context;
        private readonly IViewLoader loader;

        public PlayerNamePresenter(IPlayerNameView view, IPlayerContext context, IViewLoader loader)
        {
            this.view = view;
            this.context = context;
            this.loader = loader;
            RefreshNames();
            view.StartGame += OnStartGame;
            view.ViewRecord += OnViewRecord;
        }

        private void OnViewRecord(object sender, EventArgs e)
        {
            context.SetCurrentPlayer(view.UserName);
            loader.LoadGameRecordView();
        }

        public void RefreshNames()
        {
            view.SetNames(context.GetAllPlayers());
        }

        private void OnStartGame(object sender, EventArgs e)
        {
            context.SetCurrentPlayer(view.UserName);
            loader.LoadGameView();
        }
    }
}
