using SillyButtons.Abstractions;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SillyButtons.Views
{
    public partial class PlayerNameForm : Form, IPlayerNameView
    {
        public string UserName
        {
            get
            {
                return userNameList.Text;
            }
        }

        public event EventHandler StartGame
        {
            add
            {
                startButton.Click += value;
            }
            remove
            {
                startButton.Click -= value;
            }
        }

        public PlayerNameForm()
        {
            InitializeComponent();
            CenterToScreen();
        }

        public event EventHandler ViewRecord
        {
            add
            {
                viewRecordButton.Click += value;
            }
            remove
            {
                viewRecordButton.Click -= value;
            }
        }

        private void userNameList_TextChanged(object sender, EventArgs e)
        {
            startButton.Enabled = !string.IsNullOrEmpty(userNameList.Text);
            viewRecordButton.Enabled = userNameList.Items.Contains(userNameList.Text);
        }

        public void SetNames(IEnumerable<string> names)
        {
            userNameList.Items.Clear();
            foreach (var name in names)
            {
                userNameList.Items.Add(name);
            }
        }
    }
}
