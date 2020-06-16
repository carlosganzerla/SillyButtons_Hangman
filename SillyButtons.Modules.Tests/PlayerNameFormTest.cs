using NUnit.Framework;
using SillyButtons.Views;

namespace SillyButtons.Modules.Tests
{
    public class PlayerNameFormTest
    {
        public PlayerNameForm form;

        [SetUp]
        public void Setup()
        {
            form = new PlayerNameForm();
            form.Show();
            form.Visible = true;
        }

        [Test]
        public void TestEnterNameFromText()
        {
            form.userNameList.Text = "name";
            Assert.AreEqual("name", form.UserName);
            Assert.IsTrue(form.startButton.Enabled);
        }

        [Test]
        public void TestEnterNameFromList()
        {
            form.userNameList.Items.AddRange(new[] { "first", "second", "third" });
            form.userNameList.SelectedItem = "second";
            Assert.AreEqual("second", form.UserName);
            Assert.IsTrue(form.startButton.Enabled);
        }

        [Test]
        public void TestSetNames()
        {
            var names = new[] { "first", "second", "third" };
            form.SetNames(names);
            for (int i = 0; i < names.Length; i++)
            {
                Assert.AreEqual(names[i], form.userNameList.Items[i]);
            }
        }

        [Test]
        public void TestSetNames_NoAccumulation()
        {
            var names = new[] { "first", "second", "third" };
            form.SetNames(names);
            var otherNames = new[] { "fourth", "fifth", "sixth", "seventh" };
            form.SetNames(otherNames);
            for (int i = 0; i < otherNames.Length; i++)
            {
                Assert.AreEqual(otherNames[i], form.userNameList.Items[i]);
            }
        }


        [Test]
        public void TestDontEnterAnything()
        {
            Assert.IsFalse(form.startButton.Enabled);
        }

        [Test]
        public void TestEnterNameAndThenDelete()
        {
            form.userNameList.Text = "name";
            Assert.IsTrue(form.startButton.Enabled);
            form.userNameList.Text = "";
            Assert.IsFalse(form.startButton.Enabled);
        }

        [TearDown]
        public void Teardown()
        {
            form.Close();
        }
    }
}
