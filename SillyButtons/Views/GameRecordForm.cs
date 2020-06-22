using SillyButtons.Abstractions;
using SillyButtons.Hangman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SillyButtons.Views
{
    public partial class GameRecordForm : Form, IGameRecordView
    {
        private List<GameRecord> records = new List<GameRecord>();
        private int currentRecordIndex = 0;
        public GameRecordForm()
        {
            InitializeComponent();
            CenterToScreen();
        }

        public void ShowGameRecords(IEnumerable<GameRecord> records)
        {
            this.records = records.ToList();
            currentRecordIndex = 0;
            UpdateControls();
        }

        private void UpdateControls()
        {
            UpdateInfoText();
            SetPreviousButtonState();
            SetNextButtonState();
        }

        private void UpdateInfoText()
        {
            gameRecordInfo.Text = AppStrings.GameRecordString(records[currentRecordIndex]);
        }

        private void SetPreviousButtonState()
        {
            previousButton.Enabled = CanDecrement();
        }

        private bool CanDecrement()
        {
            return currentRecordIndex > 0;
        }

        private void SetNextButtonState()
        {
            nextButton.Enabled = CanIncrement();
        }

        private bool CanIncrement()
        {
            return currentRecordIndex < records.Count - 1;
        }

        private void previousButton_Click(object sender, EventArgs e)
        {
            if (CanDecrement())
            {
                currentRecordIndex--;
                UpdateControls();
            }
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            if (CanIncrement())
            {
                currentRecordIndex++;
                UpdateControls();
            }
        }

        public void SetTitle(string text)
        {
            Text = text;
        }
    }

}
