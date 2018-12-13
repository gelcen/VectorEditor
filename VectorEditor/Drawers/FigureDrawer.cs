using System.Drawing;
using VectorEditor.Figures;


namespace VectorEditor.Drawers
{
    public static class FigureDrawer
    {
        public static void DrawFigure(BaseFigure figure, Graphics canvas)
        {
            if (figure.GetType() == typeof(Line))
            {
                LineDrawer lineDrawer = new LineDrawer();
                lineDrawer.DrawFigure(figure, canvas);
            }
            else if (figure.GetType() == typeof(Polyline))
            {
                PolylineDrawer drawer = new PolylineDrawer();
                drawer.DrawFigure(figure, canvas);
            }
            else if (figure.GetType() == typeof(Circle))
            {
                CircleDrawer circleDrawer = new CircleDrawer();
                circleDrawer.DrawFigure(figure, canvas);
            }
            else if (figure.GetType() == typeof(Ellipse))
            {
                EllipseDrawer ellipseDrawer = new EllipseDrawer();
                ellipseDrawer.DrawFigure(figure, canvas);
            }
            else if (figure.GetType() == typeof(Polygon))
            {
                PolygonDrawer polygonDrawer = new PolygonDrawer();
                polygonDrawer.DrawFigure(figure, canvas);
            }
        }
        public static void DrawSelection(BaseFigure figure, Graphics canvas)
        {
            if (figure.GetType() == typeof(Line))
            {
                LineDrawer lineDrawer = new LineDrawer();
                lineDrawer.DrawSelection(figure, canvas);
            }
            else if (figure.GetType() == typeof(Polyline))
            {
                PolylineDrawer drawer = new PolylineDrawer();
                drawer.DrawFigure(figure, canvas);
            }
            else if (figure.GetType() == typeof(Circle))
            {
                CircleDrawer circleDrawer = new CircleDrawer();
                circleDrawer.DrawFigure(figure, canvas);
            }
            else if (figure.GetType() == typeof(Ellipse))
            {
                EllipseDrawer ellipseDrawer = new EllipseDrawer();
                ellipseDrawer.DrawFigure(figure, canvas);
            }
            else if (figure.GetType() == typeof(Polygon))
            {
                PolygonDrawer polygonDrawer = new PolygonDrawer();
                polygonDrawer.DrawFigure(figure, canvas);
            }
        }
    }
}
