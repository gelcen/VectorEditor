using System.Drawing;
using System.Drawing.Drawing2D;

namespace VectorEditor.Figures
{
    public class LineProperties
    {
        public Color Color
        {
            get;
            set;
        }

        public int Thickness
        {
            get;
            set;
        }

        public DashStyle Style
        {
            get;
            set;
        }

        public LineProperties()
        {
            Color = Color.Black;
            Thickness = 1;
            Style = DashStyle.Solid;
        }

        public LineProperties(Color lineColor, int thickness, DashStyle style)
        {
            Color = lineColor;
            Thickness = thickness;
            Style = style;
        }
    }
}
