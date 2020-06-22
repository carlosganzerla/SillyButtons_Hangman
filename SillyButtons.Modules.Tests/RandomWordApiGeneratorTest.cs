using NUnit.Framework;
using SillyButtons.Hangman;
using System.Linq;

namespace SillyButtons.Modules.Tests
{
    public class RandomWordApiGeneratorTest
    {
        private RandomWordApiGenerator generator;

        [SetUp]
        public void Setup()
        {
            generator = new RandomWordApiGenerator(AppConstants.RandomWordApiAddress);
        }

        [Test]
        public void TestRandomWordGenerator()
        {
            var word = generator.GenerateWord();
            Assert.IsTrue(word.All(c => char.IsLetter(c) && char.IsUpper(c)));
        }
    }
}
