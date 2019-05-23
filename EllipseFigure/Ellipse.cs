using Newtonsoft.Json;
using SDK;
using System.Drawing;

namespace EllipseFigure
{
    /// <inheritdoc />
    /// <summary>
    /// Класс для эллипса
    /// </summary>
    [JsonObject(MemberSerialization.Fields)]
    public class EllipseFigure:FillableFigure
    {
        /// <summary>
        /// Конструктор класса эллипс
        /// </summary>
        public EllipseFigure()
        {
            LineProperties = new LineProperties();
            Points = new Points(2);
            FillProperty = new FillProperty();
        }

        public override object Clone()
        {
            EllipseFigure clone = new EllipseFigure();

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

            return clone; ;
        }

        public override string GetFigureName()
        {
            return "Ellipse";
        }
    }
}
