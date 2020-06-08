namespace SillyButtons.Views
{
    partial class GameForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.charButtonsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.scoreCharsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.hangmanImage = new System.Windows.Forms.PictureBox();
            this.gameMessage = new System.Windows.Forms.Label();
            this.restartButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.hangmanImage)).BeginInit();
            this.SuspendLayout();
            // 
            // charButtonsPanel
            // 
            this.charButtonsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.charButtonsPanel.Location = new System.Drawing.Point(230, 94);
            this.charButtonsPanel.Name = "charButtonsPanel";
            this.charButtonsPanel.Size = new System.Drawing.Size(494, 316);
            this.charButtonsPanel.TabIndex = 0;
            // 
            // scoreCharsPanel
            // 
            this.scoreCharsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scoreCharsPanel.AutoSize = true;
            this.scoreCharsPanel.Location = new System.Drawing.Point(230, 5);
            this.scoreCharsPanel.Margin = new System.Windows.Forms.Padding(0);
            this.scoreCharsPanel.Name = "scoreCharsPanel";
            this.scoreCharsPanel.Size = new System.Drawing.Size(494, 83);
            this.scoreCharsPanel.TabIndex = 0;
            // 
            // hangmanImage
            // 
            this.hangmanImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.hangmanImage.Location = new System.Drawing.Point(12, 5);
            this.hangmanImage.Name = "hangmanImage";
            this.hangmanImage.Size = new System.Drawing.Size(212, 299);
            this.hangmanImage.TabIndex = 1;
            this.hangmanImage.TabStop = false;
            // 
            // gameMessage
            // 
            this.gameMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gameMessage.AutoSize = true;
            this.gameMessage.Location = new System.Drawing.Point(12, 421);
            this.gameMessage.Name = "gameMessage";
            this.gameMessage.Size = new System.Drawing.Size(38, 15);
            this.gameMessage.TabIndex = 2;
            this.gameMessage.Text = "label1";
            // 
            // restartButton
            // 
            this.restartButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.restartButton.AutoSize = true;
            this.restartButton.Enabled = false;
            this.restartButton.Location = new System.Drawing.Point(12, 330);
            this.restartButton.Name = "restartButton";
            this.restartButton.Size = new System.Drawing.Size(212, 53);
            this.restartButton.TabIndex = 2;
            this.restartButton.Text = "Restart Game";
            this.restartButton.Click += new System.EventHandler(this.restartButton_Click);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 445);
            this.Controls.Add(this.restartButton);
            this.Controls.Add(this.gameMessage);
            this.Controls.Add(this.hangmanImage);
            this.Controls.Add(this.scoreCharsPanel);
            this.Controls.Add(this.charButtonsPanel);
            this.Name = "GameForm";
            this.Text = "GameForm";
            this.Load += new System.EventHandler(this.GameForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.hangmanImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.FlowLayoutPanel charButtonsPanel;
        public System.Windows.Forms.FlowLayoutPanel scoreCharsPanel;
        public System.Windows.Forms.PictureBox hangmanImage;
        public System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        public System.Windows.Forms.Label gameMessage;
        public System.Windows.Forms.Button restartButton;
    }
}