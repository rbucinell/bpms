namespace BPMS {
    partial class AchievementUnlocked {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing ) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.achievementLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::BPMS.Properties.Resources.Achievement_Unlocked;
            this.pictureBox1.Location = new System.Drawing.Point( 0, 0 );
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size( 425, 85 );
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // achievementLabel
            // 
            this.achievementLabel.AutoSize = true;
            this.achievementLabel.BackColor = System.Drawing.Color.FromArgb( ((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))) );
            this.achievementLabel.Font = new System.Drawing.Font( "Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            this.achievementLabel.ForeColor = System.Drawing.Color.White;
            this.achievementLabel.Location = new System.Drawing.Point( 77, 37 );
            this.achievementLabel.Name = "achievementLabel";
            this.achievementLabel.Size = new System.Drawing.Size( 210, 20 );
            this.achievementLabel.TabIndex = 1;
            this.achievementLabel.Text = "ACHIEVEMENT STATUS";
            // 
            // AchievementUnlocked
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 425, 85 );
            this.ControlBox = false;
            this.Controls.Add( this.achievementLabel );
            this.Controls.Add( this.pictureBox1 );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AchievementUnlocked";
            this.Text = "AchievementUnlocked";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.LightGray;
            this.Load += new System.EventHandler( this.onAchievementLoad );
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label achievementLabel;
    }
}