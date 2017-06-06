using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BPMS.Code {
    public class ByeTeam : Team {

        public ByeTeam( string p1, string p2 ):base(p1,p2){
            id = 0;
            player1 = "";
            player2 = "";
            teamName = "";
        }

        new public void undoLoss() { }
        new public void undoWin() { }
        new public void winGame() { }
        new public void looseGame() { }

        new public double getRecord() {
            return 0.00;
        }
        new public void destroyStats() { }

        new protected string idDisplay() { return teamName; }
        new protected string recordDisplay() { return teamName; }
        new protected string playersDisplay() { return teamName; }
    }
}
