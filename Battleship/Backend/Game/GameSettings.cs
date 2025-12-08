using Battleship.Backend.Ships;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Backend
{
    public class GameSettings
    {
        public Size BoardSize { get; set; }
        private List<Ship> _ships { get; set; } = new List<Ship>();
        public GameSettings(int boardSize, List<Ship> ships)
        {
            BoardSize = new Size(boardSize, boardSize);
            _ships = ships;
        }
        public GameSettings(Size boardSize, List<Ship> ships) 
        {
            BoardSize = boardSize;
            _ships = ships;
        }
        public List<Ship> Ships { 
            get 
            { 
                List<Ship> clonedShips = new List<Ship>();
                foreach(var ship in _ships)
                {
                    clonedShips.Add(ShipFactory.CreateShip(ship.GetType()));
                }
                return clonedShips;
            } 
        }
    }
}
