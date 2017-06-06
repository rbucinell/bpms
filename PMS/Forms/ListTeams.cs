using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BPMS.Forms;

namespace BPMS {
    public partial class ListTeams : Form {

        //refrence to the main form
        BPMSMain myParent;
        //display list
        string[] stringNames;
        
        public ListTeams( BPMSMain parent) {
            myParent = parent;
            InitializeComponent();
            configreNamesFromTeamInfo();
            deleteFromListButton.Enabled = false;
            queueFromListButton.Enabled = false;
            Closing += new System.ComponentModel.CancelEventHandler( okButtonClicked );
            paintMe( myParent.Data.Theme, myParent.myThemeColors );
        }

        private void paintMe( string theme, string[] paint ) {
            if( theme == "default"){
                BackColor = SystemColors.Control;
                listActionsResultsBox.BackColor = SystemColors.Control;
                teamsListBox.BackColor = SystemColors.Window;
                queueFromListButton.BackColor = Color.ForestGreen;
                queueFromListButton.ForeColor = Color.Black;
                deleteFromListButton.BackColor = Color.Red;
                deleteFromListButton.ForeColor = Color.Black;
                refreshDataButton.BackColor = Color.LightBlue;
                refreshDataButton.ForeColor = Color.Black;
                okAndCloseButton.BackColor = SystemColors.Control;
                okAndCloseButton.ForeColor = Color.Black;
                statsButton.BackColor = Color.Beige;
                statsButton.ForeColor = Color.Black;
            }else{
                int use = (theme == "black") ? 6 : 8;
                BackColor = Color.FromName( paint[use] );

                use = (theme == "tke") ? 6 : 0;
                listActionsResultsBox.BackColor = Color.FromName( paint[use] );

                if (theme == "black") {
                    listActionsResultsBox.ForeColor = Color.FromName( paint[1] );
                } else {
                    listActionsResultsBox.ForeColor = Color.Black;
                }

                teamsListBox.BackColor = Color.FromName( paint[2] );
                queueFromListButton.BackColor = Color.FromName( paint[0] );
                queueFromListButton.ForeColor = Color.FromName( paint[3] );
                deleteFromListButton.BackColor = Color.FromName( paint[0] );
                deleteFromListButton.ForeColor = Color.FromName( paint[3] );
                refreshDataButton.BackColor = Color.FromName( paint[0] );
                refreshDataButton.ForeColor = Color.FromName( paint[3] );
                okAndCloseButton.BackColor = Color.FromName( paint[0] );
                okAndCloseButton.ForeColor = Color.FromName( paint[3] );
                statsButton.BackColor = Color.FromName( paint[0] );
                statsButton.ForeColor = Color.FromName( paint[3] );
            }
            
        }

        private void okButtonClicked( object sender, EventArgs e ) {
            myParent.myListForm = null;
            Dispose();
        }


        /// <summary> Queues the selected Team from the Data List </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void queueSelectedButton( object sender, EventArgs e ) {
            if (myParent.Data.Mode == SystemMode.MAIN || myParent.Data.Mode == SystemMode.SIMPLE) {
                putInLine();
            } else {
                putInTourney();
            }
        }

        private void putInLine() {
            Team temp = getSelectedTeam();
            bool isBadArgs = false;

            //Check to see if they are already in line, and if MultiQueue Is enabled
            if (!myParent.Data.MulitQueue) {
                if( myParent.Data.QueuedTeams.Contains( temp )){
                    listActionsResultsBox.Text = "Multi Queue Not allowed";
                    isBadArgs = true;
                }
            }

            //Check to See if they are already playing a game
            if (!isBadArgs) {
                if (myParent.Data.Challenger == null && myParent.Data.Winner == null ||
                    myParent.Data.Winner == null && myParent.Data.Challenger == null) {
                    isBadArgs = false;
                } else {
                    if (myParent.Data.Challenger != null) {
                        if (temp.Id == myParent.Data.Challenger.Id) {
                            isBadArgs = true;
                            listActionsResultsBox.Text = "Team cannot queue, already playing!";
                        }
                    }
                    if (myParent.Data.Winner != null) {
                        if (temp.Id == myParent.Data.Winner.Id) {
                            isBadArgs = true;
                            listActionsResultsBox.Text = "Team cannot queue, already playing!";
                        }
                    }
                }
            }
            if (!isBadArgs) {
                myParent.Data.QueuedTeams.Enqueue( temp );
                myParent.clearInputFields();
                myParent.updatePositions();
                listActionsResultsBox.Text = "Successfully placed team " + temp + " in line";
            }
        }

        private void putInTourney() {
            Team temp = getSelectedTeam();
            insertTeam(temp);
        }

        /// <summary>that deletes the selected Team from the Data List 
        /// </summary>
        private void deleteSelected( object sender, EventArgs e ) {
            if (myParent.Data.PassIsSet) {
                myParent.requestPass = new PasswordRequest( "deleteTeam" );
                myParent.requestPass.Closing += new CancelEventHandler( isAuthenticated );
                myParent.requestPass.Show();

            } else {
                string result = myParent.Data.deleteTeam( getSelectedTeam() );
                myParent.updateAllViews();
                myParent.tourneyModeForm.updateTourneySeedObjects();
                configreNamesFromTeamInfo();
                listActionsResultsBox.Text = result;
            }
            myParent.Data.RecentSave = false;
        }

        /// <summary> Authenitcation routine for delete button, if 
        /// pass is ok will call deleteSelected sub routine
        /// </summary>
        public void isAuthenticated( object sender, CancelEventArgs e ) {
            PasswordRequest theform = (PasswordRequest)sender;
            myParent.inputPassword = theform.myPassword;
            string theFunction = theform.myFunction;
            if (myParent.Data.Password == myParent.inputPassword) {
                BPMSMain.passValid = true;
            } else {
                BPMSMain.passValid = false;
            }
            if (BPMSMain.passValid) {
                string result = myParent.Data.deleteTeam( getSelectedTeam() );
                myParent.updateAllViews();
                myParent.tourneyModeForm.updateTourneySeedObjects();
                configreNamesFromTeamInfo();
                listActionsResultsBox.Text = result;
            } else {
                listActionsResultsBox.Text = "Not authenticated to delete Team";
            }
            
        }

        /// <summary> Gets the selected team from the listbox
        /// </summary>
        /// <returns>the Selected Team</returns>
        public Team getSelectedTeam() {
            if (teamsListBox.SelectedIndex >= 0 ) {
                //
                return myParent.Data.AllTeams[teamsListBox.SelectedIndex];
            } else {
                return null;
            }
        }

        /// <summary>
        /// Recalculates the data structures for ListTeam's data
        /// </summary>
        public void configreNamesFromTeamInfo() {
            stringNames = new string[myParent.Data.AllTeams.Count];

            int i = 0;
            foreach (Team cur in myParent.Data.AllTeams) {
                stringNames[i++] = cur.toStringShowAll();
            }
            listActionsResultsBox.Text = "Data Refreshed at " + DateTime.Now.TimeOfDay.Hours + ":" 
                                                              + DateTime.Now.TimeOfDay.Minutes + ".";
            teamsListBox.Items.Clear();
            teamsListBox.Items.AddRange( stringNames );
        }

        private void refreshDataButtonClick( object sender, EventArgs e ) {
            configreNamesFromTeamInfo();
            paintMe( myParent.Data.Theme, myParent.myThemeColors );
        }

        private void reportSelection( object sender, EventArgs e ) {
            if (teamsListBox.SelectedIndex >= 0 && teamsListBox.SelectedIndex < myParent.Data.AllTeams.Count) {
                deleteFromListButton.Enabled = true;
                queueFromListButton.Enabled = true;
                listActionsResultsBox.Text = "User selected: " + stringNames[teamsListBox.SelectedIndex];
            }
        }

        private void statsButton_click( object sender, EventArgs e ) {
            Team queryStats = getSelectedTeam();
            if (queryStats != null) {
                TeamStatsPopup stats = new TeamStatsPopup( queryStats );
                stats.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                stats.Location = new Point( (Location.X), (Location.Y) );
                stats.Visible = true;
                stats.BringToFront();
            }
        }

        private void teamDoubleClick(object sender, MouseEventArgs e)
        {
            Team t = getSelectedTeam();
            insertTeam( t );
        }

        private void teamEnter(object sender, EventArgs e)
        {
            Team t = getSelectedTeam();
            insertTeam( t );
        }

        private void insertTeam( Team t )
        {
            bool success = myParent.Data.addTeamToTournament(t);
            if (success)
            {
                myParent.tourneyModeForm.updateTourneySeedObjects();
                listActionsResultsBox.Text = "Successfully placed team " + t + "into tournament";
            }
            else
            {
                listActionsResultsBox.Text = t + " was not placed in tournament";
            }
        }
    }
}
