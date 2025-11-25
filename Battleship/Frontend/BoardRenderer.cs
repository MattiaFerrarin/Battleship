using BattleshipWinforms.Backend;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleshipWinforms.Frontend
{
    public static class BoardRenderer
    {
        private static Size _IndividualCellSize = new Size(30,30);
        public static TableLayoutPanel CreateBoard(Board board, Action<int, int> onCellClick)
        {
            int w = board.Width;
            int h = board.Height;

            var tlp = new TableLayoutPanel
            {
                RowCount = h,
                ColumnCount = w,
                Width = w * _IndividualCellSize.Width,
                Height = h * _IndividualCellSize.Height,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
            };

            for (int i = 0; i < w; i++)
                tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / w)); // Makes the columns be resized by percentage
            for (int j = 0; j < h; j++)
                tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / h)); // Same but for rows

            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    PictureBox picBox = new PictureBox
                    {
                        Dock = DockStyle.Fill,
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Tag = (x, y),
                        BackColor = StateToBackColor(ExternalCellState.Uncovered)
                    };

                    picBox.Click += (sender, e) =>
                    {
                        (int x, int y) coords = ((int, int))((PictureBox)sender).Tag;
                        onCellClick?.Invoke(coords.x, coords.y);
                    };

                    tlp.Controls.Add(picBox, x, y);
                }
            }

            return tlp;
        }

        public static Color StateToBackColor(ExternalCellState state)
        {
            switch (state)
            {
                case ExternalCellState.Hit:
                    return Color.Red;
                case ExternalCellState.Miss:
                    return Color.WhiteSmoke;
                default:
                    return Color.LightBlue;
            }
        }
    }
}
