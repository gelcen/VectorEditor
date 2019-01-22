using System.Drawing;

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
                if (fillableFigure == null) return null;
                var newFigure = new FillableFigure
                {
                    LineProperties =
                    {
                        Color = fillableFigure.LineProperties.Color,
                        Thickness = fillableFigure.LineProperties.Thickness,
                        Style = fillableFigure.LineProperties.Style
                    },
                    FillProperty = {FillColor = fillableFigure.FillProperty.FillColor}
                };



                foreach (var point in fillableFigure.Points.GetPoints())
                {
                    newFigure.Points.AddPoint(copyType == CopyType.CopyWithOffset
                        ? new PointF(point.X + 15, point.Y)
                        : new PointF(point.X, point.Y));
                }

                resultFigure = newFigure;
            }
            else
            {
                if (figure.GetType() == typeof(Line))
                {
                    resultFigure = new Line
                    {
                        LineProperties =
                        {
                            Color = figure.LineProperties.Color,
                            Thickness = figure.LineProperties.Thickness,
                            Style = figure.LineProperties.Style
                        }
                    };

                    foreach (var point in figure.Points.GetPoints())
                    {
                        resultFigure.Points.AddPoint(copyType == CopyType.CopyWithOffset
                            ? new PointF(point.X + 15, point.Y)
                            : new PointF(point.X, point.Y));
                    }
                }
                else if (figure.GetType() == typeof(Polyline))
                {
                    resultFigure = new Polyline
                    {
                        LineProperties =
                        {
                            Color = figure.LineProperties.Color,
                            Thickness = figure.LineProperties.Thickness,
                            Style = figure.LineProperties.Style
                        }
                    };

                    foreach (var point in figure.Points.GetPoints())
                    {
                        resultFigure.Points.AddPoint(copyType == CopyType.CopyWithOffset
                            ? new PointF(point.X + 15, point.Y)
                            : new PointF(point.X, point.Y));
                    }
                }
                else if (figure.GetType() == typeof(Circle))
                {
                    var circle = new Circle
                    {
                        LineProperties =
                        {
                            Color = figure.LineProperties.Color,
                            Thickness = figure.LineProperties.Thickness,
                            Style = figure.LineProperties.Style
                        }
                    };

                    var temp = figure as FillableFigure;
                    if (temp != null) circle.FillProperty.FillColor = temp.FillProperty.FillColor;

                    foreach (var point in figure.Points.GetPoints())
                    {
                        circle.Points.AddPoint(copyType == CopyType.CopyWithOffset
                            ? new PointF(point.X + 15, point.Y)
                            : new PointF(point.X, point.Y));
                    }

                    resultFigure = circle;
                }
                else if (figure.GetType() == typeof(Ellipse))
                {
                    var ellipse = new Ellipse
                    {
                        LineProperties =
                        {
                            Color = figure.LineProperties.Color,
                            Thickness = figure.LineProperties.Thickness,
                            Style = figure.LineProperties.Style
                        }
                    };

                    var temp = figure as FillableFigure;
                    if (temp != null) ellipse.FillProperty.FillColor = temp.FillProperty.FillColor;

                    foreach (var point in figure.Points.GetPoints())
                    {
                        ellipse.Points.AddPoint(copyType == CopyType.CopyWithOffset
                            ? new PointF(point.X + 15, point.Y)
                            : new PointF(point.X, point.Y));
                    }

                    resultFigure = ellipse;
                }
                else if (figure.GetType() == typeof(Polygon))
                {
                    var polygon = new Polygon
                    {
                        LineProperties =
                        {
                            Color = figure.LineProperties.Color,
                            Thickness = figure.LineProperties.Thickness,
                            Style = figure.LineProperties.Style
                        }
                    };

                    var temp = figure as FillableFigure;
                    if (temp != null) polygon.FillProperty.FillColor = temp.FillProperty.FillColor;

                    foreach (var point in figure.Points.GetPoints())
                    {
                        polygon.Points.AddPoint(copyType == CopyType.CopyWithOffset
                            ? new PointF(point.X + 15, point.Y)
                            : new PointF(point.X, point.Y));
                    }

                    resultFigure = polygon;
                }
            }
            return resultFigure;
        }

    }
}
