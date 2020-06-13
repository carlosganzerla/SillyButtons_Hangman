using NUnit.Framework;
using SillyButtons.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SillyButtons.Modules.Tests
{
    public class RandomWordApiGeneratorTest
    {
        private RandomWordApiGenerator generator;

        [SetUp]
        public void Setup()
        {
            generator = new RandomWordApiGenerator(Constants.RandomWordApiAddress);
        }

        [Test]
        public void TestRandomWordGenerator()
        {
            var word = generator.GenerateWord();
            Assert.IsTrue(word.All(c => char.IsLetter(c) && char.IsUpper(c)));
        }
    }
}
