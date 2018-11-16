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
    /// Класс для полилинии
    /// </summary>
    class PolyLine:Figure
    {
        /// <summary>
        /// Список точек полилинии
        /// </summary>
        public List<PointF> points = new List<PointF>();

        /// <summary>
        /// Функция добавления новых точек
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public virtual void Add(int x, int y)
        {
            points.Add(new Point(x, y));
        }

        /// <summary>
        /// Установка параметров для полилинии
        /// </summary>
        /// <param name="points">Вершины</param>
        /// <param name="lineColor">Цвет линии</param>
        /// <param name="lineThickness">Толщина линии</param>
        /// <param name="lineType">Тип линии</param>
        public void SetParameters(List<PointF> points, Color lineColor,
                                  int lineThickness, LineType lineType)
        {
            this.points = points;
            SetLineParameters(lineColor, lineThickness, lineType);
        }
    }
}
