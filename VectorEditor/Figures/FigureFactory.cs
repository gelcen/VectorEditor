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
        /// Получить свойства линии из фигуры
        /// </summary>
        /// <param name="figure">Фигура, свойства которого нужно вязть</param>
        /// <returns>Полученные свойства фигуры</returns>
        private static LineProperties GetLineProperties(BaseFigure figure) => 
            new LineProperties(figure.LineProperties.Color,
                               figure.LineProperties.Thickness,
                               figure.LineProperties.Style);

        /// <summary>
        /// Установка свойств заполнения
        /// </summary>
        /// <param name="settingFigure">Фигура, у которого берутся 
        ///свойства заполнения</param>
        /// <param name="fillableFigure">Фигура, у которого устанавливаются</param>
        private static void SetFillProperty(BaseFigure settingFigure, FillableFigure fillableFigure)
        {
            if (settingFigure is FillableFigure temp)
            {
                fillableFigure.FillProperty.FillColor = temp.FillProperty.FillColor;
            }
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

        /// <summary>
        /// Копирование фигуры
        /// </summary>
        /// <param name="figure">Копируемая фигура</param>
        /// <param name="copyType">Тип копирования</param>
        /// <returns>Копия фигуры</returns>
        public static BaseFigure CreateCopy(BaseFigure figure,
            CopyType copyType = CopyType.NormalCopy)
        {
            BaseFigure resultFigure = null;
            switch (figure)
            {
                case Line line:
                    line = new Line
                    {
                        LineProperties = GetLineProperties(figure)
                    };
                    CopyPoints(figure, line, copyType);
                    resultFigure = line;
                    break;
                case Polyline polyline:
                    polyline = new Polyline
                    {
                        LineProperties = GetLineProperties(figure)
                    };
                    CopyPoints(figure, polyline, copyType);
                    resultFigure = polyline;
                    break;
                case Circle circle:
                    circle = new Circle
                    {
                        LineProperties = GetLineProperties(figure)
                    };
                    SetFillProperty(figure, circle);
                    CopyPoints(figure, circle, copyType);
                    resultFigure = circle;
                    break;
                case Ellipse ellipse:
                    ellipse = new Ellipse
                    {
                        LineProperties = GetLineProperties(figure)
                    };
                    SetFillProperty(figure, ellipse);
                    CopyPoints(figure, ellipse, copyType);
                    resultFigure = ellipse;
                    break;
                case Polygon polygon:
                    polygon = new Polygon
                    {
                        LineProperties = GetLineProperties(figure)
                    };
                    SetFillProperty(figure, polygon);
                    CopyPoints(figure, polygon, copyType);
                    resultFigure = polygon;
                    break;
                default:        
                    break;
            }

            return resultFigure;
        }                        
        
    }
}
