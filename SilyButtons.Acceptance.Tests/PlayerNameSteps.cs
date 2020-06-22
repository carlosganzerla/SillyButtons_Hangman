using Moq;
using NUnit.Framework;
using SillyButtons.Abstractions;
using SillyButtons.Hangman;
using SillyButtons.Presenters;
using SillyButtons.Views;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using TechTalk.SpecFlow;

namespace SilyButtons.Acceptance.Tests
{
    [Binding]
    public class PlayerNameSteps
    {
        private readonly PlayerNameForm view;
        private readonly PlayerContext context;
        private PlayerNamePresenter presenter;
        private readonly Mock<IViewLoader> loader;
        private IList listItems;
        private static readonly string recordsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppConstants.GameRecordsRelativePath);

        public PlayerNameSteps()
        {
            loader = new Mock<IViewLoader>();
            view = new PlayerNameForm();
            context = new PlayerContext();
            presenter = new PlayerNamePresenter(view, context, loader.Object);
        }

        [Given(@"I am on the login screen")]
        public void GivenIAmOnTheLoginScreen()
        {
            view.Show();
        }

        [Given(@"I ""(.*)"" have never played the game")]
        public void GivenIHaveNeverPlayedTheGame(string p0)
        {
            AddPlayers("another name", "joey", "kramk");
            presenter.RefreshNames();
        }

        private void AddPlayers(params string[] players)
        {
            var orderedPlayersCaseInsensitive = players.OrderBy(x => x.ToUpper());
            foreach (var player in orderedPlayersCaseInsensitive)
            {
                context.SetCurrentPlayer(player);
            }
        }

        [Given(@"I ""(.*)"" have already played the game")]
        public void GivenIHaveAlreadyPlayedTheGame(string p0)
        {
            AddPlayers("another name", "joey", p0, "kramk");
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

        [When(@"I press view record button")]
        public void WhenIPressViewRecordButton()
        {
            view.viewRecordButton.PerformClick();
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
            Assert.IsTrue(listItems.Contains(p0));
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
            var players = context.GetAllPlayers();
            Assert.IsTrue(players.Contains(p0));
        }


        [Then(@"view record button is disabled")]
        public void ThenViewRecordButtonIsDisabled()
        {
            Assert.IsFalse(view.viewRecordButton.Enabled);
        }

        [Then(@"view record button is enabled")]
        public void ThenViewRecordButtonIsEnabled()
        {
            Assert.IsTrue(view.viewRecordButton.Enabled);
        }

        [Then(@"a record window is showm")]
        public void ThenARecordWindowIsShowm()
        {
            loader.Verify(m => m.LoadGameRecordView(), Times.Once());
            loader.VerifyNoOtherCalls();
        }


        [AfterScenario]
        public static void AfterScenario()
        {
            GC.WaitForPendingFinalizers();
            GC.Collect();
            if (Directory.Exists(recordsFilePath))
            {
                Directory.Delete(recordsFilePath, true);
            }
        }
    }
}
