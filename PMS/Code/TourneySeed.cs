using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace BPMS {
    /// <summary>
    /// A group of objects used in the tournament mode setup tab
    /// </summary>
    public class TourneySeed {

        public int pos, max;
        public Button CloseButton, UpButton, DownButton;
        public TeamTextBox Box;
        private Team team;

        public Team Team {
            get { return team; }
            set { team = value; this.Box.Team = value; }
        }

        public TourneySeed( Button c, Button d, Button u, TeamTextBox b, int p, int m ) {
            CloseButton = c;
            UpButton = u;
            DownButton = d;
            Box = b;
            team = Box.Team;
            pos = p;
            max = m;
            if (team == null) {
                c.Enabled = false;
            } else {
                c.Enabled = true;
            }
        }

        
        /// <summary>
        /// Displays all the components in the control, updating display
        /// based on data
        /// </summary>
        public void showMe() {
            CloseButton.Visible = true;
            if (UpButton != null) {
                UpButton.Visible = true;
            }
            if (DownButton != null) {
                DownButton.Visible = true;
            }
            if (Box != null) {
                Box.Visible = true;
                if (team == null) {
                    Box.Text = "BYE";
                    Box.Font = new Font( Box.Font, FontStyle.Italic);
                    Box.ForeColor = Color.Gray;
                    Box.Enabled = false;
                } else {
                    Box.Text = team.ToString();
                    Box.Font = new Font( Box.Font, FontStyle.Regular );
                    Box.ForeColor = Color.Black;
                    Box.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Hides all the components in the control
        /// </summary>
        public void hideMe() {
            CloseButton.Visible = false;
            if (UpButton != null) {
                UpButton.Visible = false;
            }
            if (DownButton != null) {
                DownButton.Visible = false;
            }
            if (Box != null) {
                Box.Visible = false;
            }
        }

    }
}
