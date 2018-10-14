using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorEditor
{
    /// <summary>
    /// Класс "Линия"
    /// </summary>
    class Line:ILineProperty
    {
        /// <summary>
        /// Начальная точка 
        /// </summary>
        private MPoint _a;

        /// <summary>
        /// Конечная точка
        /// </summary>
        private MPoint _b;

        /// <summary>
        /// Свойство для точки А
        /// </summary>
        public MPoint A
        {
            get { return _a; }
        }

        /// <summary>
        /// Свойство для точки B
        /// </summary>
        public MPoint B
        {
            get { return _b; }
        }

        /// <summary>
        /// Свойство для толщины линии
        /// </summary>
        public int LineThickness
        {
            get;
            set;
        }

        /// <summary>
        /// Свойство для цвета линии
        /// </summary>
        public Color LineColor
        {
            get;
            set;
        }

        /// <summary>
        /// Свойство для типа линии
        /// </summary>
        public LineType LineType
        {
            get;
            set;
        }

        /// <summary>
        /// Конструктор класса линии
        /// </summary>
        /// <param name="x1">A.X</param>
        /// <param name="y1">A.Y</param>
        /// <param name="x2">B.X</param>
        /// <param name="y2">B.Y</param>
        public Line(int x1, int y1,
                    int x2, int y2,
                    int lineThickness,
                    Color lineColor,
                    LineType lineType)
        {
            _a = new MPoint(x1, y1);
            _b = new MPoint(x2, y2);
            LineThickness = lineThickness;
            LineColor = lineColor;
            LineType = lineType;
        }
    }
}
