using System;
using System.Drawing;
using System.Windows.Forms;

namespace BPMS
{
    public class TeamEvent : EventArgs
    {
        public TeamEvent( string m )
        {
            Message = m;
        }
        public string Message{ get;set;}
    }

    public class Team {

        //Variables
        protected bool wonPrevious;
        protected int longestStreak;
        protected int currentStreak;
        protected int rememberStreak;
        protected string teamName;
        protected int id;


        protected TimeSpan timeOnTable;

        public delegate void TeamEventHandler( object sender, TeamEvent t );
        public event TeamEventHandler Achievement;


        #region Constructors
        /// <summary>
        /// Defualt Constructor
        /// </summary>
        /// <param name="p1">First Teamate</param>
        /// <param name="p2">Second Teamate</param>
        public Team(string p1, string p2) {
            id = BPMSMain.teamIDs++;
            Player1 = p1;
            Player2 = p2;
            currentStreak = 0;
            longestStreak = 0;
            Losses = 0;
            Wins = 0;
            wonPrevious = false;
            timeOnTable = new TimeSpan();
            teamName = null;
            Achievement += new TeamEventHandler( catchAchievement );
        }

        /// <summary>
        /// XML Based Constructor. Used by XML to read in saved teams
        /// </summary>
        /// <param name="xId"> Team Id</param>
        /// <param name="xName">Team Name</param>
        /// <param name="xP1">First Team Player</param>
        /// <param name="xP2">Second Team Player</param>
        /// <param name="streak">Team's Record Streak</param>
        public static Team FromXML(int xId, string xName, string xP1, string xP2, int wins, int losses, int streak)
        {
            Team t = new Team(xP1, xP2);
            if( xId >= BPMSMain.teamIDs )
            {
                BPMSMain.teamIDs = xId + 1;
            }
            t.id = xId;
            t.currentStreak = 0;
            t.longestStreak = streak;
            t.wonPrevious = false;
            t.timeOnTable = new TimeSpan();
            t.teamName = (xName == "") ? null : xName;
            t.Losses = losses;
            t.Wins = wins;
            return t;
        }
        #endregion

        #region Properties

        
        /// <summary>
        /// Name of the first teammate
        /// </summary>
        public string Player1
        {
            get; protected set;
        }

        /// <summary>
        /// Name of the second teammate
        /// </summary>
        public string Player2
        {
            get; protected set;
        }

        /// <summary>
        /// Team name of designated by the players
        /// </summary>
        public string TeamName
        {
            get; set;
        }

        /// <summary>
        /// Accessor for toatl wins by the team
        /// </summary>
        public int Wins
        {
            get; protected set;
        }

        /// <summary>
        /// Accessor for toatl losses by the team
        /// </summary>
        public int Losses
        {
            get; protected set;
        }

        /// <summary>
        /// Accessor for a Team's Id.
        /// </summary>
        public int Id {
            get { return id; }
        }

        /// <summary>
        /// Accessor for a Team's Id.
        /// </summary>
        public int Streak {
            get { return currentStreak; }
        }

        /// <summary>
        /// Accessor for a Team's Id.
        /// </summary>
        public int MaxStreak {
            get { return longestStreak; }
        }

        /// <summary>
        /// Gets the current Record of the team.
        /// </summary>
        public double Record
        {
            get
            {
                return (Wins + Losses) == 0 ? .000 : Math.Round((Wins / (double)(Wins + Losses)), 3);
            }
        }

        #endregion

        /// <summary>
        /// This function will reset all of the Teams stats to 0
        /// </summary>
        public void destroyStats() {
            wonPrevious = false;
            currentStreak = 0;
            Losses = 0;
            Wins = 0;
            longestStreak = 0;
            timeOnTable = new TimeSpan( 0 );
        }

        /// <summary>
        /// Method that increments a teams stats for winning a game
        /// </summary> 
        public void winGame() {
            if( !wonPrevious ) {
                currentStreak = 1;
                timeOnTable = BPMSMain.gameLength();
                wonPrevious = true;
            } else {
                currentStreak++;

                #region Achievement Event Sender
                string message = "";
                
                switch (currentStreak) {
                    case 2:
                        message = "gotGame";
                        break;
                    case 3:
                        message = "turkey";
                        break;
                    case 5:
                        message = "unstoppable";
                        break;
                    case 10:
                        message = "godlike";
                        break;
                }
                if (message != "") {
                    TeamEvent te = new TeamEvent( message );
                    Achievement( this, te );
                }
                #endregion

                if ( currentStreak > longestStreak ) {
                    longestStreak = currentStreak;
                }
                timeOnTable += BPMSMain.gameLength();
                wonPrevious = true;
            }
            rememberStreak = currentStreak;
            Wins++;
        }
        public void winGame( TimeSpan t ) {
            timeOnTable += t;
            winGame();
        }

        /// <summary>
        /// Clears current streaks of a team and updates number of
        /// losses
        /// </summary>
        public void looseGame() {
            rememberStreak = currentStreak;
            currentStreak = 0;
            wonPrevious = false;
            Losses++;
        }

        /// <summary>
        /// Clears current streaks of a team and updates number of
        /// losses, and increments amount of time on the table
        /// </summary>
        /// <param name="t">the time spent on the previous game</param>
        public void looseGame( TimeSpan t ) {
            timeOnTable += t;
            looseGame();
        }

        public void undoWin() {
            currentStreak--;// = 
            rememberStreak = currentStreak;
            if (rememberStreak < 1)
                wonPrevious = false;
            else
                wonPrevious = true;
            Wins--;
        }

        public void undoLoss() {
            currentStreak = rememberStreak;
            if (rememberStreak < 1)
                wonPrevious = false;
            else
                wonPrevious = true;
            Losses--;
        }

        public void catchAchievement( object sender, TeamEvent te ) {
            Team theSender = (Team)sender;
            string achievementMessage = theSender.toStringTeamName() + "";
            switch (te.Message) {
                case "gotGame": // Record Streak of 2
                    achievementMessage += " have got Game!";
                    break;
                case "turkey": // Record Streak of 3
                    achievementMessage += " got a Turkey!";
                    break;
                case "unstoppable": // Record Streak of 5
                    achievementMessage += " are unstoppable!!";
                    break;
                case "godlike": // Record Streak of 10
                    achievementMessage += " are godlike";
                    break;
            }

            Form f = new AchievementUnlocked( achievementMessage );
            f.Location = new Point( Screen.PrimaryScreen.WorkingArea.Location.X / 2,
                Screen.PrimaryScreen.WorkingArea.Location.Y - 50);
            f.Show();
        }


        #region ToString & Text Display Methods
        /// <summary>
        /// Reports the teams name
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            if( teamName == "" ) {
                return toStringPlayers();
            } else {
                return toStringTeamName();
            }
        }

        /// <summary>
        /// Display function for a team with focus on players data
        /// </summary>
        /// <returns>returns a stirng with the id and player's names</returns>
        public string toStringPlayers() {
            return idDisplay() + " " + recordDisplay() + " " + playersDisplay();
        }

        /// <summary>
        /// Display function for a team with focus on the teamName data
        /// </summary>
        /// <returns>returns a stirng with the id and player's names</returns>
        public string toStringTeamName() {
            if ( TeamName != null && TeamName != " " && TeamName != "_") {
                return idDisplay() + " " + recordDisplay() + " " + TeamName;
            } else {
                return idDisplay() + " " + recordDisplay() + " " + playersDisplay();
            }
        }
        public string toStringShowAll() {
            if ( TeamName == "") {
                return idDisplay() + " " + playersDisplay();
            } else {
                return idDisplay() + " \"" + TeamName + "\" " + playersDisplay();
            }
        }

        /// <summary>
        /// Simple Representation of Team Display
        /// </summary>
        /// <returns></returns>
        public string TeamNameSimple()
        {
            return (TeamName != null && TeamName != " " && TeamName != "_") ? TeamName : playersDisplay();
        }

        /// <summary>
        /// Displays the Team as a combination of the players
        /// </summary>
        /// <returns></returns>
        public string TeamPlayersSimple() {
            return playersDisplay();
        }

        protected string idDisplay() { return String.Format("[{0}]", Id); }
        protected string recordDisplay() { return String.Format("({0})", Record.ToString("F3")); }
        protected string playersDisplay() { return String.Format("{0} / {1}", Player1, Player2); }

        #endregion
    }
}
