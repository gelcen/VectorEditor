using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorEditor.Figures
{
    public class Polygon:FillableFigure
    {
        public Polygon()
        {
            _lineProperties = new LineProperties();
            _points = new Points();
            _fillProperty = new FillProperty();
        }
    }
}
