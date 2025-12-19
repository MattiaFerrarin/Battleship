using Battleship.Backend;
using Battleship.Backend.Ships;
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

            // Insert PictureBoxes and paint them appropiately
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    Color backColor;
                    if (board.Cells[x,y].ExternalState == ExternalCellState.Uncovered)
                    {
                        backColor = Color.Transparent;
                    }
                    else if (board.Cells[x, y].ExternalState == ExternalCellState.Miss)
                    {
                        backColor = StateToBackColor(CellState.Miss);
                    }
                    else
                    {
                        BoardActiveShip activeShip = board.GetShipFromCoords(new Point(x, y));
                        if (activeShip.Status == Backend.Ships.BoardActiveShipStatus.Sunk)
                        {
                            backColor = StateToBackColor(CellState.Sunk);
                        }
                        else
                        {
                            backColor = StateToBackColor(CellState.Hit);
                        }
                    }

                    PictureBox picBox = new PictureBox
                        {
                            Dock = DockStyle.Fill,
                            SizeMode = PictureBoxSizeMode.StretchImage,
                            Tag = (x, y),
                            Margin = new Padding(0),
                            BackColor = backColor
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

            // Draw previously sunk ships
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    if (tlp.GetControlFromPosition(x,y) is PictureBox picBox)
                    {
                        if(board.Cells[x, y].ExternalState == ExternalCellState.Hit)
                        {
                            BoardActiveShip activeShip = board.GetShipFromCoords(new Point(x, y));
                            if(activeShip.Status == BoardActiveShipStatus.Sunk)
                            {
                                UIHandlers.DrawShipTileOnUI(tlp, activeShip, new Point(x, y));
                            }
                        }
                    }
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
