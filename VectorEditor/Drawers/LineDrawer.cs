using System.Drawing;
using System.Drawing.Drawing2D;
using VectorEditor.Figures;

namespace VectorEditor.Drawers
{
    /// <inheritdoc />
    /// <summary>
    /// Класс-рисовальщик для линии
    /// </summary>
    public class LineDrawer : BaseDrawer
    {
        /// <inheritdoc />
        /// <summary>
        /// Нарисовать фигуру
        /// </summary>
        /// <param name="figure">Рисуемая фигура</param>
        /// <param name="canvas">Канва</param>
        public override void DrawFigure(BaseFigure figure, Graphics canvas)
        {
            if (figure.Points.GetPoints().Count != 2) return;
            var pen = new Pen(figure.LineProperties.Color,
                              figure.LineProperties.Thickness)
                { DashStyle = figure.LineProperties.Style};

            canvas.DrawLine(pen, figure.Points.GetPoints()[0],
                            figure.Points.GetPoints()[1]);
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
            DrawLineSelection(figure, canvas);
        }

        /// <summary>
        /// Рисовка маркеров для линии, полилинии, полигона
        /// </summary>
        /// <param name="figure">Рисуемая фигура</param>
        /// <param name="canvas">Канва</param>
        public static void DrawLineSelection(BaseFigure figure, Graphics canvas)
        {
            var points = figure.Points.GetPoints();
            CircleDrawer.DrawSelectionRoundShapes(points, canvas);
        }
    }
}
