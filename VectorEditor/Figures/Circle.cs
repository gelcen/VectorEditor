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
    class Circle : Figure
    {
        /// <summary>
        /// Центральная точка окружности
        /// </summary>
        private PointF _centerPoint;

        /// <summary>
        /// Радиус
        /// </summary>
        private float _radius;       

        /// <summary>
        /// Свойство для центральной точки
        /// </summary>
        public PointF CenterPoint
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

        /// <summary>
        /// Свойство для радиуса
        /// </summary>
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
        /// Установка параметров
        /// </summary>
        public void SetParameters(float x, float y, 
                     float radius,
                     Color lineColor,
                     int lineThickness,
                     Color fillColor,
                     LineType lineType)
        {
            CenterPoint = new PointF(x, y);
            Radius = radius;
            FillColor = fillColor;
            SetLineParameters(lineColor, lineThickness, lineType);
        }
    }
}
