using System.Drawing;
using VectorEditor.Figures;


namespace VectorEditor.Drawers
{
    /// <summary>
    /// Фабрика для рисовальщиков. Также рисует фигуру
    /// </summary>
    public static class FigureDrawer
    {
        /// <summary>
        /// Создать рисовальщик и нарисовать фигуру
        /// </summary>
        /// <param name="figure">Фигура для рисования</param>
        /// <param name="canvas">Канва</param>
        public static void DrawFigure(BaseFigure figure, Graphics canvas)
        {
            if (figure.GetType() == typeof(Line))
            {
                var lineDrawer = new LineDrawer();
                lineDrawer.DrawFigure(figure, canvas);
            }
            else if (figure.GetType() == typeof(Polyline))
            {
                var drawer = new PolylineDrawer();
                drawer.DrawFigure(figure, canvas);
            }
            else if (figure.GetType() == typeof(Circle))
            {
                var circleDrawer = new CircleDrawer();
                circleDrawer.DrawFigure(figure, canvas);
            }
            else if (figure.GetType() == typeof(Ellipse))
            {
                var ellipseDrawer = new EllipseDrawer();
                ellipseDrawer.DrawFigure(figure, canvas);
            }
            else if (figure.GetType() == typeof(Polygon))
            {
                var polygonDrawer = new PolygonDrawer();
                polygonDrawer.DrawFigure(figure, canvas);
            }
        }

        /// <summary>
        /// Рисовка маркеров фигуры
        /// </summary>
        /// <param name="figure"></param>
        /// <param name="canvas"></param>
        public static void DrawSelection(BaseFigure figure, Graphics canvas)
        {
            if (figure.GetType() == typeof(Line))
            {
                var lineDrawer = new LineDrawer();
                lineDrawer.DrawSelection(figure, canvas);
            }
            else if (figure.GetType() == typeof(Polyline))
            {
                var drawer = new PolylineDrawer();
                drawer.DrawSelection(figure, canvas);
            }
            else if (figure.GetType() == typeof(Circle))
            {
                var circleDrawer = new CircleDrawer();
                circleDrawer.DrawSelection(figure, canvas);
            }
            else if (figure.GetType() == typeof(Ellipse))
            {
                var ellipseDrawer = new EllipseDrawer();
                ellipseDrawer.DrawSelection(figure, canvas);
            }
            else if (figure.GetType() == typeof(Polygon))
            {
                var polygonDrawer = new PolygonDrawer();
                polygonDrawer.DrawSelection(figure, canvas);
            }
        }
    }
}
