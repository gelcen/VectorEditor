using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorEditor.Figures;

namespace VectorEditor.Observer
{
    public interface IObserver
    {
        void Update(List<Figure> figures);
    }
}
