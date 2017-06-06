using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BPMS {
    public partial class CreatePassword : Form {

        public string pass1 = "", pass2 = "";
        public string myFunction = "createPass";
        private const string salt = ""; //TODO

        public CreatePassword() {
            InitializeComponent();
        }

        private void OK_create_pass_button(object sender, EventArgs e) {
            pass1 = getMd5Hash( new_pass_textbox.Text + salt );
            pass2 = getMd5Hash( confirm_pass_textbox.Text + salt );
            if( pass1 != pass2 ) {
                new_pass_textbox.BackColor = Color.Orange;
                confirm_pass_textbox.BackColor = Color.Orange;
            } else {
                new_pass_textbox.BackColor = SystemColors.Window;
                confirm_pass_textbox.BackColor = SystemColors.Window;
                Close();
            }
        }

        private void Cancel_create_pass(object sender, EventArgs e) {
            new_pass_textbox.Text = "";
            confirm_pass_textbox.Text = "";
            new_pass_textbox.BackColor = SystemColors.Window;
            confirm_pass_textbox.BackColor = SystemColors.Window;
            pass1 = "";
            pass2 = "";
            myFunction = "";
            Close();
        }

        public static string getMd5Hash( string input ) {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash( Encoding.Default.GetBytes( input ) );

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++) {
                sBuilder.Append( data[i].ToString( "x2" ) );
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}
