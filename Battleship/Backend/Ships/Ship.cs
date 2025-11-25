using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipWinforms.Backend.Ships
{
    public abstract class Ship
    {
        public int Length { get; }
        public Orientation Orientation { get; private set; }

        protected Ship(int length, Orientation orientation)
        {
            Length = length;
            Orientation = orientation;
        }

        public ShipPart GetPartAt(int index)
        {
            if (index == 0) return ShipPart.Back;
            if (index == Length - 1) return ShipPart.Front;
            return ShipPart.Middle;
        }
    }

    public struct ShipTile
    {
        public ShipPart Part { get; }

        public ShipTile(ShipPart part)
        {
            Part = part;
        }
    }

    public enum ShipPart
    {
        Front,
        Middle,
        Back
    }
    public enum Orientation
    {
        Horizontal,
        Vertical
    }
}
