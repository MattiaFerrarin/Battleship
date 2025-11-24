using BattleshipWinforms.Backend.Ships;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace BattleshipWinforms.Backend
{
    public class Board
    {
        public int Width { get; }
        public int Height { get; }

        public BoardCell[,] Cells { get; }

        public Board(int width, int height)
        {
            Width = width;
            Height = height;

            Cells = new BoardCell[width, height];
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    Cells[x, y] = new BoardCell();
        }

        public bool CanPlaceShip(Ship ship, int startX, int startY)
        {
            // logica collisioni
            throw new NotImplementedException();
        }

        public void PlaceShip(Ship ship, int startX, int startY)
        {
            // assegna i ShipTile alle celle
            throw new NotImplementedException();
        }
    }
}
