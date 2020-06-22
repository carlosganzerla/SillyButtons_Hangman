using System.Collections.Generic;

namespace SillyButtons.Abstractions
{
    public interface IGameRecordView
    {
        void SetTitle(string text);
        void ShowGameRecords(IEnumerable<GameRecord> records);
    }
}
