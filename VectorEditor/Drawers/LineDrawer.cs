using System.Drawing;
using VectorEditor.Figures;

namespace VectorEditor.Drawers
{
    public class LineDrawer : BaseDrawer
    {
        private const int object_radius = 3;

        public override void DrawFigure(BaseFigure figure, Graphics canvas)
        {
            if (figure.Points.GetPoints().Count != 2) return;         
            Pen pen = new Pen(figure.LineProperties.Color,
                              figure.LineProperties.Thickness);
            pen.DashStyle = figure.LineProperties.Style;

            canvas.DrawLine(pen, figure.Points.GetPoints()[0],
                            figure.Points.GetPoints()[1]);
            pen.Dispose();
        }

        public override void DrawSelection(BaseFigure figure, Graphics canvas)
        {
            var points = figure.Points.GetPoints();
            foreach (var pt in points)
            {
                Rectangle rect = new Rectangle(
                    (int)pt.X - object_radius, (int)pt.Y - object_radius,
                    2 * object_radius + 1, 2 * object_radius + 1);
                canvas.FillEllipse(Brushes.White, rect);
                canvas.DrawEllipse(Pens.Black, rect);
            }
        }
    }
}
