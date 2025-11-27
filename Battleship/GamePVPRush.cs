using BattleshipWinforms.Backend;
using BattleshipWinforms.Backend.Audio;
using BattleshipWinforms.Backend.Ships;
using BattleshipWinforms.Frontend;
using BattleshipWinforms.Properties;
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

namespace BattleshipWinforms
{
    public partial class GamePVPRush : Form
    {
        private List<string> Players;
        InternalGameStatus InternalGameState;
        Queue<string> PlayerTurns;
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

        public GamePVPRush(List<string> players)
        {
            Players = players;
            InitializeComponent();
            PlayerTurns = new Queue<string>(Players);
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
                    SetupPlacingTurn(PlayerTurns.First());
                    InternalGameState = InternalGameStatus.PlacingShips;
                    goto case InternalGameStatus.PlacingShips;
                case InternalGameStatus.PlacingShips:
                    StartPlacingShipsCycle();
                    break;
                case InternalGameStatus.SettingUpAttack:
                    PlayerTurns = new Queue<string>(Players);
                    SetupAttackingTurn(PlayerTurns.First());
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
            boardPanel.Name = "boardPanel";
            boardPanel.Top = 50;
            boardPanel.Left = 50;

            Button confirmBtn = new Button()
            {
                Name = "btn_confirm",
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
                    if(PlayerTurns.Count > 1)
                    {
                        PlayerTurns.Dequeue();
                        InternalGameState = InternalGameStatus.SettingUpShipPlacement;
                        CycleHandler();
                    }
                    else
                    {
                        InternalGameState = InternalGameStatus.SettingUpAttack;
                        CycleHandler();
                    }
                }
            };

            Label labelTurn = new Label()
            {
                Name = "lbl_turn",
                Text = $"Placing Turn of {player}",
                Font = new Font("Arial", 14),
                Top = boardPanel.Top - 30,
                Left = boardPanel.Left,
                AutoSize = true
            };

            Button undoBtn = new Button()
            {
                Name = "btn_undo",
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
                    Board board = _GameHandler.Boards[PlayerTurns.First()];
                    BoardActiveShip lastActiveShipBoard = board.Ships.Find(sh => sh.Ship == lastShip);
                    Point lastShipPos = lastActiveShipBoard.Position;
                    board.RemoveShip(lastActiveShipBoard);
                    RemoveShipFromUI(lastActiveShipBoard.Ship, lastShipPos.X, lastShipPos.Y);
                    _shipsToPlace.Push(lastShip);
                    this.Controls.Find("btn_confirm", false).First().Enabled = false;
                    UpdateShipQueueUI();
                }
            };

            FlowLayoutPanel shipsQueue = new FlowLayoutPanel()
            {
                Name = "flp_shipsQueue",
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                WrapContents = true,
                Top = undoBtn.Bottom + 10,
                Left = boardPanel.Left,
                Padding = new Padding(5)
            };

            ReplaceControl(boardPanel, "boardPanel");
            ReplaceControl(confirmBtn, "btn_confirm");
            ReplaceControl(labelTurn, "lbl_turn");
            ReplaceControl(undoBtn, "btn_undo");
            ReplaceControl(shipsQueue, "flp_shipsQueue");
        }

        private void StartPlacingShipsCycle()
        {
            var ships = _GameSettings.Ships;
            foreach (var ship in ships)
            {
                _shipsToPlace.Push(ship);
            }
            UpdateShipQueueUI();
        }

        private void SetupAttackingTurn(string player)
        {
            TableLayoutPanel boardPanel = BoardRenderer.CreateBoard(_GameHandler.Boards[player], OnBoardCellClickAttacking, OnBoardCellEnterAttacking, OnBoardCellLeaveAttacking);
            boardPanel.Name = "boardPanel";
            boardPanel.Top = 50;
            boardPanel.Left = 50;

            Label labelTurn = new Label()
            {
                Name = "lbl_turn",
                Text = $"Placing Turn of {player}",
                Font = new Font("Arial", 14),
                Top = boardPanel.Top - 30,
                Left = boardPanel.Left,
                AutoSize = true
            };

            ReplaceControl(boardPanel, "boardPanel");
            ReplaceControl(labelTurn, "lbl_turn");
            if (this.Controls.Find("flp_shipsQueue", false).First() != null) this.Controls.Remove(this.Controls.Find("flp_shipsQueue", false).First());
            if (this.Controls.Find("btn_undo", false).First() != null) this.Controls.Remove(this.Controls.Find("btn_undo", false).First());
            if (this.Controls.Find("btn_confirm", false).First() != null) this.Controls.Remove(this.Controls.Find("btn_confirm", false).First());
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

            (int x, int y) coords = ((int, int)) ((PictureBox)sender).Tag;
            LastVisitedCellCoords = new Point(coords.x, coords.y);
            if (_GameHandler.Boards[PlayerTurns.First()].CanPlaceShip(_shipsToPlace.First(), new Point(coords.x, coords.y)))
            {
                DrawShipOnUI(_shipsToPlace.First(),coords.x,coords.y);
            }
        }
        private void OnBoardCellLeavePlacing(object sender, EventArgs e)
        {
            if (_shipsToPlace.Count < 1)
                return;

            (int x, int y) coords = ((int, int))((PictureBox)sender).Tag;
            if (_GameHandler.Boards[PlayerTurns.First()].CanPlaceShip(_shipsToPlace.First(), new Point(coords.x, coords.y)))
            {
                RemoveShipFromUI(_shipsToPlace.First(), coords.x, coords.y);
            }
        }
        private void OnRPressed()
        {
            if (InternalGameState == InternalGameStatus.PlacingShips)
            {
                if (_shipsToPlace.Count > 0)
                {
                    if (_GameHandler.Boards[PlayerTurns.First()].CanPlaceShip(_shipsToPlace.First(), new Point(LastVisitedCellCoords.X, LastVisitedCellCoords.Y)))
                    {
                        RemoveShipFromUI(_shipsToPlace.First(), LastVisitedCellCoords.X, LastVisitedCellCoords.Y);
                    }
                    _shipsToPlace.First().Rotate();
                    OnBoardCellEnterPlacing(((TableLayoutPanel)this.Controls.Find("boardPanel", false).First()).GetControlFromPosition(LastVisitedCellCoords.X, LastVisitedCellCoords.Y), EventArgs.Empty);
                }
            }
        }
        private void OnBoardCellClickPlacing(int x, int y)
        {
            if (InternalGameState == InternalGameStatus.PlacingShips)
            {
                if (_shipsToPlace.Count > 0)
                {
                    Board board = _GameHandler.Boards[PlayerTurns.First()];

                    if (!board.CanPlaceShip(_shipsToPlace.First(), new Point(x, y)))
                    {
                        return;
                    }

                    board.PlaceShip(_shipsToPlace.First(), new Point(x, y));

                    DrawShipOnUI(_shipsToPlace.First(), x, y);
                    _shipsPlacedHistory.Push(_shipsToPlace.Pop());
                }
                if(_shipsToPlace.Count < 1)
                {
                    this.Controls.Find("btn_confirm", false).First().Enabled = true;
                }
            }
            UpdateShipQueueUI();
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

            Board board = _GameHandler.Boards[PlayerTurns.First()];
            if (board.Cells[x, y].ExternalState != ExternalCellState.Uncovered)
                return;

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

                    UpdateCellColorUI(sx, sy, CellState.Sunk);
                }
                DrawShipOnUI(sunkShip.Ship, sunkShip.Position.X, sunkShip.Position.Y);
            }

            ExternalCellState extCellState = board.Cells[x, y].ExternalState;
            CellState colorCellState =
                extCellState == ExternalCellState.Hit && retVal == BoardActiveShipStatus.Sunk ? CellState.Sunk :
                extCellState == ExternalCellState.Hit && retVal != BoardActiveShipStatus.Sunk ? CellState.Hit :
                extCellState == ExternalCellState.Miss ? CellState.Miss : CellState.Uncovered;

            UpdateCellColorUI(x,y, colorCellState);


            List<BoardActiveShip> remainingShips = GetRemainingShips(board);
            // ------------------------------------------------------------
            //  UNFLEXIBLE SINCE IT KIND OF WORKS WELL ONLY WITH 2 PLAYERS
            // ------------------------------------------------------------
            if (remainingShips.Count == 0) // Won                  
            {
                AudioHandler.PlaySound(SoundType.Victory);
                MessageBox.Show($"{PlayerTurns.First()} has won!");
                PlayerTurns.Dequeue();
                
                InternalGameState = InternalGameStatus.None;
                CycleHandler();
            }
        }
        private void OnBoardCellEnterAttacking(object sender, EventArgs e)
        {
            // (int x, int y) coords = ((int, int))((PictureBox)sender).Tag;
            // Paint yellow or something the tile
        }
        private void OnBoardCellLeaveAttacking(object sender, EventArgs e)
        {
            // (int x, int y) coords = ((int, int))((PictureBox)sender).Tag;
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
        #region UIFunctions
        //
        // UI Functions
        //

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
        #endregion
        #region MiscellaneousUtility
        //
        // Miscellaneous Utility
        //
        private void ReplaceControl(Control control, string name)
        {
            var oldControl = this.Controls.Find(name, false).FirstOrDefault();
            if (oldControl != null) this.Controls.Remove(oldControl);
            this.Controls.Add(control);
        }
        public enum CellState
        {
            Hit,
            Sunk,
            Miss,
            Uncovered
        }
        #endregion
    }
}
