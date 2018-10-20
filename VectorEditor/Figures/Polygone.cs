using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorEditor.Figures
{
    /// <summary>
    /// Класс для хранения параметров многоугольника
    /// </summary>
    class Polygone : PolyLine, IFillable
    {
        /// <summary>
        /// Количество вершин
        /// </summary>
        int _pointsCount;

        /// <summary>
        /// Конструктор класса для многоугольника
        /// </summary>
        /// <param name="pointsCount">Количество вершин</param>
        /// <param name="lineThickness">Толщина линии</param>
        /// <param name="lineColor">Цвет линии</param>
        /// <param name="lineType">Тип линии</param>
        /// <param name="fillColor">Цвет заливки</param>
        public Polygone(int pointsCount, int lineThickness,
                        Color lineColor,
                        LineType lineType,
                        Color fillColor)
        {
            PointsCount = pointsCount;
            SetProperties(lineThickness, lineColor, lineType);
            FillColor = fillColor;
        }

        /// <summary>
        /// Функция для добавления вершин
        /// </summary>
        /// <param name="x">Координата х</param>
        /// <param name="y">Координата y</param>
        public override void Add(int x, int y)
        {
            if (points.Count <= PointsCount)
            {
                base.Add(x, y);
            }
            else
            {
                throw new Exception("Количество вершин больше заданного.");
            }
        }

        /// <summary>
        /// Свойство для цвета заливки
        /// </summary>
        public Color FillColor
        {
            get;
            set;
        }

        /// <summary>
        /// Свойство для количества вершин
        /// </summary>
        public int PointsCount
        {
            get
            {
                return _pointsCount;
            }

            private set
            {
                _pointsCount = value;
            }
        }
    }
}
