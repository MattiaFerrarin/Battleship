using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleship
{
    public partial class NewGamePage : Form
    {
        public NewGamePage()
        {
            InitializeComponent();
        }

        private void txb_player_Leave(object sender, EventArgs e)
        {
            TextBox txb = sender as TextBox;
            if(txb.Text == "")
            {
                txb.Text = (string)txb.Tag;
            }
        }

        private void btn_newPvP_Click(object sender, EventArgs e)
        {
            GamePVP GamePVP = new GamePVP(new List<string>() { "Player 1" });
            GamePVP.WindowState = FormWindowState.Maximized;
            this.Hide();
            GamePVP.FormClosed += (s, args) => this.Close();
            GamePVP.Show();
        }

        private void btn_newPvPRush_Click(object sender, EventArgs e)
        {
            GamePVPRush gamePVPRush = new GamePVPRush(new List<string>() { "Player 1" });
            gamePVPRush.WindowState = FormWindowState.Maximized;
            this.Hide();
            gamePVPRush.FormClosed += (s, args) => this.Close();
            gamePVPRush.Show();
        }

        private void btn_newPvC_Click(object sender, EventArgs e)
        {
            GamePVC GamePVC = new GamePVC(new List<string>() { "Player 1" });
            GamePVC.WindowState = FormWindowState.Maximized;
            this.Hide();
            GamePVC.FormClosed += (s, args) => this.Close();
            GamePVC.Show();
        }
    }
}
