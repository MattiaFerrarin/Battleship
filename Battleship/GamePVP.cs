using BattleshipWinforms.Backend;
using BattleshipWinforms.Backend.Ships;
using BattleshipWinforms.Frontend;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleshipWinforms
{
    public partial class GamePVP : Form
    {
        private List<string> Players = new List<string>() { "player " };
        GameStatus GameState;
        GameHandler _GameHandler;

        // Ship Renderers access
        private readonly Dictionary<Type, IShipRenderer> _ShipRenderers = new Dictionary<Type, IShipRenderer>()
        {
            { typeof(BattleshipShip), new BattleshipRenderer() },
            { typeof(CarrierShip), null },
            { typeof(SubmarineShip), null },
            { typeof(DestroyerShip), null },
            { typeof(RescueShip), null }
        };
        // For Placing cycle
        private Ship _shipBeingPlaced;

        public GamePVP()
        {
            InitializeComponent();

            GameState = GameStatus.None;
            _GameHandler = new GameHandler(10, Players);

            TableLayoutPanel boardPanel = BoardRenderer.CreateBoard(_GameHandler.Boards[Players[0]], OnBoardCellClick);

            this.Controls.Add(boardPanel);

            StartPlacingShipsCycle();
        }

        private void StartPlacingShipsCycle()
        {
            GameState = GameStatus.PlacingShips;

            _shipBeingPlaced = new BattleshipShip(Backend.Ships.Orientation.Horizontal);
            // Start the placing ships cycle
            // on condition jump to StartAttackingCycle()
        }

        private void StartAttackingCycle()
        {
            GameState = GameStatus.Attacking;
            // Start Attacking cycle
            // On condition end game
        }

        private void OnBoardCellClick(int x, int y)
        {
            if(GameState == GameStatus.PlacingShips)
            {
                Board board = _GameHandler.Boards[Players[0]];

                if (!ShipPositioningHandler.CanPlaceShip(board, _shipBeingPlaced, new Point(x, y)))
                {
                    MessageBox.Show("Cannot place ship here.");
                    return;
                }

                ShipPositioningHandler.PlaceShip(board, _shipBeingPlaced, new Point(x, y));

                DrawShipOnUI(_shipBeingPlaced, x, y);

                // TODO: Move to next ship in placing queue
                return;
            }
            else if(GameState == GameStatus.Attacking)
            {
                ExternalCellState result = _GameHandler.ShootAt(_GameHandler.Boards[Players[0]], x, y);

                UpdateCellUI(x, y, result);
            }
        }

        private void DrawShipOnUI(Ship ship, int startX, int startY)
        {
            var renderer = _ShipRenderers[ship.GetType()];

            TableLayoutPanel tlp = Controls.OfType<TableLayoutPanel>().First();

            for (int i = 0; i < ship.Length; i++)
            {
                int x = ship.Orientation == Backend.Ships.Orientation.Horizontal ? startX + i : startX;
                int y = ship.Orientation == Backend.Ships.Orientation.Vertical ? startY + i : startY;

                PictureBox pb = (PictureBox)tlp.GetControlFromPosition(x, y);

                ShipPart part = ship.GetPartAt(i);
                pb.Image = renderer.GetImage(part, ship.Orientation);
            }
        }

        // TO CHANGE SO IT ALSO PLACES IMAGES
        private void UpdateCellUI(int x, int y, ExternalCellState state)
        {
            // Gets the controls of type TableLayoutPanel and picks the first (and only one)
            TableLayoutPanel boardTLP = Controls.OfType<TableLayoutPanel>().First();
            // Uses a function of the TLP class that gets the control inside a certain cell (from row and col)
            PictureBox pb = (PictureBox)boardTLP.GetControlFromPosition(x, y);

            pb.BackColor = BoardRenderer.StateToBackColor(state);
        }
    }
}
