namespace BPMS {
    partial class TourneyFAQsForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( TourneyFAQsForm ) );
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.demoQueueButton = new System.Windows.Forms.Button();
            this.demoBackForMore = new System.Windows.Forms.Button();
            this.demoQueueUs = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.seedSettingComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.bracketLabel = new System.Windows.Forms.Label();
            this.bracketSizeComboBox = new System.Windows.Forms.ComboBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.demoUpButton = new System.Windows.Forms.Button();
            this.demoDownButton = new System.Windows.Forms.Button();
            this.labelSeed2 = new System.Windows.Forms.Label();
            this.demoRemoveButton = new System.Windows.Forms.Button();
            this.demoSeedBox = new BPMS.TeamTextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point( 12, 7 );
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size( 360, 39 );
            this.label1.TabIndex = 0;
            this.label1.Text = "This is the help section regarding the tournament mode please click on a \r\nbutton" +
                " for detailed help on a topic. If your answer is not solved please refer \r\nto th" +
                "e README or email the developer\r\n";
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabControl1.Controls.Add( this.tabPage1 );
            this.tabControl1.Controls.Add( this.tabPage2 );
            this.tabControl1.Controls.Add( this.tabPage3 );
            this.tabControl1.Controls.Add( this.tabPage4 );
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl1.Location = new System.Drawing.Point( 0, 51 );
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size( 392, 465 );
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add( this.label2 );
            this.tabPage1.Controls.Add( this.demoQueueButton );
            this.tabPage1.Controls.Add( this.demoBackForMore );
            this.tabPage1.Controls.Add( this.demoQueueUs );
            this.tabPage1.Location = new System.Drawing.Point( 4, 25 );
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding( 3 );
            this.tabPage1.Size = new System.Drawing.Size( 384, 436 );
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Inserting Teams";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point( 3, 69 );
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size( 376, 221 );
            this.label2.TabIndex = 10;
            this.label2.Text = resources.GetString( "label2.Text" );
            // 
            // demoQueueButton
            // 
            this.demoQueueButton.BackColor = System.Drawing.Color.ForestGreen;
            this.demoQueueButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.demoQueueButton.Location = new System.Drawing.Point( 145, 16 );
            this.demoQueueButton.Name = "demoQueueButton";
            this.demoQueueButton.Size = new System.Drawing.Size( 75, 23 );
            this.demoQueueButton.TabIndex = 9;
            this.demoQueueButton.Text = "&Queue";
            this.demoQueueButton.UseVisualStyleBackColor = false;
            // 
            // demoBackForMore
            // 
            this.demoBackForMore.BackColor = System.Drawing.Color.Green;
            this.demoBackForMore.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            this.demoBackForMore.Location = new System.Drawing.Point( 255, 6 );
            this.demoBackForMore.Name = "demoBackForMore";
            this.demoBackForMore.Size = new System.Drawing.Size( 100, 43 );
            this.demoBackForMore.TabIndex = 8;
            this.demoBackForMore.Text = "Back for More!";
            this.demoBackForMore.UseVisualStyleBackColor = false;
            // 
            // demoQueueUs
            // 
            this.demoQueueUs.BackColor = System.Drawing.Color.LimeGreen;
            this.demoQueueUs.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            this.demoQueueUs.Location = new System.Drawing.Point( 11, 6 );
            this.demoQueueUs.Name = "demoQueueUs";
            this.demoQueueUs.Size = new System.Drawing.Size( 100, 43 );
            this.demoQueueUs.TabIndex = 7;
            this.demoQueueUs.Text = "Queue Us!";
            this.demoQueueUs.UseVisualStyleBackColor = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add( this.label4 );
            this.tabPage2.Controls.Add( this.label3 );
            this.tabPage2.Controls.Add( this.seedSettingComboBox );
            this.tabPage2.Controls.Add( this.label5 );
            this.tabPage2.Controls.Add( this.bracketLabel );
            this.tabPage2.Controls.Add( this.bracketSizeComboBox );
            this.tabPage2.Location = new System.Drawing.Point( 4, 25 );
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding( 3 );
            this.tabPage2.Size = new System.Drawing.Size( 384, 436 );
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Tournament Settings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point( 84, 191 );
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size( 284, 221 );
            this.label4.TabIndex = 27;
            this.label4.Text = resources.GetString( "label4.Text" );
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point( 84, 64 );
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size( 271, 65 );
            this.label3.TabIndex = 26;
            this.label3.Text = resources.GetString( "label3.Text" );
            // 
            // seedSettingComboBox
            // 
            this.seedSettingComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.seedSettingComboBox.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            this.seedSettingComboBox.FormattingEnabled = true;
            this.seedSettingComboBox.Items.AddRange( new object[] {
            "Manual",
            "Input Order",
            "Rating",
            "Random"} );
            this.seedSettingComboBox.Location = new System.Drawing.Point( 6, 157 );
            this.seedSettingComboBox.Name = "seedSettingComboBox";
            this.seedSettingComboBox.Size = new System.Drawing.Size( 119, 21 );
            this.seedSettingComboBox.TabIndex = 25;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            this.label5.Location = new System.Drawing.Point( 3, 141 );
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size( 90, 13 );
            this.label5.TabIndex = 24;
            this.label5.Text = "Seeding Settings:";
            // 
            // bracketLabel
            // 
            this.bracketLabel.AutoSize = true;
            this.bracketLabel.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            this.bracketLabel.Location = new System.Drawing.Point( 8, 13 );
            this.bracketLabel.Name = "bracketLabel";
            this.bracketLabel.Size = new System.Drawing.Size( 70, 13 );
            this.bracketLabel.TabIndex = 23;
            this.bracketLabel.Text = "Bracket Size:";
            // 
            // bracketSizeComboBox
            // 
            this.bracketSizeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bracketSizeComboBox.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            this.bracketSizeComboBox.FormattingEnabled = true;
            this.bracketSizeComboBox.Items.AddRange( new object[] {
            "4",
            "8",
            "16"} );
            this.bracketSizeComboBox.Location = new System.Drawing.Point( 10, 29 );
            this.bracketSizeComboBox.Name = "bracketSizeComboBox";
            this.bracketSizeComboBox.Size = new System.Drawing.Size( 64, 21 );
            this.bracketSizeComboBox.TabIndex = 22;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add( this.label6 );
            this.tabPage3.Controls.Add( this.demoUpButton );
            this.tabPage3.Controls.Add( this.demoDownButton );
            this.tabPage3.Controls.Add( this.labelSeed2 );
            this.tabPage3.Controls.Add( this.demoRemoveButton );
            this.tabPage3.Controls.Add( this.demoSeedBox );
            this.tabPage3.Location = new System.Drawing.Point( 4, 25 );
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size( 384, 436 );
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Modifying Participant List";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point( 41, 76 );
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size( 252, 260 );
            this.label6.TabIndex = 41;
            this.label6.Text = resources.GetString( "label6.Text" );
            // 
            // demoUpButton
            // 
            this.demoUpButton.BackColor = System.Drawing.Color.Gainsboro;
            this.demoUpButton.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            this.demoUpButton.ForeColor = System.Drawing.Color.Black;
            this.demoUpButton.Location = new System.Drawing.Point( 280, 17 );
            this.demoUpButton.Name = "demoUpButton";
            this.demoUpButton.Size = new System.Drawing.Size( 28, 20 );
            this.demoUpButton.TabIndex = 40;
            this.demoUpButton.Text = "Λ";
            this.demoUpButton.UseVisualStyleBackColor = false;
            // 
            // demoDownButton
            // 
            this.demoDownButton.BackColor = System.Drawing.Color.Gainsboro;
            this.demoDownButton.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            this.demoDownButton.ForeColor = System.Drawing.Color.Black;
            this.demoDownButton.Location = new System.Drawing.Point( 251, 17 );
            this.demoDownButton.Name = "demoDownButton";
            this.demoDownButton.Size = new System.Drawing.Size( 24, 20 );
            this.demoDownButton.TabIndex = 39;
            this.demoDownButton.Text = "V";
            this.demoDownButton.UseVisualStyleBackColor = false;
            // 
            // labelSeed2
            // 
            this.labelSeed2.AutoSize = true;
            this.labelSeed2.BackColor = System.Drawing.Color.Transparent;
            this.labelSeed2.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            this.labelSeed2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelSeed2.Location = new System.Drawing.Point( 28, 19 );
            this.labelSeed2.Name = "labelSeed2";
            this.labelSeed2.Size = new System.Drawing.Size( 15, 13 );
            this.labelSeed2.TabIndex = 38;
            this.labelSeed2.Text = "#";
            // 
            // demoRemoveButton
            // 
            this.demoRemoveButton.BackColor = System.Drawing.Color.Red;
            this.demoRemoveButton.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            this.demoRemoveButton.ForeColor = System.Drawing.Color.Black;
            this.demoRemoveButton.Location = new System.Drawing.Point( 44, 17 );
            this.demoRemoveButton.Name = "demoRemoveButton";
            this.demoRemoveButton.Size = new System.Drawing.Size( 24, 20 );
            this.demoRemoveButton.TabIndex = 36;
            this.demoRemoveButton.Text = "X";
            this.demoRemoveButton.UseVisualStyleBackColor = false;
            // 
            // demoSeedBox
            // 
            this.demoSeedBox.BackColor = System.Drawing.Color.White;
            this.demoSeedBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.demoSeedBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.demoSeedBox.ForeColor = System.Drawing.Color.Black;
            this.demoSeedBox.Location = new System.Drawing.Point( 70, 17 );
            this.demoSeedBox.Name = "demoSeedBox";
            this.demoSeedBox.ReadOnly = true;
            this.demoSeedBox.Size = new System.Drawing.Size( 179, 20 );
            this.demoSeedBox.TabIndex = 37;
            this.demoSeedBox.TabStop = false;
            this.demoSeedBox.Team = null;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point( 4, 25 );
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size( 384, 436 );
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Play";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // TourneyFAQsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 392, 516 );
            this.Controls.Add( this.tabControl1 );
            this.Controls.Add( this.label1 );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject( "$this.Icon" )));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TourneyFAQsForm";
            this.Text = "Help - Tournament Mode";
            this.tabControl1.ResumeLayout( false );
            this.tabPage1.ResumeLayout( false );
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout( false );
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout( false );
            this.tabPage3.PerformLayout();
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button demoBackForMore;
        private System.Windows.Forms.Button demoQueueUs;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button demoQueueButton;
        private System.Windows.Forms.ComboBox seedSettingComboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label bracketLabel;
        private System.Windows.Forms.ComboBox bracketSizeComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button demoUpButton;
        private System.Windows.Forms.Button demoDownButton;
        private System.Windows.Forms.Label labelSeed2;
        private System.Windows.Forms.Button demoRemoveButton;
        private TeamTextBox demoSeedBox;
        private System.Windows.Forms.Label label6;
    }
}