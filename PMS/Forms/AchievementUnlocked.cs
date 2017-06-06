using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BPMS {
    public partial class AchievementUnlocked : Form {
        Timer t;
        public AchievementUnlocked( string alert) {
            InitializeComponent();
            achievementLabel.Text = alert.ToUpper();
        }

        private void onAchievementLoad( object sender, EventArgs e ) {
           t = new Timer();
           t.Interval = 3000;
           t.Tick += new EventHandler( timesUpHanddler );
           t.Start();
        }

        public void timesUpHanddler( object sender, EventArgs e ) {
            Timer timer = (Timer)sender;
            t.Stop();
            this.Dispose();
        }
    }
}
