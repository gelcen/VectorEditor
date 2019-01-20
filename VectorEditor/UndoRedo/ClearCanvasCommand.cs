using System.Collections.Generic;
using VectorEditor.Figures;
using VectorEditor.Model;

namespace VectorEditor.UndoRedo
{
    public class ClearCanvasCommand : ICommand
    {
        private IModel _model;
        private List<BaseFigure> _figures;

        public ClearCanvasCommand(IModel model)
        {
            _model = model;
            _figures = new List<BaseFigure>();
            foreach (var figure in model.getFigureList())
            {
                _figures.Add(FigureFactory.CreateCopy(figure));
            }
        }

        public override string ToString()
        {
            return "Canvas has been cleared";
        }


        public void Do()
        {

            _model.ClearCanvas();

        }

        public void Undo()
        {
            foreach (var figure in _figures)
            {
                _model.AddFigure(figure);
            }
        }
    }
}
