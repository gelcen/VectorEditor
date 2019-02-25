using System.Drawing;
using System.Linq;
using VectorEditor.Figures;

namespace VectorEditor.Drawers
{
    /// <inheritdoc />
    /// <summary>
    /// Класс для рисовки полилинии
    /// </summary>
    public class PolylineDrawer : BaseDrawer
    {
        /// <inheritdoc />
        /// <summary>
        /// Нарисовать фигуру
        /// </summary>
        /// <param name="figure">Рисуемая фигура</param>
        /// <param name="canvas">Канва</param>
        public override void DrawFigure(BaseFigure figure, Graphics canvas)
        {
            var points = figure.Points.GetPoints();
            if (points.Count < 2) return;

            var pen = new Pen(figure.LineProperties.Color,
                              figure.LineProperties.Thickness)
                { DashStyle = figure.LineProperties.Style};

            canvas.DrawLines(pen, points.ToArray());
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
            LineDrawer.DrawLineSelection(figure, canvas);
        }
    }
}
