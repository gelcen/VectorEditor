using System.Collections.Generic;
using VectorEditor.Figures;
using VectorEditor.Model;
using VectorEditor.Presenter;

namespace VectorEditor.Presenter
{
    /// <summary>
    /// Presenter class
    /// </summary>
    public class Presenter : IObserver
    {
        private readonly IView _view;
        private readonly IModel _model;
        private IBaseHandler _currentHandler;

        public Presenter(IView view, IModel model)
        {
            _currentHandler = null;

            _view = view;
            _model = model;

            _model.NewProject();

            _view.ToolPicked += _view_ToolPicked;
            _view.ParametersChanged += _view_ParametersChanged;
            

            _model.RegisterObserver(this);
        }

        private void _view_ParametersChanged(object sender, View.FigureParameters e)
        {
            _currentHandler.FigureParameters = e;
        }

        private void _view_ToolPicked(object sender, Item e)
        {
            if (e == Item.Line)
            {
                _currentHandler = new LineHandler(_view.Canvas, _view.FigureParameters);
                _currentHandler.FigureCreated += _currentHandler_FigureCreated;
                _view.CurrentHandler = (LineHandler)_currentHandler;
            }
            else if (e == Item.Polyline)
            {
                _currentHandler = new PolylineHandler(_view.Canvas, _view.FigureParameters);
                _currentHandler.FigureCreated += _currentHandler_FigureCreated;
                _view.CurrentHandler = (PolylineHandler)_currentHandler;
            }
            else if (e == Item.Circle)
            {
                _currentHandler = new CircleHandler(_view.Canvas, _view.FigureParameters);
                _currentHandler.FigureCreated += _currentHandler_FigureCreated;
                _view.CurrentHandler = (CircleHandler)_currentHandler;
            }
        }

        private void _currentHandler_FigureCreated(object sender, BaseFigure e)
        {
            _model.AddFigure(e);
            _view.Figures = _model.getFigureList();
        }

        /// <summary>
        /// Обработчик события создания фигуры в представлении
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _view_FigureCreated(object sender, FigureCreatedEventArgs e)
        {
            _model.AddFigure(e.Figure);
        }

        /// <summary>
        /// Обновление состояния представления
        /// </summary>
        /// <param name="figures"></param>
        public void Update(List<BaseFigure> figures)
        {

        }
    }
}
