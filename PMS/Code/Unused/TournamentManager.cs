using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace BPMS.Code {
    class TournamentManager {
        public enum TSize{
            St = 8,
            Q = 4, 
            S = 2,
            F = 1
        };

        #region Singleton Code
        private static readonly TournamentManager _instance = new TournamentManager();
        private TournamentManager() { }
        public static TournamentManager Instance {
            get { return _instance; }
        }
        #endregion

        public Control Parent{ get; private set; }

        public TourneyMatchUp[] Starters { get; set; }
        public TourneyMatchUp[] Quarters { get; set; }
        public TourneyMatchUp[] Semis { get; set; }
        public TourneyMatchUp Finals { get; set; }
        public TeamTextBox TourneyWinner { get; set; }

        public void init( TourneyMatchUp[] startingTeams, Control p, TourneyMatchUp[] brackets ) {
            Parent = p;
            TourneyWinner = new TeamTextBox();
            setBrackets(brackets);
            //initTeams( startingTeams );
            //Starters = brackets;
        }

        /// <summary>
        /// Sets the tournematchups to the manager 
        /// </summary>
        /// <param name="b"></param>
        private void setBrackets(TourneyMatchUp[] b) {
            Starters = new TourneyMatchUp[] { b[0], b[1], b[2], b[3], b[4], b[5], b[6], b[7] };
            orderAndSet( Starters, 4 );
            Quarters = new TourneyMatchUp[] { b[8], b[9], b[10], b[11]};
            orderAndSet( Starters, 3 );
            Semis = new TourneyMatchUp[]    { b[12], b[13] };
            orderAndSet( Starters, 2 );
            Finals = b[14];
            orderAndSet( Starters, 1 );
        }

        /// <summary> Sub function to choose which array to start 
        /// updating depending on starting bracket
        /// </summary>
        public void updateWinners( TourneyMatchUp updated) {
            if( Finals.State == MatchState.VictorFound ) {
                    //update finals and winner
                TourneyWinner.Team = Finals.Winner;
                TourneyWinner.Update();
                //////
                ///TODO FINISH GAME
                //////
            } else {
                if( Semis.Contains<TourneyMatchUp>( updated ) ) {
                    // update semis and finals
                    Team team1 = null, team2 = null;
                    if( Semis[0].State == MatchState.VictorFound || Semis[0].State == MatchState.ByeRound ) {
                        team1 = Semis[0].Winner;
                    }
                    if( Semis[1].State == MatchState.VictorFound || Semis[1].State == MatchState.ByeRound ) {
                        team2 = Semis[1].Winner;
                    }
                    Finals.reset( team1, team2 );
                } else if( Quarters.Contains<TourneyMatchUp>( updated ) ) {
                    int curRowCounter = 0, nextRowCounter = 0;
                    for( int i = 0; i < Quarters.Length; i++ ) {
                        Team first = ( Quarters[curRowCounter].State == MatchState.VictorFound ) ? Quarters[curRowCounter].Winner : null;
                        Team second = ( Quarters[curRowCounter+1].State == MatchState.VictorFound ) ? Quarters[curRowCounter+1].Winner : null;
                        Semis[nextRowCounter].reset( first, second );
                        curRowCounter += 2;
                        nextRowCounter++;
                    }
                    // update quarters and semis
                } else {
                    int curRowCounter = 0, nextRowCounter = 0;
                    for( int i = 0; i < Starters.Length; i++ ) {
                        Team first = ( Starters[curRowCounter].State == MatchState.VictorFound ) ? Starters[curRowCounter].Winner : null;
                        Team second = ( Starters[curRowCounter + 1].State == MatchState.VictorFound ) ? Starters[curRowCounter + 1].Winner : null;
                        Quarters[nextRowCounter].reset( first, second );
                        curRowCounter += 2;
                        nextRowCounter++;
                    }
                    //update starters and quarters
                }
            }
        }

        /// <summary> updates the current bracket then moves to the next lower one
        /// </summary>
        /// <param name="t"> Current bracket size</param>
        private void scanArray2( TSize t ) {
           /* //String a = GetControlSummary( Parent, 0 );
            TourneyMatchUp[] temp = null;
            switch (t) {
                case TSize.St:
                    temp = getArray( TSize.St );
                    updateArray( temp, getArray( TSize.Q ), 4 );
                    //orderAndSet( temp, 4 );
                    scanArray( TSize.Q );
                    break;
                case TSize.Q:
                    temp = getArray( TSize.Q );
                    updateArray( temp, getArray( TSize.S ), 3 );
                    //orderAndSet( temp, 3 );
                    scanArray( TSize.S );
                    break;
                case TSize.S:
                    temp = getArray( TSize.S );
                    updateArray( temp, getArray( TSize.F ), 2 );
                    //orderAndSet( temp, 2 );
                    scanArray( TSize.F );
                    break;
                case TSize.F:
                    temp = getArray( TSize.F );
                    updateArray( temp, TourneyWinner );
                    //orderAndSet( temp, 1 );
                    break;
            }*/
            //Parent.Invalidate();
        }

        private void updateArray( TourneyMatchUp[] matchups, TourneyMatchUp[] nextRow, int t ) {
            int pos = 0;

            for (int i = 0; i < matchups.Length; i += 2) {
                if (matchups[i].InCompletedState() && matchups[i + 1].InCompletedState()) {
                    nextRow[pos] = new TourneyMatchUp( matchups[i].Winner, matchups[i + 1].Winner );
                    nextRow[pos].Parent = Parent;
                    nextRow[pos].Location = getLocationInfoForControl( t-1, pos );
                    nextRow[pos].Visible = true;
                } else {
                    Parent.Invalidate();
                    if (nextRow[pos].InCompletedState()) {
                        nextRow[pos].button_click( nextRow[pos].back, EventArgs.Empty );
                    }
                    if (nextRow[pos] != null) {
                        Parent.Parent.Controls.Remove( nextRow[pos] );
                        //nextRow[pos].Dispose();
                    }
                    nextRow[pos] = new TourneyMatchUp( null, null );
                }
                pos++;
                //        //nextRow[pos].RedTeam = matchups[i].Winner;
                //        //nextRow[pos].BlueTeam = matchups[i + 1].Winner;
                //        nextRow[pos].Visible = true;
                //    } else {
                //        //First check to see if we have to revert the previus
                //        TourneyMatchUp tmu  =nextRow[pos];
                //        if (tmu.Winner != null) {
                //            tmu.Winner.undoWin();
                //            if (tmu.Winner == tmu.RedTeam) {
                //                tmu.BlueTeam.undoLoss();
                //            } else {
                //                tmu.RedTeam.undoLoss();
                //            }
                //        }
                //        //after its reverted invisitble
                //        tmu.Parent = null;
                //        tmu.Visible = false;
                //    }
                //    pos++;
            }

            //for (int i = 0; i < matchups.Length; i += 2) {
            //    if (matchups[i] == null || matchups[i + 1] == null) {
            //        if (nextRow[pos] != null) {
            //            nextRow[pos].Visible = false;
            //            //nextRow[pos].Dispose();
            //            //nextRow[pos].removeMe();
            //        }
            //        nextRow[pos] = null;
            //        pos++;
            //        continue;
            //    }
            //    if (matchups[i].Winner == null || matchups[i + 1].Winner == null) {
            //        if (nextRow[pos] != null) {
            //            nextRow[pos].Visible = false;
            //            //nextRow[pos].Dispose();
            //            //nextRow[pos].removeMe();
            //            nextRow[pos].Visible = false;
            //        }

            //        //nextRow[pos] = null;
            //    } else {
            //        nextRow[pos] = new TourneyMatchUp( matchups[i].Winner, matchups[i + 1].Winner );
            //        nextRow[pos].Visible = true;
            //    }
            //    pos++;
            //}
            
        }
        private void updateArray( TourneyMatchUp[] matchups, TeamTextBox team ) {
            if (matchups[0].InCompletedState()) {
                team = new TeamTextBox( matchups[0].Winner );
                team.Visible = true;
                team.Parent = Parent;
                team.Location = getLocationInfoForControl( 0, 0 );
            } else {
                team.Visible = false;
            }
        }

        /// <summary> Deprecated? </summary>
        /// <param name="matchups"></param>
        /// <param name="depth"></param>
        private void orderAndSet( TourneyMatchUp[] matchups, int depth ) {
            for (int i = 0; i < matchups.Length; i++) {
                if (matchups[i] != null) {
                    Point p = getLocationInfoForControl( depth, i );
                    //if (matchups[i].Location != p)
                        matchups[i].Location = p;
                    //if (matchups[i].Parent != parent)
                        matchups[i].Parent = Parent;

                }
            }
        }

        /// <summary> Given a size, it returns the propper array
        /// </summary>
        /// <param name="t"> a size depiced in an enum</param>
        /// <returns></returns>
        public TourneyMatchUp[] getArray( TSize t ) {
            TourneyMatchUp[] returnMe = null;
            switch (t) {
                case TSize.St:
                    returnMe = Starters;
                    break;
                case TSize.Q:
                    returnMe = Quarters;
                    break;
                case TSize.S:
                    returnMe = Semis;
                    break;
                case TSize.F:
                    //returnMe = Finals;
                    break;
            }
            return returnMe;
        }
        
        private void shutoffGame() {
            foreach (TourneyMatchUp tmu in Starters) {
                tmu.Enabled = false;
            }
        }

        /// <summary> Sets the Location based on what layer the control is in the brackets and how far in controls-wise it is.
        /// </summary>
        /// <param name="depth"></param>
        /// <param name="howManyDown"></param>
        /// <returns></returns>
        private Point getLocationInfoForControl( int depth, int howManyDown ){
            //(4)starters,(3)quarters,(2)semis,(1)finals,(0)winner
            Point pt = new Point();
            switch (depth) {
                case 4:
                    pt.X = 8;
                    pt.Y = 11 + (howManyDown*57);
                    break;
                case 3:
                    pt.X = 141;
                    pt.Y = 37 + (howManyDown * 114);
                    break;
                case 2:
                    pt.X = 284;
                    pt.Y = 97 + (howManyDown * 228);
                    break;
                case 1:
                    pt.X = 400;
                    pt.Y = 170+ (howManyDown * 57);
                    break;
                case 0:
                    //TODO
                    pt.X = 427; //proper size?: 570;
                    pt.Y = 204; //proper size?: 214;
                    break;
            }
            return pt;
        }

        public void destroyTourney() {
            Starters = destroy( Starters );
            Quarters = destroy( Quarters );
            Semis = destroy( Semis );
            Finals = destroy( Finals );
            TourneyWinner = destroy( TourneyWinner );
        }

        private TourneyMatchUp[] destroy( TourneyMatchUp[] matchups ) {
            if (matchups != null) {
                foreach (TourneyMatchUp t in matchups) {
                    if (t != null)
                        t.Visible = false;
                }
            }
            return null;
        }
        private TourneyMatchUp destroy( TourneyMatchUp finalists ) {
            finalists.Dispose();
            finalists.Visible = false;
            finalists = null;
            return null;
        }
        private TeamTextBox destroy( TeamTextBox champ ) {
            champ.Dispose();
            champ.Visible = false;
            champ = null;
            return null;
        }
        
        /// <summary> STACK OVERFLOW HELPER METHOD
        /// </summary>
        /// <param name="rootControl"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public static string GetControlSummary( Control rootControl, int level ) {
            string result = "";
            foreach (Control childControl in rootControl.Controls) {
                result += new string( ' ', level * 4 ) + childControl.Name + " (" + childControl.GetType().Name + ")";
                result += (childControl is TourneyMatchUp) ? ((TourneyMatchUp)childControl).RedTeam + " vs " + ((TourneyMatchUp)childControl).BlueTeam : "";
                result += "\r\n";
                result += GetControlSummary( childControl, level + 1 );
            }
            return result;
        }

    }
}
