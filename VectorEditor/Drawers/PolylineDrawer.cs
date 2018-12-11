using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorEditor.Figures;

namespace VectorEditor.Drawers
{
    public class PolylineDrawer : BaseDrawer
    {
        public override void DrawFigure(BaseFigure figure, Graphics canvas)
        {
            var points = figure.Points.GetPoints();
            if (points.Count < 2) return;

            Pen pen = new Pen(figure.LineProperties.Color,
                              figure.LineProperties.Thickness);
            pen.DashStyle = figure.LineProperties.Style;

            canvas.DrawLines(pen, points.ToArray());
        }

        public override void DrawSelection(BaseFigure figure, Graphics canvas)
        {

        }
    }
}
