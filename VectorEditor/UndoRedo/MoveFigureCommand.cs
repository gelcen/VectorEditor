using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorEditor.Figures;
using VectorEditor.Model;

namespace VectorEditor.UndoRedo
{
    public class MoveFigureCommand : ICommand
    {
        IModel _model;
        List<BaseFigure> _originalFigures;
        List<BaseFigure> _newFigures;

        public MoveFigureCommand(IModel model, List<BaseFigure> originals, List<BaseFigure> news)
        {
            _model = model;
            _originalFigures = originals;
            _newFigures = news;
        }

        public void Do()
        {
            
        }

        public void Undo()
        {
            
        }
    }
}
