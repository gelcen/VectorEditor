using System;
using VectorEditor.Figures;

namespace VectorEditor.Model
{
    public class FigureCreatedEventArgs:EventArgs
    {
        public BaseFigure Figure
        {
            get;
            set;
        }
    }
}