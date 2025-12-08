using Battleship.Backend.Ships;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace Battleship.Backend
{
    public class Board
    {
        public int Width { get; }
        public int Height { get; }

        public List<BoardActiveShip> Ships { get; } = new List<BoardActiveShip>();
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

        // TO REMAKE ENTIRELY THIS FUNCTION
        public BoardActiveShipStatus? Hit(Point coords)
        {
            foreach(var ship in Ships)
            {
                if (ship.IsAtPosition(coords))
                {
                    if (ship.Ship.Orientation == Orientation.Horizontal)
                    {
                        ship.Hit(coords.X - ship.Position.X);
                    }
                    else
                    {
                        ship.Hit(coords.Y - ship.Position.Y);
                    }
                    Cells[coords.X, coords.Y].Hit();
                    return ship.Status;
                }
            }
            if (Cells[coords.X,coords.Y].ExternalState == ExternalCellState.Uncovered)
                Cells[coords.X, coords.Y].Hit();
            return null;
        }

        public bool CanPlaceShip(Ship ship, Point pos)
        {
            if (ship.Orientation == Orientation.Horizontal)
            {
                if (pos.X + ship.Length > Width)
                    return false;
                for (int i = pos.X; i < pos.X + ship.Length; i++)
                {
                    if (Cells[i, pos.Y].InternalState == InternalCellState.Occupied)
                        return false;
                }
                return true;
            }
            else
            {
                if (pos.Y + ship.Length > Height)
                    return false;
                for (int i = pos.Y; i < pos.Y + ship.Length; i++)
                {
                    if (Cells[pos.X, i].InternalState == InternalCellState.Occupied)
                        return false;
                }
                return true;
            }
        }

        public void PlaceShip(Ship ship, Point pos)
        {
            if(!CanPlaceShip(ship, pos))
                throw new InvalidOperationException("Cannot place ship at the specified position");
            if (ship.Orientation == Orientation.Horizontal)
            {
                for (int i = 0; i < ship.Length; i++)
                {
                    Cells[pos.X + i, pos.Y].PlaceShipTile(new ShipTile(ship.GetPartAt(i)));
                }
            }
            else
            {
                for (int i = 0; i < ship.Length; i++)
                {
                    Cells[pos.X, pos.Y + i].PlaceShipTile(new ShipTile(ship.GetPartAt(i)));
                }
            }
            Ships.Add(new BoardActiveShip(ship, pos));
        }

        public void RemoveShip(BoardActiveShip activeShip)
        {
            Point pos = activeShip.Position;
            Ship ship = activeShip.Ship;
            if (ship.Orientation == Orientation.Horizontal)
            {
                for (int i = 0; i < ship.Length; i++)
                {
                    Cells[pos.X + i, pos.Y].RemoveShipTile();
                }
            }
            else
            {
                for (int i = 0; i < ship.Length; i++)
                {
                    Cells[pos.X, pos.Y + i].RemoveShipTile();
                }
            }
            Ships.Remove(activeShip);
        }

        public BoardActiveShip GetShipFromCoords(Point coords)
        {
            foreach(var activeShip in Ships)
            {
                if(activeShip.IsAtPosition(coords))
                    return activeShip;
            }
            return null;
        }
    }
}
