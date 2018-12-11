using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorEditor.Figures;

namespace VectorEditor.Drawers
{
    public class PolygonDrawer : BaseDrawer
    {
        public override void DrawFigure(BaseFigure figure, Graphics canvas)
        {
            var polygon = figure as FillableFigure;
            if (polygon == null) return;

            var points = polygon.Points.GetPoints();
            if (points.Count < 2) return;

            Brush brush = new SolidBrush(polygon.FillProperty.FillColor);
            canvas.FillPolygon(brush, points.ToArray());
            brush.Dispose();

            Pen pen = new Pen(polygon.LineProperties.Color,
                              polygon.LineProperties.Thickness);
            pen.DashStyle = polygon.LineProperties.Style;

            canvas.DrawPolygon(pen, points.ToArray());

            pen.Dispose();
        }

        public override void DrawSelection(BaseFigure figure, Graphics canvas)
        {
            
        }
    }
}
