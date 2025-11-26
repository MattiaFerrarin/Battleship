using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipWinforms.Backend.Ships
{
    public class BoardActiveShip
    {
        public Ship Ship { get; }
        public bool[] Hits { get; }
        public Point Position { get; }
        public BoardActiveShipStatus Status { get; private set; } = BoardActiveShipStatus.Untouched;
        public BoardActiveShip(Ship ship, Point position)
        {
            Ship = ship;
            Position = position;
            Hits = new bool[ship.Length];
        }
        public void Hit(int pos)
        {
            if (Status == BoardActiveShipStatus.Sunk)
                return;
            if(Status == BoardActiveShipStatus.Untouched)
                Status = BoardActiveShipStatus.Hit;
            Hits[pos] = true;
            if(!Hits.Contains(false))
            {
                Status = BoardActiveShipStatus.Sunk;
            }
        }
        public bool IsAtPosition(Point pos)
        {
            if (Ship.Orientation == Orientation.Horizontal)
            {
                return pos.Y == Position.Y && pos.X >= Position.X && pos.X < Position.X + Ship.Length;
            }
            else
            {
                return pos.X == Position.X && pos.Y >= Position.Y && pos.Y < Position.Y + Ship.Length;
            }
        }
    }
    public enum BoardActiveShipStatus
    {
        Untouched,
        Hit,
        Sunk
    }
}
