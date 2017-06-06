using System;

namespace BPMS.Models
{
    public interface ITeam
    {
        Guid TeamID { get; }
        string TeamName { get; }
    }
}