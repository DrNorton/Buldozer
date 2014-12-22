using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models
{
    public class CoordinateForGrid
    {
        private readonly int _columnNumber;
        private readonly int _rowNumber;

        public int Column { get; set; }
        public int Row { get; set; }

        public CoordinateForGrid(int columnNumber, int rowNumber)
        {
            _columnNumber = columnNumber;
            _rowNumber = rowNumber;
        }
        public CoordinateForGrid SummLeftCell()
        {
            var newColumn = Column - 1;
            if (newColumn < 0)
            {
                return null;
            }
            var leftCell = new CoordinateForGrid(_columnNumber, _rowNumber);
            leftCell.Column = newColumn;
            leftCell.Row = Row;
            return leftCell;
        }

        public CoordinateForGrid SummRightCell()
        {
            var newColumn = Column + 1;
            if (newColumn > _columnNumber - 1)
            {
                return null;
            }
            var rightCell = new CoordinateForGrid(_columnNumber, _rowNumber);
            rightCell.Column = newColumn;
            rightCell.Row = Row;
            return rightCell;
        }

        public CoordinateForGrid SummTopCell()
        {
            var newRow = Row - 1;
            if (newRow < 0)
            {
                return null;
            }
            var topCell = new CoordinateForGrid(_columnNumber, _rowNumber);
            topCell.Column = Column;
            topCell.Row = newRow;
            return topCell;
        }

        public CoordinateForGrid SummBottomCell()
        {
            var newRow = Row + 1;
            if (newRow > _rowNumber - 1)
            {
                return null;
            }
            var bottomCell = new CoordinateForGrid(_columnNumber, _rowNumber);
            bottomCell.Column = Column;
            bottomCell.Row = newRow;
            return bottomCell;
        }
    }
}
