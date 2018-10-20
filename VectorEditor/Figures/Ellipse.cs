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
    class Ellipse : ILineProperty, IFillable
    {
        /// <summary>
        /// Координаты верхнего левого угла
        /// </summary>
        MPoint _A;

        /// <summary>
        /// Ширина прямоугольника, описывающего эллипс
        /// </summary>
        int width;

        /// <summary>
        /// Высота прямоугольника, описывающего эллипс
        /// </summary>
        int height;

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

        public int Width
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

        public int Height
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

        public MPoint A
        {
            get
            {
                return _A;
            }

            set
            {
                _A = value;
            }
        }

        /// <summary>
        /// Конструктор класса Эллипс
        /// </summary>
        /// <param name="x">Координата х верхнего левого угла</param>
        /// <param name="y">Координата у верхнего левого угла</param>
        /// <param name="width">Ширина прямоугольника</param>
        /// <param name="height">Высота прямоугольника</param>
        /// <param name="lineColor">Цвет линии</param>
        /// <param name="fillColor">Цвет заливки</param>
        /// <param name="lineThickness">Толщина линии</param>
        /// <param name="lineType">Тип линии</param>
        public Ellipse(int x, int y, int width, int height,
                       Color lineColor, Color fillColor,
                       int lineThickness, LineType lineType)
        {
            A = new MPoint(x, y);
            Width = width;
            Height = height;
            LineColor = lineColor;
            FillColor = fillColor;
            LineThickness = lineThickness;
            LineType = lineType;
        }
    }
}
