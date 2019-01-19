using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorEditor.Figures;
using VectorEditor.Model;

namespace VectorEditor.UndoRedo
{
    public class DeleteFigureCommand : ICommand
    {
        private IModel _model;
        private Dictionary<int, BaseFigure> _deletedFigures;

        public DeleteFigureCommand(IModel model, Dictionary<int, BaseFigure> deletedFigures)
        {
            _model = model;
            _deletedFigures = deletedFigures;            
        }

        public override string ToString()
        {
            return "Deleted figure or figures";
        }
        public void Do()
        {
            foreach (var figure in _deletedFigures)
            {
                _model.DeleteFigure(figure.Value);
            }
        }

        public void Undo()
        {
            foreach (var figure in _deletedFigures)
            {
                _model.AddFigure(figure.Value);
            }
        }
    }
}
