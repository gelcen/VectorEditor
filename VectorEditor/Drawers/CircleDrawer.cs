using System;
using System.Collections.Generic;
using System.Drawing;
using VectorEditor.Figures;

namespace VectorEditor.Drawers
{
    /// <inheritdoc />
    /// <summary>
    /// Класс-рисовальщик для окружности
    /// </summary>
    public class CircleDrawer : BaseDrawer
    {
        /// <inheritdoc />
        /// <summary>
        /// Нарисовать фигуру
        /// </summary>
        /// <param name="figure">Рисуемая фигура</param>
        /// <param name="canvas">Канва</param>
        public override void DrawFigure(BaseFigure figure, Graphics canvas)
        {
            FillableFigure circle;

            IReadOnlyList<PointF> points;

            if (!GetFillableFigure(figure, out circle, out points)) return;

            var circleRect = MakeRectangle(points[0], points[1], 
                RoundShapeType.Circle);

            Brush brush = new SolidBrush(circle.FillProperty.FillColor);
            canvas.FillEllipse(brush, circleRect);
            brush.Dispose();

            var pen = new Pen(circle.LineProperties.Color,
                              circle.LineProperties.Thickness)
                { DashStyle = circle.LineProperties.Style};

            canvas.DrawEllipse(pen, circleRect);
            pen.Dispose();
        }

        /// <summary>
        /// Создание прямоугольника
        /// </summary>
        /// <param name="pointA">Левая верхняя точка</param>
        /// <param name="pointB">Нижнаяя правая точка</param>
        /// <param name="shapeType">Тип фигуры</param>
        /// <returns></returns>
        public static Rectangle MakeRectangle(PointF pointA, 
            PointF pointB, 
            RoundShapeType shapeType)
        {
            var width = (int) Math.Abs(pointA.X - pointB.X);
            var height = (int) Math.Abs(pointA.Y - pointB.Y);

            var radius = Math.Max(width, height);

            var x = (int)Math.Min(pointA.X, pointB.X);
            var y = (int)Math.Min(pointA.Y, pointB.Y);

            return shapeType == 
                   RoundShapeType.Circle ? new Rectangle(x, y, radius, radius) : 
                                           new Rectangle(x, y, width, height);
        }

        /// <summary>
        /// Рисовка маркеров и границ у округлых фигур
        /// </summary>
        /// <param name="points">Точки</param>
        /// <param name="canvas">Канва</param>
        public static void DrawSelectionRoundShapes(IReadOnlyCollection<PointF> points, 
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

        /// <inheritdoc />
        /// <summary>
        /// Нарисовать маркеры
        /// </summary>
        /// <param name="figure">Рисуемая фигура</param>
        /// <param name="canvas">Канва</param>
        public override void DrawSelection(BaseFigure figure, Graphics canvas)
        {
            FillableFigure circle;

            IReadOnlyList<PointF> points;

            if (!GetFillableFigure(figure, out circle, out points)) return;

            DrawSelectionRoundShapes(points, canvas);

            var circleRect = MakeRectangle(points[0], points[1], 
                RoundShapeType.Circle);

            var pen = new Pen(Color.Black, 1)
                          { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash};

            canvas.DrawRectangle(pen, circleRect);

            pen.Dispose();
        }

        /// <summary>
        /// Получить заливаемую фигуру и его точки
        /// </summary>
        /// <param name="inFigure">Фигура на вход</param>
        /// <param name="outFigure">Заполняемая фигура</param>
        /// <param name="points">Точки фигуры</param>
        /// <returns></returns>
        public static bool GetFillableFigure(BaseFigure inFigure,
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
