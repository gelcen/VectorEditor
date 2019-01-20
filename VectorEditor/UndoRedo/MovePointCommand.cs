using System.Collections.Generic;
using VectorEditor.Figures;
using VectorEditor.Model;

namespace VectorEditor.UndoRedo
{
    public class MovePointCommand:ICommand
    {
        IModel _model;
        Dictionary<int, BaseFigure> _beforePointState;
        Dictionary<int, BaseFigure> _newPointState;

        public MovePointCommand(IModel model,
                                 Dictionary<int, BaseFigure> beforePointState,
                                 Dictionary<int, BaseFigure> newPointState)
        {
            _model = model;
            _beforePointState = new Dictionary<int, BaseFigure>();
            _newPointState = new Dictionary<int, BaseFigure>();
            foreach (KeyValuePair<int, BaseFigure> entry in beforePointState)
            {
                int index = entry.Key;
                _beforePointState.Add(index, FigureFactory.CreateCopy(entry.Value));
            }
            foreach (KeyValuePair<int, BaseFigure> entry in newPointState)
            {
                int index = entry.Key;
                _newPointState.Add(index, FigureFactory.CreateCopy(entry.Value));
            }
        }

        public void Do()
        {
            foreach (var figure in _newPointState)
            {
                _model.MoveFigure(figure.Key, figure.Value);
            }
        }

        public void Undo()
        {
            foreach (var figure in _beforePointState)
            {
                _model.MoveFigure(figure.Key, figure.Value);
            }
        }

        public override string ToString()
        {
            return "Point is moved ";
        }
    }
}
