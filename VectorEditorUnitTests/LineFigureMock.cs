using SDK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorEditorUnitTests
{
    class LineFigureMock : BaseFigure
    {
        public LineFigureMock()
        {
            _lineProperties = new LineProperties();
            _points = new Points(2);
        }

        public override object Clone()
        {
            LineFigureMock clone = new LineFigureMock();

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
            return "LineFigureMock";
        }
    }
}
