using BattleshipWinforms.Backend.Ships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BattleshipWinforms.GamePVPRush;
using System.Windows.Forms;
using System.Drawing;

namespace BattleshipWinforms.Frontend
{
    public static class UIHandlers
    {
        private static readonly Dictionary<Type, IShipRenderer> _ShipRenderers = new Dictionary<Type, IShipRenderer>()
        {
            { typeof(BattleshipShip), new BattleshipRenderer() },
            { typeof(CarrierShip), new CarrierRenderer() },
            { typeof(SubmarineShip), new SubmarineRenderer() },
            { typeof(DestroyerShip), new DestroyerRenderer() },
            { typeof(RescueShip), new RescueRenderer() }
        };
        public static void UpdateShipQueueUI(FlowLayoutPanel shipsQueueflp, Stack<Ship> shipsToPlace)
        {
            shipsQueueflp.Controls.Clear();
            foreach (var ship in shipsToPlace.Reverse())
            {
                PictureBox pb = new PictureBox()
                {
                    Width = 50,
                    Height = 50,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Image = ShipIconRenderer.GetImage(ship)
                };
                shipsQueueflp.Controls.Add(pb);
            }
        }

        public static void DrawShipOnUI(TableLayoutPanel boardtlp, Ship ship, Point coords)
        {
            var renderer = _ShipRenderers[ship.GetType()];

            for (int i = 0; i < ship.Length; i++)
            {
                int x = ship.Orientation == Backend.Ships.Orientation.Horizontal ? coords.X + i : coords.X;
                int y = ship.Orientation == Backend.Ships.Orientation.Vertical ? coords.Y + i : coords.Y;

                PictureBox pb = (PictureBox)boardtlp.GetControlFromPosition(x, y);

                ShipPart part = ship.GetPartAt(i);
                pb.Image = renderer.GetImage(part, ship.Orientation);
            }
        }

        public static void RemoveShipFromUI(TableLayoutPanel boardtlp, Ship ship, Point coords)
        {
            for (int i = 0; i < ship.Length; i++)
            {
                int x = ship.Orientation == Backend.Ships.Orientation.Horizontal ? startX + i : startX;
                int y = ship.Orientation == Backend.Ships.Orientation.Vertical ? startY + i : startY;
                PictureBox pb = (PictureBox)boardtlp.GetControlFromPosition(x, y);
                pb.Image = null;
            }
        }

        public static void UpdateCellColorUI(TableLayoutPanel boardtlp, Point coords, CellState state)
        {
            PictureBox pb = (PictureBox)boardtlp.GetControlFromPosition(x, y);

            pb.BackColor = BoardRenderer.StateToBackColor(state);
        }
    }
}
