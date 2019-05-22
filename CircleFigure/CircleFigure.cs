using Newtonsoft.Json;
using SDK;
using System.Drawing;

namespace CircleFigure
{
    /// <inheritdoc />
    /// <summary>
    /// Класс для окружности
    /// </summary>
    [JsonObject(MemberSerialization.Fields)]
    public class CircleFigure:FillableFigure
    {
        /// <summary>
        /// Конструктор класса окружности
        /// </summary>
        public CircleFigure()
        {
            _lineProperties = new LineProperties();
            _points = new Points(2);
            _fillProperty = new FillProperty();
        }

        public override object Clone()
        {
            CircleFigure clone = new CircleFigure();

            clone.LineProperties.Color = _lineProperties.Color;
            clone.LineProperties.Style = _lineProperties.Style;
            clone.LineProperties.Thickness = _lineProperties.Thickness;

            clone.FillProperty.FillColor = _fillProperty.FillColor;
            clone.FillProperty.IsFilled = _fillProperty.IsFilled;

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
            return "Circle";
        }
    }
}
