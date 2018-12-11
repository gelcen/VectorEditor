using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorEditor.Figures;

namespace VectorEditor.Drawers
{
    public class LineDrawer : BaseDrawer
    {
        public override void DrawFigure(BaseFigure figure, Graphics canvas)
        {
            if (figure.Points.GetPoints().Count != 2) return;         
            Pen pen = new Pen(figure.LineProperties.Color,
                              figure.LineProperties.Thickness);
            pen.DashStyle = figure.LineProperties.Style;

            canvas.DrawLine(pen, figure.Points.GetPoints()[0],
                            figure.Points.GetPoints()[1]);
        }

        public override void DrawSelection(BaseFigure figure, Graphics canvas)
        {
            
        }
    }
}
