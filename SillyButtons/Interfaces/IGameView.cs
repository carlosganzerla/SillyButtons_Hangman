using System;

namespace SillyButtons.Interfaces
{
    public interface IGameView
    {
        event EventHandler StartGame;
        event EventHandler<char> GuessMade;
        void BlockButton(char letter);
        void SetScoreWord(string word);
        void ShowHangman(int remainingGuesses);
        void EnableCharacters(bool enabled);
        void SetStatusMessage(string message);
    }
}
