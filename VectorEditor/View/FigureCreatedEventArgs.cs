using System;
using VectorEditor.Figures;

namespace VectorEditor.Model
{
    public class FigureCreatedEventArgs:EventArgs
    {
        public Figure Figure
        {
            get;
            set;
        }
    }
}