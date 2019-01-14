using System;
using VectorEditor.View;

namespace VectorEditor.Presenter
{
    interface IPropertyChanged
    {
        event EventHandler<FigureParameters> ParametersChanged;
    }
}
