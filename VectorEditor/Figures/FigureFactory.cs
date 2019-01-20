using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorEditor.Figures
{
    /// <summary>
    /// Статический класс. Фабрика фигур.
    /// </summary>
    public static class FigureFactory
    {
        /// <summary>
        /// Создание фигур
        /// </summary>
        /// <param name="type">Тип фигур</param>
        /// <returns>Новая фигура</returns>
        public static BaseFigure CreateFigure(Item type)
        {
            switch (type)
            {
                case Item.Line:
                    return new Line();
                case Item.Polyline:
                    return new Polyline();
                case Item.Polygon:
                    return new Polygon();
                case Item.Circle:
                    return new Circle();
                case Item.Ellipse:
                    return new Ellipse();
                default:
                    return null;
            }
        }

        /// <summary>
        /// Создание фигур с заливкой
        /// </summary>
        /// <param name="type">Тип фигуры</param>
        /// <returns>Новая фигура</returns>
        public static BaseFigure CreateFillableFigure(Item type)
        {
            switch (type)
            {
                case Item.Polygon:
                    return new Polygon();
                case Item.Circle:
                    return new Circle();
                case Item.Ellipse:
                    return new Ellipse();
                default:
                    return null;
            }
        }

        /// <summary>
        /// Перечисления для типов копирования.
        /// Есть нормальное и со сдигом координат. 
        /// </summary>
        public enum CopyType
        {
            NormalCopy,
            CopyWithOffset
        }

        /// <summary>
        /// Статический метод для создания копии фигуры
        /// </summary>
        /// <param name="figure">Фигура для копирования</param>
        /// <param name="copyType">Тип копирования</param>
        /// <returns>Копия фигуры</returns>
        public static BaseFigure CreateCopy(BaseFigure figure, CopyType copyType=CopyType.NormalCopy)
        {
            BaseFigure resultFigure = null;
            if (figure.GetType() == typeof(FillableFigure))
            {
                var fillableFigure = figure as FillableFigure;
                FillableFigure newFigure = new FillableFigure();

                newFigure.LineProperties.Color = fillableFigure.LineProperties.Color;
                newFigure.LineProperties.Thickness = fillableFigure.LineProperties.Thickness;
                newFigure.LineProperties.Style = fillableFigure.LineProperties.Style;

                newFigure.FillProperty.FillColor = fillableFigure.FillProperty.FillColor;

                foreach (var point in fillableFigure.Points.GetPoints())
                {
                    if (copyType == CopyType.CopyWithOffset)
                    {
                        newFigure.Points.AddPoint(new PointF(point.X + 15, point.Y));
                    }
                    else
                    {
                        newFigure.Points.AddPoint(new PointF(point.X, point.Y));
                    }
                    
                }

                resultFigure = newFigure;
            }
            else
            {
                if (figure.GetType() == typeof(Line))
                {
                    resultFigure = new Line();
                    resultFigure.LineProperties.Color = figure.LineProperties.Color;
                    resultFigure.LineProperties.Thickness = figure.LineProperties.Thickness;
                    resultFigure.LineProperties.Style = figure.LineProperties.Style;

                    foreach (var point in figure.Points.GetPoints())
                    {
                        if (copyType == CopyType.CopyWithOffset)
                        {
                            resultFigure.Points.AddPoint(new PointF(point.X + 15, point.Y));
                        }
                        else
                        {
                            resultFigure.Points.AddPoint(new PointF(point.X, point.Y));
                        }
                        
                    }
                }
                else if (figure.GetType() == typeof(Polyline))
                {
                    resultFigure = new Polyline();
                    resultFigure.LineProperties.Color = figure.LineProperties.Color;
                    resultFigure.LineProperties.Thickness = figure.LineProperties.Thickness;
                    resultFigure.LineProperties.Style = figure.LineProperties.Style;

                    foreach (var point in figure.Points.GetPoints())
                    {
                        if (copyType == CopyType.CopyWithOffset)
                        {
                            resultFigure.Points.AddPoint(new PointF(point.X + 15, point.Y));
                        }
                        else
                        {
                            resultFigure.Points.AddPoint(new PointF(point.X, point.Y));
                        }
                    }
                }
                else if (figure.GetType() == typeof(Circle))
                {
                    Circle circle = new Circle();
                    circle.LineProperties.Color = figure.LineProperties.Color;
                    circle.LineProperties.Thickness = figure.LineProperties.Thickness;
                    circle.LineProperties.Style = figure.LineProperties.Style;

                    var temp = figure as FillableFigure;
                    circle.FillProperty.FillColor = temp.FillProperty.FillColor;

                    foreach (var point in figure.Points.GetPoints())
                    {
                        if (copyType == CopyType.CopyWithOffset)
                        {
                            circle.Points.AddPoint(new PointF(point.X + 15, point.Y));
                        }
                        else
                        {
                            circle.Points.AddPoint(new PointF(point.X, point.Y));
                        }
                    }

                    resultFigure = circle;
                }
                else if (figure.GetType() == typeof(Ellipse))
                {
                    Ellipse ellipse = new Ellipse();
                    ellipse.LineProperties.Color = figure.LineProperties.Color;
                    ellipse.LineProperties.Thickness = figure.LineProperties.Thickness;
                    ellipse.LineProperties.Style = figure.LineProperties.Style;

                    var temp = figure as FillableFigure;
                    ellipse.FillProperty.FillColor = temp.FillProperty.FillColor;

                    foreach (var point in figure.Points.GetPoints())
                    {
                        if (copyType == CopyType.CopyWithOffset)
                        {
                            ellipse.Points.AddPoint(new PointF(point.X + 15, point.Y));
                        }
                        else
                        {
                            ellipse.Points.AddPoint(new PointF(point.X, point.Y));
                        }
                    }

                    resultFigure = ellipse;
                }
                else if (figure.GetType() == typeof(Polygon))
                {
                    Polygon polygon = new Polygon();
                    polygon.LineProperties.Color = figure.LineProperties.Color;
                    polygon.LineProperties.Thickness = figure.LineProperties.Thickness;
                    polygon.LineProperties.Style = figure.LineProperties.Style;

                    var temp = figure as FillableFigure;
                    polygon.FillProperty.FillColor = temp.FillProperty.FillColor;

                    foreach (var point in figure.Points.GetPoints())
                    {
                        if (copyType == CopyType.CopyWithOffset)
                        {
                            polygon.Points.AddPoint(new PointF(point.X + 15, point.Y));
                        }
                        else
                        {
                            polygon.Points.AddPoint(new PointF(point.X, point.Y));
                        }
                    }

                    resultFigure = polygon;
                }
            }
            return resultFigure;
        }

    }
}
