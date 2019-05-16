using SDK;
using System.Drawing;

namespace PolylineFigure
{
    /// <inheritdoc />
    /// <summary>
    /// Класс полилинии 
    /// </summary>
    public class PolylineFigure: BaseFigure
    {
        /// <summary>
        /// Конструктор класса полилинии 
        /// </summary>
        public PolylineFigure()
        {
            _lineProperties = new LineProperties();
            _points = new Points();
        }

        public override object Clone()
        {
            PolylineFigure clone = new PolylineFigure();

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
            return "Polyline";
        }
    }
}
