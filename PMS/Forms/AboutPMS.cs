using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace BPMS {
    public partial class AboutPMS : Form {
        public AboutPMS() {
            InitializeComponent();
            versionLabel.Text = "Version: 1.7.0";
        }
        
        private void okClose( object sender, EventArgs e ) {
            this.Dispose();
        }
    }
}
