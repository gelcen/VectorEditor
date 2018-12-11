using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorEditor.Figures;

namespace VectorEditor.Drawers
{
    public class EllipseDrawer : BaseDrawer
    {
        public override void DrawFigure(BaseFigure figure, Graphics canvas)
        {
            var circle = figure as FillableFigure;
            if (circle == null) return;

            var points = circle.Points.GetPoints();
            if (points.Count != 2) return;

            int width = (int)Math.Abs(points[0].X - points[1].X);
            int height = (int)Math.Abs(points[0].Y - points[1].Y);

            int x = (int)Math.Min(points[0].X, points[1].X);
            int y = (int)Math.Min(points[0].Y, points[1].Y);

            Rectangle circleRect = new Rectangle(x, y, width, height);

            Brush brush = new SolidBrush(circle.FillProperty.FillColor);

            canvas.FillEllipse(brush, circleRect);

            brush.Dispose();

            Pen pen = new Pen(circle.LineProperties.Color,
                              circle.LineProperties.Thickness);
            pen.DashStyle = circle.LineProperties.Style;

            canvas.DrawEllipse(pen, circleRect);

            pen.Dispose();
        }

        public override void DrawSelection(BaseFigure figure, Graphics canvas)
        {
            
        }
    }
}
