using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Grid
{
    public enum CellContent
    {
        WorkingElement = 0, //Полость по которой бульдозер может ездить 0
        Buldozer = 1, //Сам бульдозер 1
        Stone = 2, //Камень
        Target = 3, //Цель
        FencyElement = 4,// ограждение забора 2
        NotWorkingElement = 5,//пустая область, по которой можно ездить 3
        StoneWithTarget=6
    }
}
