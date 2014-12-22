using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Grid
{
    public class GridCell
    {
        private Vector2 _position;
        private CellContent _currentContent;
        private int _number;
        private readonly int _columnNumber;
        private readonly int _rowNumber;
        private GameObject _kamen;
        private bool _finishFlag=false;

        public delegate void FinishFlagReceive(bool receive, GridCell cell);
        public event FinishFlagReceive OnFinishFlag;

        public GridCell(Vector2 position, int number, int columnNumber, int rowNumber)
        {
            _position = position;
            _number = number;
            _columnNumber = columnNumber;
            _rowNumber = rowNumber;
        }



        public int Number
        {
            get { return _number; }
        }

        public CellContent GetContent()
        {
            return _currentContent;
        }

        public Vector2 Position
        {
            get { return _position; }
        }

        public int ColumnNumber
        {
            get { return _columnNumber; }
        }

        public int RowNumber
        {
            get { return _rowNumber; }
        }

        public GameObject GetKamen()
        {
            return _kamen;
        }

        public void SetKamen(GameObject kamen)
        {
            _kamen = kamen;
            if (kamen != null)
            {
                if (_currentContent == CellContent.Target)
                {
                    _finishFlag = true;
                    OnFinishFlag(_finishFlag,this);
                }
                _currentContent = CellContent.Stone;
            }
            else
            {
                if (_finishFlag)
                {
                    _currentContent = CellContent.Target;
                    _finishFlag = false;
                    OnFinishFlag(_finishFlag, this);
                }
                else
                {
                    _currentContent = CellContent.WorkingElement;
                }
                
            }
        }


        public void SetContentNumber(int cellElement)
        {
            _currentContent = (CellContent)cellElement;
        }
    }
}
