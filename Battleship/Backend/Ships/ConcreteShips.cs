using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Backend.Ships
{
    public static class ShipFactory
    {
        private static readonly Dictionary<Type, Func<Ship>> _registry = new Dictionary<Type, Func<Ship>>()
        {
            { typeof(CarrierShip), () => new CarrierShip() },
            { typeof(BattleshipShip), () => new BattleshipShip() },
            { typeof(SubmarineShip), () => new SubmarineShip() },
            { typeof(DestroyerShip), () => new DestroyerShip() },
            { typeof(RescueShip), () => new RescueShip() }
        };
        public static Ship CreateShip(Type type)
        {
            if(_registry.TryGetValue(type, out var creator))
                return creator();
            throw new ArgumentException($"Ship type {type} is not registered in the factory");
        }
    }

    class CarrierShip : Ship
    {
        public CarrierShip(Orientation orientation = Orientation.Horizontal) : base(5, orientation)
        {
        }
    }
    class BattleshipShip : Ship
    {
        public BattleshipShip(Orientation orientation = Orientation.Horizontal) : base(4, orientation)
        {
        }
    }
    class SubmarineShip : Ship
    {
        public SubmarineShip(Orientation orientation = Orientation.Horizontal) : base(3, orientation)
        {
        }
    }
    class DestroyerShip : Ship
    {
        public DestroyerShip(Orientation orientation = Orientation.Horizontal) : base(2, orientation)
        {
        }
    }
    class RescueShip : Ship
    {
        public RescueShip(Orientation orientation = Orientation.Horizontal) : base(1, orientation)
        {
        }
    }
}
