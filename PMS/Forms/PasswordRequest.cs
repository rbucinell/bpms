using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BPMS {
    public partial class PasswordRequest : Form {

        public string myPassword;
        public string myFunction;

        public PasswordRequest(string function) {
            InitializeComponent();
            myPassword = "";
            myFunction = function;
        }

        public string getPassword() {
            return CreatePassword.getMd5Hash( inputPasswordBox.Text );
        }

        private void OKisClicked(object sender, EventArgs e) {
            myPassword = getPassword();
            this.Close();
        }

        private void CancelisClicked(object sender, EventArgs e) {
            myPassword = "";
            myFunction = "cancelled";
            this.Close();
        }
    }
}
