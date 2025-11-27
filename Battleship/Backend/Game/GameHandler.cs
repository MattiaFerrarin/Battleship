using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipWinforms.Backend
{
    public class GameHandler
    {
        public Dictionary<string, Board> Boards { get; } = new Dictionary<string, Board>();

        public delegate void CellHitEventHandler(int x, int y, ExternalCellState state);
        public event CellHitEventHandler CellHit;

        public GameHandler(Size size, List<string> players)
        {
            if (size.Width < 1)
                size.Width = 1;
            if (size.Height < 1)
                size.Height = 1;
            foreach (string player in players)
            {
                Boards.Add(player, new Board(size.Width, size.Height));
            }
        }

        public ExternalCellState? ShootAt(Board board, int x, int y)
        {
            try
            {
                var state = board.Cells[x, y].Hit();
                CellHit?.Invoke(x, y, state);
                return state;
            }
            catch{ }
            return null;
        }
    }
    public enum InternalGameStatus
    {
        None,
        SettingUpShipPlacement,
        PlacingShips,
        SettingUpAttack,
        Attacking
    }
}
