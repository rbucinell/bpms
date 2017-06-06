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

            this.Name = " Stats Block";
            id_textbox.Text = t.Id + "";
            if (t.Name == "" || t.Name == null) {
                name_textbox.Text = "<< No Team Name >>";
            } else {
                name_textbox.Text = t.Name;
            }
            player1_textbox.Text = t.player1;
            player2_textbox.Text = t.player2;
            win_textbox.Text = t.Wins + "";
            loose_textbox.Text = t.Losses + "";
            streak_textbox.Text = t.MaxStreak + "";
            rating_textbox.Text = t.getRecord()+ "";
            int losses = (t.Losses != 0) ? t.Losses : 1;
            ratio_textbox.Text = t.Wins / losses + "";
            tot_textbox.Text = "";
            tot_textbox.Visible = false;
            tot_label.Visible = false;
        }

        private void ok_button_click( object sender, EventArgs e ) {
            this.Dispose();
        }
    }
}
