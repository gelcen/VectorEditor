using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorEditor.Figures
{
    /// <summary>
    /// Класс для создания фигур. Простая фабрика
    /// </summary>
    public static class FigureFactory
    {
        /// <summary>
        /// Фигуры
        /// </summary>
        private static Dictionary<Item, Figure> figures =
            new Dictionary<Item, Figure>();

        /// <summary>
        /// Создание фигуры
        /// </summary>
        /// <param name="item">Тип фигуры</param>
        /// <returns></returns>
        public static Figure CreateFigure(Item item)
        {
            if (figures.Count ==0)
            {
                figures.Add(Item.Line, new Line());
                figures.Add(Item.Polygon, new Polygone());
                figures.Add(Item.Polyline, new PolyLine());
                figures.Add(Item.Circle, new Circle());
                figures.Add(Item.Ellipse, new Ellipse());
            }

            return figures[item];
        }

        public static Figure CreateFigure(Item item, List<PointF> points, 
                                          int lineThickness, Color lineColor,
                                          LineType lineType, Color fillColor)
        {
            float x, y, lx, ly;
            Figure figure = null;
            switch (item)
            {
                case Item.Line:
                    Line line = new Line();
                    line.SetParameters(points[0].X, points[0].Y,
                            points[1].X, points[1].Y,
                            lineThickness,
                            lineColor,
                            lineType);
                    figure = line;
                    break;
                case Item.Polyline:
                    PolyLine polyline = new PolyLine();
                    polyline.SetParameters(points, lineColor, lineThickness, lineType);
                    figure = polyline;
                    break;
                case Item.Polygon:
                    Polygone polygone = new Polygone();
                    polygone.SetParameters(points.Count, lineThickness,
                        lineColor, lineType, fillColor);
                    figure = polygone;
                    break;
                case Item.Circle:
                    Circle circle = new Circle();
                    x = points[0].X;
                    y = points[0].Y;
                    lx = points[1].X;
                    ly = points[1].Y;
                    float rad = (float)Math.Sqrt(Math.Pow((lx - x), 2) + Math.Pow((ly - y), 2));
                    circle.SetParameters(x, y, rad, lineColor,
                                        lineThickness, fillColor, lineType);
                    figure = circle;
                    break;
                case Item.Ellipse:
                    x = points[0].X;
                    y = points[0].Y;
                    lx = points[1].X;
                    ly = points[1].Y;
                    Ellipse ellipse = new Ellipse();
                    ellipse.SetParameters(x, y, lx-x, ly-ly, lineColor,
                                        fillColor, lineThickness, lineType);
                    figure = ellipse;
                    break;
                default:
                    break;
            }

            return figure;
        }


        /// <summary>
        /// Функция установки параметров для линии
        /// </summary>
        /// <param name="figure"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="lineThickness"></param>
        /// <param name="lineColor"></param>
        /// <param name="lineType"></param>
        /// <returns></returns>
        public static Figure SetParameters(Figure figure, 
                    int x1, int y1,
                    int x2, int y2,
                    int lineThickness,
                    Color lineColor,
                    LineType lineType)
        {
            if (figure.GetType() != typeof(Line))
            {
                throw new Exception("Неверный тип данных");
            }
            Line line = new Line();
            line.SetParameters(x1, y1,
                    x2, y2,
                    lineThickness,
                    lineColor,
                    lineType);
            figure = line;
            return figure;
        }

        /// <summary>
        /// Перегрузка для полилинии
        /// </summary>
        /// <param name="figure"></param>
        /// <param name="lineThickness"></param>
        /// <param name="lineColor"></param>
        /// <param name="lineType"></param>
        /// <returns></returns>
        public static Figure SetParameters(Figure figure,
                    List<PointF> points,
                    int lineThickness,
                    Color lineColor,
                    LineType lineType)
        {
            PolyLine polyline = new PolyLine();
            polyline.SetParameters(points, lineColor, lineThickness, lineType);
            figure = polyline;
            return figure;
        }

        /// <summary>
        /// Перегрузка для полигона
        /// </summary>
        /// <param name="figure"></param>
        /// <param name="pointsCount"></param>
        /// <param name="lineThickness"></param>
        /// <param name="lineColor"></param>
        /// <param name="lineType"></param>
        /// <param name="fillColor"></param>
        /// <returns></returns>
        public static Figure SetParameters(Figure figure,
                        int pointsCount, int lineThickness,
                        Color lineColor,
                        LineType lineType,
                        Color fillColor)
        {
            Polygone polygone = new Polygone();
            polygone.SetParameters(pointsCount, lineThickness,
                lineColor, lineType, fillColor);
            figure = polygone;
            return figure;
        }

        /// <summary>
        /// Перегрузка для окружности
        /// </summary>
        /// <param name="figure"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="radius"></param>
        /// <param name="lineColor"></param>
        /// <param name="lineThickness"></param>
        /// <param name="fillColor"></param>
        /// <param name="lineType"></param>
        /// <returns></returns>
        public static Figure SetParameters(Figure figure,
                     int x, int y,
                     float radius,
                     Color lineColor,
                     int lineThickness,
                     Color fillColor,
                     LineType lineType)
        {
            Circle circle = new Circle();
            circle.SetParameters(x, y, radius, lineColor, 
                                lineThickness, fillColor, lineType);
            figure = circle;
            return figure;
        }

        /// <summary>
        /// Перегрузка для эллипса
        /// </summary>
        /// <param name="figure"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="lineColor"></param>
        /// <param name="fillColor"></param>
        /// <param name="lineThickness"></param>
        /// <param name="lineType"></param>
        /// <returns></returns>
        public static Figure SetParameters(Figure figure,
                       int x, int y, int width, int height,
                       Color lineColor, Color fillColor,
                       int lineThickness, LineType lineType)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.SetParameters(x, y, width, height, lineColor, 
                                fillColor, lineThickness, lineType);
            figure = ellipse;
            return figure;
        }

    }
}
