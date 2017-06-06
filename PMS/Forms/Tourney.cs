using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BPMS.Code;

namespace BPMS {

    public partial class Tourney : Form {

        #region Configuration member variables
        public TourneySeed[] seedObjects = new TourneySeed[16];

        private BPMSMain myParent;
        private TabPage setupPage, playPage;

        // Total number of brackets in the tournement
        public int numBrackets;

        //Determines if the tournament needs to reset
        public bool isChanged = true;

        // Prefrence on System Setting
        public int seedingSetting = -1;

        public Label[] seedingLabels = new Label[16];

        #endregion

        #region Play game member variables

        TourneyMatchUp[] preliminaries;
        TourneyMatchUp[] allMatchups;
        TeamTextBox winrar = new TeamTextBox();
        public TabControl TabControl {
            get { return tabControl; }
        }

        #endregion

        /// <summary> Default Constructor for the Tournament Form
        /// </summary>
        public Tourney(BPMSMain parent) {
            InitializeComponent();
            allMatchups = new TourneyMatchUp[] {tourneyMatchUp0, tourneyMatchUp1, tourneyMatchUp2,
                                                tourneyMatchUp3, tourneyMatchUp4, tourneyMatchUp5,
                                                tourneyMatchUp6, tourneyMatchUp7, tourneyMatchUp8,
                                                tourneyMatchUp9,  tourneyMatchUp10, tourneyMatchUp11,
                                                tourneyMatchUp12, tourneyMatchUp13,  tourneyMatchUp14 };
            myParent = parent;
            setupPage = tabControl.TabPages[0];
            playPage = tabControl.TabPages[1];
            tabControl.TabPages.Remove( playPage );
            tabControl.SelectedIndex = 0;
            #region Labels displaying seeding
            seedingLabels[0] = labelSeed1;
            seedingLabels[1] = labelSeed2;
            seedingLabels[2] = labelSeed3;
            seedingLabels[3] = labelSeed4;
            seedingLabels[4] = labelSeed5;
            seedingLabels[5] = labelSeed6;
            seedingLabels[6] = labelSeed7;
            seedingLabels[7] = labelSeed8;
            seedingLabels[8] = labelSeed9;
            seedingLabels[9] = labelSeed10;
            seedingLabels[10] = labelSeed11;
            seedingLabels[11] = labelSeed12;
            seedingLabels[12] = labelSeed13;
            seedingLabels[13] = labelSeed14;
            seedingLabels[14] = labelSeed15;
            seedingLabels[15] = labelSeed16;
            #endregion

            numBrackets = 8;
            //if( myParent.Data.t
            //inTournament = new Team[ numBrackets ];
            
            createTourneySeedObjects();
            
            bracketSizeComboBox.SelectedIndex = 1;
            seedSettingComboBox.SelectedIndex = 0;
            updateTourneySeedObjects();
        }

        /// <summary>  Initial Creation of the Tourney Seed Object list
        /// </summary>
        private void createTourneySeedObjects() {
            for( int i = 1; i <= 16; i++ ) {
                if (i == 1) {
                    seedObjects[0] = new TourneySeed( (Button)Controls.Find( "removeButtonSeed" + i, true )[0], (Button)Controls.Find( "moveDownButton" + i, true )[0], null, (TeamTextBox)Controls.Find( "teamSeedBox" + i, true )[0], 1, numBrackets );
                } else if (i == 16) {
                    seedObjects[15] = new TourneySeed( (Button)Controls.Find( "removeButtonSeed" + i, true )[0], null, (Button)Controls.Find( "moveupButton" + i, true )[0], (TeamTextBox)Controls.Find( "teamSeedBox" + i, true )[0], 16, numBrackets );
                } else {
                    seedObjects[i - 1] = new TourneySeed( (Button)Controls.Find( "removeButtonSeed" + i, true )[0], (Button)Controls.Find( "moveDownButton" + i, true )[0], (Button)Controls.Find( "moveupButton" + i, true )[0], (TeamTextBox)Controls.Find( "teamSeedBox" + i, true )[0], i, numBrackets );
                    if (i == numBrackets) {
                        seedObjects[i - 1].DownButton.Visible = false;
                    }
                }
                seedObjects[i - 1].hideMe();
                if (i <= numBrackets) {
                    try {
                        seedObjects[i - 1].Team = (myParent.Data.InTourney[i - 1] != null) ? myParent.Data.InTourney[i - 1] : null;
                    } catch (IndexOutOfRangeException) { /*Past numbrackets we dont care*/
                    } catch (ArgumentOutOfRangeException) { /*Past numbrackets we dont care*/ }
                
                    seedObjects[i - 1].showMe();
                }
            }
            int count = 0;
            for (int i = 0; i < myParent.Data.InTourney.Length; i++) {
                TourneySeed t = seedObjects[i];
                t.Box = new TeamTextBox( myParent.Data.InTourney[count++] );
                if (t.UpButton != null)
                    t.UpButton.Click += new EventHandler( upButton_Click );
                if (t.DownButton != null)
                    t.DownButton.Click += new EventHandler( downButton_Click );
                if (t.CloseButton != null) {
                    t.CloseButton.Click += new EventHandler( removeFromTourneyHandler );
                    if (t.Team == null) {
                        t.CloseButton.Enabled = false;
                    } else {
                        t.CloseButton.Enabled = true;
                    }
                }
            }

            for( int i = 0; i < 16; i++ ) {
                if( i < numBrackets ) {
                    seedingLabels[i].Visible = true;
                } else {
                    seedingLabels[i].Visible = false;
                }
            }
        }

        /// <summary> Updates all the TourneySeed Objects
        /// </summary>
        public void updateTourneySeedObjects() {
            for( int i = 1; i <= 16; i++ ) {
                if (seedObjects[i - 1] != null) {
                    seedObjects[i - 1].hideMe();
                }
                if (i == 1) {
                    seedObjects[0] = new TourneySeed( (Button)Controls.Find( "removeButtonSeed" + i, true )[0], (Button)Controls.Find( "moveDownButton" + i, true )[0], null, (TeamTextBox)Controls.Find( "teamSeedBox" + i, true )[0], 1, numBrackets );
                } else if (i == 16) {
                    seedObjects[15] = new TourneySeed( (Button)Controls.Find( "removeButtonSeed" + i, true )[0], null, (Button)Controls.Find( "moveupButton" + i, true )[0], (TeamTextBox)Controls.Find( "teamSeedBox" + i, true )[0], 16, numBrackets );
                } else if (i == numBrackets) {
                    seedObjects[i - 1] = new TourneySeed( (Button)Controls.Find( "removeButtonSeed" + i, true )[0], null, (Button)Controls.Find( "moveupButton" + i, true )[0], (TeamTextBox)Controls.Find( "teamSeedBox" + i, true )[0], 16, numBrackets );
                } else {
                    seedObjects[i - 1] = new TourneySeed(
                        (Button)Controls.Find( "removeButtonSeed" + i, true )[0],
                        (Button)Controls.Find( "moveDownButton" + i, true )[0],
                        (Button)Controls.Find( "moveupButton" + i, true )[0],
                        (TeamTextBox)Controls.Find( "teamSeedBox" + i, true )[0],
                        i, numBrackets );
                }
                
                if( i > numBrackets ) {
                    seedObjects[i - 1].hideMe();
                } else {
                    try {
                        seedObjects[i - 1].Team = (myParent.Data.InTourney[i - 1] != null) ? myParent.Data.InTourney[i - 1] : null;
                    } catch (IndexOutOfRangeException) {
                    } catch (ArgumentOutOfRangeException) { }
                    seedObjects[i - 1].showMe();
                }
                seedObjects[i - 1].CloseButton.Enabled = (seedObjects[i - 1].Team != null);

                if( i <= numBrackets ) {
                    seedingLabels[i-1].Visible = true;
                } else {
                    seedingLabels[i-1].Visible = false;
                }
            }
        }

        #region changing the seed of teams in toureny

        /// <summary>
        /// Event handler for up button on a Tourney Seed, moves current up
        /// and the one above, down.
        /// </summary>
        public void upButton_Click(object sender, EventArgs e) {
            Button theSender = (Button)sender;
            string name = theSender.Name.Replace( "moveUpButton", "" );
            int position = Int32.Parse( name );
            increaseSeed(position);
        }

        /// <summary>
        /// Event handler for down button on a Tourney Seed, moves current one
        /// down and the one below, up.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void downButton_Click(object sender, EventArgs e) {
            Button theSender = (Button)sender;
            string name = theSender.Name.Replace( "moveDownButton", "" );
            int position = Int32.Parse( name );
            decreaseSeed( position );
        }

        /// <summary>
        /// Private method to encapsulate the swapSeed method, to decrease the
        /// current Tourney seed
        /// </summary>
        /// <param name="i">Target seed number to move down</param>
        private void decreaseSeed( int i ) {
            isChanged = true;
            swapSeed( i, i + 1 );
        }

        /// <summary>
        /// Private method to encapsulate the swapSeed method, to increase the
        /// current Tourney seed
        /// </summary>
        /// <param name="i">Target seed number to move up</param>
        private void increaseSeed( int i ) {
            isChanged = true;
            swapSeed( i, i - 1 );
        }

        /// <summary>
        /// Swaps two seeds of two TourenySeedObject Objects
        /// </summary>
        /// <param name="first">The first TourenySeedObject's Seed</param>
        /// <param name="second">The second TourenySeedObject's Seed</param>
        private void swapSeed( int first, int second ) {

            Team temp = myParent.Data.InTourney[first - 1];
            myParent.Data.InTourney[first - 1] = myParent.Data.InTourney[second - 1];
            myParent.Data.InTourney[second - 1] = temp;
            updateTourneySeedObjects();
        }
        #endregion  

        #region Settings EventListeners

        /// <summary> Event Handler for remove button on a Tourney Seed. Does not delete from records
        /// </summary>
        public void removeFromTourneyHandler( object sender, EventArgs e ) {
            isChanged = true;
            Button theSender = (Button)sender;
            string numOnly = theSender.Name.Replace( "removeButtonSeed", "" );
            int seed = Int32.Parse( numOnly );
            myParent.Data.InTourney[seed - 1] = null;
            updateTourneySeedObjects();
        }

        /// <summary>  When the user changes the dropdown value, update the availible brackets and the allotted teams threshold
        /// </summary>
        private void brackSizeListChange(object sender, EventArgs e) {
            isChanged = true;
            //If the combo isn't black
            if( ( (ComboBox)sender ).SelectedItem != null ) {
                //get number from comboBox
                numBrackets = Int32.Parse((string)( (ComboBox)sender ).SelectedItem);

                switch (numBrackets) {
                    case 16:
                        playTab.BackgroundImage = global::BPMS.Properties.Resources._16manTourneyBG;
                        break;
                    case 8:
                        playTab.BackgroundImage = global::BPMS.Properties.Resources._8manTourneyBG;
                        break;
                    case 4:
                        playTab.BackgroundImage = global::BPMS.Properties.Resources._4manTourneyBG;
                        break;
                }


                //copy the teams array with only the top seeds persiting if size was decreased
                Team[] temp = new Team[numBrackets];
                for( int i = 0; i < numBrackets; i++ ) {
                    if (i >= myParent.Data.InTourney.Length) {
                        temp[i] = null;
                        continue;
                    }
                    if (myParent.Data.InTourney[i] != null) {
                        temp[i] = myParent.Data.InTourney[i];
                    } else {
                        temp[i] = null;

                    }
                    //try {
                    //    temp[i] = (myParent.Data.InTourney[i] != null) ? myParent.Data.InTourney[i] : null;
                    //} catch (IndexOutOfRangeException) { /*Once again, I dont really care*/
                    //} catch (ArgumentOutOfRangeException) {/*Once again, I dont really care*/}
                }
                myParent.Data.InTourney = temp;
            }
            //update list
            updateTourneySeedObjects();
        }

        /// <summary>  Changes the settins that all users to modify 
        /// </summary>
        private void seedingSettingListChange(object sender, EventArgs e) {
            isChanged = true;
            if( ( (ComboBox)sender ).SelectedItem != null )
                seedingSetting = ((ComboBox)sender ).SelectedIndex;

            switch( seedingSetting ) {
            case 0: //Manual
                for( int i = 0; i < seedingLabels.Length; i++ ) {
                    seedingLabels[i].Text = ( i + 1 ) + "";
                }
                foreach( TourneySeed s in seedObjects ) {
                    if( s.UpButton != null ) {
                        s.UpButton.Enabled = true;
                        s.UpButton.Visible = true;
                    }
                    if( s.DownButton != null ) {
                        s.DownButton.Enabled = true;
                        s.DownButton.Visible = true;
                    }
                }
                break;
            case 1: //Input Order
                for( int i = 0; i < seedingLabels.Length; i++ ) {
                    seedingLabels[i].Text = ( i + 1 ) + "";
                }
                foreach( TourneySeed s in seedObjects ){
                    if( s.UpButton != null ) {
                        s.UpButton.Enabled = false;
                        s.UpButton.Visible = true;
                    }
                    if( s.DownButton != null ) {
                        s.DownButton.Enabled = false;
                        s.DownButton.Visible = true;
                    }
                }
                break;
            case 2: //Rating
                for( int i = 0; i < seedingLabels.Length; i++ ) {
                    seedingLabels[i].Text = "-";
                }
                foreach( TourneySeed s in seedObjects ) {
                    if( s.UpButton != null ) {
                        s.UpButton.Enabled = false;
                        s.UpButton.Visible = true;
                    }
                    if( s.DownButton != null ) {
                        s.DownButton.Enabled = false;
                        s.DownButton.Visible = true;
                    }
                }
                break;
            case 3: //Random
                for( int i = 0; i < seedingLabels.Length; i++ ) {
                    seedingLabels[i].Text = "?";
                }
                foreach( TourneySeed s in seedObjects ) {
                    if( s.UpButton != null ) {
                        s.UpButton.Enabled = false;
                        s.UpButton.Visible = false;
                    }
                    if( s.DownButton != null ) {
                        s.DownButton.Enabled = false;
                        s.DownButton.Visible = false;
                    }
                }
                break;
            default: //hasn't been set, dont do anything
                break;
            }
            updateTourneySeedObjects();
        }
        
        #region Call back to Main Event handlers
        //Not the best coding standard, but its going to save me a lot of work
        private void mainModeMenuItem_Click(object sender, EventArgs e) {
            if( !tabControl.TabPages.Contains( setupPage ) )
                tabControl.TabPages.Add( setupPage );
            myParent.switchMode(sender, e);
        }

        private void commonIssuesToolStripMenuItem_Click(object sender, EventArgs e) {
            myParent.showMeFAQmenuClick(sender, e);
        }

        private void donateToolStripMenuItem_Click(object sender, EventArgs e) {
            myParent.DonateButton_Click(sender, e);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
            myParent.aboutMenuItemClickEvent(sender, e);
        }
        
        private void tourneySetPass_Click(object sender, EventArgs e) {
            myParent.Set_Password_MenuItem(sender, e);
        }

        private void tourneyChangePassword_Click(object sender, EventArgs e) {
            myParent.change_pass_MenuItem(sender, e);
        }

        private void tourneyClearPassword_Click(object sender, EventArgs e) {
            myParent.clearPassword_MenuItem(sender, e);
        }

        private void newTournamentToolStripMenuItem_Click(object sender, EventArgs e) {
            numBrackets = 8;
            seedingSetting = 0;
            bracketSizeComboBox.SelectedIndex = 1;
            seedSettingComboBox.SelectedIndex = 0;
            myParent.Data.InTourney = new Team[numBrackets];
            updateTourneySeedObjects();
        }

        private void listTeamsToolStripMenuItem_Click(object sender, EventArgs e) {
            myParent.File_ListTeams_ClickHandler( sender, e );
        }

        private void statsToolStripMenuItem_Click(object sender, EventArgs e) {
            myParent.File_Clear_Stats(sender, e);
        }

        private void tournamentListToolStripMenuItem_Click(object sender, EventArgs e) {
            myParent.Data.InTourney = new Team[numBrackets];
            updateTourneySeedObjects();
        }

        private void saveQuitToolStripMenuItem_Click(object sender, EventArgs e) {
            myParent.quitToolStripMenuItem_Click(sender, e);
        }

        private void tourneyPromptSaveClose(object sender, FormClosingEventArgs e) {
            myParent.PromptSaveClose(sender, e);
        }

        private void saveButton_click( object sender, EventArgs e ) {
            myParent.saveData_click( sender, e );
        }
        #endregion

        private void new_team_for_tourney_button_click(object sender, EventArgs e) {
            bool isBadArgs = false;

            //If first text box is empty, make it yellow and display error
            if (tourneyP1textBox.Text == "") {
                tourneyP1textBox.BackColor = Color.Yellow;
                isBadArgs = true;
            }

            //If second text box is empty, make it yellow and display error
            if (tourneyP2textBox.Text == "") {
                tourneyP2textBox.BackColor = Color.Yellow;
                isBadArgs = true;
            }

            //Look through the teams, if the name is taken show err
            foreach (Team team in myParent.Data.AllTeams) {
                if (team.TeamName == tourneyTNametextBox.Text) {
                    tourneyTNametextBox.BackColor = Color.Orange;
                    tourneyTNametextBox.Text = "<In use: " + tourneyTNametextBox.Text + ">";
                    isBadArgs = true;
                    break;
                }
            }

            if( !isBadArgs){
                Team temp = new Team( tourneyP1textBox.Text, tourneyP2textBox.Text );

                if( tourneyTNametextBox.Text != ""){
                    temp.TeamName = tourneyTNametextBox.Text; 
                }

                //temp.Achievement += new Team.TeamEventHandler( myParent.catchAchievement );

                tourneyTNametextBox.Text = "";
                //inQ.Enqueue( temp );
                myParent.Data.AllTeams.Add( temp );

                enterTeamIntoTourney( temp );
                clearTourneyFields();

                if (myParent.myListForm != null) {
                    myParent.myListForm.configreNamesFromTeamInfo();
                }
            }
            myParent.Data.RecentSave = false;
        }

        private void return_team_queue_button_click(object sender, EventArgs e) {
            int teamcode = 0;
            string teamname = "";
            Team temp = null;
            int inputState = 0;

            if (tourneyReturnTNametextBox.Text == "") {
                if (tourneyReturnIDtextBox.Text == "")
                    inputState = 0;
                else
                    inputState = 1;
            } else {
                if (tourneyReturnIDtextBox.Text == "")
                    inputState = 2;
                else
                    inputState = 3;
            }

            switch ( inputState ){
                case 0: //both fields empty
                    tourneyReturnTNametextBox.BackColor = Color.Yellow;
                    tourneyReturnIDtextBox.BackColor = Color.Yellow;
                    break;
                case 1: //only ID
                    try {
                        teamcode = Convert.ToInt32( tourneyReturnIDtextBox.Text );
                        if (myParent.Data.hasTeam( teamcode )) {
                            temp = myParent.Data.getTeam( teamcode );
                            enterTeamIntoTourney( temp );
                            clearTourneyFields();
                        }else{
                            clearTourneyFields();
                            tourneyReturnIDtextBox.Text = "<"+teamcode+"> DNE";
                            tourneyReturnIDtextBox.BackColor = Color.Orange;
                        }
                    } catch (Exception) {
                        clearTourneyFields();
                        tourneyReturnIDtextBox.Text = "<Error>: Not a number";
                        tourneyReturnIDtextBox.BackColor = Color.Red;
                    }
                    break;
                case 2: // only name
                    teamname = tourneyReturnTNametextBox.Text;

                    /////////////Konami Easter Egg ///////////////
                    if (teamname == "uuddlrlrba" || teamname == "upupdowndownleftrightleftrightba") {
                        myParent.Data.AddContraTeam();
                        //myParent.teamsNameID.Add( contra.Name, contra.Id );
                        tourneyReturnTNametextBox.Text = "";
                        pictureBox1.Image = new Bitmap( BPMS.Properties.Resources.bill_lance_code );
                        enterTeamIntoTourney( myParent.Data.getTeam("Contra") );
                    } else {
                    ///////////End Easter egg, do real function///
                        try {
                            if (myParent.Data.hasTeam( teamname )) {
                                temp = myParent.Data.getTeam( teamname );
                                enterTeamIntoTourney( temp );
                            } else {
                                clearTourneyFields();
                                tourneyReturnTNametextBox.Text = "<"+ teamname +"> DNE";
                                tourneyReturnTNametextBox.BackColor = Color.Orange;
                            }
                        } catch (Exception) {
                            clearTourneyFields();
                            tourneyReturnTNametextBox.Text = "<Error>";
                            tourneyReturnTNametextBox.BackColor = Color.Red;
                        }
                    }
                    break;
                case 3: //both name and ID
                    Team temp2 = null;
                    try {
                        teamcode = Convert.ToInt32( tourneyReturnIDtextBox.Text );
                        teamname = tourneyReturnTNametextBox.Text;

                        if (myParent.Data.hasTeam( teamname ) && myParent.Data.hasTeam( teamcode )) {
                            temp = myParent.Data.getTeam( teamname );
                            temp2 = myParent.Data.getTeam( teamcode );
                            if (temp == temp2) {
                                clearTourneyFields();
                                enterTeamIntoTourney( temp );
                            } else {
                                clearTourneyFields();
                                tourneyReturnIDtextBox.Text = teamcode + "";
                                tourneyReturnTNametextBox.Text = teamname;
                                tourneyReturnTNametextBox.BackColor = Color.Olive;
                                tourneyReturnIDtextBox.BackColor = Color.Olive;
                            }
                        } else {
                            clearTourneyFields();
                            tourneyReturnIDtextBox.Text = teamcode + "";
                            tourneyReturnTNametextBox.Text = teamname;
                            tourneyReturnTNametextBox.BackColor = Color.Orange;
                            tourneyReturnIDtextBox.BackColor = Color.Orange;
                        }
                    } catch (Exception) {
                        clearTourneyFields();
                        tourneyReturnIDtextBox.Text = "<Error>";
                        tourneyReturnTNametextBox.Text = "<Error>";
                        tourneyReturnIDtextBox.BackColor = Color.Red;
                        tourneyReturnTNametextBox.BackColor = Color.Red;
                    }
                    break;
            }
        }
        #endregion

        private void clearTourneyFields() {
            tourneyP1textBox.Text = "";
            tourneyP1textBox.BackColor = SystemColors.Window;

            tourneyP2textBox.Text = "";
            tourneyP2textBox.BackColor = SystemColors.Window;

            tourneyTNametextBox.Text = "";
            tourneyTNametextBox.BackColor = SystemColors.Window;

            tourneyReturnTNametextBox.Text = "";
            tourneyReturnTNametextBox.BackColor = SystemColors.Window;

            tourneyReturnIDtextBox.Text = "";
            tourneyReturnIDtextBox.BackColor = SystemColors.Window;
        }

        public void enterTeamIntoTourney( Team temp ) {
            bool teamInserted = false;
            for( int i = 0; i < numBrackets; i++ ) {
                if( myParent.Data.InTourney[i] == temp ) {
                    MessageBox.Show( "Tournament already contains that team, try another" );
                    teamInserted = true;
                    break;
                }
            }
            if( !teamInserted ) {
                for( int i = 0; i < numBrackets; i++ ) {
                    if( myParent.Data.InTourney[i] == null ) {
                        isChanged = true;
                        myParent.Data.InTourney[i] = temp;
                        teamInserted = true;
                        break;
                    }
                }
                if( teamInserted ) {
                    clearTourneyFields();
                    updateTourneySeedObjects();
                } else {
                    MessageBox.Show( "Tournament is full, please remove another team if you wish to enter this one" );
                    clearTourneyFields();
                }
            }
        }

        public void showTournamentPlay() {
            //Get Seed setting from combo box
            string seedSetting = (string)seedSettingComboBox.SelectedItem;
            //Sort the teams according to seeding rule
            switch( seedSetting ) {
                case "Manual":
                case "Input Order":
                    //Both of these we dont need to apply an algorthim because
                    //all work has been done manually (or not at all )
                    break;
                case "Rating":
                    sortByRating( myParent.Data.InTourney );
                    break;
                case "Random":
                    sortRandomly( myParent.Data.InTourney );
                    break;
            }

            //second thing we need to do is create matchups and save to manager
            Team[] teams = myParent.Data.InTourney;
            //first clear out any old matchups

            preliminaries = new TourneyMatchUp[numBrackets / 2];
            int pos = 0;

            for( int i = 0; i < teams.Length; i += 2 ) {
                Team t1 = teams[i];
                Team t2 = teams[i + 1];
                preliminaries[pos++] = new TourneyMatchUp( t1, t2 );
            }

            drawTournamentStart( preliminaries ); //This works 7/24/10

        }

        /// <summary> Swaps random indexes a random amount of times
        /// </summary>
        /// <param name="team"> Data stored</param>
        private void sortRandomly( Team[] team ) {
            Random r = new Random();
            int randomChangeAmount = r.Next(0, 300 );
            for( int i = 0; i < randomChangeAmount; i++ ) {
                int index1 = r.Next(0,team.Length);
                int index2 = r.Next(0,team.Length);
                Team temp = team[index1];
                team[index1] = team[index2];
                team[index2] = temp;
            }
        }

        /// <summary> Sorts teams based on the their record, then re-orders them in a seeding fashion
        ///  ie: 1v8, 3v6, 5v4, 7v2
        /// </summary>
        /// <param name="team">The Data</param>
        private void sortByRating( Team[] team ) {
            int counter = 0;
            while( counter != team.Length ) {
                int highestValIndex = counter;
                for( int i = counter; i < team.Length; i++ ) {
                    //set the highestValIndex to the index that contains the team with the highest record (want best record first)
                    highestValIndex = ( team[highestValIndex].Record >= team[i].Record ) ? highestValIndex : i;
                }
                //swap that team with the current position
                Team temp = team[counter];
                team[counter] = team[highestValIndex];
                team[highestValIndex] = temp;
                //move to next position
                counter++;
            }
            //Then sort them based on
            Team[] sortedTeams = new Team[team.Length];

            int high = 0, low = team.Length-1;
            //order by seeding
            for( int i = 0; i < team.Length; i++ ) {
                if( i % 2 == 0 ) {
                    //If its even
                    sortedTeams[i] = team[high];
                    high += 2;
                } else {
                    //Else its odd
                    sortedTeams[i] = team[low];
                    low -= 2;
                }
            }
            //once sorted, set back to actual teams and finish
            team = sortedTeams;
        }
        public void TempFixdrawTournamentStart(TourneyMatchUp[] firstRound)
        {
            for (int i = 0; i < firstRound.Length; i++)
            {
                firstRound[i].resetMatch();//(firstRound[i].RedTeam, firstRound[i].BlueTeam);
                firstRound[i].Parent = playTab;
                firstRound[i].Visible = true;
                
            }


            //TODO:
            //Place First Round in new place
            //Link Match
            setFirstRoundToPropperList(firstRound);
            linkAllMatchUps();

            champions.Visible = false;
            champions.Parent = playTab;
            champions.Location = new Point(574, 217);
        }

        public void drawTournamentStart(TourneyMatchUp[] firstRound)
        {
            
            //Make sure the match ups are in the starting state
            for( int i = 0; i < firstRound.Length; i++ ) {
                firstRound[i].resetMatch();
                //firstRound[i].reset( firstRound[i].RedTeam, firstRound[i].BlueTeam );
            }

            //clearOut();

            //get propper depth linking
            setFirstRoundToPropperList( firstRound );
            linkAllMatchUps();

            champions.Visible = false;
            champions.Parent = playTab;
            champions.Location = new Point( 574, 217 );
        }

        private void clearOut()
        {
            tourneyMatchUp0.Visible = false;
            tourneyMatchUp1.Visible = false;
            tourneyMatchUp2.Visible = false;
            tourneyMatchUp3.Visible = false;
            tourneyMatchUp4.Visible = false;
            tourneyMatchUp5.Visible = false;
            tourneyMatchUp6.Visible = false;
            tourneyMatchUp7.Visible = false;

            tourneyMatchUp0.Parent = playTab;
            tourneyMatchUp1.Parent = playTab;
            tourneyMatchUp2.Parent = playTab;
            tourneyMatchUp3.Parent = playTab;
            tourneyMatchUp4.Parent = playTab;
            tourneyMatchUp5.Parent = playTab;
            tourneyMatchUp6.Parent = playTab;
            tourneyMatchUp7.Parent = playTab;

            tourneyMatchUp0 = new TourneyMatchUp();
            tourneyMatchUp1 = new TourneyMatchUp();
            tourneyMatchUp2 = new TourneyMatchUp();
            tourneyMatchUp3 = new TourneyMatchUp();
            tourneyMatchUp4 = new TourneyMatchUp();
            tourneyMatchUp5 = new TourneyMatchUp();
            tourneyMatchUp6 = new TourneyMatchUp();
            tourneyMatchUp7 = new TourneyMatchUp();
        }

        private void linkAllMatchUps() {
            tourneyMatchUp0.NextMatch = tourneyMatchUp8;
            tourneyMatchUp1.NextMatch = tourneyMatchUp8;
            tourneyMatchUp2.NextMatch = tourneyMatchUp9;
            tourneyMatchUp3.NextMatch = tourneyMatchUp9;
            tourneyMatchUp4.NextMatch = tourneyMatchUp10;
            tourneyMatchUp5.NextMatch = tourneyMatchUp10;
            tourneyMatchUp6.NextMatch = tourneyMatchUp11;
            tourneyMatchUp7.NextMatch = tourneyMatchUp11;
            tourneyMatchUp8.NextMatch = tourneyMatchUp12;
            tourneyMatchUp9.NextMatch = tourneyMatchUp12;
            tourneyMatchUp10.NextMatch = tourneyMatchUp13;
            tourneyMatchUp11.NextMatch = tourneyMatchUp13;
            tourneyMatchUp12.NextMatch = tourneyMatchUp14;
            tourneyMatchUp13.NextMatch = tourneyMatchUp14;
            tourneyMatchUp14.NextMatch = null;

            tourneyMatchUp8.PreviousTopMatch = tourneyMatchUp0;
            tourneyMatchUp8.PreviousBottomMatch = tourneyMatchUp1;
            tourneyMatchUp9.PreviousTopMatch = tourneyMatchUp2;
            tourneyMatchUp9.PreviousBottomMatch = tourneyMatchUp3;
            tourneyMatchUp10.PreviousTopMatch = tourneyMatchUp4;
            tourneyMatchUp10.PreviousBottomMatch = tourneyMatchUp5;
            tourneyMatchUp11.PreviousTopMatch = tourneyMatchUp6;
            tourneyMatchUp11.PreviousBottomMatch = tourneyMatchUp7;
            tourneyMatchUp12.PreviousTopMatch = tourneyMatchUp8;
            tourneyMatchUp12.PreviousBottomMatch = tourneyMatchUp9;
            tourneyMatchUp13.PreviousTopMatch = tourneyMatchUp10;
            tourneyMatchUp13.PreviousBottomMatch = tourneyMatchUp11;
            tourneyMatchUp14.PreviousTopMatch = tourneyMatchUp12;
            tourneyMatchUp14.PreviousBottomMatch = tourneyMatchUp13;
        }

        private void setFirstRoundToPropperList( TourneyMatchUp[] firstRound ) {
            foreach( TourneyMatchUp t in firstRound ) {
                if( t.Visible == false ) {
                    t.Visible = true;
                }
                t.Parent = playTab;
            }
            
            if( firstRound.Length == 8 ) {
                tourneyMatchUp0 = firstRound[ 0 ];
                tourneyMatchUp1 = firstRound[ 1 ];
                tourneyMatchUp2 = firstRound[ 2 ];
                tourneyMatchUp3 = firstRound[ 3 ];
                tourneyMatchUp4 = firstRound[ 4 ];
                tourneyMatchUp5 = firstRound[ 5 ];
                tourneyMatchUp6 = firstRound[ 6 ];
                tourneyMatchUp7 = firstRound[ 7 ];
            } else if( firstRound.Length == 4 ) {
                tourneyMatchUp8 = firstRound [ 0 ];
                tourneyMatchUp9 = firstRound [ 1 ];
                tourneyMatchUp10 = firstRound [ 2 ];
                tourneyMatchUp11 = firstRound[3];
            }else if( firstRound.Length == 2 ){
                tourneyMatchUp12 = firstRound[ 0 ];
                tourneyMatchUp13 = firstRound[ 1 ];
            }
            setMatchupLocations();
        }

        private void setMatchupLocations() {
            
            foreach( TourneyMatchUp t in allMatchups ) {
                t.Parent = playTab;
                t.MyParent = this;
            }
            tourneyMatchUp0.Location = getLocationInfoForControl( 4, 0 );
            tourneyMatchUp1.Location = getLocationInfoForControl( 4, 1 );
            tourneyMatchUp2.Location = getLocationInfoForControl( 4, 2 );
            tourneyMatchUp3.Location = getLocationInfoForControl( 4, 3 );
            tourneyMatchUp4.Location = getLocationInfoForControl( 4, 4 );
            tourneyMatchUp5.Location = getLocationInfoForControl( 4, 5 );
            tourneyMatchUp6.Location = getLocationInfoForControl( 4, 6 );
            tourneyMatchUp7.Location = getLocationInfoForControl( 4, 7 );
            tourneyMatchUp8.Location = getLocationInfoForControl( 3, 0 );
            tourneyMatchUp9.Location = getLocationInfoForControl( 3, 1 );
            tourneyMatchUp10.Location = getLocationInfoForControl( 3, 2 );
            tourneyMatchUp11.Location = getLocationInfoForControl( 3, 3 );
            tourneyMatchUp12.Location = getLocationInfoForControl( 2, 0 );
            tourneyMatchUp13.Location = getLocationInfoForControl( 2, 1 );
            tourneyMatchUp14.Location = getLocationInfoForControl( 1, 0 );
        }

        /// <summary> Sets the Location based on what layer the control is in the brackets and how far in controls-wise it is.
        /// </summary>
        /// <param name="depth"></param>
        /// <param name="howManyDown"></param>
        /// <returns></returns>
        private Point getLocationInfoForControl( int depth, int howManyDown ) {
            //(4)starters,(3)quarters,(2)semis,(1)finals,(0)winner
            Point pt = new Point();
            switch( depth ) {
                case 4:
                    pt.X = 8;
                    pt.Y = 11 + ( howManyDown * 57 );
                    break;
                case 3:
                    pt.X = 141;
                    pt.Y = 37 + ( howManyDown * 114 );
                    break;
                case 2:
                    pt.X = 284;
                    pt.Y = 97 + ( howManyDown * 228 );
                    break;
                case 1:
                    pt.X = 433;
                    pt.Y = 201;
                    break;
                case 0:
                    //TODO
                    pt.X = 500; //proper size?: 570;
                    pt.Y = 204; //proper size?: 214;
                    break;
            }
            return pt;
        }

        public void declareWinner( Team t) {
            champions.Team = t;
            champions.Visible = true;
            champions.displayName();
            foreach( TourneyMatchUp tmu in allMatchups ) {
                tmu.drawDone();
            }
            returnToSetupButton.Visible = true;
            resetTourneyButton.Visible = true;
        }

        private void returnToSetupButton_Click( object sender, EventArgs e ) {
            for( int i = 0; i < allMatchups.Length; i++ ) {
                allMatchups[i].Dispose();
                allMatchups[i] = new TourneyMatchUp();
                allMatchups[i].Visible = false;
            }
            for( int i = 0; i < allMatchups.Length; i++ ) {
                allMatchups[i].clear();
            }
            drawTournamentStart( preliminaries );
            tabControl.TabPages.Remove( playPage );
            tabControl.TabPages.Add( setupPage );
            tabControl.SelectedTab = tabControl.TabPages[0];
        }

        private void resetTourneyButton_Click( object sender, EventArgs e ) {
            /*for( int i = 0; i < this.Controls.Count; i++ ) {
                if( this.Controls[i] is TourneyMatchUp ) {
                    TourneyMatchUp temp = (TourneyMatchUp)this.Controls[i];
                    clearTheMatchUp(temp);
                }
            }*/
            drawTournamentStart( preliminaries );
        }

        private void clearTheMatchUp(TourneyMatchUp matchup)
        {
            matchup.clear();
            matchup.reset(null, null);
            matchup.Visible = false;
            if (matchup.NextMatch != null)
                clearTheMatchUp(matchup.NextMatch);
        }

        private void startTourneyButton_Click( object sender, EventArgs e ) {
            tabControl.TabPages.Remove( setupPage );
            tabControl.TabPages.Add( playPage );
            tabControl.SelectedTab = tabControl.TabPages[0];
            showTournamentPlay();
        }

        /// <summary>
        /// Will loop through each match-up, if the match up is a bye round,
        /// it will ensure that the concurrent match is set
        /// </summary>
        public void checkTourneyForByeRound()
        {
            for (int i = 0; i < allMatchups.Length; i++)
            {
                if (allMatchups[i].State == MatchState.ByeRound)
                {
                    TourneyMatchUp nextMatch = allMatchups[i].NextMatch;
                    if (nextMatch.RedTeam != allMatchups[i].Winner ||
                        nextMatch.BlueTeam != allMatchups[i].Winner)
                    {
                        nextMatch.addTeamToMatchUp(allMatchups[i].Winner);
                    }
                }
            }
        }
    }
}
