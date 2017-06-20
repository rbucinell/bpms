using System;
using System.Windows.Forms;

namespace BPMS
{
    public partial class AboutPMS : Form {
        public AboutPMS() {
            InitializeComponent();
            versionLabel.Text = String.Format("Version: {0} - {1}", Application.ProductVersion.ToString(), Properties.Resources.BuildDate.ToString());
        }
        
        private void okClose( object sender, EventArgs e ) {
            Dispose();
        }
    }
}
