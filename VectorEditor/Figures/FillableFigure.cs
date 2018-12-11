using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorEditor.Figures
{
    public class FillableFigure: BaseFigure
    {
        protected FillProperty _fillProperty;

        public FillProperty FillProperty
        {
            get
            {
                return _fillProperty;
            }
            set
            {
                _fillProperty = value;
            }
        }
    }
}
