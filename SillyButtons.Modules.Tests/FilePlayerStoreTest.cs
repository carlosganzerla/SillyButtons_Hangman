using NUnit.Framework;
using SillyButtons.Hangman.Stores;
using System;
using System.IO;
using System.Linq;

namespace SillyButtons.Modules.Tests
{
    public class FilePlayerStoreTest
    {
        private FilePlayerStore store;
        private static readonly string filePath = Path.Combine(Directory.GetCurrentDirectory(), "players.store");

        [SetUp]
        public void Setup()
        {
            store = new FilePlayerStore(filePath);
        }

        private void CreateStoreFile()
        {
            File.WriteAllText(filePath, "player1\r\nplayer2\r\nplayer3\r\n");
        }

        [Test]
        public void TestStoreNewPlayer_StoreFileDoesNotExist()
        {
            store.StorePlayer("name");
            FileAssert.Exists(filePath);
            Assert.AreEqual("name\r\n", File.ReadAllText(filePath));
        }

        [Test]

        public void TestStoreNewPlayer_StoreFileAlreadyExists()
        {
            CreateStoreFile();
            store.StorePlayer("name");
            FileAssert.Exists(filePath);
            Assert.AreEqual("player1\r\nplayer2\r\nplayer3\r\nname\r\n", File.ReadAllText(filePath));
        }

        [Test]
        public void TestStoreDuplicatePlayer()
        {
            store.StorePlayer("name");
            store.StorePlayer("name");
            store.StorePlayer("name");
            store.StorePlayer("name");
            FileAssert.Exists(filePath);
            Assert.AreEqual("name\r\n", File.ReadAllText(filePath));
        }
        [Test]

        public void TestGetPlayerNames_StoreFileDoesNotExist()
        {
            var names = store.GetPlayerList();
            Assert.AreEqual(0, names.Count());
            FileAssert.Exists(filePath);
        }
        [Test]

        public void TestGetPlayerNames_StoreFileAlreadyExists()
        {
            CreateStoreFile();
            var names = store.GetPlayerList();
            Assert.AreEqual(3, names.Count());
            Assert.AreEqual("player1", names.ElementAt(0));
            Assert.AreEqual("player2", names.ElementAt(1));
            Assert.AreEqual("player3", names.ElementAt(2));

        }

        [TearDown]
        public void Teardown()
        {
            GC.WaitForPendingFinalizers();
            GC.Collect();
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

    }
}
