using SDK;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace VectorEditor.Presenter
{
    /// <summary>
    /// Класс для 
    /// выборки фигур.
    /// Использует GraphicsPath
    /// </summary>
    public class Selector : IFigureSelector
    {
        /// <summary>
        /// GraphicsPath
        /// </summary>
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
        /// Определяет находится ли точка на маркере
        /// </summary>
        /// <param name="selectedFigures">Фигуры</param>
        /// <param name="mousePoint">Точка нажатия</param>
        /// <param name="pickedPoint">Точка (маркер)</param>
        /// <param name="pickedFigureIndex">Индекс фигуры</param>
        /// <returns>Индекс маркера (точки)</returns>
        public int IsPointOnMarker(Dictionary<int, BaseFigure> selectedFigures,
                                     PointF mousePoint,
                                     out PointF pickedPoint,
                                     out int pickedFigureIndex)
        {
            int pickedMarkerIndex;

            foreach (var figure in selectedFigures)
            {
                var count = figure.Value.Points.GetPoints().Count;

                for (var j = 0; j < count; j++)
                {
                    var distance = Points.FindDistanceBetween(mousePoint,
                                   figure.Value.Points.GetPoints()[j]);

                    if (distance >= 9) continue;

                    pickedPoint = figure.Value.Points.GetPoints()[j];
                    pickedMarkerIndex = j;
                    pickedFigureIndex = figure.Key;
                    return pickedMarkerIndex;
                }
            }

            pickedMarkerIndex = -1;
            pickedFigureIndex = -1;
            pickedPoint = new PointF(-1, -1);
            return pickedMarkerIndex;
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
