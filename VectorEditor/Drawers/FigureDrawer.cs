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
            //else if (figure.GetType() == typeof(Polygone))
            //{
            //    PolygoneDrawer polygoneDrawer = new PolygoneDrawer((Polygone)figure, canvas);
            //    polygoneDrawer.Draw();
            //}
        }
    }
}
