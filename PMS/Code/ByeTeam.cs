namespace BPMS.Code
{
    public class ByeTeam : Team
    {
        public ByeTeam(string p1, string p2) 
            : base(p1, p2)
        {
            id = 0;
            Player1 = " ";
            Player2 = " ";
            teamName = "BYE";
        }
    }
}
