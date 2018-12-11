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
            if (_currentHandler != null)
            {
                _currentHandler.RemoveHandlers();
            }
            if (e == Item.Line)
            {
                _currentHandler = new LineHandler(_view.Canvas, _view.FigureParameters);
                _currentHandler.FigureCreated += _currentHandler_FigureCreated;
                _view.CurrentHandler = (LineHandler)_currentHandler;
                _view.CurrentHandler.FigureChanged += delegate { _view.Canvas.Refresh(); };
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
