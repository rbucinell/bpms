using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace BPMS {

    /// <summary>
    /// Class override the textbox function in order to add more display functionality
    /// to the textbox in regards to Teams
    /// </summary>
    public class TeamTextBox : TextBox{

        /// <summary>
        /// Accessor and mutator for the textbox's stored Team
        /// </summary>
        public Team Team { get; set; }
        private bool isSimple;
        public bool IsSimple {
            get { return isSimple; }
            set {
                isSimple = value;
                displayName();
            }
        }

        public TeamTextBox() {
            IsSimple = false;
            Team = null;
            coreConstruct();
        }
        
        public TeamTextBox( Team t ) {
            IsSimple = false;
            Team = t;
            coreConstruct();
        }

        private void coreConstruct() {
            this.ReadOnly = true;
            this.BackColor = Color.White;
            this.MouseEnter += new EventHandler( TeamTextBox_MouseEnter );
            this.MouseLeave += new EventHandler( TeamTextBox_MouseLeave );
            displayName();
        }
        public void displayName(){
            if (Team == null) {
                Text = "";
            } else {
                if (IsSimple) {
                    this.Text = this.Text = Team.TeamNameSimple();
                } else {
                    this.Text = this.Text = Team.toStringTeamName();
                }
            }
        }
        public void TeamTextBox_MouseLeave( object sender, EventArgs e ) {
            if (((TeamTextBox)sender).Team != null) {
                displayName();
            }
        }

        public void TeamTextBox_MouseEnter( object sender, EventArgs e ) {
            if (((TeamTextBox)sender).Team != null) {
                if (IsSimple) {
                    this.Text = Team.TeamPlayersSimple();
                } else {
                    this.Text = Team.toStringPlayers();
                }
            }
        }
            
    }
}
