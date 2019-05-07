using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorEditor.Figures;

namespace VectorEditor.Presenter
{
    /// <summary>
    /// Класс для 
    /// выборки фигур.
    /// Использует GraphicsPath
    /// </summary>
    public class Selector
    {
        public GraphicsPath Path
        {
            private set;
            get;
        }

        /// <summary>
        /// Получить фигуру, на которую указывает курсор
        /// </summary>
        /// <param name="point">Точка мышки</param>
        /// <returns>Фигура</returns>
        public int GetFigurePointOn(Point point, 
                                    Dictionary<int, BaseFigure> figures)
        {
            int result = -1;

            Path = new GraphicsPath();
            Pen pickPen = new Pen(Color.Transparent, 3);

            foreach (var figure in figures)
            {
                AddFigureToGraphicsPath(figure.Value);

                if (figure.Value is FillableFigure)
                {
                    if (Path.IsVisible(point))
                    {
                        result = figure.Key;
                        break;
                    }
                }
                else
                {
                    if (Path.IsOutlineVisible(point, pickPen))
                    {
                        result = figure.Key;
                        break;
                    }
                }                
            }

            return result;
        }

        /// <summary>
        /// Добавить фигуру в GraphicsPath
        /// </summary>
        /// <param name="path">GraphicsPath</param>
        /// <param name="figure">Добавляемая фигура</param>
        private void AddFigureToGraphicsPath(BaseFigure figure)
        {
            var points = figure.Points.GetPoints();

            if (figure is FillableFigure fillable)
            {
                if (points.Count <= 2)
                {
                    var rect = fillable.Points.GetRectangle();
                    Path.AddEllipse(rect);
                }
                else
                {
                    Path.AddPolygon(points.ToArray());
                }
            }
            else
            {
                Path.AddLines(points.ToArray());
            }
        }

        /// <summary>
        /// Выбор группой
        /// </summary>
        /// <param name="figures">Фигуры на канве</param>
        /// <param name="rectangle">Прямоугольник выбора</param>
        /// <returns>Выбранные фигуры</returns>
        public Dictionary<int, BaseFigure> GetFiguresInRect(
               Dictionary<int, BaseFigure> figures,
               Rectangle rectangle)
        {
            var selectedFigures = new Dictionary<int, BaseFigure>();

            foreach (var figure in figures)
            {
                var figureRect = figure.Value.Points.GetRectangle();

                if (rectangle.IntersectsWith(figureRect))
                {
                    selectedFigures.Add(figure.Key, figure.Value);
                }
            }

            return selectedFigures;
        }
    }
}
