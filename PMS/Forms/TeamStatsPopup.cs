using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BPMS.Forms {
    public partial class TeamStatsPopup : Form {
        public TeamStatsPopup( Team t ) {
            InitializeComponent();

            Name = " Stats Block";
            id_textbox.Text = t.Id + "";
            if (t.TeamName == "" || t.TeamName == null) {
                name_textbox.Text = "<< No Team Name >>";
            } else {
                name_textbox.Text = t.TeamName;
            }
            player1_textbox.Text = t.Player1;
            player2_textbox.Text = t.Player2;
            win_textbox.Text = t.Wins + "";
            loose_textbox.Text = t.Losses + "";
            streak_textbox.Text = t.MaxStreak + "";
            rating_textbox.Text = t.Record+ "";
            int losses = (t.Losses != 0) ? t.Losses : 1;
            ratio_textbox.Text = t.Wins / losses + "";
            tot_textbox.Text = "";
            tot_textbox.Visible = false;
            tot_label.Visible = false;
        }

        private void ok_button_click( object sender, EventArgs e ) {
            Dispose();
        }
    }
}
