namespace SillyButtons.Views
{
    partial class GameRecordForm
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
            this.gameRecordInfo = new System.Windows.Forms.Label();
            this.previousButton = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // gameRecordInfo
            // 
            this.gameRecordInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gameRecordInfo.Location = new System.Drawing.Point(64, 9);
            this.gameRecordInfo.Name = "gameRecordInfo";
            this.gameRecordInfo.Size = new System.Drawing.Size(229, 247);
            this.gameRecordInfo.TabIndex = 0;
            // 
            // previousButton
            // 
            this.previousButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.previousButton.AutoSize = true;
            this.previousButton.Location = new System.Drawing.Point(12, 119);
            this.previousButton.Name = "previousButton";
            this.previousButton.Size = new System.Drawing.Size(46, 29);
            this.previousButton.TabIndex = 1;
            this.previousButton.Text = " <<";
            this.previousButton.Click += new System.EventHandler(this.previousButton_Click);
            // 
            // nextButton
            // 
            this.nextButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nextButton.AutoSize = true;
            this.nextButton.Location = new System.Drawing.Point(299, 119);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(43, 29);
            this.nextButton.TabIndex = 1;
            this.nextButton.Text = ">>";
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // GameRecordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 265);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.previousButton);
            this.Controls.Add(this.gameRecordInfo);
            this.MaximumSize = new System.Drawing.Size(370, 304);
            this.MinimumSize = new System.Drawing.Size(370, 304);
            this.Name = "GameRecordForm";
            this.Text = "Game Record History of \"Player\"";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label gameRecordInfo;
        public System.Windows.Forms.Button nextButton;
        public System.Windows.Forms.Button previousButton;
    }
}