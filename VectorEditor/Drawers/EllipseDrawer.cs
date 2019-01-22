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

            var ellipseRectangle= CircleDrawer.MakeRectangle(points[0], points[1], RoundShapeType.Ellipse);

            Brush brush = new SolidBrush(circle.FillProperty.FillColor);

            canvas.FillEllipse(brush, ellipseRectangle);

            brush.Dispose();

            var pen = new Pen(circle.LineProperties.Color,
                              circle.LineProperties.Thickness)
                { DashStyle = circle.LineProperties.Style};

            canvas.DrawEllipse(pen, ellipseRectangle);

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

            CircleDrawer.DrawSelectionRoundShapes(points, canvas);

            var rectangle = CircleDrawer.MakeRectangle(points[0], points[1], RoundShapeType.Ellipse);

            var pen = new Pen(Color.Black, 1)
                          { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash};

            canvas.DrawRectangle(pen, rectangle);

            pen.Dispose();
        }
    }
}
