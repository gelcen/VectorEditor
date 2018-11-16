using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorEditor.Figures;

namespace VectorEditor
{
    /// <summary>
    /// Класс "Линия"
    /// </summary>
    class Line:Figure
    {
        /// <summary>
        /// Начальная точка 
        /// </summary>
        private PointF _a;

        /// <summary>
        /// Конечная точка
        /// </summary>
        private PointF _b;

        /// <summary>
        /// Свойство для точки А
        /// </summary>
        public PointF A
        {
            get { return _a; }
        }

        /// <summary>
        /// Свойство для точки B
        /// </summary>
        public PointF B
        {
            get { return _b; }
        }


        /// <summary>
        /// Установка параметров класса линии
        /// </summary>
        /// <param name="x1">A.X</param>
        /// <param name="y1">A.Y</param>
        /// <param name="x2">B.X</param>
        /// <param name="y2">B.Y</param>
        public void SetParameters(float x1, float y1,
                    float x2, float y2,
                    int lineThickness,
                    Color lineColor,
                    LineType lineType)
        {
            _a = new PointF(x1, y1);
            _b = new PointF(x2, y2);
            SetLineParameters(lineColor, lineThickness, lineType);
        }
    }
}
