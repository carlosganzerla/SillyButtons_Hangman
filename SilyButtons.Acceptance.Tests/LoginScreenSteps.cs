using Moq;
using NUnit.Framework;
using SillyButtons.Interfaces;
using SillyButtons.Presenters;
using SillyButtons.Stores;
using SillyButtons.Views;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using TechTalk.SpecFlow;

namespace SilyButtons.Acceptance.Tests
{
    [Binding]
    public class LoginScreenSteps
    {
        private readonly LoginForm view;
        private readonly FilePlayerStore store;
        private LoginPresenter presenter;
        private readonly Mock<IViewLoader> loader;

        private IList listItems;
        private static readonly string filePath = Path.Combine(Directory.GetCurrentDirectory(), "players.store");

        private void CreateStoreFile(params string[] names)
        {
            File.WriteAllText(filePath, string.Join("\r\n", names) + "\r\n");
        }


        public LoginScreenSteps()
        {
            loader = new Mock<IViewLoader>();
            view = new LoginForm();
            store = new FilePlayerStore(filePath);
            presenter = new LoginPresenter(view, store, loader.Object);
        }

        [Given(@"I am on the login screen")]
        public void GivenIAmOnTheLoginScreen()
        {
            view.Show();
        }

        [Given(@"I ""(.*)"" have never played the game")]
        public void GivenIHaveNeverPlayedTheGame(string p0)
        {
            CreateStoreFile("another name", "joey", "kramk");
            presenter.RefreshNames();
        }

        [Given(@"I ""(.*)"" have already played the game")]
        public void GivenIHaveAlreadyPlayedTheGame(string p0)
        {
            CreateStoreFile("another name", "joey", p0, "kramk");
            presenter.RefreshNames();
        }

        [Given(@"start button is enabled")]
        public void GivenStartButtonIsEnabled()
        {
            view.startButton.Enabled = true;
        }

        [When(@"I enter ""(.*)"" in the name field")]
        public void WhenIEnterInTheNameField(string p0)
        {
            view.userNameList.Text = p0;
        }

        [When(@"I check the existing names list")]
        public void WhenICheckTheExistingNamesList()
        {
            listItems = view.userNameList.Items;
        }

        [When(@"I select ""(.*)"" from the list")]
        public void WhenISelectFromTheList(string p0)
        {
            view.userNameList.SelectedItem = p0;
        }

        [When(@"I enter nothing")]
        public void WhenIEnterNothing()
        {
        }

        [When(@"I delete the name which I entered")]
        public void WhenIDeleteTheNameWhichIEntered()
        {
            view.userNameList.Text = "";
        }

        [When(@"I press start button")]
        public void WhenIPressStartButton()
        {
            view.startButton.PerformClick();
        }

        [Then(@"start button is enabled")]
        public void ThenStartButtonIsEnabled()
        {
            Assert.IsTrue(view.startButton.Enabled);
        }

        [Then(@"""(.*)"" should not be on the list")]
        public void ThenShouldNotBeOnTheList(string p0)
        {
            Assert.IsTrue(listItems.Count > 0);
            for (int i = 0; i < listItems.Count; i++)
            {
                Assert.AreNotEqual(p0, listItems[i]);
            }
        }

        [Then(@"""(.*)"" should be on the list")]
        public void ThenShouldBeOnTheList(string p0)
        {
            Assert.IsTrue(listItems.Count > 0);
            Assert.AreEqual(p0, listItems[2]);
        }

        [Then(@"start button is disabled")]
        public void ThenStartButtonIsDisabled()
        {
            Assert.IsFalse(view.startButton.Enabled);
        }

        [Then(@"a new game starts")]
        public void ThenANewGameStarts()
        {
            loader.Verify(m => m.LoadGameView(), Times.Once());
            loader.VerifyNoOtherCalls();
        }

        [Then(@"""(.*)"" is stored")]
        public void ThenIsStored(string p0)
        {
            var players = store.GetPlayerList();
            Assert.AreEqual(1, players.Count());
            Assert.AreEqual(p0, players.ElementAt(0));
        }


        [AfterScenario]
        public static void AfterScenario()
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
