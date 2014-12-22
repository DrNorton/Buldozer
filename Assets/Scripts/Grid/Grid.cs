using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Grid
{
    public class Grid
    {
        private int _columnNumber = 12;
        private int _rowNumber = 10;
        private readonly ScoreInfo _scorePopup;
        private readonly SettingsProvider _provider;

        private GridCell[,] _grid;
        private int _numberTarget;

        public Grid(string content, int columnNumber, int rowNumber,ScoreInfo scorePopup,SettingsProvider provider)
        {
            _columnNumber = columnNumber;
            _rowNumber = rowNumber;
            _scorePopup = scorePopup;
            _provider = provider;
            _grid = new GridCell[ColumnNumber, RowNumber];
            GenerateCellPositions();
            GenerateContent(content);
            _numberTarget = GetByTarget(content);
        }

        private int GetByTarget(string content)
        {
            int number = 0;
            foreach (var c in content.ToCharArray())
            {
                if (c == '3')
                {
                    number++;
                }
            }
            return number;
        }

        public int RowNumber
        {
            get { return _rowNumber; }
        }

        public int ColumnNumber
        {
            get { return _columnNumber; }
        }

        public GridCell GetCell(int column, int row)
        {
            return _grid[column, row];
        }

        public void GenerateCellPositions()
        {
            var number = 0;
            for (int i = 0; i < RowNumber; i++)
            {
                for (int j = 0; j < ColumnNumber; j++)
                {
                    var cell = new GridCell(new Vector2(j - ColumnNumber / 2, RowNumber / 2 - i), number, j, i);
                    cell.OnFinishFlag += cell_OnFinishFlag;
                    _grid[j, i] = cell;
                    number++;
                }
            }
        }

        void cell_OnFinishFlag(bool receive, GridCell cell)
        {
          if (receive)
            {
                _numberTarget = _numberTarget - 1;
                Debug.LogWarning("Пойнт готов");
            }
            else
            {
                _numberTarget = _numberTarget + 1;
                Debug.LogWarning("снять пойнт");
            }

          if (_numberTarget == 0)
          {
             _scorePopup.ShowNextLevel(_provider);
          }

          Debug.LogWarning("Всего пойнтов "+_numberTarget);
           
            
        }

        public void GenerateContent(string contentString)
        {
            for (int i = 0; i < RowNumber; i++)
            {
                for (int j = 0; j < ColumnNumber; j++)
                {
                    var element = contentString[_grid[j, i].Number].ToString();
                    _grid[j, i].SetContentNumber(int.Parse(element));
                }
            }
        }



    }
}
