using BattleshipWinforms.Backend.Ships;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipWinforms.Frontend
{
    public interface IShipRenderer
    {
        Image GetImage(ShipPart part, Orientation orientation);
    }
}
