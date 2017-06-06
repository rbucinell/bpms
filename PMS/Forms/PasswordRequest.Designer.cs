namespace BPMS {
    partial class PasswordRequest {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if( disposing && ( components != null ) ) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( PasswordRequest ) );
            this.label1 = new System.Windows.Forms.Label();
            this.inputPasswordBox = new System.Windows.Forms.TextBox();
            this.enterPassButton = new System.Windows.Forms.Button();
            this.cancelPassButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point( 12, 9 );
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size( 119, 13 );
            this.label1.TabIndex = 0;
            this.label1.Text = "Please Enter Password:";
            // 
            // inputPasswordBox
            // 
            this.inputPasswordBox.Location = new System.Drawing.Point( 15, 25 );
            this.inputPasswordBox.Name = "inputPasswordBox";
            this.inputPasswordBox.PasswordChar = '*';
            this.inputPasswordBox.Size = new System.Drawing.Size( 180, 20 );
            this.inputPasswordBox.TabIndex = 1;
            // 
            // enterPassButton
            // 
            this.enterPassButton.Location = new System.Drawing.Point( 226, 4 );
            this.enterPassButton.Name = "enterPassButton";
            this.enterPassButton.Size = new System.Drawing.Size( 75, 23 );
            this.enterPassButton.TabIndex = 2;
            this.enterPassButton.Text = "OK";
            this.enterPassButton.UseVisualStyleBackColor = true;
            this.enterPassButton.Click += new System.EventHandler( this.OKisClicked );
            // 
            // cancelPassButton
            // 
            this.cancelPassButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelPassButton.Location = new System.Drawing.Point( 226, 33 );
            this.cancelPassButton.Name = "cancelPassButton";
            this.cancelPassButton.Size = new System.Drawing.Size( 75, 23 );
            this.cancelPassButton.TabIndex = 3;
            this.cancelPassButton.Text = "Cancel";
            this.cancelPassButton.UseVisualStyleBackColor = true;
            this.cancelPassButton.Click += new System.EventHandler( this.CancelisClicked );
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point( 12, 48 );
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size( 156, 13 );
            this.label2.TabIndex = 4;
            this.label2.Text = "(Will close if password is wrong)";
            // 
            // PasswordRequest
            // 
            this.AcceptButton = this.enterPassButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelPassButton;
            this.ClientSize = new System.Drawing.Size( 313, 64 );
            this.ControlBox = false;
            this.Controls.Add( this.label2 );
            this.Controls.Add( this.cancelPassButton );
            this.Controls.Add( this.enterPassButton );
            this.Controls.Add( this.inputPasswordBox );
            this.Controls.Add( this.label1 );
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject( "$this.Icon" )));
            this.Name = "PasswordRequest";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Authorization Required";
            this.TopMost = true;
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox inputPasswordBox;
        private System.Windows.Forms.Button enterPassButton;
        private System.Windows.Forms.Button cancelPassButton;
        private System.Windows.Forms.Label label2;
    }
}