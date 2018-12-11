using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorEditor.Figures
{
    public class Polyline: BaseFigure
    {
        public Polyline()
        {
            _lineProperties = new LineProperties();
            _points = new Points();
        }        

    }
}
