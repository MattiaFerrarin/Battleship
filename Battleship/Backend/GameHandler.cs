using System;
using System.Collections.Generic;
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

        public GameHandler(int size, List<string> players)
        {
            if (size < 1)
                size = 1;
            foreach (string player in players)
            {
                Boards.Add(player, new Board(size, size));
            }
        }

        public ExternalCellState ShootAt(Board board, int x, int y)
        {
            var state = board.Cells[x, y].Hit();

            CellHit?.Invoke(x, y, state);

            return state;
        }
    }
    public enum GameStatus
    {
        None,
        PlacingShips,
        Attacking
    }
}
