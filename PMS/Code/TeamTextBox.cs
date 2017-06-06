using System;
using System.Windows.Forms;
using System.Drawing;

namespace BPMS
{
    /// <summary>
    /// Class override the textbox function in order to add more display functionality
    /// to the textbox in regards to Teams
    /// </summary>
    public class TeamTextBox : TextBox
    {
        /// <summary>
        /// Accessor and mutator for the textbox's stored Team
        /// </summary>
        public Team Team { get; set; }
        private bool isSimple;

        public bool IsSimple
        {
            get { return isSimple; }
            set
            {
                isSimple = value;
                displayName();
            }
        }

        public TeamTextBox()
        {
            IsSimple = false;
            Team = null;
            coreConstruct();
        }

        public TeamTextBox(Team t)
        {
            IsSimple = false;
            Team = t;
            coreConstruct();
        }

        private void coreConstruct()
        {
            ReadOnly = true;
            BackColor = Color.White;
            MouseEnter += new EventHandler(TeamTextBox_MouseEnter);
            MouseLeave += new EventHandler(TeamTextBox_MouseLeave);
            displayName();
        }

        public void displayName()
        {
            if( Team == null )
            {
                Text = "";
            }
            else
            {
                if( IsSimple )
                {
                    Text = Text = Team.TeamNameSimple();
                }
                else
                {
                    Text = Text = Team.toStringTeamName();
                }
            }
        }

        public void TeamTextBox_MouseLeave(object sender, EventArgs e)
        {
            if( ((TeamTextBox)sender).Team != null )
            {
                displayName();
            }
        }

        public void TeamTextBox_MouseEnter(object sender, EventArgs e)
        {
            if( ((TeamTextBox)sender).Team != null )
            {
                Text = IsSimple ? Team.TeamPlayersSimple() : Team.toStringPlayers();
            }
        }
    }
}
