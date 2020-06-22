using System;
using System.Collections.Generic;

namespace SillyButtons.Abstractions
{
    public interface IPlayerNameView
    {
        string UserName { get; }
        void SetNames(IEnumerable<string> names);
        event EventHandler StartGame;
        event EventHandler ViewRecord;
    }
}
