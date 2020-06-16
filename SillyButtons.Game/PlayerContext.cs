using SillyButtons.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace SillyButtons.Hangman
{
    public class PlayerContext : IPlayerContext
    {
        private string playerName;
        private static readonly string recordFilesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Constants.GameRecordsRelativePath);

        private string RecordFilePath
        {
            get
            {
                return Path.Combine(recordFilesPath, $"{playerName}.hgr");
            }
        }
        public void SaveGameRecord(GameRecord record)
        {
            AssertPlayerName();
            var records = ReadRecordFile();
            records.Add(record);
            SaveRecordsToFile(records);
        }

        private void AssertPlayerName()
        {
            if (string.IsNullOrEmpty(playerName))
            {
                throw new InvalidOperationException("Cannot save a game record without setting the current player name");
            }
        }

        private List<GameRecord> ReadRecordFile()
        {
            CreateFileIfNotExists();
            string recordFileText = File.ReadAllText(RecordFilePath);
            return JsonSerializer.Deserialize<List<GameRecord>>(recordFileText);
        }

        private void CreateFileIfNotExists()
        {
            if (!File.Exists(RecordFilePath))
            {
                Directory.CreateDirectory(recordFilesPath);
                SaveRecordsToFile(new List<GameRecord>());
            }
        }

        private void SaveRecordsToFile(List<GameRecord> records)
        {
            File.WriteAllText(RecordFilePath, JsonSerializer.Serialize(records));
        }

        public void SetPlayerName(string playerName)
        {
            this.playerName = playerName;
        }
    }
}
