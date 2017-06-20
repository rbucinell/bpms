using System;
using System.Drawing;
using System.Windows.Forms;

namespace BPMS
{

    public enum MatchState {
        Empty,
        NoVictor,
        ByeRound,
        VictorFound
    };

    public class TourneyMatchUp : Panel
    {
        public const int MATCH_HEIGHT = 121;
        public const int MATCH_WIDTH = 55;

        private Panel redZone, blueZone;
        private TeamTextBox redTeamBox, blueTeamBox;
        private Button redWin, blueWin;
        private bool recentlyModified;
        public Button back;

        public bool isFinalsMatch;

        public Tourney MyParent { get; set; }
        public Team Winner { get; set; }
        public Team RedTeam { get; set; }
        public Team BlueTeam { get; set; }
        public MatchState State { get; set; }

        public TourneyMatchUp NextMatch { get; set; }
        public TourneyMatchUp PreviousTopMatch { get; set; }
        public TourneyMatchUp PreviousBottomMatch { get; set; }

        public static int counter = 0;

        #region Construction
        /// <summary> Constructor
        /// </summary>
        /// <param name="red">Team to be placed in the red team position</param>
        /// <param name="blue">Team to be placed in the blue team position</param>
        public TourneyMatchUp( Team red = null, Team blue=null ): base() {
            setupAll( red, blue );
            counter++;
        }

        public TourneyMatchUp() : base()
        {
            setupAll(null, null);
            counter++;
        }

        /// <summary> Private constructor helper to setup all components
        /// </summary>
        private void setupAll( Team red, Team blue ) {
            RedTeam = red;
            BlueTeam = blue;
            Winner = null;
            recentlyModified = true;
            NextMatch = null;
            PreviousTopMatch = null;
            PreviousBottomMatch = null;
            back = new Button();
            //sets the state of the control
            setState();

            //Instantiates controls contained within this panel
            instantiateControls();            

            //Sets Design information about Panel
            constructTourneyMatchUp();

            //default to hide the panel until ready
            Visible = false;
        }

        /// <summary> Private constructor helper to setup all components
        /// </summary>
        private void instantiateControls() {
            redZone = new Panel();
            blueZone = new Panel();
            back = new Button();

            redTeamBox = new TeamTextBox( RedTeam );
            blueTeamBox = new TeamTextBox( BlueTeam );
            redTeamBox.IsSimple = true;
            blueTeamBox.IsSimple = true;

            redWin = new Button();
            blueWin = new Button();
            back = new Button();
        }

        /// <summary> Constructs all of the controls
        /// </summary>
        private void constructTourneyMatchUp() {
            BorderStyle = BorderStyle.Fixed3D;
            BackColor = Color.Gray;
            Size = new Size( MATCH_HEIGHT, MATCH_WIDTH );
            
            redZone.Parent = this;
            redZone.Location = new Point( redZone.Parent.Location.X, redZone.Parent.Location.Y );
            redZone.Size = new Size( 118, 26 );
            redZone.BackColor = Color.Red;

            blueZone.Parent = this;
            blueZone.Location = new Point( blueZone.Parent.Location.X, blueZone.Parent.Location.Y + blueZone.Parent.Height / 2 );
            blueZone.Size = new Size( 118, 30 );
            blueZone.BackColor = Color.Blue;
            
            back.Size = new Size( 20, 20 );
            back.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            back.BackColor = Color.Yellow;
            back.Text = "X";
            back.Visible = false;
                       
            redTeamBox.Parent = redZone;
            redTeamBox.Location = new Point( redTeamBox.Parent.Location.X + 2, redTeamBox.Parent.Location.Y + 3 );
            redTeamBox.Size = new Size( 91, 23 );

            blueTeamBox.Parent = blueZone;
            blueTeamBox.Location = new Point( redTeamBox.Location.X, redTeamBox.Location.Y ); 
            blueTeamBox.Size = new Size( 91, 23 );

            redWin.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            redWin.Size = new Size( 23, 21 );
            redWin.Name = "redWin";
            redWin.Location = new Point( redTeamBox.Location.X + redTeamBox.Width + 1, redTeamBox.Location.Y );
            redWin.BackColor = Color.Green;
            redWin.Parent = redZone;
            redWin.Text = ">";
            redWin.Enabled = true;
            redWin.Visible = true;

            blueWin.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            blueWin.Size = new Size( 23, 21 );
            blueWin.Name = "blueWin";
            blueWin.Location = new Point( blueTeamBox.Location.X + blueTeamBox.Width + 1, blueTeamBox.Location.Y);
            blueWin.Text = ">";
            blueWin.Parent = blueZone;
            blueWin.BackColor = Color.Green;
            blueWin.Enabled = true;
            blueWin.Visible = true;

            back.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
            back.Size = new Size( 23, 23 );
            back.Name = "back";
            back.Location = new Point( blueTeamBox.Location.X + blueTeamBox.Width + 1, blueTeamBox.Location.Y - 2 );
            back.BackColor = Color.Yellow;
            back.Text = "X";
            back.Visible = false;
            back.Enabled = false;

            redrawComponents();

            blueWin.Click += new EventHandler( button_click );
            redWin.Click += new EventHandler( button_click );
            back.Click += new EventHandler( button_click );

        }
        #endregion

        /// <summary>
        /// Resests the match, reverting any actions taken during the match back to the begining state.
        /// </summary>
        public void resetMatch()
        {
            //sets flag that signal system that object has been modified recently
            recentlyModified = true;
           
            switch( State )
            {
                case MatchState.ByeRound:
                    //Get the winner by default, does not affect their win/loss record
                    Winner = (RedTeam == null) ? BlueTeam : RedTeam;
                    State = MatchState.ByeRound;
                    break;
                case MatchState.VictorFound:
                    //First we must fix the team's records
                    if (Winner == RedTeam)
                    {
                        RedTeam.undoWin();
                        BlueTeam.undoLoss();

                    }
                    else
                    {
                        BlueTeam.undoWin();
                        RedTeam.undoLoss();
                    }
                    //Clear the winner 
                    Winner = null;
                    //set the state back to no victor found
                    State = MatchState.NoVictor;
                    break;
                case MatchState.NoVictor:
                case MatchState.Empty:
                    break;
            }
            redrawComponents();
        }

        /// <summary>
        /// Sets the match
        /// </summary>
        /// <param name="red"></param>
        /// <param name="blue"></param>
        public void setMatch(Team red, Team blue)
        {
            Winner = null;
            RedTeam = red;
            BlueTeam = blue;

            //Once teams have been set, set the appropriate state
            if (RedTeam == null)
            {
                if (BlueTeam == null)
                {
                    State = MatchState.Empty;
                }
                else
                {
                    State = MatchState.ByeRound;
                    Winner = BlueTeam;
                }
            }
            else
            {
                if (BlueTeam == null)
                {
                    State = MatchState.ByeRound;
                    Winner = RedTeam;
                }
                else
                {
                    State = MatchState.NoVictor;
                }
            }

        }

        /// <summary> resets the team back to uncompleted state
        /// </summary>
        [Obsolete("rewritten for better usage, use resetMatch()")]
        public void reset( Team red, Team blue ) {
            if ( State == MatchState.VictorFound) {
                undoCompletion();
            }
            //RedTeam = red;
            //redTeamBox.Team = RedTeam;
            //BlueTeam = blue;
            //blueTeamBox.Team = BlueTeam;
            Winner = null;
            setState();
            recentlyModified = true;
            redrawComponents();

        }
        

        /// <summary> Calculates the state of the matchup based on entrants and winners
        /// </summary>
        private void setState() {
            if ( RedTeam == null && BlueTeam == null && Winner == null) {
                State = MatchState.Empty;
            } else if ((RedTeam == null && BlueTeam != null) || (RedTeam != null && BlueTeam == null)) {
                if (RedTeam != null) {
                    Winner = RedTeam;
                } else {
                    Winner = BlueTeam;
                }
                State = MatchState.ByeRound;
            } else if (RedTeam != null && BlueTeam != null) {
                if (Winner == null) {
                    State = MatchState.NoVictor;
                } else {
                    State = MatchState.VictorFound;
                }
            }
        }

        /// <summary> Button listener for entire panel, will determin actions based on the sender
        /// </summary>
        /// <param name="sender">the button that was pressed</param>
        /// <param name="e">Click event</param>
        public void button_click( object sender, EventArgs e ) {
            Button b = (Button)sender;
            if (b.Name != "back")
            {
                if (b.Name == "blueWin")
                {
                    Winner = BlueTeam;
                    updateTeams(BlueTeam, RedTeam);
                }
                else
                {
                    Winner = RedTeam;
                    updateTeams(RedTeam, BlueTeam);
                }
                State = MatchState.VictorFound;
                
                //If either team wins, check to see if its the last game
                if (isFinalsMatch || Name == "tourneyMatchUp14")
                {
                    MyParent.declareWinner(Winner);
                }
                else
                {
                    NextMatch.addTeamToMatchUp(Winner);
                }

            //else revert
            }else{
                if( NextMatch != null)
                    NextMatch.removeTeamFromMatchUp(Winner);
                undoCompletion();
                Winner = null;
                State = MatchState.NoVictor;
            }
            recentlyModified = true;
            redrawComponents();
        }

        /// <summary> undo's the winnings, if the team had one a game
        /// </summary>
        private void undoCompletion() {
            if ( State == MatchState.VictorFound) {
                if (Winner == RedTeam) {
                    RedTeam.undoWin();
                    BlueTeam.undoLoss();
                } else {
                    BlueTeam.undoWin();
                    RedTeam.undoLoss();
                }
                Winner = null;
            }
            
        }

        /// <summary> tells the winner param to win a game, and the looser param to loose a game
        /// </summary>
        /// <param name="winner">The winning team</param>
        /// <param name="looser">The loosing team</param>
        private void updateTeams( Team winner, Team looser ) {
            winner.winGame();
            looser.looseGame();
        }

        /// <summary> Sub routine to add logitch to coloring the panels
        /// </summary>
        public void addTeamToMatchUp( Team t ) {
            if( Visible == false )
                Visible = true;

            if (RedTeam == null) {
                RedTeam = t;
                redTeamBox.Team = RedTeam;
                redWin.Visible = true;
                if (BlueTeam != null) {
                    redWin.Enabled = true;
                    State = MatchState.NoVictor;
                } else {
                    redWin.Enabled = false;
                    State = MatchState.ByeRound;
                }
            } else if (BlueTeam == null) {
                BlueTeam = t;
                blueTeamBox.Team = BlueTeam;
                blueWin.Visible = true;
                if (RedTeam != null) {
                    blueWin.Enabled = true;
                    State = MatchState.NoVictor;
                } else {
                    blueWin.Enabled = false;
                    State = MatchState.ByeRound;
                }
            }
            recentlyModified = true;
            redrawComponents();
        }

        public void removeTeamFromMatchUp( Team t ) {
            //Little recurusion, hope it works
            if( State == MatchState.VictorFound ) {
                undoCompletion();
                NextMatch.removeTeamFromMatchUp(Winner);
                Winner = null;
            }

            //clear the team
            if( t == RedTeam ) {
                setMatch( null, BlueTeam );
            } else {
                setMatch(RedTeam, null);
            }
        }

        /// <summary> Will return if the game matchup has determined a winner or not
        /// </summary>
        /// <returns>returns true if a winner has been determined</returns>
        public bool InCompletedState() {
            if (this == null) {
                return false;
            }
            if ( State == MatchState.VictorFound ||
                State == MatchState.ByeRound ||
                State == MatchState.Empty) {

                return true;
            } else {
                return false;
            }
        }

        /// <summary> Clears any winners and all fed teams. Then removes the view
        /// </summary>
        public void clear() {
            Winner = null;
            RedTeam = null;
            BlueTeam = null;
            State = MatchState.Empty;
            recentlyModified = true;
            redrawComponents();
        }

        #region Drawing Functions - Recolor & Design based on state
        private void redrawComponents() {
            if( recentlyModified ) {
                switch( State ) {
                    case MatchState.ByeRound:
                        if( RedTeam != null ) {
                            drawRedByeRound();
                        } else {
                            drawBlueByeRound();
                        }
                        break;
                    case MatchState.Empty:
                        drawEmptyMatchup();
                        break;
                    case MatchState.NoVictor:
                        drawProperMatchup();
                        break;
                    case MatchState.VictorFound:
                        drawWinnerMatchUp();
                        break;
                }
                //after done redrawing, set it back to no modify
                recentlyModified = false;
            }
            back.Visible = false;
        }
        private void drawEmptyMatchup() {
            drawBYEpanel( redZone, redTeamBox, redWin );
            drawBYEpanel( blueZone, blueTeamBox, blueWin );
        }
        private void drawRedByeRound() {
            redZone.BackColor = Color.Red;
            redZone.Enabled = true;
            redTeamBox.Enabled = true;
            redTeamBox.displayName();
            redWin.Visible = true;
            redWin.Enabled = false;
            redWin.BackColor = Color.Gray;
            blueWin.Visible = false;
            back.Visible = false;
            drawBYEpanel( blueZone, blueTeamBox, blueWin );
        }
        private void drawBlueByeRound() {
            blueZone.BackColor = Color.Blue;
            blueZone.Enabled = true;
            blueTeamBox.Enabled = true;
            blueTeamBox.displayName();
            blueWin.Visible=true;
            blueWin.Enabled = true;
            blueWin.BackColor = Color.Gray;
            redWin.Visible = false;
            back.Visible = false;
            drawBYEpanel( redZone, redTeamBox, redWin );
        }
        private void drawBYEpanel( Panel zone, TeamTextBox text, Button b ){
            zone.BackColor = Color.DarkGray;
            zone.Enabled = false;
            text.Text = "BYE";
            text.Enabled = false;
            b.Visible = false;
        }
        private void drawProperMatchup() {
            //Zones
            redZone.Enabled = true;
            redZone.BackColor = Color.Red;
            blueZone.Enabled = true;
            blueZone.BackColor = Color.Blue;
            //Textboxes
            redTeamBox.Enabled = true;
            redTeamBox.displayName();
            blueTeamBox.Enabled = true;
            blueTeamBox.displayName();
            //Buttons
            redWin.Visible = true;
            redWin.Enabled = true;
            redWin.BackColor = Color.Green;
            blueWin.Visible = true;
            blueWin.Enabled = true;
            blueWin.BackColor = Color.Green;
        }
        private void drawWinnerMatchUp(){
            Panel winZone, looseZone;
            TeamTextBox winText, looseText;
            Button winButton, looseButton;
            //Setup winner and looser controls
            if (Winner == RedTeam) {
                winZone = redZone;
                looseZone = blueZone;
                winText = redTeamBox;
                looseText = blueTeamBox;
                winButton = redWin;
                looseButton = blueWin;
                
                //back.Location = new Point( redTeamBox.Location.X + redTeamBox.Width + 1, redTeamBox.Location.Y );
            } else {
                winZone = blueZone;
                looseZone = redZone;
                winText = blueTeamBox;
                looseText = redTeamBox;
                winButton = blueWin;
                looseButton = redWin;
                back.Parent = winZone;
                //back.Location = new Point( redTeamBox.Location.X + redTeamBox.Width + 1, redTeamBox.Location.Y );
            }

            //modify winner and loser controls
            back.Parent = winZone;
            back.Enabled = true;
            back.Visible = true;
            back.Location = winButton.Location;
            winText.displayName();
            winButton.Visible = false;
            winButton.Enabled = false;
            
            looseZone.BackColor = Color.DarkGray;
            looseZone.Enabled = false;
            looseText.displayName();
            looseText.Enabled = false;
            looseButton.Visible = false;

        }
        public void drawDone() {
            redWin.Visible = false;
            blueWin.Visible = false;
            back.Visible = false;
            redZone.BackColor = Color.DarkGray;
            blueZone.BackColor = Color.DarkGray;
        }
        #endregion
    }
}
