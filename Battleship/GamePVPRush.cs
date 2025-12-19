using Battleship.Backend;
using Battleship.Backend.Audio;
using Battleship.Backend.Ships;
using Battleship.Frontend;
using Battleship.Properties;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using static Battleship.Utils;

namespace Battleship
{
    public partial class GamePVPRush : Form
    {
        private List<string> Players;
        InternalGameStatus InternalGameState;
        string CurrentPlayer;
        GameHandler _GameHandler;
        GameSettings _GameSettings = new GameSettings(10, new List<Ship>() {
            new BattleshipShip(),
            new SubmarineShip(),
            new SubmarineShip(),
            new DestroyerShip(),
            new DestroyerShip(),
            new RescueShip(),
        });

        // For Placing cycle
        private Stack<Ship> _shipsToPlace = new Stack<Ship>();
        private Stack<Ship> _shipsPlacedHistory = new Stack<Ship>();
          // For Removing the ship rendering when rotating
          private Point LastVisitedCellCoords;
        // Internal Gamespecific names
        private readonly Dictionary<string,string> ControlNames = new Dictionary<string, string>()
        {
            { "board", "tlp_boardPanel" },
            { "turnLabel", "lbl_turn" },
            { "confirmButton", "btn_confirm" },
            { "undoButton", "btn_undo" },
            { "shipsQueue", "flp_shipsQueue" }
        };

        public GamePVPRush(List<string> players)
        {
            Players = players;
            InitializeComponent();
            CurrentPlayer = Players.First();
            InternalGameState = InternalGameStatus.SettingUpShipPlacement;
            CycleHandler();
        }

        #region CycleHandlers
        private void CycleHandler()
        {
            switch (InternalGameState)
            {
                case InternalGameStatus.SettingUpShipPlacement:
                    _GameHandler = new GameHandler(_GameSettings.BoardSize, Players);
                    SetupPlacingTurn(CurrentPlayer);
                    InternalGameState = InternalGameStatus.PlacingShips;
                    goto case InternalGameStatus.PlacingShips;
                case InternalGameStatus.PlacingShips:
                    StartPlacingShipsCycle();
                    break;
                case InternalGameStatus.SettingUpAttack:
                    SetupAttackingTurn(CurrentPlayer);
                    InternalGameState = InternalGameStatus.Attacking;
                    goto case InternalGameStatus.Attacking;
                case InternalGameStatus.Attacking:
                    StartAttackingCycle();
                    break;
            }
        }
        private void SetupPlacingTurn(string player)
        {
            TableLayoutPanel boardPanel = BoardRenderer.CreateBoard(_GameHandler.Boards[player], OnBoardCellClickPlacing, OnBoardCellEnterPlacing, OnBoardCellLeavePlacing);
            boardPanel.Name = ControlNames["board"];
            boardPanel.Top = 50;
            boardPanel.Left = 50;

            Button confirmBtn = new Button()
            {
                Name = ControlNames["confirmButton"],
                Text = "Confirm Placement",
                Top = boardPanel.Bottom + 10,
                Left = boardPanel.Left,
                Size = new Size(120, 30),
                Enabled = false
            };
            confirmBtn.Click += (s, e) =>
            {
                if(_shipsToPlace.Count < 1)
                {
                    InternalGameState = InternalGameStatus.SettingUpAttack;
                    CycleHandler();
                }
            };

            Label labelTurn = new Label()
            {
                Name = ControlNames["turnLabel"],
                Text = $"Placing Turn of {player}",
                Font = new Font("Arial", 14),
                Top = boardPanel.Top - 30,
                Left = boardPanel.Left,
                AutoSize = true
            };

            Button undoBtn = new Button()
            {
                Name = ControlNames["undoButton"],
                Text = "Undo Last Ship",
                Top = confirmBtn.Top,
                Left = confirmBtn.Right + 10,
                Size = new Size(120, 30),
            };
            undoBtn.Click += (s, e) =>
            {
                if(_shipsPlacedHistory.Count > 0)
                {
                    Ship lastShip = _shipsPlacedHistory.Pop();
                    Board board = _GameHandler.Boards[CurrentPlayer];
                    BoardActiveShip lastActiveShipBoard = board.Ships.Find(sh => sh.Ship == lastShip);
                    Point lastShipPos = lastActiveShipBoard.Position;
                    board.RemoveShip(lastActiveShipBoard);
                    UIHandlers.RemoveShipFromUI((TableLayoutPanel)GetSpecificControl(this, ControlNames["board"]), lastActiveShipBoard.Ship, new Point(lastShipPos.X, lastShipPos.Y));
                    _shipsToPlace.Push(lastShip);
                    this.Controls.Find("btn_confirm", false).First().Enabled = false;
                    UIHandlers.UpdateShipQueueUI((FlowLayoutPanel)GetSpecificControl(this, ControlNames["shipsQueue"]), _shipsToPlace);
                }
            };

            FlowLayoutPanel shipsQueue = new FlowLayoutPanel()
            {
                Name = ControlNames["shipsQueue"],
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                WrapContents = true,
                Top = undoBtn.Bottom + 10,
                Left = boardPanel.Left,
                Padding = new Padding(5)
            };

            ReplaceControl(this, boardPanel, ControlNames["board"]);
            ReplaceControl(this, confirmBtn, ControlNames["confirmButton"]);
            ReplaceControl(this, labelTurn, ControlNames["turnLabel"]);
            ReplaceControl(this, undoBtn, ControlNames["undoButton"]);
            ReplaceControl(this, shipsQueue, ControlNames["shipsQueue"]);
        }
        private void StartPlacingShipsCycle()
        {
            var ships = _GameSettings.Ships;
            foreach (var ship in ships)
            {
                _shipsToPlace.Push(ship);
            }
            UIHandlers.UpdateShipQueueUI((FlowLayoutPanel)GetSpecificControl(this, ControlNames["shipsQueue"]), _shipsToPlace);
        }
        private void SetupAttackingTurn(string player)
        {
            TableLayoutPanel boardPanel = BoardRenderer.CreateBoard(_GameHandler.Boards[player], OnBoardCellClickAttacking, OnBoardCellEnterAttacking, OnBoardCellLeaveAttacking);
            boardPanel.Name = ControlNames["board"];
            boardPanel.Top = 50;
            boardPanel.Left = 50;

            Label labelTurn = new Label()
            {
                Name = ControlNames["turnLabel"],
                Text = $"Attacking Turn of {player}",
                Font = new Font("Arial", 14),
                Top = boardPanel.Top - 30,
                Left = boardPanel.Left,
                AutoSize = true
            };

            ReplaceControl(this, boardPanel, ControlNames["board"]);
            ReplaceControl(this, labelTurn, ControlNames["turnLabel"]);
            if (this.Controls.Find(ControlNames["shipsQueue"], false).First() != null) this.Controls.Remove(this.Controls.Find(ControlNames["shipsQueue"], false).First());
            if (this.Controls.Find(ControlNames["undoButton"], false).First() != null) this.Controls.Remove(this.Controls.Find(ControlNames["undoButton"], false).First());
            if (this.Controls.Find(ControlNames["confirmButton"], false).First() != null) this.Controls.Remove(this.Controls.Find(ControlNames["confirmButton"], false).First());
        }
        private void StartAttackingCycle()
        {
        }
        #endregion
        #region PlacingTurn
        //
        // Placing Turn
        //
        private void OnBoardCellEnterPlacing(object sender, EventArgs e)
        {
            if (_shipsToPlace.Count < 1)
                return;

            Point coords = new Point((((int, int))((PictureBox)sender).Tag).Item1, (((int, int))((PictureBox)sender).Tag).Item2);
            LastVisitedCellCoords = coords;
            if (_GameHandler.Boards[CurrentPlayer].CanPlaceShip(_shipsToPlace.First(), coords))
            {
                UIHandlers.DrawShipOnUI((TableLayoutPanel)GetSpecificControl(this, ControlNames["board"]), _shipsToPlace.First(),coords);
            }
        }
        private void OnBoardCellLeavePlacing(object sender, EventArgs e)
        {
            if (_shipsToPlace.Count < 1)
                return;

            Point coords = new Point((((int, int))((PictureBox)sender).Tag).Item1, (((int, int))((PictureBox)sender).Tag).Item2);
            if (_GameHandler.Boards[CurrentPlayer].CanPlaceShip(_shipsToPlace.First(), coords))
            {
                UIHandlers.RemoveShipFromUI((TableLayoutPanel)GetSpecificControl(this, ControlNames["board"]), _shipsToPlace.First(), coords);
            }
        }
        private void OnRPressed()
        {
            if (InternalGameState == InternalGameStatus.PlacingShips)
            {
                if (_shipsToPlace.Count > 0)
                {
                    if (_GameHandler.Boards[CurrentPlayer].CanPlaceShip(_shipsToPlace.First(), LastVisitedCellCoords))
                    {
                        UIHandlers.RemoveShipFromUI((TableLayoutPanel)GetSpecificControl(this, ControlNames["board"]), _shipsToPlace.First(), LastVisitedCellCoords);
                    }
                    _shipsToPlace.First().Rotate();
                    OnBoardCellEnterPlacing(((TableLayoutPanel)this.Controls.Find(ControlNames["board"], false).First()).GetControlFromPosition(LastVisitedCellCoords.X, LastVisitedCellCoords.Y), EventArgs.Empty);
                }
            }
        }
        private void GamePVP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.R)
            {
                OnRPressed();
                return;
            }

            if (!e.Control && e.KeyCode == Keys.R)
            {
                OnRPressed();
                return;
            }
        }
        private void OnBoardCellClickPlacing(int x, int y)
        {
            if (InternalGameState == InternalGameStatus.PlacingShips)
            {
                if (_shipsToPlace.Count > 0)
                {
                    Board board = _GameHandler.Boards[CurrentPlayer];

                    if (!board.CanPlaceShip(_shipsToPlace.First(), new Point(x, y)))
                    {
                        return;
                    }

                    board.PlaceShip(_shipsToPlace.First(), new Point(x, y));

                    UIHandlers.DrawShipOnUI((TableLayoutPanel)GetSpecificControl(this, ControlNames["board"]), _shipsToPlace.First(), new Point(x, y));
                    _shipsPlacedHistory.Push(_shipsToPlace.Pop());
                }

                if (_shipsToPlace.Count < 1) // Not an else because if the ship gets removed in the same turn it needs to be enabled
                {
                    this.Controls.Find("btn_confirm", false).First().Enabled = true;
                }
            }
            UIHandlers.UpdateShipQueueUI((FlowLayoutPanel)GetSpecificControl(this, ControlNames["shipsQueue"]),_shipsToPlace);
        }
        #endregion
        #region AttackingTurn
        //
        // Attacking Turn
        //
        private void OnBoardCellClickAttacking(int x, int y)
        {
            if (InternalGameState != InternalGameStatus.Attacking)
                return;

            Board board = _GameHandler.Boards[CurrentPlayer];
            if (board.Cells[x, y].ExternalState != ExternalCellState.Uncovered)
                return;

            // REMEMBER TO REMAKE THE .HIT() FUNCTION
            BoardActiveShipStatus? retVal = board.Hit(new Point(x,y));
            if (retVal == null)
                AudioHandler.PlaySound(SoundType.Miss);
            if (retVal == BoardActiveShipStatus.Hit)
                AudioHandler.PlaySound(SoundType.Hit);
            else if(retVal == BoardActiveShipStatus.Sunk)
            {
                AudioHandler.PlaySound(SoundType.Sunk);
                BoardActiveShip sunkShip = board.GetShipFromCoords(new Point(x,y));
                for (int i = 0; i < sunkShip.Ship.Length; i++)
                {
                    int sx = sunkShip.Ship.Orientation == Backend.Ships.Orientation.Horizontal
                             ? sunkShip.Position.X + i
                             : sunkShip.Position.X;

                    int sy = sunkShip.Ship.Orientation == Backend.Ships.Orientation.Vertical
                             ? sunkShip.Position.Y + i
                             : sunkShip.Position.Y;

                    UIHandlers.UpdateCellColorUI((TableLayoutPanel)GetSpecificControl(this, ControlNames["board"]), new Point(sx, sy), CellState.Sunk);
                }
                UIHandlers.DrawShipOnUI((TableLayoutPanel)GetSpecificControl(this, ControlNames["board"]), sunkShip.Ship, sunkShip.Position);
            }

            ExternalCellState extCellState = board.Cells[x, y].ExternalState;
            CellState colorCellState =
                extCellState == ExternalCellState.Hit && retVal == BoardActiveShipStatus.Sunk ? CellState.Sunk :
                extCellState == ExternalCellState.Hit && retVal != BoardActiveShipStatus.Sunk ? CellState.Hit :
                extCellState == ExternalCellState.Miss ? CellState.Miss : CellState.Uncovered;

            UIHandlers.UpdateCellColorUI((TableLayoutPanel)GetSpecificControl(this, ControlNames["board"]), new Point(x,y), colorCellState);


            List<BoardActiveShip> remainingShips = GetRemainingShips(board);
            if (remainingShips.Count == 0) // Won                  
            {
                AudioHandler.PlaySound(SoundType.Victory);
                MessageBox.Show($"{CurrentPlayer} has lost all his ships!\nThe game has been won!");
                
                InternalGameState = InternalGameStatus.None;
            }
        }
        private void OnBoardCellEnterAttacking(object sender, EventArgs e)
        {
            // Paint yellow or something the tile
        }
        private void OnBoardCellLeaveAttacking(object sender, EventArgs e)
        {
            // Paint light blue or something the tile (untouched tile color)
        }
        private List<BoardActiveShip> GetRemainingShips(Board board)
        {
            List<BoardActiveShip> remainingShips = new List<BoardActiveShip>();
            foreach (var ship in board.Ships)
            {
                if (ship.Status != BoardActiveShipStatus.Sunk)
                    remainingShips.Add(ship);
            }
            return remainingShips;
        }
        #endregion
    }
}
