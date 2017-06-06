using System;
using System.Collections.Generic;
using System.Linq;

namespace BPMS.Models
{
    public class Team : ITeam
    {
        #region Properties

        public Guid TeamID { get; private set; }
        public Player Player1 { get; protected set; }
        public Player Player2 { get; protected set; }
        public List<MatchRecord> MatchHistory { get; protected set; }

        public int Wins
        {
            get
            {
                return MatchHistory.Count( x => x.MatchWon == true );
            }
        }
        public int Losses
        {
            get
            {
                return MatchHistory.Count( x => x.MatchWon == false );
            }

        }
        public double Record
        {
            get 
            {
                if( MatchHistory.Count == 0 )
                    return .000;
                return Math.Round( (double)Wins / MatchHistory.Count, 3 ); 
            }
        }

        public int CurrentStreak
        {
            get
            {
                IOrderedEnumerable<MatchRecord> matchesByMostRecent = MatchHistory.OrderByDescending( x => x.MatchPlayedOn );
                return matchesByMostRecent.TakeWhile(match => match.MatchWon != false).Count();
            }
        }
        public int MaxStreak
        {
            get 
            {
                int streak = 0, max = 0;
                foreach( MatchRecord match in MatchHistory.OrderBy( x => x.MatchPlayedOn ) )
                {
                    if( match.MatchWon == true )
                        streak++;
                    else
                        streak = 0;
                    max = ( streak > max ) ? streak : max;
                }
                return max;
            }
        }

        private string _teamName;
        public string TeamName
        {
            get
            {
                if (String.IsNullOrEmpty(_teamName))
                    return Player1.Name + " / " + Player2;
                return _teamName;
            }
            set { _teamName = value; }
        }

        #endregion

        /// <summary>
        /// Create a new team with two players, Entering a team Name is optional
        /// </summary>
        /// <param name="p1">First person on the team</param>
        /// <param name="p2">Second person on the team</param>
        /// <param name="teamName">Team's name</param>
        public Team(Player p1, Player p2, string teamName = null)
        {
            Player1 = p1;
            Player2 = p2;
            TeamName = teamName;
            MatchHistory = new List<MatchRecord>();
            TeamID = Guid.NewGuid();
        }


        #region ToString
        public override string ToString()
        {
            if( _teamName == string.Empty )
                return PlayersOnlyToString();
            return TeamNameToString();
        }

        private string PlayersOnlyToString()
        {
            const string formatStr = "[{0}] {1} / {2}";
            return String.Format( formatStr, this.Record, this.Player1.Name, this.Player2.Name );
        }

        private string TeamNameToString()
        {
            const string formatStr = "[{0}] {1}";
            return String.Format( formatStr, this.Record, this.TeamName );
        }
        #endregion
    }
}
