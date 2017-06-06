namespace BPMS.Code
{
    public class ByeTeam : Team
    {
        public ByeTeam(string p1, string p2) 
            : base(p1, p2)
        {
            id = 0;
            player1 = "";
            player2 = "";
            teamName = "";
        }

        new public void undoLoss() { }
        new public void undoWin() { }
        new public void winGame() { }
        new public void looseGame() { }

        new public void destroyStats() { }
        
        new protected string idDisplay() { return TeamName; }
        new protected string recordDisplay() { return TeamName; }
        new protected string playersDisplay() { return TeamName; }
    }
}
