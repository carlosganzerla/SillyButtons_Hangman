namespace SillyButtons.Abstractions
{
    public interface IPlayerContext
    {
        void SetPlayerName(string playerName);
        void SaveGameRecord(GameRecord record);
    }
}
