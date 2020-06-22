using SillyButtons.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace SillyButtons.Hangman
{
    public class PlayerContext : IPlayerContext
    {
        private string playerName;
        private static readonly string recordFilesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppConstants.GameRecordsRelativePath);

        public string RecordFilePath
        {
            get
            {
                return Path.Combine(recordFilesPath, $"{playerName}.hgr");
            }
        }

        public string CurrentPlayer => playerName;

        public void SaveCurrentPlayerRecord(GameRecord record)
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
            string recordFileText = File.ReadAllText(RecordFilePath);
            return JsonSerializer.Deserialize<List<GameRecord>>(recordFileText);
        }

        private void SaveRecordsToFile(List<GameRecord> records)
        {
            File.WriteAllText(RecordFilePath, JsonSerializer.Serialize(records));
        }

        public void SetCurrentPlayer(string playerName)
        {
            this.playerName = playerName;
            CreateFileIfNotExists();
        }

        private void CreateFileIfNotExists()
        {
            if (!File.Exists(RecordFilePath))
            {
                Directory.CreateDirectory(recordFilesPath);
                SaveRecordsToFile(new List<GameRecord>());
            }
        }

        public IEnumerable<string> GetAllPlayers()
        {
            if (Directory.Exists(recordFilesPath))
            {
                return Directory.GetFiles(recordFilesPath, "*.hgr")
                    .Select(x => Path.GetFileNameWithoutExtension(x));
            }
            else
            {
                return Array.Empty<string>();
            }
        }

        public IEnumerable<GameRecord> GetCurrentPlayerRecords()
        {
            AssertPlayerName();
            return ReadRecordFile();
        }
    }
}
