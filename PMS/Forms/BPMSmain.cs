using BPMS.Code;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace BPMS
{
    /// <summary>
    /// Delete type Enum, for different deletion option of queued teams
    /// </summary>
    public enum ListRemoveType
    {
        DISABLED = 0,
        CLICK,
        ID
    };

    public partial class BPMSMain : Form
    {
        /// <summary> Main Variablles </summary>
        public static int teamIDs = 100;

        //Data
        private SystemData systemData;

        //Supplementary Forms
        public ListTeams myListForm = null;
        public Tourney tourneyModeForm = null;

        //Form addins
        public Button[] inLineCloseButtons = new Button[8];
        public TeamTextBox[] inLineTextBoxs = new TeamTextBox[8];

        //Validation
        public string inputPassword;
        public static bool passValid;
        public PasswordRequest requestPass;
        public CreatePassword createPass;

        //random
        public string[] myThemeColors;
        public int theButtonNum;
        public Team recordHolder = null;
        public static DateTime startGame, endGame;

        private bool hasPromptedSave = false;

        /// <summary>
        /// Constructor for new Main Gui
        /// </summary>
        /// <param name="save">Path of the save file to be read from and written to</param>
        public BPMSMain(string save)
        {

            //Create Data
            systemData = new SystemData(save);
            systemData.Mode = SystemMode.MAIN;
            systemData.RecentSave = true;

            //Create Form(s)
            InitializeComponent();
            tourneyModeForm = new Tourney(this);
            tourneyModeForm.Visible = false;

            //Theme the form(s)
            forceTheme(systemData.Theme);

            //setupPasswords
            if( systemData.Password != "" )
            {
                systemData.PassIsSet = true;

                setPasswordToolStripMenuItem1.Enabled = false;
                tourneyModeForm.tourneySetPass.Enabled = false;

                changePasswordToolStripMenuItem1.Enabled = true;
                tourneyModeForm.tourneyChangePassword.Enabled = true;

                deletePasswordToolStripMenuItem.Enabled = true;
                tourneyModeForm.tourneyClearPassword.Enabled = true;
            }

            #region Queue Buttons Setup
            inLineCloseButtons[0] = inlineCancel0;
            inLineCloseButtons[1] = inlineCancel1;
            inLineCloseButtons[2] = inlineCancel2;
            inLineCloseButtons[3] = inlineCancel3;
            inLineCloseButtons[4] = inlineCancel4;
            inLineCloseButtons[5] = inlineCancel5;
            inLineCloseButtons[6] = inlineCancel6;
            inLineCloseButtons[7] = inlineCancel7;

            foreach( Button b in inLineCloseButtons )
            {
                b.Click += new System.EventHandler(inLineClose_Click);
            }

            inLineTextBoxs[0] = (TeamTextBox)inlineBox0;
            inLineTextBoxs[1] = inlineBox1;
            inLineTextBoxs[2] = inlineBox2;
            inLineTextBoxs[3] = inlineBox3;
            inLineTextBoxs[4] = inlineBox4;
            inLineTextBoxs[5] = inlineBox5;
            inLineTextBoxs[6] = inlineBox6;
            inLineTextBoxs[7] = inlineBox7;
            #endregion

            recordStreaklabel.Text = "Longest Streak: n/a";
            recordByLabel.Text = "by: n/a";
        }

        /// <summary> 
        /// Public access to the data for the system
        /// </summary>
        public SystemData Data
        {
            get { return systemData; }
            set { systemData = value; }
        }

        /// <summary> 
        /// Gets the game length 
        /// </summary>
        /// <returns>Returns the timespan of the game</returns>
        /// <!--Currently Not Used-->
        public static TimeSpan gameLength()
        {
            return startGame - endGame;
        }

        /// <summary>  
        /// Updates the Now Playing Fields
        /// </summary>
        private void UpdateNowPlayingDisplay()
        {
            if( systemData.Winner == null )
            {
                leftWinnerButton.Enabled = false;
                winnersPlayer1TexBox.Text = "";
                winnersPlayer2TexBox.Text = "";
                winnersTeamIdTexBox.Text = "";
            }
            else
            {
                leftWinnerButton.Enabled = true;
                winnersTeamIdTexBox.Text = String.IsNullOrEmpty(systemData.Winner.TeamName) ? 
                    systemData.Winner.Id.ToString() : systemData.Winner.TeamName;
                winnersPlayer1TexBox.Text = systemData.Winner.Player1;
                winnersPlayer2TexBox.Text = systemData.Winner.Player2;
            }
            if( systemData.Challenger == null )
            {
                rightWinnerButton.Enabled = false;
                challengersTeamIdTexBox.Text = "";
                challengerPlayer1TexBox.Text = "";
                challengerPlayer2TexBox.Text = "";
            }
            else
            {
                rightWinnerButton.Enabled = true;
                challengersTeamIdTexBox.Text = String.IsNullOrEmpty(systemData.Challenger.TeamName) ? 
                    systemData.Challenger.Id.ToString() : systemData.Challenger.TeamName;
                challengerPlayer1TexBox.Text = systemData.Challenger.Player1;
                challengerPlayer2TexBox.Text = systemData.Challenger.Player2;
            }
        }

        /// <summary> 
        /// Updates the visual components of the queue
        /// </summary>
        private void updateQueuesView()
        {
            //todo update visual boxes
            Team[] upSoon = new Team[systemData.QueuedTeams.Count];
            systemData.QueuedTeams.CopyTo(upSoon, 0);
            for( int i = 0; i < inLineTextBoxs.Length; i++ )
            {
                if( i < systemData.QueuedTeams.Count )
                {
                    inLineTextBoxs[i].Visible = true;
                    inLineCloseButtons[i].Visible = true;
                    inLineTextBoxs[i].Team = upSoon[i];
                    inLineTextBoxs[i].Text = inLineTextBoxs[i].Team.ToString();
                }
                else
                {
                    inLineTextBoxs[i].Visible = false;
                    inLineCloseButtons[i].Visible = false;
                }
            }
        }

        /// <summary>
        /// Updates the record holder, and the associated text as well
        /// </summary>
        private void updateRecordHolder()
        {
            //not finished!!!!!
            if( recordHolder != null )
            {
                if( (systemData.Winner != null) && (systemData.Winner.MaxStreak > recordHolder.MaxStreak) )
                {
                    recordHolder = systemData.Winner;
                }
                recordStreaklabel.Text = "Longest Streak: " + recordHolder.MaxStreak;
                recordByLabel.Text = "by: " + recordHolder.toStringTeamName();
                if( recordStreaklabel.Text == "Longest Streak: 0" )
                    recordStreaklabel.Text = "Longest Streak: 1";
            }
            else
            {
                recordHolder = systemData.Winner;
            }
        }

        ///<summary> 
        ///Refreshes all the views on the main screen
        ///</summary>
        public void updateAllViews()
        {
            UpdateNowPlayingDisplay();
            updateQueuesView();
            updatePositions();
        }

        /// <summary> 
        /// All textfields and buttons get updated with this call 
        /// </summary>
        public void updatePositions()
        {
            if( systemData.Winner == null )
            {
                if( systemData.Challenger != null )
                {
                    systemData.Winner = systemData.Challenger;
                    systemData.Challenger = null;
                }
                else
                {
                    switch( systemData.QueuedTeams.Count )
                    {
                        case 1:
                            systemData.Winner = systemData.QueuedTeams.Dequeue();
                            break;
                        case 0:
                            break;
                        default:
                            systemData.Winner = systemData.QueuedTeams.Dequeue();
                            systemData.Challenger = systemData.QueuedTeams.Dequeue();
                            if( systemData.Winner.Id == systemData.Challenger.Id )
                            {
                                systemData.Challenger = null;
                                updatePositions();
                            }
                            break;
                    }
                }
            }
            else
            {
                if( systemData.Challenger != null )
                {
                    if( systemData.Winner.Id == systemData.Challenger.Id )
                    {
                        systemData.Challenger = null;
                        updatePositions();
                    }
                }
            }

            if( systemData.Challenger == null )
            {
                if( systemData.QueuedTeams.Count >= 1 )
                {
                    systemData.Challenger = systemData.QueuedTeams.Dequeue();
                    if( systemData.Winner.Id == systemData.Challenger.Id )
                    {
                        systemData.Challenger = null;
                        updatePositions();
                    }
                }
            }

            updateQueuesView();
            UpdateNowPlayingDisplay();
            updateRecordHolder();
        }

        /// <summary>  
        /// Helper method to clear all input fields
        /// </summary>
        public void clearInputFields()
        {
            teamIDInputBox.Text = "";
            teamNameInputBox.Text = "";
            teamNameInputBox.BackColor = SystemColors.Window;
            teamIDInputBox.BackColor = SystemColors.Window;
            newTeamNameTextbox.BackColor = SystemColors.Window;
            newTeamNameTextbox.Text = "";
            newTeamP1TextBox.BackColor = SystemColors.Window;
            newTeamP1TextBox.Text = "";
            newTeamP2TextBox.BackColor = SystemColors.Window;
            newTeamP2TextBox.Text = "";
        }

        /// <summary> Deletes all teams, records and data. only thing to persist is save file
        /// </summary>
        public void clearAll()
        {
            passValid = false;

            systemData = new SystemData(systemData.SaveFile);
            recordStreaklabel.Text = "Longest Streak: n/a";
            recordByLabel.Text = "by: n/a";
            teamIDs = 100;

            winnersPlayer1TexBox.Text = "";
            winnersPlayer2TexBox.Text = "";
            winnersTeamIdTexBox.Text = "";

            updateAllViews();
        }

        #region Event Handlers

        /// <summary> Button functionality to remove an item in line
        /// </summary>
        private void inLineClose_Click(object sender, EventArgs e)
        {
            if( systemData.PassToRemove )
            {
                string objectName = ((System.Windows.Forms.Control)(sender)).Name;
                int buttonNum = Convert.ToInt32(objectName.Substring(objectName.Length - 1));
                systemData.removeFromQueue(buttonNum);
                updateQueuesView();
            }
            else
            {
                string objectName = ((System.Windows.Forms.Control)(sender)).Name;
                int buttonNum = Convert.ToInt32(objectName.Substring(objectName.Length - 1));
                theButtonNum = buttonNum;
                requestPass = new PasswordRequest("removeQueue");
                requestPass.Closing += new CancelEventHandler(GetPasswordOnRequstClose);
                requestPass.Show();

            }
        }

        /// <summary> Button will insert a new team into the queue assuming proper params
        /// in associated textbox
        /// </summary>
        private void newTeamButton_Click(object sender, EventArgs e)
        {
            bool isBadArgs = false;

            //If first text box is empty, make it yellow and display error
            if( newTeamP1TextBox.Text == "" )
            {
                newTeamP1TextBox.BackColor = Color.Yellow;
                isBadArgs = true;
            }

            //If second text box is empty, make it yellow and display error
            if( newTeamP2TextBox.Text == "" )
            {
                newTeamP2TextBox.BackColor = Color.Yellow;
                isBadArgs = true;
            }

            //If Team name already exists, make it orange and display error
            foreach( Team team in systemData.AllTeams )
            {
                if( team.TeamName == newTeamNameTextbox.Text || team.TeamName == newTeamNameTextbox.Text )
                {
                    newTeamNameTextbox.BackColor = Color.Orange;
                    newTeamNameTextbox.Text = "<In use: " + team.TeamName + ">";
                    isBadArgs = true;
                    break;
                }
            }

            if( !isBadArgs )
            {
                Team temp = new Team(newTeamP1TextBox.Text, newTeamP2TextBox.Text);

                if( newTeamNameTextbox.Text != "" )
                {
                    temp.TeamName = newTeamNameTextbox.Text;
                }

                newTeamNameTextbox.Text = "";
                systemData.QueuedTeams.Enqueue(temp);
                systemData.AllTeams.Add(temp);

                updatePositions();
                clearInputFields();

                if( myListForm != null )
                {
                    myListForm.configreNamesFromTeamInfo();
                }
            }
            Data.RecentSave = false;
            if( Visible == true )
                newTeamP1TextBox.Focus();
        }

        /// <summary>  Button will insert a team that has already played before enabling
        /// them to keep stats, once again assuming they have the proper input
        /// from associated text field
        /// </summary>
        private void queueTeamButton_Click(object sender, EventArgs e)
        {

            int boxState = 0;
            bool isBadArgs = false;
            int teamcode = 0;
            string teamname = "";
            Team temp = null, temp2 = null;

            //Determine which comibination of fields are highlighted
            if( teamIDInputBox.Text == "" && teamNameInputBox.Text == "" )
            {
                boxState = 1;
            }
            else
            {
                if( teamIDInputBox.Text == "" )
                {
                    boxState = 3;
                }
                else if( teamNameInputBox.Text == "" )
                {
                    boxState = 2;
                }
                else
                {
                    boxState = 4;
                }
            }

            switch( boxState )
            {
                case 1: //Both empty
                    teamIDInputBox.BackColor = Color.Yellow;
                    teamNameInputBox.BackColor = Color.Yellow;
                    isBadArgs = true;
                    break;
                case 2: //Just ID
                    try
                    {
                        teamcode = Convert.ToInt32(teamIDInputBox.Text);
                    }
                    catch( Exception )
                    {
                        teamIDInputBox.BackColor = Color.Red;
                        teamNameInputBox.BackColor = SystemColors.Window;
                        isBadArgs = true;
                    }


                    //Go through the list of teams and look for one w/ the same ID
                    foreach( Team t in systemData.AllTeams )
                    {
                        if( t.Id == teamcode )
                        {
                            temp = t;
                            break;
                        }
                    }

                    //If we havent found it display error colors
                    if( temp == null )
                    {
                        teamIDInputBox.BackColor = Color.Orange;
                        teamNameInputBox.BackColor = SystemColors.Window;
                        isBadArgs = true;
                    }

                    if( Visible == true )
                        teamIDInputBox.Focus();
                    else
                        tourneyModeForm.tourneyReturnIDtextBox.Focus();
                    break;
                case 3: //Just Name
                    teamname = teamNameInputBox.Text;

                    /////////////Konami Easter Egg ///////////////
                    if( teamname == "uuddlrlrba" || teamname == "upupdowndownleftrightleftrightba" )
                    {
                        systemData.AddContraTeam();
                        teamNameInputBox.Text = "";
                        pictureBox1.Image = new Bitmap(BPMS.Properties.Resources.bill_lance_code);
                        temp = systemData.getTeam("Contra");
                    }
                    else
                    {
                        ///////////End Easter egg, do real function///
                        try
                        {
                            if( !systemData.hasTeam(teamname) )
                            {
                                teamNameInputBox.BackColor = Color.Orange;
                                isBadArgs = true;
                            }
                            else
                            {
                                temp = systemData.getTeam(teamname);
                            }
                        }
                        catch( Exception )
                        {
                            teamNameInputBox.BackColor = Color.Red;
                            teamIDInputBox.BackColor = SystemColors.Window;
                            isBadArgs = true;
                        }
                    }
                    if( Visible == true )
                        teamNameInputBox.Focus();
                    break;
                case 4: //Both
                    try
                    {
                        teamcode = Convert.ToInt32(teamIDInputBox.Text);
                        teamname = teamNameInputBox.Text;
                    }
                    catch( Exception )
                    {
                        teamIDInputBox.BackColor = Color.Red;
                        teamNameInputBox.BackColor = Color.Red;
                        isBadArgs = true;
                    }
                    try
                    {
                        temp = systemData.AllTeams[teamcode];
                    }
                    catch( Exception )
                    {
                        teamIDInputBox.BackColor = Color.Orange;
                        teamNameInputBox.BackColor = SystemColors.Window;
                        isBadArgs = true;
                    }
                    try
                    {
                        if( !systemData.hasTeam(teamname) )
                        {
                            teamNameInputBox.BackColor = Color.Orange;
                        }
                        else
                        {
                            temp2 = systemData.getTeam(teamname);
                        }
                    }
                    catch( Exception )
                    {
                        teamNameInputBox.BackColor = Color.Red;
                        teamIDInputBox.BackColor = SystemColors.Window;
                    }

                    if( (temp == null || temp2 == null) || temp.Id != temp2.Id )
                    {
                        teamNameInputBox.BackColor = Color.Olive;
                        teamIDInputBox.BackColor = Color.Olive;
                        isBadArgs = true;
                    }
                    if( Visible == true )
                        teamNameInputBox.Focus();
                    break;
            }



            if( !allowMultiQueueToolStripMenuItem.Checked )
            {
                if( systemData.QueuedTeams.Contains(temp) )
                {
                    teamIDInputBox.BackColor = Color.Orange;
                    teamNameInputBox.BackColor = Color.Orange;
                    teamIDInputBox.Text = "No Multi Queue";
                    isBadArgs = true;
                }
            }

            if( !isBadArgs )
            {
                if( systemData.Challenger != null )
                {
                    if( temp.Id == systemData.Winner.Id || temp.Id == systemData.Challenger.Id )
                    {
                        teamIDInputBox.BackColor = Color.Orange;
                        teamNameInputBox.BackColor = Color.Orange;
                        isBadArgs = true;
                    }
                }
            }
            if( !isBadArgs )
            {
                systemData.QueuedTeams.Enqueue(temp);
                clearInputFields();
                updatePositions();
            }
        }

        /// <summary> When Winnner Circles win again </summary>
        private void leftWinnerButton_Click(object sender, EventArgs e)
        {
            if( systemData.Challenger != null )
            {
                systemData.Winner.winGame();
                systemData.Challenger.looseGame();
            }
            systemData.Challenger = null;
            if( systemData.QueuedTeams.Count != 0 )
                systemData.Challenger = systemData.QueuedTeams.Dequeue();
            updatePositions();
            Data.RecentSave = false;
        }

        /// <summary> When Challengers win
        /// </summary>
        private void rightWinnerButton_Click(object sender, EventArgs e)
        {
            systemData.Challenger.winGame();
            systemData.Winner.looseGame();
            systemData.Winner = systemData.Challenger;
            systemData.Challenger = null;
            if( systemData.QueuedTeams.Count != 0 )
                systemData.Challenger = systemData.QueuedTeams.Dequeue();
            updatePositions();
            Data.RecentSave = false;
        }

        /// <summary> Helper function to verify if players are already in a match
        /// </summary>
        private bool eitherCurPlaying(Team one, Team two)
        {
            if( systemData.Winner == null && systemData.Challenger == null )
            {
                return false;
            }
            else
            {
                if( systemData.Winner == one || systemData.Winner == two || systemData.Challenger == one || systemData.Challenger == two )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>  Menu: File > Quit
        /// </summary>
        public void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //systemData.saveData();
            //this.Dispose();
            PromptSaveClose(null, null);
        }

        public void saveData_click(object sender, EventArgs e)
        {
            systemData.saveData();
        }

        /// <summary>
        /// Overrride for the red [x] close button, will prompt if user wants
        /// to save the data
        /// </summary>
        public void PromptSaveClose(object sender, FormClosingEventArgs e)
        {
            // Confirm user wants to close
            if( !Data.RecentSave )
            {
                if( !hasPromptedSave )
                {
                    hasPromptedSave = true;
                    DialogResult d = MessageBox.Show(this, "Do you wish to save?", "There is unsaved data", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if( d == DialogResult.Yes )
                    {
                        systemData.saveData();
                    }
                }
            }
            Application.Exit();
        }

        /// <summary> Menu: File > New Night
        /// *PASS PROTECT*
        /// </summary>
        private void newNightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if( !systemData.PassIsSet )
            {
                clearAll();
            }
            else
            {
                //TODO Create a Password Prompt
                requestPass = new PasswordRequest("newNight");
                requestPass.Closing += new CancelEventHandler(GetPasswordOnRequstClose);
                requestPass.Show();
            }
        }

        public void File_Clear_Stats(object sender, EventArgs e)
        {
            if( !systemData.PassIsSet )
            {
                foreach( Team t in systemData.AllTeams )
                {
                    t.destroyStats();
                }
                if( systemData.Winner != null )
                    systemData.Winner.destroyStats();
                if( systemData.Challenger != null )
                    systemData.Challenger.destroyStats();
                if( recordHolder != null )
                    recordHolder.destroyStats();
            }
            else
            {
                //TODO Create a Password Prompt
                requestPass = new PasswordRequest("noStats");
                requestPass.Closing += new CancelEventHandler(GetPasswordOnRequstClose);
                requestPass.Show();
            }
        }

        private void File_Clear_Queue(object sender, EventArgs e)
        {
            if( !systemData.PassIsSet )
            {
                //Clear Queue
                systemData.QueuedTeams = new Queue<Team>();
                updateQueuesView();
            }
            else
            {
                //TODO Create a Password Prompt
                requestPass = new PasswordRequest("noQueue");
                requestPass.Closing += new CancelEventHandler(GetPasswordOnRequstClose);
                requestPass.Show();
            }
        }

        private void File_Clear_All_ButStats(object sender, EventArgs e)
        {
            if( !systemData.PassIsSet )
            {
                //Clear PPl
                systemData.QueuedTeams = new Queue<Team>();
                if( systemData.Winner != null )
                {
                    systemData.Winner.looseGame();
                    systemData.Winner = null;
                }
                if( systemData.Challenger != null )
                {
                    systemData.Challenger.looseGame();
                    systemData.Challenger = null;
                }
                winnersPlayer1TexBox.Text = "";
                winnersPlayer2TexBox.Text = "";
                winnersTeamIdTexBox.Text = "";
                updateAllViews();
            }
            else
            {
                //TODO Create a Password Prompt
                requestPass = new PasswordRequest("noPeople");
                requestPass.Closing += new CancelEventHandler(GetPasswordOnRequstClose);
                requestPass.Show();
            }
        }

        private void isMultiQueueEnabledclick(object sender, EventArgs e)
        {
            if( !systemData.PassIsSet )
            {
                if( !allowMultiQueueToolStripMenuItem.Checked )
                {
                    allowMultiQueueToolStripMenuItem.Checked = false;
                    systemData.MulitQueue = false;
                }
                else
                {
                    allowMultiQueueToolStripMenuItem.Checked = true;
                    systemData.MulitQueue = true;
                }
            }
            else
            {
                requestPass = new PasswordRequest("multiQueue");
                requestPass.Closing += new CancelEventHandler(GetPasswordOnRequstClose);
                requestPass.Show();
            }
        }

        public void Set_Password_MenuItem(object sender, EventArgs e)
        {
            //TODO Create a Password Prompt
            createPass = new CreatePassword();
            createPass.Closing += new CancelEventHandler(GetCreationOnRequestClose);
            createPass.Show();
        }

        private void GetCreationOnRequestClose(object sender, CancelEventArgs e)
        {
            CreatePassword theform = (CreatePassword)sender;
            string function = theform.myFunction;
            string theNewPass = theform.pass1;
            if( function == "createPass" )
            {
                systemData.PassIsSet = true;
                systemData.Password = theNewPass;

                changePasswordToolStripMenuItem1.Enabled = true;
                tourneyModeForm.tourneyChangePassword.Enabled = true;

                deletePasswordToolStripMenuItem.Enabled = true;
                tourneyModeForm.tourneyClearPassword.Enabled = true;

                setPasswordToolStripMenuItem1.Enabled = false;
                tourneyModeForm.tourneySetPass.Enabled = false;

                passwordRequiredToolStripMenuItem.Enabled = true;
            }
            else
            {
                changePasswordToolStripMenuItem1.Enabled = false;
                tourneyModeForm.tourneyChangePassword.Enabled = false;

                deletePasswordToolStripMenuItem.Enabled = false;
                tourneyModeForm.tourneyClearPassword.Enabled = false;

                setPasswordToolStripMenuItem1.Enabled = true;
                tourneyModeForm.tourneySetPass.Enabled = true;

                passwordRequiredToolStripMenuItem.Enabled = false;
            }
        }

        public void change_pass_MenuItem(object sender, EventArgs e)
        {
            requestPass = new PasswordRequest("changeMyPass");
            requestPass.Closing += new CancelEventHandler(GetPasswordOnRequstClose);
            requestPass.Show();
        }

        private void IremovePassReqMenu(object sender, EventArgs e)
        {
            if( !systemData.PassIsSet )
            {
                passwordRequiredToolStripMenuItem.Checked = true;
                passwordRequiredToolStripMenuItem.Enabled = false;

                deletePasswordToolStripMenuItem.Checked = false;
                deletePasswordToolStripMenuItem.Enabled = true;
                justClickToolStripMenuItem.Checked = false;
                justClickToolStripMenuItem.Enabled = true;
                systemData.PassToRemove = false;
                foreach( Button b in inLineCloseButtons )
                {
                    b.Enabled = true;
                }
            }
            else
            {
                requestPass = new PasswordRequest("removableByPass");
                requestPass.Closing += new CancelEventHandler(GetPasswordOnRequstClose);
                requestPass.Show();
            }
        }

        private void IremoveClickReqMenu(object sender, EventArgs e)
        {
            if( !systemData.PassIsSet )
            {
                justClickToolStripMenuItem.Checked = true;
                justClickToolStripMenuItem.Enabled = false;

                passwordRequiredToolStripMenuItem.Checked = false;
                if( systemData.PassIsSet )
                    passwordRequiredToolStripMenuItem.Enabled = true;
                deletePasswordToolStripMenuItem.Checked = false;
                deletePasswordToolStripMenuItem.Enabled = true;
                systemData.PassToRemove = true;
                foreach( Button b in inLineCloseButtons )
                {
                    b.Enabled = true;
                }
            }
            else
            {
                requestPass = new PasswordRequest("removableByClick");
                requestPass.Closing += new CancelEventHandler(GetPasswordOnRequstClose);
                requestPass.Show();
            }
        }

        private void IremoveDisableReqMenu(object sender, EventArgs e)
        {
            if( !systemData.PassIsSet )
            {
                deletePasswordToolStripMenuItem.Checked = true;
                deletePasswordToolStripMenuItem.Enabled = false;

                passwordRequiredToolStripMenuItem.Checked = false;
                if( systemData.PassIsSet )
                    passwordRequiredToolStripMenuItem.Enabled = true;
                justClickToolStripMenuItem.Checked = false;
                justClickToolStripMenuItem.Enabled = true;
                systemData.PassToRemove = true;
                foreach( Button b in inLineCloseButtons )
                {
                    b.Enabled = false;
                }
            }
            else
            {
                requestPass = new PasswordRequest("removableByDisable");
                requestPass.Closing += new CancelEventHandler(GetPasswordOnRequstClose);
                requestPass.Show();
            }
        }

        /// <summary>
        /// Calls the password validation winform with the function arguemnt 'clear'.
        /// Provided proper password will call the Closing event handler and process
        /// clearing of the password fields
        /// </summary>
        public void clearPassword_MenuItem(object sender, EventArgs e)
        {
            requestPass = new PasswordRequest("clearMyPass");
            requestPass.Closing += new CancelEventHandler(GetPasswordOnRequstClose);
            requestPass.Show();
        }

        /// <summary> Processes Data after closing a password request
        /// </summary>
        private void GetPasswordOnRequstClose(object sender, CancelEventArgs e)
        {
            PasswordRequest theform = (PasswordRequest)sender;
            inputPassword = theform.myPassword;
            string theFunction = theform.myFunction;
            if( systemData.Password == inputPassword )
            {
                passValid = true;
            }
            else
            {
                passValid = false;
            }
            if( passValid )
            {
                switch( theFunction )
                {
                    case "newNight":
                        //Prompt Password
                        clearAll();
                        break;
                    case "deleteTeam":
                        //string result = Data.deleteTeam( myListForm.getSelectedTeam() );
                        //updateAllViews();
                        //tourneyModeForm.updateTourneySeedObjects();
                        //myListForm.configreNamesFromTeamInfo();
                        //myListForm.listActionsResultsBox.Text = result;
                        break;
                    case "noStats":
                        foreach( Team t in systemData.AllTeams )
                        {
                            t.destroyStats();
                        }
                        if( systemData.Winner != null )
                            systemData.Winner.destroyStats();
                        if( systemData.Challenger != null )
                            systemData.Challenger.destroyStats();
                        if( recordHolder != null )
                            recordHolder.destroyStats();
                        break;
                    case "noQueue":
                        systemData.QueuedTeams = new Queue<Team>();
                        updateQueuesView();
                        break;
                    case "noPeople":
                        systemData.QueuedTeams = new Queue<Team>();
                        if( systemData.Winner != null )
                        {
                            systemData.Winner.looseGame();
                            systemData.Winner = null;
                        }
                        if( systemData.Challenger != null )
                        {
                            systemData.Challenger.looseGame();
                            systemData.Challenger = null;
                        }
                        winnersPlayer1TexBox.Text = "";
                        winnersPlayer2TexBox.Text = "";
                        winnersTeamIdTexBox.Text = "";
                        updateAllViews();
                        break;
                    case "clearMyPass":
                        setPasswordToolStripMenuItem1.Enabled = true;
                        tourneyModeForm.tourneySetPass.Enabled = true;

                        changePasswordToolStripMenuItem1.Enabled = false;
                        tourneyModeForm.tourneyChangePassword.Enabled = false;

                        deletePasswordToolStripMenuItem.Enabled = false;
                        tourneyModeForm.tourneyClearPassword.Enabled = false;
                        systemData.PassIsSet = false;
                        passValid = false;
                        break;
                    case "changeMyPass":
                        createPass = new CreatePassword();
                        createPass.Closing += new CancelEventHandler(GetCreationOnRequestClose);
                        createPass.Show();
                        break;
                    case "multiQueue":
                        if( !allowMultiQueueToolStripMenuItem.Checked )
                        {
                            allowMultiQueueToolStripMenuItem.Checked = false;
                            systemData.MulitQueue = false;
                        }
                        else
                        {
                            allowMultiQueueToolStripMenuItem.Checked = true;
                            systemData.MulitQueue = true;
                        }
                        break;
                    case "removableByPass":
                        passwordRequiredToolStripMenuItem.Checked = true;
                        passwordRequiredToolStripMenuItem.Enabled = false;

                        disabledToolStripMenuItem.Checked = false;
                        disabledToolStripMenuItem.Enabled = true;
                        justClickToolStripMenuItem.Checked = false;
                        justClickToolStripMenuItem.Enabled = true;
                        systemData.PassToRemove = false;
                        foreach( Button b in inLineCloseButtons )
                        {
                            b.Enabled = true;
                        }
                        break;
                    case "removableByClick":
                        justClickToolStripMenuItem.Checked = true;
                        justClickToolStripMenuItem.Enabled = false;

                        passwordRequiredToolStripMenuItem.Checked = false;
                        passwordRequiredToolStripMenuItem.Enabled = true;
                        disabledToolStripMenuItem.Checked = false;
                        disabledToolStripMenuItem.Enabled = true;
                        systemData.PassToRemove = true;
                        foreach( Button b in inLineCloseButtons )
                        {
                            b.Enabled = true;
                        }
                        break;
                    case "removableByDisable":
                        disabledToolStripMenuItem.Checked = true;
                        disabledToolStripMenuItem.Enabled = false;

                        passwordRequiredToolStripMenuItem.Checked = false;
                        passwordRequiredToolStripMenuItem.Enabled = true;
                        justClickToolStripMenuItem.Checked = false;
                        justClickToolStripMenuItem.Enabled = true;
                        systemData.PassToRemove = true;
                        foreach( Button b in inLineCloseButtons )
                        {
                            b.Enabled = false;
                        }
                        break;
                    case "removeQueue":
                        systemData.removeFromQueue(theButtonNum);
                        break;
                }
                updatePositions();
            }
        }

        #endregion

        /// <summary> Displays a List of all the teams in the memory. Will allow 
        /// Queuing teams, and deleting teams (only way to actually delete).
        /// </summary>
        public void File_ListTeams_ClickHandler(object sender, EventArgs e)
        {
            if( myListForm == null )
            {
                myListForm = new ListTeams(this);
                myListForm.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                myListForm.Location = new Point((Location.X), (Location.Y));
                myListForm.Visible = true;
            }
            else
            {
                myListForm.BringToFront();
            }
        }

        /// <summary> Displays the about this software form
        /// </summary>
        public void aboutMenuItemClickEvent(object sender, EventArgs e)
        {
            AboutPMS about = new AboutPMS();
            about.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            about.ShowDialog(this);
        }

        /// <summary> 
        /// Event handler for clicking the Donate "Button"
        /// </summary>
        public void DonateButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.paypal.me/rbucinell");
        }

        /// <summary> Event handeler for dpressing mouse over donate button
        /// </summary>
        private void mouseDownDonateClick(object sender, MouseEventArgs e)
        {
            donatePixButton.BackColor = System.Drawing.Color.Transparent;
            donatePixButton.Image = global::BPMS.Properties.Resources.donate_down;
        }

        /// <summary> Event handeler for releasing mouse over donate button
        /// </summary>
        private void mouseDonateClick(object sender, MouseEventArgs e)
        {
            donatePixButton.BackColor = System.Drawing.Color.Transparent;
            donatePixButton.Image = global::BPMS.Properties.Resources.donate;
        }

        /// <summary> Displays the FAQ form
        /// </summary>
        public void showMeFAQmenuClick(object sender, EventArgs e)
        {
            if( systemData.Mode == SystemMode.MAIN )
            {
                FAQsForm faq = new FAQsForm();
                faq.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                faq.ShowDialog(this);//.Show( new FAQsForm() );
            }
            else if( systemData.Mode == SystemMode.TOURNEY_SETUP )
            {
                TourneyFAQsForm faq = new TourneyFAQsForm();
                faq.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                faq.ShowDialog(this);//.Show( new FAQsForm() );
            }
        }

        private void ThemeClicked(object sender, EventArgs e)
        {

            deafaultToolStripMenuItem.Checked = false;
            redToolStripMenuItem.Checked = false;
            blueToolStripMenuItem.Checked = false;
            yellowToolStripMenuItem.Checked = false;
            greenToolStripMenuItem.Checked = false;
            blackToolStripMenuItem.Checked = false;
            tKEToolStripMenuItem.Checked = false;

            ToolStripMenuItem clicked = (ToolStripMenuItem)sender;
            clicked.Checked = true;

            ThemePreset clickedTheme = ThemePreset.Default;

            switch( clicked.Name )
            {
                case "deafaultToolStripMenuItem":
                    clickedTheme = ThemePreset.Default;
                    break;
                case "redToolStripMenuItem":
                    clickedTheme = ThemePreset.Red;
                    break;
                case "blueToolStripMenuItem":
                    clickedTheme = ThemePreset.Blue;
                    break;
                case "yellowToolStripMenuItem":
                    clickedTheme = ThemePreset.Yellow;
                    break;
                case "greenToolStripMenuItem":
                    clickedTheme = ThemePreset.Green;
                    break;
                case "blackToolStripMenuItem":
                    clickedTheme = ThemePreset.Black;
                    break;
                case "tKEToolStripMenuItem":
                    clickedTheme = ThemePreset.TKE;
                    break;
                default:
                    clickedTheme = ThemePreset.Default;
                    break;

            }
            Theme theme = Theme.GetTheme(clickedTheme);
            systemData.Theme = theme.Name;
            if( systemData.Theme != "default" )
            {
                myThemeColors = theme.ThemeColors();
                ApplyTheme(theme);
            }
        }

        public void forceTheme(string theme)
        {
            deafaultToolStripMenuItem.Checked = false;
            redToolStripMenuItem.Checked = false;
            blueToolStripMenuItem.Checked = false;
            yellowToolStripMenuItem.Checked = false;
            greenToolStripMenuItem.Checked = false;
            blackToolStripMenuItem.Checked = false;
            tKEToolStripMenuItem.Checked = false;
            object force = new object();
            switch( theme )
            {
                case "default":
                    force = deafaultToolStripMenuItem;
                    break;
                case "red":
                    force = redToolStripMenuItem;
                    break;
                case "blue":
                    force = blueToolStripMenuItem;
                    break;
                case "yellow":
                    force = yellowToolStripMenuItem;
                    break;
                case "green":
                    force = greenToolStripMenuItem;
                    break;
                case "black":
                    force = blackToolStripMenuItem;
                    break;
                case "tke":
                    force = tKEToolStripMenuItem;
                    break;
                default:
                    force = deafaultToolStripMenuItem;
                    break;
            }
            EventArgs ea = EventArgs.Empty;
            ThemeClicked(force, ea);

        }

        public void ApplyTheme(Theme theme)
        {
            menuStrip1.BackColor = theme.MenuBackColor;
            menuStrip1.ForeColor = theme.MenuForeColor;
            foreach( ToolStripMenuItem tool in menuStrip1.Items )
            {
                tool.BackColor = theme.MenuBackColor;
                tool.ForeColor = theme.MenuForeColor;
                foreach( ToolStripMenuItem drop in tool.DropDownItems )
                {
                    drop.BackColor = theme.MenuBackColor;
                    drop.ForeColor = theme.MenuForeColor;
                }
            }

            #region Personal Invitation
            statsToolStripMenuItem1.BackColor = theme.MenuBackColor;
            statsToolStripMenuItem1.ForeColor = theme.MenuForeColor;
            queueToolStripMenuItem1.BackColor = theme.MenuBackColor;
            queueToolStripMenuItem1.ForeColor = theme.MenuForeColor;
            allleaveStatsToolStripMenuItem.BackColor = theme.MenuBackColor;
            allleaveStatsToolStripMenuItem.ForeColor = theme.MenuForeColor;
            allleaveStatsToolStripMenuItem.BackColor = theme.MenuBackColor;
            allleaveStatsToolStripMenuItem.ForeColor = theme.MenuForeColor;
            passwordRequiredToolStripMenuItem.BackColor = theme.MenuBackColor;
            passwordRequiredToolStripMenuItem.ForeColor = theme.MenuForeColor;
            disabledToolStripMenuItem.BackColor = theme.MenuBackColor;
            disabledToolStripMenuItem.ForeColor = theme.MenuForeColor;
            setPasswordToolStripMenuItem1.BackColor = theme.MenuBackColor;
            setPasswordToolStripMenuItem1.ForeColor = theme.MenuForeColor;
            changePasswordToolStripMenuItem1.BackColor = theme.MenuBackColor;
            changePasswordToolStripMenuItem1.ForeColor = theme.MenuForeColor;
            deletePasswordToolStripMenuItem.BackColor = theme.MenuBackColor;
            deletePasswordToolStripMenuItem.ForeColor = theme.MenuForeColor;
            #endregion

            //Color main area
            BackColor = theme.MainBackColor;
            ForeColor = theme.MainForeColor;
            teamNameLabel.ForeColor = theme.MainForeColor;
            teamIDLabel.ForeColor = theme.MainForeColor;
            label18.ForeColor = theme.MainForeColor;

            newTeamButton.BackColor = theme.NewTeamButtonBackColor;
            newTeamButton.ForeColor = theme.NewTeamButtonForeColor;

            queueTeamButton.BackColor = theme.QueueTeamButtonBackColor;
            queueTeamButton.ForeColor = theme.QueueTeamButtonForeColor;

            leftWinnerButton.BackColor = theme.WinnerButtonBackColor;
            leftWinnerButton.ForeColor = theme.WinnerButtonForeColor;

            rightWinnerButton.BackColor = theme.WinnerButtonBackColor;
            rightWinnerButton.ForeColor = theme.WinnerButtonForeColor;

            whosPlayingPanel.BackColor = theme.WhosPlayingBackColor;

        }

        #region MouseOver Functions

        private void recordGlideOn(object sender, EventArgs e)
        {
            Label theSender = (Label)sender;
            if( recordHolder != null )
            {
                theSender.Text = recordHolder.toStringPlayers();
            }
        }

        private void recordGlideOff(object sender, EventArgs e)
        {
            Label theSender = (Label)sender;
            if( recordHolder != null )
            {
                theSender.Text = "by: " + recordHolder.toStringTeamName();
            }
        }
        #endregion


        public void switchMode(object sender, EventArgs e)
        {
            SystemMode temp = systemData.Mode;
            if( systemData.Mode == SystemMode.MAIN )
            {
                systemData.Mode = SystemMode.TOURNEY_SETUP;
                DialogResult dr;
                //Push the line into tournament as a grace to the user iff tournament is empty
                bool isEmpty = true;
                for( int i = 0; i < Data.InTourney.Length; i++ )
                {
                    if( Data.InTourney[i] != null )
                    {
                        isEmpty = false;
                        break;
                    }
                }

                //if its empty populate the tournament
                if( isEmpty )
                {
                    if( Data.Winner != null )
                    {

                        dr = MessageBox.Show("Would you like to transfer your current teams into the tournament?",
                                                   "Transfer Teams?",
                                                   MessageBoxButtons.YesNo);
                    }
                    else
                    {
                        dr = DialogResult.No;
                    }
                    if( dr == DialogResult.Yes )
                    {
                        int index = 0;
                        if( Data.Winner != null )
                        {
                            Data.InTourney[index++] = Data.Winner;
                            Data.Winner = null;
                            if( Data.Challenger != null )
                            {
                                Data.InTourney[index++] = Data.Challenger;
                                Data.Challenger = null;
                            }
                        }
                        for( ; index < Data.InTourney.Length; index++ )
                        {
                            if( Data.QueuedTeams.Count != 0 )
                            {
                                Data.InTourney[index] = Data.QueuedTeams.Dequeue();
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }

                tourneyModeForm.updateTourneySeedObjects();
                tourneyModeForm.Visible = true;
                Visible = false;
                tourneyModeForm.TabControl.SelectedIndex = 0;
                tourneyModeForm.Location = Location;
                tourneyModeForm.BringToFront();
            }
            else if( systemData.Mode == SystemMode.TOURNEY_SETUP )
            {
                updateAllViews();
                systemData.Mode = SystemMode.MAIN;
                tourneyModeForm.Visible = false;
                Visible = true;
                Location = tourneyModeForm.Location;
                BringToFront();
            }
        }
    }
}
