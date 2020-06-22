using System.Collections.Generic;

namespace SillyButtons.Abstractions
{
    public interface IPlayerContext
    {
        string CurrentPlayer { get; }
        void SetCurrentPlayer(string playerName);
        void SaveCurrentPlayerRecord(GameRecord record);
        IEnumerable<GameRecord> GetCurrentPlayerRecords();
        IEnumerable<string> GetAllPlayers();

    }
}
