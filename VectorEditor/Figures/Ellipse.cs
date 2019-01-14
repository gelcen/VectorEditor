using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorEditor.Figures
{
    public class Ellipse:FillableFigure
    {
        public Ellipse()
        {
            _lineProperties = new LineProperties();
            _points = new Points(2);
            _fillProperty = new FillProperty();
        }

    }
}
