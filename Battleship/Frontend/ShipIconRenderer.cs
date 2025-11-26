using BattleshipWinforms.Backend.Ships;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleshipWinforms.Frontend
{
    public static class ShipIconRenderer
    {
        public static readonly Dictionary<Type, Func<Image>> _registry = new Dictionary<Type, Func<Image>>()
        {
            { typeof(CarrierShip), () => Properties.Resources.CarrierShipIcon },
            { typeof(BattleshipShip), () => Properties.Resources.BattleshipShipIcon },
            { typeof(SubmarineShip), () => Properties.Resources.SubmarineShipIcon },
            { typeof(DestroyerShip), () => Properties.Resources.DestroyerShipIcon },
            { typeof(RescueShip), () => Properties.Resources.RescueShipIcon }
        };
        public static Image GetImage(Ship ship)
        {
            if(_registry.TryGetValue(ship.GetType(), out var returner))
                return returner();
            else
                throw new ArgumentException("No image registered for ship type " + ship.GetType().Name);
        }
    }
}
