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
        public int LineThickness;
        public int LineType;
        public Color LineColor;
        public Color FillColor;
        
        public FigureParameters(int lineThickness, int lineType,
                                Color lineColor, Color fillColor)
        {
            this.LineThickness = lineThickness;
            LineType = lineType;
            LineColor = lineColor;
            FillColor = fillColor;
        }
    }
}
