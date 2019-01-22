using System;
using System.Drawing;
using VectorEditor.Figures;

namespace VectorEditor.Drawers
{
    /// <summary>
    /// Класс-рисовальщик для эллипса
    /// </summary>
    public class EllipseDrawer : BaseDrawer
    {
        /// <summary>
        /// Нарисовать фигуру
        /// </summary>
        /// <param name="figure">Рисуемая фигура</param>
        /// <param name="canvas">Канва</param>
        public override void DrawFigure(BaseFigure figure, Graphics canvas)
        {
            var circle = figure as FillableFigure;
            if (circle == null) return;

            var points = circle.Points.GetPoints();
            if (points.Count != 2) return;

            var width = (int)Math.Abs(points[0].X - points[1].X);
            var height = (int)Math.Abs(points[0].Y - points[1].Y);

            var x = (int)Math.Min(points[0].X, points[1].X);
            var y = (int)Math.Min(points[0].Y, points[1].Y);

            var circleRect = new Rectangle(x, y, width, height);

            Brush brush = new SolidBrush(circle.FillProperty.FillColor);

            canvas.FillEllipse(brush, circleRect);

            brush.Dispose();

            var pen = new Pen(circle.LineProperties.Color,
                              circle.LineProperties.Thickness)
                { DashStyle = circle.LineProperties.Style};

            canvas.DrawEllipse(pen, circleRect);

            pen.Dispose();
        }

        /// <inheritdoc />
        /// <summary>
        /// Нарисовать маркеры
        /// </summary>
        /// <param name="figure">Рисуемая фигура</param>
        /// <param name="canvas">Канва</param>
        public override void DrawSelection(BaseFigure figure, Graphics canvas)
        {
            var ellipse = figure as FillableFigure;
            if (ellipse == null) return;

            var points = ellipse.Points.GetPoints();
            if (points.Count != 2) return;

            foreach (var pt in points)
            {
                var rect = new Rectangle(
                    (int)pt.X - ObjectRadius, (int)pt.Y - ObjectRadius,
                    2 * ObjectRadius + 1, 2 * ObjectRadius + 1);
                canvas.FillEllipse(Brushes.White, rect);
                canvas.DrawEllipse(Pens.Black, rect);
            }

            var width = (int)Math.Abs(points[0].X - points[1].X);
            var height = (int)Math.Abs(points[0].Y - points[1].Y);

            var x = (int)Math.Min(points[0].X, points[1].X);
            var y = (int)Math.Min(points[0].Y, points[1].Y);

            var circleRect = new Rectangle(x, y, width, height);

            var pen = new Pen(Color.Black, 1)
                          { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash};

            canvas.DrawRectangle(pen, circleRect);

            pen.Dispose();
        }
    }
}
