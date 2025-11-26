using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleshipWinforms
{
    public partial class StartPage : Form
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GamePVPRush gamePVP = new GamePVPRush(new List<string>() { "Player 1" });
            gamePVP.WindowState = FormWindowState.Maximized;
            gamePVP.Show();
        }
    }
}
