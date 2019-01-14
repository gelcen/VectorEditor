using System;
using System.Collections.Generic;
using VectorEditor.Figures;
using VectorEditor.Model;

namespace VectorEditor.UndoRedo
{
    public class AddFigureCommand:ICommand
    {
        private IModel _model;
        private BaseFigure _figure;

        public AddFigureCommand(IModel model, BaseFigure figure)
        {
            _model = model;
            _figure = figure;
        }

        public override string ToString()
        {
            return "Added " + _figure.GetType();
        }

        public void Do()
        {
            _model.AddFigure(_figure);
        }

        public void Undo()
        {
            _model.DeleteFigure(_figure);
        }

    }

}
