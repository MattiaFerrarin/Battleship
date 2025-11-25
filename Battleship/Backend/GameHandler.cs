using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipWinforms.Backend
{
    public class GameHandler
    {
        public Board PlayerBoard { get; }
        public Board EnemyBoard { get; }

        public delegate void CellHitEventHandler(int x, int y, ExternalCellState state);
        public event CellHitEventHandler CellHit;

        public GameHandler(int size)
        {
            PlayerBoard = new Board(size, size);
            EnemyBoard = new Board(size, size);
        }

        public ExternalCellState ShootEnemy(int x, int y)
        {
            var state = EnemyBoard.Cells[x, y].Hit();

            CellHit?.Invoke(x, y, state);

            return state;
        }
    }
}
