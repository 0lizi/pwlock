namespace pwlock
{
    partial class LockScreenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockScreenForm));
            this.UserNameLabel = new System.Windows.Forms.Label();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.LockedLabel = new System.Windows.Forms.Label();
            this.SubmitPasswordButton = new System.Windows.Forms.Button();
            this.ProfileIcon = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ProfileIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // UserNameLabel
            // 
            this.UserNameLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.UserNameLabel.AutoSize = true;
            this.UserNameLabel.Font = new System.Drawing.Font("Segoe UI", 33F);
            this.UserNameLabel.ForeColor = System.Drawing.Color.White;
            this.UserNameLabel.Location = new System.Drawing.Point(202, 200);
            this.UserNameLabel.MinimumSize = new System.Drawing.Size(403, 0);
            this.UserNameLabel.Name = "UserNameLabel";
            this.UserNameLabel.Size = new System.Drawing.Size(403, 60);
            this.UserNameLabel.TabIndex = 4;
            this.UserNameLabel.Text = "label2";
            this.UserNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PasswordTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.PasswordTextBox.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.PasswordTextBox.Location = new System.Drawing.Point(254, 274);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.Size = new System.Drawing.Size(309, 38);
            this.PasswordTextBox.TabIndex = 6;
            this.PasswordTextBox.TextChanged += new System.EventHandler(this.PasswordTextBox_TextChanged);
            // 
            // LockedLabel
            // 
            this.LockedLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LockedLabel.AutoSize = true;
            this.LockedLabel.BackColor = System.Drawing.Color.Transparent;
            this.LockedLabel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LockedLabel.ForeColor = System.Drawing.Color.White;
            this.LockedLabel.Location = new System.Drawing.Point(270, 256);
            this.LockedLabel.Name = "LockedLabel";
            this.LockedLabel.Size = new System.Drawing.Size(0, 25);
            this.LockedLabel.TabIndex = 8;
            // 
            // SubmitPasswordButton
            // 
            this.SubmitPasswordButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.SubmitPasswordButton.AutoSize = true;
            this.SubmitPasswordButton.BackColor = System.Drawing.Color.Transparent;
            this.SubmitPasswordButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SubmitPasswordButton.BackgroundImage")));
            this.SubmitPasswordButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SubmitPasswordButton.Location = new System.Drawing.Point(517, 273);
            this.SubmitPasswordButton.Name = "SubmitPasswordButton";
            this.SubmitPasswordButton.Size = new System.Drawing.Size(45, 38);
            this.SubmitPasswordButton.TabIndex = 9;
            this.SubmitPasswordButton.UseVisualStyleBackColor = false;
            this.SubmitPasswordButton.Click += new System.EventHandler(this.SubmitPasswordButton_Click);
            // 
            // ProfileIcon
            // 
            this.ProfileIcon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ProfileIcon.BackColor = System.Drawing.Color.Transparent;
            this.ProfileIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ProfileIcon.Location = new System.Drawing.Point(300, 29);
            this.ProfileIcon.Name = "ProfileIcon";
            this.ProfileIcon.Size = new System.Drawing.Size(199, 184);
            this.ProfileIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ProfileIcon.TabIndex = 1;
            this.ProfileIcon.TabStop = false;
            // 
            // LockScreenForm
            // 
            this.AcceptButton = this.SubmitPasswordButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(800, 415);
            this.Controls.Add(this.SubmitPasswordButton);
            this.Controls.Add(this.LockedLabel);
            this.Controls.Add(this.PasswordTextBox);
            this.Controls.Add(this.UserNameLabel);
            this.Controls.Add(this.ProfileIcon);
            this.Name = "LockScreenForm";
            this.Text = "LockScreenForm";
            this.TransparencyKey = System.Drawing.Color.Black;
            this.Load += new System.EventHandler(this.LockScreenForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ProfileIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox ProfileIcon;
        private System.Windows.Forms.Label UserNameLabel;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.Label LockedLabel;
        private System.Windows.Forms.Button SubmitPasswordButton;
    }
}

