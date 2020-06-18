using TechTalk.SpecFlow;

namespace SilyButtons.Acceptance.Tests
{
    [Binding]
    public class GameHistoryScreenSteps
    {
        [Given(@"My name is ""(.*)""")]
        public void GivenMyNameIs(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"I have played (.*) games")]
        public void GivenIHavePlayedGames(int p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"I have never played any game")]
        public void GivenIHaveNeverPlayedAnyGame()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"I have played (.*) game")]
        public void GivenIHavePlayedGame(int p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"the date was ""(.*)""")]
        public void GivenTheDateWas(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"I have guessed the characters ""(.*)"" in this game")]
        public void GivenIHaveGuessedTheCharactersInThisGame(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"I had (.*) wrong guesses")]
        public void GivenIHadWrongGuesses(int p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"the secret word was ""(.*)""")]
        public void GivenTheSecretWordWas(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"the result was a defeat")]
        public void GivenTheResultWasADefeat()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I open the history screen")]
        public void WhenIOpenTheHistoryScreen()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I select ""(.*)"" on the player's list")]
        public void WhenISelectOnThePlayerSList(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"(.*) games appear on my history")]
        public void ThenGamesAppearOnMyHistory(int p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"""(.*)"" should not be player's list")]
        public void ThenShouldNotBePlayerSList(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"my history shows the date as ""(.*)""")]
        public void ThenMyHistoryShowsTheDateAs(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"my history shows the guessed characters as ""(.*)""")]
        public void ThenMyHistoryShowsTheGuessedCharactersAs(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"my history shows (.*) wrong guesses")]
        public void ThenMyHistoryShowsWrongGuesses(int p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"my hisotry shows the secret word as ""(.*)""")]
        public void ThenMyHisotryShowsTheSecretWordAs(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"my history shows that I have lost")]
        public void ThenMyHistoryShowsThatIHaveLost()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
