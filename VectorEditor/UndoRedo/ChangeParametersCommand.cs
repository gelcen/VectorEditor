using System.Collections.Generic;
using VectorEditor.Figures;
using VectorEditor.Model;
using VectorEditor.View;

namespace VectorEditor.UndoRedo
{
    public class ChangeParametersCommand : ICommand
    {
        private IModel _model;
        private Dictionary<int, BaseFigure> _beforeState;
        private FigureParameters _newParameters;

        public ChangeParametersCommand(IModel model, 
                                       Dictionary<int, BaseFigure> beforeState, 
                                       FigureParameters newParameters)
        {
            _model = model;
            _beforeState = beforeState;
            _newParameters = newParameters;
        }

        public void Do()
        {
            foreach (var figure in _beforeState)
            {
                _model.ChangeFigureParameters(figure.Key, _newParameters);                
            }
        }

        public override string ToString()
        {
            return "Changed figure parameters";
        }

        public void Undo()
        {
            foreach (var figure in _beforeState)
            {
                FigureParameters parameters = parameters = GetParameters(figure.Value);
                _model.ChangeFigureParameters(figure.Key, parameters);
            }
        }

        private FigureParameters GetParameters(BaseFigure figure)
        {
            //REWORK
            FigureParameters parameters;
            parameters.FillColor = System.Drawing.Color.AliceBlue;
            parameters.LineColor = System.Drawing.Color.AliceBlue;
            parameters.LineThickness = 1;
            parameters.LineType = 2;
            //***

            parameters.LineColor = figure.LineProperties.Color;
            parameters.LineType = (int)figure.LineProperties.Style;
            parameters.LineThickness = figure.LineProperties.Thickness;
            if (figure.GetType() == typeof(Circle) ||
                figure.GetType() == typeof(Ellipse) ||
                figure.GetType() == typeof(Polygon))
            {
                var tempFigure = figure as FillableFigure;
                //if (tempFigure == null) return;
                parameters.FillColor = tempFigure.FillProperty.FillColor;
            }
            return parameters;
        }
    }
}
