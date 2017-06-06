using System;

namespace BPMS.Models
{
    public class MatchRecord : IComparable<MatchRecord>
    {
        public DateTime MatchPlayedOn { get; private set; }
        public TimeSpan MatchDuration { get; set; }
        public Team Team { get; set; }
        public Team Opponent { get; set; }

        private bool _matchResolved;
        public bool? MatchWon { get; set; }

        /// <summary>Creates a MatchRecord object where the match has not been played yet.
        /// </summary>
        /// <param name="team"></param>
        /// <param name="opponent"></param>
        /// <param name="matchStartTime"></param>
        public MatchRecord(Team team, Team opponent, DateTime matchStartTime = default(DateTime) )
        {
            CommonConstruction(team, opponent, matchStartTime, null);
        }

        /// <summary>Creates a MatchRecord object where the match has already been resolved.
        /// </summary>
        /// <param name="team"></param>
        /// <param name="opponent"></param>
        /// <param name="matchStartTime"></param>
        /// <param name="matchWon"></param>
        /// <param name="matchDuration"></param>
        public MatchRecord(Team team, Team opponent, DateTime matchStartTime, bool matchWon,
                           TimeSpan matchDuration = default(TimeSpan))
        {
            CommonConstruction(team, opponent, matchStartTime, matchWon);
            MatchDuration = matchDuration;
        }

        private void CommonConstruction(Team team, Team opponent, DateTime matchStartTime, bool? matchResult)
        {
            Team = team;
            Opponent = opponent;
            MatchPlayedOn = matchStartTime;
            MatchWon = matchResult;
            _matchResolved = MatchWon == null ? true : false;
        }

        public void ResolveMatch(bool matchWon, TimeSpan duration = default (TimeSpan))
        {
            _matchResolved = true;
            MatchWon = matchWon;
            MatchDuration = duration;
        }

        public int CompareTo( MatchRecord match )
        {
            if( MatchPlayedOn > match.MatchPlayedOn )
                return -1;
            return MatchPlayedOn < match.MatchPlayedOn ? 1 : 0;
        }
    }
}
