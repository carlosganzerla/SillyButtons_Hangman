using SillyButtons.Abstractions;
using SillyButtons.Hangman;
using System;
using System.Linq;
using System.Windows.Forms;

namespace SillyButtons.Views
{
    public partial class GameForm : Form, IGameView
    {
        public event EventHandler<char> GuessMade;
        public event EventHandler StartGame;
        public GameForm()
        {
            InitializeComponent();
            AddAlphabetButtons();
            CenterToScreen();
        }

        private void AddAlphabetButtons()
        {
            for (int i = 0; i < AppConstants.Alphabet.Length; i++)
            {
                var button = GuiHelper.CreateAlphabetButton(AppConstants.Alphabet[i]);
                charButtonsPanel.Controls.Add(button);
                button.Click += OnGuessMade;
            }
        }
        private void OnGuessMade(object sender, EventArgs e)
        {
            if (sender is Button button && button.Tag is char letter)
            {
                GuessMade?.Invoke(this, letter);
            }
        }
        public void SetScoreWord(string word)
        {
            scoreCharsPanel.Controls.Clear();
            for (int i = 0; i < word.Length; i++)
            {
                scoreCharsPanel.Controls.Add(GuiHelper.CreateCharScoreLabel(word[i]));
            }
        }

        public void ShowHangman(int remainingGuesses)
        {
            hangmanImage.Image = GuiHelper.GetHangmanImage(remainingGuesses);
        }

        public void BlockButton(char letter)
        {
            var button = charButtonsPanel.GetCharacterButton(letter);
            if (button != null)
            {
                button.Enabled = false;
            }
        }

        public void EnableCharacters(bool enabled)
        {
            var buttons = charButtonsPanel.Controls.OfType<Button>();
            foreach (var button in buttons)
            {
                button.Enabled = enabled;
            }
            restartButton.Enabled = !enabled;
        }

        public void SetStatusMessage(string message)
        {
            gameMessage.Text = message;
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            StartGame?.Invoke(this, e);
        }

        private void restartButton_Click(object sender, EventArgs e)
        {
            StartGame?.Invoke(this, e);
        }
    }
}
