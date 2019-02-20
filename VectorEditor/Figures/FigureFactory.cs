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
        public static BaseFigure CreateFigure(ToolType type)
        {
            switch (type)
            {
                case ToolType.Line:
                    return new Line();
                case ToolType.Polyline:
                    return new Polyline();
                case ToolType.Polygon:
                    return new Polygon();
                case ToolType.Circle:
                    return new Circle();
                case ToolType.Ellipse:
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
        public static BaseFigure CreateFillableFigure(ToolType type)
        {
            switch (type)
            {
                case ToolType.Polygon:
                    return new Polygon();
                case ToolType.Circle:
                    return new Circle();
                case ToolType.Ellipse:
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
        public static BaseFigure CreateCopy(BaseFigure figure, 
            CopyType copyType=CopyType.NormalCopy)
        {
            BaseFigure resultFigure = null;
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

                CopyPoints(figure, resultFigure, copyType);
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

                CopyPoints(figure, resultFigure, copyType);
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
                if (temp != null)
                {
                    circle.FillProperty.FillColor = temp.FillProperty.FillColor;
                }


                CopyPoints(figure, circle, copyType);

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
                if (temp != null)
                {
                    ellipse.FillProperty.FillColor = temp.FillProperty.FillColor;
                }

                CopyPoints(figure, ellipse, copyType);

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
                if (temp != null)
                {
                    polygon.FillProperty.FillColor = temp.FillProperty.FillColor;
                }

                CopyPoints(figure, polygon, copyType);

                resultFigure = polygon;
            }

            return resultFigure;
        }

        /// <summary>
        /// Копирование точек фигур
        /// </summary>
        /// <param name="originalFigure">Оригинальная фигура</param>
        /// <param name="copyFigure">Новая фигура</param>
        /// <param name="copyType">Тип копирования</param>
        private static void CopyPoints(BaseFigure originalFigure,
                                       BaseFigure copyFigure,
                                       CopyType copyType)
        {
            foreach (var point in originalFigure.Points.GetPoints())
            {
                copyFigure.Points.AddPoint(copyType == CopyType.CopyWithOffset
                                                    ? new PointF(point.X + 15, point.Y)
                                                    : new PointF(point.X, point.Y));
            }
        }
    }
}
