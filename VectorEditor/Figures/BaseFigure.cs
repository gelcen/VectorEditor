using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorEditor.Figures
{
    public abstract class BaseFigure
    {
        protected LineProperties _lineProperties;
        protected Points _points;

        public LineProperties LineProperties
        {
            get
            {
                return _lineProperties;
            }
            set
            {
                _lineProperties = value;
            }
        }

        public Points Points
        {
            get
            {
                return _points;
            }
            set
            {
                _points = value;
            }
        }
    }
}
