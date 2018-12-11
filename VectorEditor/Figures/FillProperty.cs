using System.Drawing;

namespace VectorEditor.Figures
{
    public class FillProperty
    {
        public Color FillColor
        {
            get;
            set;
        }

        public FillProperty()
        {
            FillColor = Color.Transparent;
        }

        public FillProperty(Color fillColor)
        {
            FillColor = fillColor;
        }
    }
}
