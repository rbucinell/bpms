using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace BPMS {

    public class TeamEvent : EventArgs {
        public TeamEvent( string m ) {
            this.Message = m;
        }
        public string Message{ get;set;}
    }
    public class Team {

        //Variables
        public string player1;
        public string player2;

        protected bool wonPrevious;
        protected int longestStreak;
        protected int currentStreak;
        protected int rememberStreak;
        protected string teamName;
        protected int id;
        protected int totalLoss, totalWin;


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
            id = PMSmain.teamIDs++;
            player1 = p1;
            player2 = p2;
            currentStreak = 0;
            longestStreak = 0;
            totalLoss = 0;
            totalWin = 0;
            wonPrevious = false;
            timeOnTable = new TimeSpan();
            teamName = null;
            this.Achievement += new Team.TeamEventHandler( catchAchievement );
        }

        /// <summary>
        /// XML Based Constructor. Used by XML to read in saved teams
        /// </summary>
        /// <param name="xId"> Team Id</param>
        /// <param name="xName">Team Name</param>
        /// <param name="xP1">First Team Player</param>
        /// <param name="xP2">Second Team Player</param>
        /// <param name="streak">Team's Record Streak</param>
        public Team( int xId, string xName, string xP1, string xP2, int wins, int losses, int streak ) {
            id = xId;
            if (xId >= PMSmain.teamIDs) {
                PMSmain.teamIDs = xId + 1;
            }
            player1 = xP1;
            player2 = xP2;
            currentStreak = 0;
            longestStreak = streak;
            wonPrevious = false;
            timeOnTable = new TimeSpan();
            xName = xName.Replace(" ","");
            teamName = (xName == "")? null : xName;
            totalLoss = losses;
            totalWin = wins;
            this.Achievement += new Team.TeamEventHandler( catchAchievement );
        }
        #endregion

        #region Properties
        public string Name {
            get { return teamName; }
            set { teamName = value; }
        }
        /// <summary>
        /// Accessor for toatl wins by the team
        /// </summary>
        public int Wins {
            get { return totalWin; }
        }

        /// <summary>
        /// Accessor for toatl losses by the team
        /// </summary>
        public int Losses {
            get { return totalLoss; }
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
        #endregion

        public double getRecord() {
            if (totalWin + totalLoss == 0) {
                return .000;
            } else {
                return Math.Round( ((double)totalWin / ((double)(totalWin + totalLoss)) + .00000), 3 );
            }
        }


        /// <summary>
        /// This function will reset all of the Teams stats to 0
        /// </summary>
        public void destroyStats() {
            wonPrevious = false;
            currentStreak = 0;
            totalLoss = 0;
            totalWin = 0;
            longestStreak = 0;
            timeOnTable = new TimeSpan( 0 );
        }

        /// <summary>
        /// Method that increments a teams stats for winning a game
        /// </summary> 
        public void winGame() {
            if( !wonPrevious ) {
                currentStreak = 1;
                timeOnTable = PMSmain.gameLength();
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
                timeOnTable += PMSmain.gameLength();
                wonPrevious = true;
            }
            rememberStreak = currentStreak;
            totalWin++;
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
            totalLoss++;
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
            totalWin--;
        }

        public void undoLoss() {
            currentStreak = rememberStreak;
            if (rememberStreak < 1)
                wonPrevious = false;
            else
                wonPrevious = true;
            totalLoss--;
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
            return this.idDisplay() + " " + this.recordDisplay() + " " + playersDisplay();
        }

        /// <summary>
        /// Display function for a team with focus on the teamName data
        /// </summary>
        /// <returns>returns a stirng with the id and player's names</returns>
        public string toStringTeamName() {
            if (this.Name != null && this.Name != " " && this.Name != "_") {
                return this.idDisplay() + " " + this.recordDisplay() + " " + this.Name;
            } else {
                return this.idDisplay() + " " + this.recordDisplay() + " " + this.playersDisplay();
            }
        }
        public string toStringShowAll() {
            if (this.Name == "") {
                return this.idDisplay() + " " + playersDisplay();
            } else {
                return this.idDisplay() + " \"" + this.Name + "\" " + playersDisplay();
            }
        }

        public string TeamNameSimple() {
            if (this.Name != null && this.Name != " " && this.Name != "_") {
                return this.Name;
            } else {
                return this.playersDisplay();
            }
        }

        public string TeamPlayersSimple() {
            return this.playersDisplay();
        }

        protected string idDisplay() { return "["+ this.id +"]"; }
        protected string recordDisplay() { return "(" + ((getRecord() == 0) ? ".000" : getRecord() + "") + ")"; }
        protected string playersDisplay() { return player1 + " / " + player2; }

        #endregion
    }
}
