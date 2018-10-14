using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorEditor.Figures
{
    /// <summary>
    /// Класс для окружности
    /// </summary>
    class Circle : ILineProperty, IFillable
    {
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

        public Color FillColor
        {
            get;
            set;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public Circle()
        {

        }
    }
}
