namespace BPMS {
    partial class ListTeams {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListTeams));
            this.deleteFromListButton = new System.Windows.Forms.Button();
            this.teamsListBox = new System.Windows.Forms.ListBox();
            this.okAndCloseButton = new System.Windows.Forms.Button();
            this.queueFromListButton = new System.Windows.Forms.Button();
            this.listActionsResultsBox = new System.Windows.Forms.TextBox();
            this.refreshDataButton = new System.Windows.Forms.Button();
            this.statsButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // deleteFromListButton
            // 
            this.deleteFromListButton.BackColor = System.Drawing.Color.Red;
            this.deleteFromListButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.deleteFromListButton.Location = new System.Drawing.Point(194, 9);
            this.deleteFromListButton.Name = "deleteFromListButton";
            this.deleteFromListButton.Size = new System.Drawing.Size(75, 23);
            this.deleteFromListButton.TabIndex = 1;
            this.deleteFromListButton.Text = "&Delete";
            this.deleteFromListButton.UseVisualStyleBackColor = false;
            this.deleteFromListButton.Click += new System.EventHandler(this.deleteSelected);
            // 
            // teamsListBox
            // 
            this.teamsListBox.FormattingEnabled = true;
            this.teamsListBox.Location = new System.Drawing.Point(15, 66);
            this.teamsListBox.Name = "teamsListBox";
            this.teamsListBox.Size = new System.Drawing.Size(254, 420);
            this.teamsListBox.TabIndex = 2;
            this.teamsListBox.SelectedIndexChanged += new System.EventHandler(this.reportSelection);
            this.teamsListBox.Enter += new System.EventHandler(this.teamEnter);
            this.teamsListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.teamDoubleClick);
            // 
            // okAndCloseButton
            // 
            this.okAndCloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.okAndCloseButton.Location = new System.Drawing.Point(194, 492);
            this.okAndCloseButton.Name = "okAndCloseButton";
            this.okAndCloseButton.Size = new System.Drawing.Size(75, 25);
            this.okAndCloseButton.TabIndex = 3;
            this.okAndCloseButton.Text = "&OK";
            this.okAndCloseButton.UseVisualStyleBackColor = true;
            this.okAndCloseButton.Click += new System.EventHandler(this.okButtonClicked);
            // 
            // queueFromListButton
            // 
            this.queueFromListButton.BackColor = System.Drawing.Color.ForestGreen;
            this.queueFromListButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.queueFromListButton.Enabled = false;
            this.queueFromListButton.Location = new System.Drawing.Point(15, 9);
            this.queueFromListButton.Name = "queueFromListButton";
            this.queueFromListButton.Size = new System.Drawing.Size(75, 23);
            this.queueFromListButton.TabIndex = 4;
            this.queueFromListButton.Text = "&Queue";
            this.queueFromListButton.UseVisualStyleBackColor = false;
            this.queueFromListButton.Click += new System.EventHandler(this.queueSelectedButton);
            // 
            // listActionsResultsBox
            // 
            this.listActionsResultsBox.Location = new System.Drawing.Point(15, 38);
            this.listActionsResultsBox.Name = "listActionsResultsBox";
            this.listActionsResultsBox.ReadOnly = true;
            this.listActionsResultsBox.Size = new System.Drawing.Size(254, 20);
            this.listActionsResultsBox.TabIndex = 5;
            // 
            // refreshDataButton
            // 
            this.refreshDataButton.BackColor = System.Drawing.Color.LightCyan;
            this.refreshDataButton.Location = new System.Drawing.Point(15, 494);
            this.refreshDataButton.Name = "refreshDataButton";
            this.refreshDataButton.Size = new System.Drawing.Size(75, 23);
            this.refreshDataButton.TabIndex = 6;
            this.refreshDataButton.Text = "Refresh";
            this.refreshDataButton.UseVisualStyleBackColor = false;
            this.refreshDataButton.Click += new System.EventHandler(this.refreshDataButtonClick);
            // 
            // statsButton
            // 
            this.statsButton.BackColor = System.Drawing.Color.Beige;
            this.statsButton.Location = new System.Drawing.Point(101, 9);
            this.statsButton.Name = "statsButton";
            this.statsButton.Size = new System.Drawing.Size(75, 23);
            this.statsButton.TabIndex = 7;
            this.statsButton.Text = "Stats";
            this.statsButton.UseVisualStyleBackColor = false;
            this.statsButton.Click += new System.EventHandler(this.statsButton_click);
            // 
            // ListTeams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 529);
            this.Controls.Add(this.statsButton);
            this.Controls.Add(this.refreshDataButton);
            this.Controls.Add(this.listActionsResultsBox);
            this.Controls.Add(this.deleteFromListButton);
            this.Controls.Add(this.queueFromListButton);
            this.Controls.Add(this.okAndCloseButton);
            this.Controls.Add(this.teamsListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ListTeams";
            this.Text = "List of Teams";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button deleteFromListButton;
        private System.Windows.Forms.ListBox teamsListBox;
        private System.Windows.Forms.Button okAndCloseButton;
        private System.Windows.Forms.Button queueFromListButton;
        public  System.Windows.Forms.TextBox listActionsResultsBox;
        private System.Windows.Forms.Button refreshDataButton;
        private System.Windows.Forms.Button statsButton;
    }
}