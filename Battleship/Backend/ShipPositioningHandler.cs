using BattleshipWinforms.Backend.Ships;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipWinforms.Backend
{
    public static class ShipPositioningHandler
    {
        public static bool CanPlaceShip(Board board, Ship ship, Point pos)
        {
            if(ship.Orientation == Orientation.Horizontal)
            {
                if (pos.X + ship.Length > board.Width)
                    return false;
                for (int i=pos.X; i < pos.X + ship.Length; i++)
                {
                    if (board.Cells[i, pos.Y].InternalState == InternalCellState.Occupied)
                        return false;
                }
                return true;
            }
            else
            {
                if (pos.Y + ship.Length > board.Height)
                    return false;
                for (int i = pos.Y; i < pos.Y + ship.Length; i++)
                {
                    if (board.Cells[pos.X, i].InternalState == InternalCellState.Occupied)
                        return false;
                }
                return true;
            }
        }
        public static void PlaceShip(Board board, Ship ship, Point pos)
        {
            if (ship.Orientation == Orientation.Horizontal)
            {
                for (int i = 0; i < ship.Length; i++)
                {
                    board.Cells[pos.X + i, pos.Y].PlaceShipTile(new ShipTile(ship.GetPartAt(i)));
                }
            }
            else
            {
                for (int i = 0; i < ship.Length; i++)
                {
                    board.Cells[pos.X, pos.Y+i].PlaceShipTile(new ShipTile(ship.GetPartAt(i)));
                }
            }
        }
    }
}
