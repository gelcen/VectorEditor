using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorEditor.Figures
{
    /// <summary>
    /// Абстрактный класс для фигуры
    /// </summary>
    public abstract class Figure : ILineProperty, IFillable
    {

        public Color FillColor
        {
            get;
            set;
        }

        public Color LineColor
        {
            get;
            set;
        }

        public int LineThickness
        {
            get;
            set;
        }

        public LineType LineType
        {
            get;
            set;
        }

        /// <summary>
        /// Функция установки парметров линии
        /// </summary>
        public void SetLineParameters(Color lineColor,
            int lineThickness, LineType lineType)
        {
            LineColor = lineColor;
            LineThickness = lineThickness;
            LineType = lineType;
        }
    }
}
