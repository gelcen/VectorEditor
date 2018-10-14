using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorEditor
{
    /// <summary>
    /// Класс "Точка"
    /// </summary>
    class MPoint
    {
        /// <summary>
        /// Приватное поле для координаты x
        /// </summary>
        private int _x;

        /// <summary>
        /// Приватное поле для координаты y
        /// </summary>
        private int _y;

        /// <summary>
        /// Свойство для поля X
        /// </summary>
        public int X
        {
            get
            {
                return _x;
            }
            set
            {              
                _x = value;
            }
        }

        /// <summary>
        /// Свойство для поля Y
        /// </summary>
        public int Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }

        /// <summary>
        /// Конструктор класса "Point"
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public MPoint(int x, int y)
        {
            X = x;
            Y = y;
        }

    }
}
