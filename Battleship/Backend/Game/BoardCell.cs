using BattleshipWinforms.Backend.Ships;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipWinforms.Backend
{
    public class BoardCell
    {
        public InternalCellState InternalState { get; private set; }
        public ExternalCellState ExternalState { get; private set; }
        public ShipTile? ShipTile { get; private set; }

        public bool IsHit => ExternalState == ExternalCellState.Hit;

        public BoardCell()
        {
            InternalState = InternalCellState.Empty;
            ExternalState = ExternalCellState.Uncovered;
        }

        public void RemoveShipTile()
        {
            InternalState = InternalCellState.Empty;
            ShipTile = null;
        }

        public void PlaceShipTile(ShipTile tile)
        {
            InternalState = InternalCellState.Occupied;
            ShipTile = tile;
        }

        public ExternalCellState Hit()
        {
            if (ExternalState != ExternalCellState.Uncovered)
                throw new InvalidOperationException("Cell already uncovered");

            ExternalState = InternalState == InternalCellState.Occupied ? ExternalCellState.Hit : ExternalCellState.Miss;
            return ExternalState;
        }
    }
    public enum InternalCellState
    {
        Empty,
        Occupied
    }
    public enum ExternalCellState
    {
        Uncovered,
        Miss,
        Hit
    }
}
