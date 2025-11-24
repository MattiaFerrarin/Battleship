using BattleshipWinforms.Backend;
using BattleshipWinforms.Frontend;
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
    public partial class GamePVP : Form
    {
        public GamePVP()
        {
            InitializeComponent();

            controller = new GameHandler(10);

            TableLayoutPanel boardPanel = BoardRenderer.CreateBoard(
                controller.EnemyBoard,
                OnBoardCellClick
            );

            this.Controls.Add(boardPanel);
        }

        GameHandler controller;

        private void OnBoardCellClick(int x, int y)
        {
            var result = controller.ShootEnemy(x, y);

            UpdateCellUI(x, y, result);
        }

        private void UpdateCellUI(int x, int y, ExternalCellState state)
        {
            // Gets the controls of type TableLayoutPanel and picks the first (and only one)
            TableLayoutPanel boardTLP = Controls.OfType<TableLayoutPanel>().First();
            // Uses a function of the TLP class that gets the control inside a certain cell (from row and col)
            PictureBox pb = (PictureBox)boardTLP.GetControlFromPosition(x, y);

            if (state == ExternalCellState.Hit)
                pb.BackColor = Color.Red;
            else if (state == ExternalCellState.Miss)
                pb.BackColor = Color.WhiteSmoke;
        }
    }
}
