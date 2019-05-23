using Newtonsoft.Json;
using SDK;
using System.Drawing;

namespace PolygonFigure
{
    /// <inheritdoc />
    /// <summary>
    /// Класс Полигон
    /// </summary>
    [JsonObject(MemberSerialization.Fields)]
    public class PolygonFigure:FillableFigure
    {
        /// <summary>
        /// Конструктор класса полигона
        /// </summary>
        public PolygonFigure()
        {
            _lineProperties = new LineProperties();
            _points = new Points(0, 3);
            _fillProperty = new FillProperty();
        }

        public override object Clone()
        {
            PolygonFigure clone = new PolygonFigure();

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
            return "Polygon";
        }
    }
}
