using SillyButtons.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SillyButtons.Hangman.Stores
{
    public class FilePlayerStore : IPlayerStore
    {
        private readonly string filePath;

        public FilePlayerStore(string filePath)
        {
            this.filePath = filePath;
        }

        public IEnumerable<string> GetPlayerList()
        {
            if (File.Exists(filePath))
            {
                return File.ReadAllLines(filePath);
            }
            else
            {
                File.WriteAllText(filePath, "");
                return Array.Empty<string>();
            }
        }

        public void StorePlayer(string name)
        {
            using (var fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            using (var sr = new StreamReader(fs))
            using (var sw = new StreamWriter(fs))
            {
                string[] names = sr.ReadToEnd().Split("\r\n");
                if (!names.Contains(name)) sw.WriteLine(name);
            }
        }
    }
}
