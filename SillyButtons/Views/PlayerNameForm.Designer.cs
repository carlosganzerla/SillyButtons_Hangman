namespace SillyButtons.Views
{
    partial class PlayerNameForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.userNameList = new System.Windows.Forms.ComboBox();
            this.startButton = new System.Windows.Forms.Button();
            this.nameLabel = new System.Windows.Forms.Label();
            this.viewRecordButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // userNameList
            // 
            this.userNameList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.userNameList.FormattingEnabled = true;
            this.userNameList.Location = new System.Drawing.Point(12, 64);
            this.userNameList.Name = "userNameList";
            this.userNameList.Size = new System.Drawing.Size(241, 23);
            this.userNameList.TabIndex = 0;
            this.userNameList.TextChanged += new System.EventHandler(this.userNameList_TextChanged);
            // 
            // startButton
            // 
            this.startButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.startButton.BackColor = System.Drawing.SystemColors.Control;
            this.startButton.Enabled = false;
            this.startButton.Location = new System.Drawing.Point(12, 93);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(100, 39);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "Start Game";
            this.startButton.UseVisualStyleBackColor = false;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(12, 46);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(150, 15);
            this.nameLabel.TabIndex = 2;
            this.nameLabel.Text = "Enter or selecct your name:";
            // 
            // viewRecordButton
            // 
            this.viewRecordButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.viewRecordButton.BackColor = System.Drawing.SystemColors.Control;
            this.viewRecordButton.Enabled = false;
            this.viewRecordButton.Location = new System.Drawing.Point(153, 93);
            this.viewRecordButton.Name = "viewRecordButton";
            this.viewRecordButton.Size = new System.Drawing.Size(100, 39);
            this.viewRecordButton.TabIndex = 1;
            this.viewRecordButton.Text = "View Player\'s Record";
            this.viewRecordButton.UseVisualStyleBackColor = false;
            // 
            // PlayerNameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(265, 167);
            this.Controls.Add(this.viewRecordButton);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.userNameList);
            this.MaximumSize = new System.Drawing.Size(281, 206);
            this.MinimumSize = new System.Drawing.Size(281, 206);
            this.Name = "PlayerNameForm";
            this.Text = "Player Name";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ComboBox userNameList;
        public System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Label nameLabel;
        public System.Windows.Forms.Button viewRecordButton;
    }
}

