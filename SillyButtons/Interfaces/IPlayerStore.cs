using System.Collections.Generic;

namespace SillyButtons.Interfaces
{
    public interface IPlayerStore
    {
        IEnumerable<string> GetPlayerList();
        void StorePlayer(string name);
    }
}