using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorEditor.View;

namespace VectorEditor.Presenter
{
    interface IPropertyChanged
    {
        event EventHandler<FigureParameters> ParametersChanged;
    }
}
