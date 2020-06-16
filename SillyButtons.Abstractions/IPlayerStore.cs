using System.Collections.Generic;

namespace SillyButtons.Abstractions
{
    public interface IPlayerStore
    {
        IEnumerable<string> GetPlayerList();
        void StorePlayer(string name);
    }
}