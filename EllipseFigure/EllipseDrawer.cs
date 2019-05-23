using SDK;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace EllipseFigure
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


            if (!GetFillableFigure(figure,
                                   out FillableFigure ellipse,
                                   out IReadOnlyList<PointF> points))
            {
                return;
            }

            var ellipseRectangle= MakeRectangle(
                points[0], points[1]);

            Color fillColor;

            fillColor = ellipse.FillProperty.IsFilled ?
                ellipse.FillProperty.FillColor : Color.Transparent;

            Brush brush = new SolidBrush(fillColor);

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

            if (!GetFillableFigure(figure, 
                                   out ellipse, 
                                   out points))
            {
                return;
            }
            
            DrawSelectionRoundShapes(points, canvas);

            var rectangle = MakeRectangle(
                points[0], points[1]);

            var pen = new Pen(Color.Black, 1)
                          { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash};

            canvas.DrawRectangle(pen, rectangle);

            pen.Dispose();
        }

        /// <summary>
        /// Создание прямоугольника
        /// </summary>
        /// <param name="pointA">Левая верхняя точка</param>
        /// <param name="pointB">Нижнаяя правая точка</param>
        /// <returns></returns>
        private Rectangle MakeRectangle(PointF pointA,
            PointF pointB)
        {
            var width = (int)Math.Abs(pointA.X - pointB.X);
            var height = (int)Math.Abs(pointA.Y - pointB.Y);

            var x = (int)Math.Min(pointA.X, pointB.X);
            var y = (int)Math.Min(pointA.Y, pointB.Y);

            return new Rectangle(x, y, width, height);
        }

        /// <summary>
        /// Рисовка маркеров и границ у округлых фигур
        /// </summary>
        /// <param name="points">Точки</param>
        /// <param name="canvas">Канва</param>
        private void DrawSelectionRoundShapes(IReadOnlyCollection<PointF> points,
                                              Graphics canvas)
        {
            foreach (var pt in points)
            {
                var rect = new Rectangle(
                    (int)pt.X - ObjectRadius, (int)pt.Y - ObjectRadius,
                    2 * ObjectRadius + 1, 2 * ObjectRadius + 1);
                canvas.FillEllipse(Brushes.White, rect);
                canvas.DrawEllipse(Pens.Black, rect);
            }
        }

        /// <summary>
        /// Получить заливаемую фигуру и его точки
        /// </summary>
        /// <param name="inFigure">Фигура на вход</param>
        /// <param name="outFigure">Заполняемая фигура</param>
        /// <param name="points">Точки фигуры</param>
        /// <returns></returns>
        private static bool GetFillableFigure(BaseFigure inFigure,
                                       out FillableFigure outFigure,
                                       out IReadOnlyList<PointF> points)
        {
            var fillableFigure = inFigure as FillableFigure;
            if (fillableFigure != null)
            {
                outFigure = fillableFigure;
                points = fillableFigure.Points.GetPoints();
                if (points.Count != 2)
                {
                    return false;
                }
                return true;
            }
            outFigure = null;
            points = null;
            return false;
        }
    }
}
