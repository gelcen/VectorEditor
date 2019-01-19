using System.Collections.Generic;
using VectorEditor.Figures;
using VectorEditor.Model;

namespace VectorEditor.UndoRedo
{
    public class MoveFigureCommand : ICommand
    {
        IModel _model;
        IView _view;
        Dictionary<int, BaseFigure> _beforeState;
        Dictionary<int, BaseFigure> _newState;

        public MoveFigureCommand(IModel model, 
                                 Dictionary<int, BaseFigure> beforeState,
                                 Dictionary<int, BaseFigure> newState,
                                 IView view)
        {
            _model = model;
            _view = view;
            _beforeState = new Dictionary<int, BaseFigure>();
            _newState = new Dictionary<int, BaseFigure>();

            foreach (KeyValuePair<int, BaseFigure> entry in beforeState)
            {
                int index = entry.Key;
                _beforeState.Add(index, FigureFactory.CreateCopy(entry.Value));
            }
            foreach(KeyValuePair < int, BaseFigure > entry in newState)
            {
                int index = entry.Key;
                _newState.Add(index, FigureFactory.CreateCopy(entry.Value));
            }
        }

        public void Do()
        {
            foreach (var figure in _newState)
            {
                _model.MoveFigure(figure.Key, figure.Value);
            }
            _view.Canvas.Refresh();
        }

        public void Undo()
        {
            foreach (var figure in _beforeState)
            {
                _model.MoveFigure(figure.Key, figure.Value);
            }
            _view.Canvas.Refresh();
        }

        public override string ToString()
        {
            return "Figure or figures are moved ";
        }
    }
}
