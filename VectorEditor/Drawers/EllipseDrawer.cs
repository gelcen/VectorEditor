using System;
using System.Drawing;
using VectorEditor.Figures;

namespace VectorEditor.Drawers
{
    /// <inheritdoc />
    /// <summary>
    /// Класс-рисовальщик для эллипса
    /// </summary>
    public class EllipseDrawer : BaseDrawer
    {
        /// <inheritdoc />
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

            var ellipseRectangle= RectangleForEllipse(points[0], points[1]);

            Brush brush = new SolidBrush(circle.FillProperty.FillColor);

            canvas.FillEllipse(brush, ellipseRectangle);

            brush.Dispose();

            var pen = new Pen(circle.LineProperties.Color,
                              circle.LineProperties.Thickness)
                { DashStyle = circle.LineProperties.Style};

            canvas.DrawEllipse(pen, ellipseRectangle);

            pen.Dispose();
        }

        /// <summary>
        /// Создание прямоугольника
        /// </summary>
        /// <param name="pointA">Левая верхняя точка</param>
        /// <param name="pointB">Нижнаяя правая точка</param>
        /// <returns></returns>
        public static Rectangle RectangleForEllipse(PointF pointA, PointF pointB)
        {
            var width = (int)Math.Abs(pointA.X - pointB.X);
            var height = (int)Math.Abs(pointA.Y - pointB.Y);

            var x = (int)Math.Min(pointA.X, pointB.X);
            var y = (int)Math.Min(pointA.Y, pointB.Y);

            return new Rectangle(x, y, width, height);
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

            var rectangle = RectangleForEllipse(points[0], points[1]);

            var pen = new Pen(Color.Black, 1)
                          { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash};

            canvas.DrawRectangle(pen, rectangle);

            pen.Dispose();
        }
    }
}
