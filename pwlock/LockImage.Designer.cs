namespace pwlock
{
    partial class LockImage
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
            this.components = new System.ComponentModel.Container();
            this.timelab = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timelab2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timelab
            // 
            this.timelab.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.timelab.BackColor = System.Drawing.Color.Transparent;
            this.timelab.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timelab.ForeColor = System.Drawing.Color.White;
            this.timelab.Location = new System.Drawing.Point(9, 347);
            this.timelab.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.timelab.Name = "timelab";
            this.timelab.Size = new System.Drawing.Size(314, 64);
            this.timelab.TabIndex = 0;
            // 
            // timelab2
            // 
            this.timelab2.BackColor = System.Drawing.Color.Transparent;
            this.timelab2.Font = new System.Drawing.Font("Segoe UI", 60F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timelab2.ForeColor = System.Drawing.Color.White;
            this.timelab2.Location = new System.Drawing.Point(6, 282);
            this.timelab2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.timelab2.Name = "timelab2";
            this.timelab2.Size = new System.Drawing.Size(359, 96);
            this.timelab2.TabIndex = 1;
            // 
            // LockImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 415);
            this.Controls.Add(this.timelab);
            this.Controls.Add(this.timelab2);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "LockImage";
            this.Text = "LockImage";
            this.Load += new System.EventHandler(this.LockImage_Load);
            this.Click += new System.EventHandler(this.LockImage_Click);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LockImage_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label timelab;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label timelab2;
    }
}