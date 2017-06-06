namespace BPMS {
    partial class CreatePassword {
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
            this.createPass_OK = new System.Windows.Forms.Button();
            this.createPass_Cancel = new System.Windows.Forms.Button();
            this.confirm_pass_textbox = new System.Windows.Forms.TextBox();
            this.new_pass_textbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // createPass_OK
            // 
            this.createPass_OK.Location = new System.Drawing.Point( 255, 23 );
            this.createPass_OK.Name = "createPass_OK";
            this.createPass_OK.Size = new System.Drawing.Size( 75, 23 );
            this.createPass_OK.TabIndex = 3;
            this.createPass_OK.Text = "OK";
            this.createPass_OK.UseVisualStyleBackColor = true;
            this.createPass_OK.Click += new System.EventHandler( this.OK_create_pass_button );
            // 
            // createPass_Cancel
            // 
            this.createPass_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.createPass_Cancel.Location = new System.Drawing.Point( 255, 62 );
            this.createPass_Cancel.Name = "createPass_Cancel";
            this.createPass_Cancel.Size = new System.Drawing.Size( 75, 23 );
            this.createPass_Cancel.TabIndex = 4;
            this.createPass_Cancel.Text = "Cancel";
            this.createPass_Cancel.UseVisualStyleBackColor = true;
            this.createPass_Cancel.Click += new System.EventHandler( this.Cancel_create_pass );
            // 
            // confirm_pass_textbox
            // 
            this.confirm_pass_textbox.Location = new System.Drawing.Point( 12, 64 );
            this.confirm_pass_textbox.Name = "confirm_pass_textbox";
            this.confirm_pass_textbox.PasswordChar = '*';
            this.confirm_pass_textbox.Size = new System.Drawing.Size( 228, 20 );
            this.confirm_pass_textbox.TabIndex = 2;
            // 
            // new_pass_textbox
            // 
            this.new_pass_textbox.Location = new System.Drawing.Point( 12, 25 );
            this.new_pass_textbox.Name = "new_pass_textbox";
            this.new_pass_textbox.PasswordChar = '*';
            this.new_pass_textbox.Size = new System.Drawing.Size( 228, 20 );
            this.new_pass_textbox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point( 12, 48 );
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size( 91, 13 );
            this.label1.TabIndex = 4;
            this.label1.Text = "Confirm Password";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point( 12, 9 );
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size( 78, 13 );
            this.label2.TabIndex = 5;
            this.label2.Text = "New Password";
            // 
            // CreatePassword
            // 
            this.AcceptButton = this.createPass_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.createPass_Cancel;
            this.ClientSize = new System.Drawing.Size( 340, 97 );
            this.ControlBox = false;
            this.Controls.Add( this.label2 );
            this.Controls.Add( this.label1 );
            this.Controls.Add( this.new_pass_textbox );
            this.Controls.Add( this.confirm_pass_textbox );
            this.Controls.Add( this.createPass_Cancel );
            this.Controls.Add( this.createPass_OK );
            this.Name = "CreatePassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create a new password";
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button createPass_OK;
        private System.Windows.Forms.Button createPass_Cancel;
        private System.Windows.Forms.TextBox confirm_pass_textbox;
        private System.Windows.Forms.TextBox new_pass_textbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}