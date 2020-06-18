using System;

namespace SillyButtons.Abstractions
{
    public interface IGameView
    {
        event EventHandler StartGame;
        event EventHandler<char> GuessMade;
        event EventHandler Closed;
        void BlockButton(char letter);
        void SetScoreWord(string word);
        void ShowHangman(int remainingGuesses);
        void EnableCharacters(bool enabled);
        void SetStatusMessage(string message);
    }
}
