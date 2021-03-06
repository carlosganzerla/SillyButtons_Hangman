﻿using SillyButtons.Abstractions;
using SillyButtons.Hangman;
using System;

namespace SillyButtons.Presenters
{
    public class GamePresenter
    {
        private readonly IGameView view;
        private readonly IHangmanGame game;
        private readonly IWordGenerator generator;

        public GamePresenter(IGameView view, IHangmanGame game, IWordGenerator generator)
        {
            this.view = view;
            this.game = game;
            this.generator = generator;
            view.GuessMade += OnGuessMade;
            view.StartGame += OnStartGame;
            view.Closed += OnGameClosed;
        }

        private void OnGameClosed(object sender, EventArgs e)
        {
            game.Concede();
        }

        private void OnStartGame(object sender, EventArgs e)
        {
            game.SetSecretWord(generator.GenerateWord().ToUpper());
            view.SetScoreWord(game.DisplayWord);
            view.ShowHangman(game.RemainingGuesses);
            view.EnableCharacters(true);
            SetGameViewStatus();
        }

        private void OnGuessMade(object sender, char e)
        {
            game.MakeGuess(e);
            view.SetScoreWord(game.DisplayWord);
            view.ShowHangman(game.RemainingGuesses);
            view.BlockButton(e);
            SetGameViewStatus();
        }

        private void SetGameViewStatus()
        {
            switch (game.Status)
            {
                case GameStatus.Lost:
                    view.EnableCharacters(false);
                    view.SetStatusMessage(AppStrings.GameLostMessage);
                    break;
                case GameStatus.Won:
                    view.EnableCharacters(false);
                    view.SetStatusMessage(AppStrings.GameWonMessage);
                    break;
                default:
                    view.SetStatusMessage(AppStrings.GamePlayingMessage(game));
                    break;
            }
        }
    }
}
