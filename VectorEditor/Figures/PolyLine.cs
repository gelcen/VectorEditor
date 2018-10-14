using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorEditor
{
    /// <summary>
    /// Класс для полилинии
    /// </summary>
    class PolyLine:ILineProperty
    {
        public List<Point> points = new List<Point>();

        public void Add(int x, int y)
        {
            points.Add(new Point(x, y));
        }

        public void SetProperties(int lineThickness, 
                        Color lineColor,
                        LineType lineType)
        {
            LineThickness = lineThickness;
            LineColor = lineColor;
            LineType = lineType;
        }

        public int LineThickness
        {
            get;
            set;
        }

        public Color LineColor
        {
            get;
            set;
        }

        public LineType LineType
        {
            get;
            set;
        }


    }
}
