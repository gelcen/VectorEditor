using System.Collections.Generic;
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
            FillableFigure ellipse;

            IReadOnlyList<PointF> points;

            if (!CircleDrawer.GetFillableFigure(figure,
                out ellipse,
                out points))
            {
                return;
            }

            var ellipseRectangle= CircleDrawer.MakeRectangle(
                points[0], points[1],
                RoundShapeType.Ellipse);

            Brush brush = new SolidBrush(ellipse.FillProperty.FillColor);

            canvas.FillEllipse(brush, ellipseRectangle);

            brush.Dispose();

            var pen = new Pen(ellipse.LineProperties.Color,
                               ellipse.LineProperties.Thickness)
                { DashStyle = ellipse.LineProperties.Style};

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
            FillableFigure ellipse;

            IReadOnlyList<PointF> points;

            if (!CircleDrawer.GetFillableFigure(figure, 
                                            out ellipse, 
                                            out points))
            {
                return;
            }
            
            CircleDrawer.DrawSelectionRoundShapes(points, canvas);

            var rectangle = CircleDrawer.MakeRectangle(
                points[0], points[1], 
                RoundShapeType.Ellipse);

            var pen = new Pen(Color.Black, 1)
                          { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash};

            canvas.DrawRectangle(pen, rectangle);

            pen.Dispose();
        }
    }
}
