using BattleshipWinforms.Backend.Ships;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipWinforms.Frontend
{
    public class BattleshipRenderer : IShipRenderer
    {
        public Image GetImage(ShipPart part, Orientation orientation)
        {
            if (orientation == Orientation.Horizontal)
            {
                switch (part)
                {
                    case ShipPart.Front:
                        return Properties.Resources.Ship_Battleship_Front_Hor;
                    case ShipPart.Middle:
                        return Properties.Resources.Ship_Battleship_Middle_Hor;
                    case ShipPart.Back:
                        return Properties.Resources.Ship_Battleship_Back_Hor;
                    default:
                        return null;
                }
            }
            else
            {
                switch (part)
                {
                    case ShipPart.Front:
                        return Properties.Resources.Ship_Battleship_Front_Ver;
                    case ShipPart.Middle:
                        return Properties.Resources.Ship_Battleship_Middle_Ver;
                    case ShipPart.Back:
                        return Properties.Resources.Ship_Battleship_Back_Ver;
                    default:
                        return null;
                }
            }
        }
    }
}
