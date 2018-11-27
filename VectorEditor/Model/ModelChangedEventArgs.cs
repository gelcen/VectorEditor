using System.Collections.Generic;
using VectorEditor.Figures;

namespace VectorEditor.Model
{
    public class ModelChangedEventArgs
    {
        public List<Figure> Figures
        {
            get;
            set;
        }
    }
}