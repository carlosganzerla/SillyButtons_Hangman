﻿namespace SillyButtons.Abstractions
{
    public interface IHangmanGame
    {
        string DisplayWord { get; }
        string GuessedCharacters { get; }
        GameStatus Status { get; }
        int RemainingGuesses { get; }
        void MakeGuess(char guess);
        void SetSecretWord(string word);
        void Concede();
    }
}
