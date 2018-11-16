using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorEditor.Figures
{
    /// <summary>
    /// Класс, содержащий параметры эллипса
    /// </summary>
    class Ellipse : Figure
    {
        /// <summary>
        /// Координаты верхнего левого угла
        /// </summary>
        PointF _a;

        /// <summary>
        /// Ширина прямоугольника, описывающего эллипс
        /// </summary>
        float width;

        /// <summary>
        /// Высота прямоугольника, описывающего эллипс
        /// </summary>
        float height;

        /// <summary>
        /// Свойство для ширины
        /// </summary>
        public float Width
        {
            get
            {
                return width;
            }

            set
            {
                width = value;
            }
        }

        /// <summary>
        /// Свойство для высоты
        /// </summary>
        public float Height
        {
            get
            {
                return height;
            }

            set
            {
                height = value;
            }
        }

        /// <summary>
        /// Свойство для точки
        /// </summary>
        public PointF A
        {
            get
            {
                return _a;
            }

            set
            {
                _a = value;
            }
        }

        /// <summary>
        /// Установка свойств
        /// </summary>
        /// <param name="x">Координата х верхнего левого угла</param>
        /// <param name="y">Координата у верхнего левого угла</param>
        /// <param name="width">Ширина прямоугольника</param>
        /// <param name="height">Высота прямоугольника</param>
        /// <param name="lineColor">Цвет линии</param>
        /// <param name="fillColor">Цвет заливки</param>
        /// <param name="lineThickness">Толщина линии</param>
        /// <param name="lineType">Тип линии</param>
        public void SetParameters(float x, float y, float width, float height,
                       Color lineColor, Color fillColor,
                       int lineThickness, LineType lineType)
        {
            A = new PointF(x, y);
            Width = width;
            Height = height;            
            FillColor = fillColor;
            SetLineParameters(lineColor, lineThickness, lineType);            
        }
    }
}
