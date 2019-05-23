using Newtonsoft.Json;
using SDK;
using System.Drawing;

namespace LineFigure
{
    /// <inheritdoc />
    /// <summary>
    /// Класс для линии
    /// </summary>
    [JsonObject(MemberSerialization.Fields)]
    public class LineFigure:BaseFigure
    {
        /// <summary>
        /// Конструктор класса для линии
        /// </summary>
        public LineFigure()
        {
            _lineProperties = new LineProperties();
            _points = new Points(2);
        }

        public override object Clone()
        {
            LineFigure clone = new LineFigure();

            clone.LineProperties.Color = _lineProperties.Color;
            clone.LineProperties.Style = _lineProperties.Style;
            clone.LineProperties.Thickness = _lineProperties.Thickness;

            foreach (var point in _points.GetPoints())
            {
                clone.Points.AddPoint(new PointF(
                    point.X,
                    point.Y));
            }

            return clone;
        }

        public override string GetFigureName()
        {
            return "Line";
        }
    }
}
