using System;

namespace BPMS.Models
{
    public class ByeTeam : ITeam
    {
        public Guid TeamID { get; private set; }
        public string TeamName { get; private set; }

        public ByeTeam()
        {
            TeamID = Guid.Empty;
            TeamName = "BYE";
        }

        public override string ToString()
        {
            return TeamName;
        }

    }
}
