using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorEditor.Figures;
using VectorEditor.Model;

namespace VectorEditor.UndoRedo
{
    public class MovePointCommand:ICommand
    {
        IModel _model;
        IView _view;
        Dictionary<int, BaseFigure> _beforePointState;
        Dictionary<int, BaseFigure> _newPointState;

        public MovePointCommand(IModel model,
                                 Dictionary<int, BaseFigure> beforePointState,
                                 Dictionary<int, BaseFigure> newPointState,
                                 IView view)
        {
            _model = model;
            _view = view;
            _beforePointState = beforePointState;
            _newPointState = newPointState;
        }

        public void Do()
        {
            foreach (var figure in _newPointState)
            {
                _model.MoveFigure(figure.Key, figure.Value);
            }
            _view.Canvas.Refresh();
        }

        public void Undo()
        {
            foreach (var figure in _beforePointState)
            {
                _model.MoveFigure(figure.Key, figure.Value);
            }
            _view.Canvas.Refresh();
        }

        public override string ToString()
        {
            return "Point is moved ";
        }
    }
}
