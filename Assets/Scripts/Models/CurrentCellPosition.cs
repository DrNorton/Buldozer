using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Grid;
using UnityEngine;


namespace Assets.Scripts.Models
{
    public class CurrentCellPosition
    {
        private readonly int _columnNumber;
        private readonly int _rowNumber;
        private readonly Assets.Scripts.Grid.Grid _grid;
        
        private GridCell _currentCell;
        private GridCell _leftCell;
        private GridCell _rightCell;
        private GridCell _topCell;
        private GridCell _bottomCell;

        public CurrentCellPosition(int columnNumber, int rowNumber, Assets.Scripts.Grid.Grid grid)
        {
            _columnNumber = columnNumber;
            _rowNumber = rowNumber;
            _grid = grid;
        }

        public void SetCurrentCell(GridCell cell)
        {
            _currentCell = cell;
            FindNeighborsForCurrentCell();
        }

        public bool NavigateTo(BulldozerState state)
        {
            GridCell stateCell = null;
            switch (state)
            {
                case BulldozerState.Top:
                    stateCell = _topCell;

                    break;

                case BulldozerState.Bottom:
                    stateCell = _bottomCell;
                    break;

                case BulldozerState.Left:
                    stateCell = _leftCell;
                    break;

                case BulldozerState.Right:
                    stateCell = _rightCell;
                    break;
            }
            if (!DoNavigate(state, stateCell)) return false;

            return true;
        }

        private bool DoNavigate(BulldozerState state, GridCell cell)
        {
            if (cell == null) return false;
            if (CheckContent(cell, state) == false) return false;
            if (ChangeKamenIfExists(cell, state) == false) return false;
            SetCurrentCell(cell);
            return true;
        }

        private bool ChangeKamenIfExists(GridCell cell, BulldozerState state)
        {
            if (cell.GetKamen() != null)
            {
                var kamen = cell.GetKamen();
                if (StoneNotInWallAndNotStone(cell, state) == false) return false;
                if (StoneChangeGridPosition(cell, state) == false) return false;


                switch (state)
                {

                    case BulldozerState.Top:

                        Move(new Vector3(0, 1, 0), kamen);


                        break;

                    case BulldozerState.Bottom:
                        Move(new Vector3(0, -1, 0), kamen);

                        break;

                    case BulldozerState.Left:
                        Move(new Vector3(-1, 0, 0), kamen);


                        break;

                    case BulldozerState.Right:
                        Move(new Vector3(1, 0, 0), kamen);

                        break;
                }


            }
            return true;
        }

        private bool StoneNotInWallAndNotStone(GridCell cell, BulldozerState state)
        {
            var cell2 = new CoordinateForGrid(_columnNumber, _rowNumber) { Row = cell.RowNumber, Column = cell.ColumnNumber };
            //Проверяем дальше, есть ли стена, чтобы не пихать камень в стену

            switch (state)
            {
                case BulldozerState.Bottom:
                    var gridCell = NeighbohoodCell(cell2.SummBottomCell());
                    if (gridCell.GetContent() == CellContent.FencyElement || gridCell.GetContent() == CellContent.Stone || gridCell.GetContent() == CellContent.NotWorkingElement)
                    {
                        return false;
                    }

                    break;

                case BulldozerState.Left:
                    var gridCell2 = NeighbohoodCell(cell2.SummLeftCell());
                    if (gridCell2.GetContent() == CellContent.FencyElement || gridCell2.GetContent() == CellContent.Stone || gridCell2.GetContent() == CellContent.NotWorkingElement)
                    {
                        return false;
                    }
                    break;

                case BulldozerState.Right:
                    var gridCell3 = NeighbohoodCell(cell2.SummRightCell());
                    if (gridCell3.GetContent() == CellContent.FencyElement || gridCell3.GetContent() == CellContent.Stone || gridCell3.GetContent() == CellContent.NotWorkingElement)
                    {
                        return false;
                    }
                    break;

                case BulldozerState.Top:
                    var gridCell4 = NeighbohoodCell(cell2.SummTopCell());
                    if (gridCell4.GetContent() == CellContent.FencyElement || gridCell4.GetContent() == CellContent.Stone || gridCell4.GetContent() == CellContent.NotWorkingElement)
                    {
                        return false;
                    }
                    break;

                default:
                    return false;
            }
            return true;
        }

        private bool StoneChangeGridPosition(GridCell cell, BulldozerState state)
        {
            var newCell =
                FindDirection(
                    new CoordinateForGrid(_columnNumber, _rowNumber) { Row = cell.RowNumber, Column = cell.ColumnNumber },
                    state);
            if (newCell.GetContent() == CellContent.FencyElement)
            {
                return false;
            }
            var clone = cell.GetKamen();
            cell.SetKamen(null);
            newCell.SetKamen(clone);
       
            return true;
        }

        private void ChangeKamenPosition()
        {

        }



        private void Move(Vector3 t, GameObject obj)
        {
            obj.transform.position += t;
        }


        private void FindNeighborsForCurrentCell()
        {
            var currentCoordinate = new CoordinateForGrid(_columnNumber, _rowNumber)
            {
                Column = _currentCell.ColumnNumber,
                Row = _currentCell.RowNumber
            };
            FillNeighbohoodCell(out _leftCell, currentCoordinate.SummLeftCell());
            FillNeighbohoodCell(out _rightCell, currentCoordinate.SummRightCell());
            FillNeighbohoodCell(out _topCell, currentCoordinate.SummTopCell());
            FillNeighbohoodCell(out _bottomCell, currentCoordinate.SummBottomCell());

        }

        private GridCell FindDirection(CoordinateForGrid cell, BulldozerState state)
        {
            switch (state)
            {
                case BulldozerState.Bottom:
                    return NeighbohoodCell(cell.SummBottomCell());

                    break;

                case BulldozerState.Left:
                    return NeighbohoodCell(cell.SummLeftCell());

                    break;

                case BulldozerState.Right:
                    return NeighbohoodCell(cell.SummRightCell());

                    break;

                case BulldozerState.Top:
                    return NeighbohoodCell(cell.SummTopCell());

                    break;

                default:
                    return null;
            }

        }

        private GridCell NeighbohoodCell(CoordinateForGrid coordinate)
        {
            if (coordinate == null)
            {
                return null;
            }
            else
            {
                return _grid.GetCell(coordinate.Column, coordinate.Row);
            }
        }


        private bool CheckContent(GridCell cell, BulldozerState state)
        {
            if (cell.GetContent() == CellContent.FencyElement ||cell.GetContent()==CellContent.NotWorkingElement)
            {
                return false;
            }

            return true;
        }

        private void FillNeighbohoodCell(out GridCell cell, CoordinateForGrid coordinate)
        {
            if (coordinate == null)
            {
                cell = null;
            }
            else
            {
                cell = _grid.GetCell(coordinate.Column, coordinate.Row);
            }

        }

    }
}
