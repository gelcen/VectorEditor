﻿using System.Drawing;
using System.Linq;
using VectorEditor.Figures;

namespace VectorEditor.Drawers
{
    /// <summary>
    /// Класс для рисовки полигона
    /// </summary>
    public class PolygonDrawer : BaseDrawer
    {
        /// <inheritdoc />
        /// <summary>
        /// Нарисовать фигуру
        /// </summary>
        /// <param name="figure">Рисуемая фигура</param>
        /// <param name="canvas">Канва</param>
        public override void DrawFigure(BaseFigure figure, Graphics canvas)
        {
            var polygon = figure as FillableFigure;
            if (polygon == null) return;

            var points = polygon.Points.GetPoints();
            if (points.Count < 2) return;

            Brush brush = new SolidBrush(polygon.FillProperty.FillColor);
            canvas.FillPolygon(brush, points.ToArray());
            brush.Dispose();

            var pen = new Pen(polygon.LineProperties.Color,
                              polygon.LineProperties.Thickness)
                { DashStyle = polygon.LineProperties.Style};

            canvas.DrawPolygon(pen, points.ToArray());

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
