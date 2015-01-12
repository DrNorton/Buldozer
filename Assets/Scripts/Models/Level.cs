using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models
{
    public class Level
    {
        private int _columnRows;
        private int _columnNumbers;
        public bool IsShowing { get; set; }
        public string LevelContent { get; set; }
        public int Number { get; set; }

        public Level()
        {
            ColumnNumbers = 16;
            ColumnRows = 10;
        }
        public int ColumnNumbers
        {
            get { return _columnNumbers; }
            set { _columnNumbers = value; }
        }

        public int ColumnRows
        {
            get { return _columnRows; }
            set { _columnRows = value; }
        }
    }
}
