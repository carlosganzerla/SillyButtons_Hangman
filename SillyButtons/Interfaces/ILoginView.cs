using System;
using System.Collections.Generic;

namespace SillyButtons.Interfaces
{
    public interface ILoginView
    {
        string UserName { get; }
        void SetNames(IEnumerable<string> names);
        event EventHandler StartGame;
    }
}
