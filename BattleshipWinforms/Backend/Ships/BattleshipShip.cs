using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipWinforms.Backend.Ships
{
    class BattleshipShip : Ship
    {
        public BattleshipShip(Orientation orientation) : base(4, orientation)
        {
        }
    }
}
