using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipWinforms.Backend.Ships
{
    class CarrierShip : Ship
    {
        public CarrierShip(Orientation orientation) : base(5, orientation)
        {
        }
    }
    class BattleshipShip : Ship
    {
        public BattleshipShip(Orientation orientation) : base(4, orientation)
        {
        }
    }
    class SubmarineShip : Ship
    {
        public SubmarineShip(Orientation orientation) : base(3, orientation)
        {
        }
    }
    class DestroyerShip : Ship
    {
        public DestroyerShip(Orientation orientation) : base(2, orientation)
        {
        }
    }
    class RescueShip : Ship
    {
        public RescueShip(Orientation orientation) : base(1, orientation)
        {
        }
    }
}
