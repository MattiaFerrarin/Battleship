using Battleship.Backend;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using static Battleship.Utils;

namespace Battleship.Frontend
{
    public static class BoardRenderer
    {
        private static Size _IndividualCellSize = new Size(50,50);
        public static TableLayoutPanel CreateBoard(Board board, Action<int, int> onCellClick, EventHandler onMouseEnter, EventHandler onMouseLeave)
        {
            int w = board.Width;
            int h = board.Height;

            var tlp = new TableLayoutPanel
            {
                RowCount = h,
                ColumnCount = w,
                Width = w * _IndividualCellSize.Width,
                Height = h * _IndividualCellSize.Height,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,
                AutoSize = false,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };

            for (int i = 0; i < w; i++)
                tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, _IndividualCellSize.Width-1));
            for (int j = 0; j < h; j++)
                tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, _IndividualCellSize.Height-1));

            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    PictureBox picBox = new PictureBox
                    {
                        Dock = DockStyle.Fill,
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Tag = (x, y),
                        Margin = new Padding(0),
                        BackColor = Color.Transparent
                        //BackColor = StateToBackColor(ExternalCellState.Uncovered)
                    };

                    picBox.Click += (sender, e) =>
                    {
                        (int x, int y) coords = ((int, int))((PictureBox)sender).Tag;
                        onCellClick?.Invoke(coords.x, coords.y);
                    };
                    picBox.MouseEnter += onMouseEnter;
                    picBox.MouseLeave += onMouseLeave;

                    tlp.Controls.Add(picBox, x, y);
                }
            }

            return tlp;
        }

        public static Color StateToBackColor(CellState state)
        {
            switch (state)
            {
                case CellState.Hit:
                    return Color.DarkRed;
                case CellState.Sunk:
                    return Color.Black;
                case CellState.Miss:
                    return Color.DarkBlue;
                case CellState.Uncovered:
                default:
                    return Color.LightBlue;
            }
        }
    }
}
