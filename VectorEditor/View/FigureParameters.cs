using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorEditor.View
{
    public struct FigureParameters
    {
        public Pen LinePen;
        public Color FillColor;
        public List<PointF> Points;
        public FigureParameters(Pen pen, Color fillColor, List<PointF> points)
        {
            LinePen = pen;
            FillColor = fillColor;
            Points = points == null ? new List<PointF>() : new List<PointF>(points);
        }
    }
}
