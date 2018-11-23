using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VectorEditor.Figures;

namespace VectorEditor.Drawers
{
    public static class FigureDrawer
    {
        public static void DrawFigure(Figure figure, PictureBox canvas)
        {
            if (figure.GetType() == typeof(Line))
            {
                LineDrawer lineDrawer = new LineDrawer((Line)figure, canvas);
                lineDrawer.Draw();
            }
            else if (figure.GetType() == typeof(PolyLine))
            {
                PolyLineDrawer drawer = new PolyLineDrawer((PolyLine)figure, canvas);
                drawer.Draw();
            }
            else if (figure.GetType() == typeof(Circle))
            {
                CircleDrawer circleDrawer = new CircleDrawer((Circle)figure, canvas);
                circleDrawer.Draw();
            }
            else if (figure.GetType() == typeof(Ellipse))
            {
                EllipseDrawer ellipseDrawer = new EllipseDrawer((Ellipse)figure, canvas);
                ellipseDrawer.Draw();
            }
            else if (figure.GetType() == typeof(Polygone))
            {
                PolygoneDrawer polygoneDrawer = new PolygoneDrawer((Polygone)figure, canvas);
                polygoneDrawer.Draw();
            }
        }
    }
}
