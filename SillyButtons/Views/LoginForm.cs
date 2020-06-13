using SillyButtons.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SillyButtons.Views
{
    public partial class LoginForm : Form, ILoginView
    {
        public string UserName
        {
            get
            {
                return userNameList.Text;
            }
        }
        public LoginForm()
        {
            InitializeComponent();
            CenterToScreen();
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

        private void userNameList_TextChanged(object sender, EventArgs e)
        {
            startButton.Enabled = !string.IsNullOrEmpty(userNameList.Text);
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
