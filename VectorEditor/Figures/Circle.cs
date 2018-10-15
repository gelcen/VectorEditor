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

        private MPoint _centerPoint;

        private float _radius;


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
        /// Color of filling
        /// </summary>
        public Color FillColor
        {
            get;
            set;
        }

        public MPoint CenterPoint
        {
            get
            {
                return _centerPoint;
            }

            set
            {
                _centerPoint = value;
            }
        }

        public float Radius
        {
            get
            {
                return _radius;
            }

            set
            {
                _radius = value;
            }
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public Circle(int x, int y, 
                     float radius,
                     Color lineColor,
                     int lineThickness,
                     LineType lineType)
        {
            CenterPoint = new MPoint(x, y);
            Radius = radius;
            LineColor = lineColor;
            LineThickness = lineThickness;
            LineType = lineType;
        }
    }
}
