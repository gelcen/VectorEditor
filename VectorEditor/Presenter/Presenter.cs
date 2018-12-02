using System.Collections.Generic;
using VectorEditor.Figures;

namespace VectorEditor.Model
{
    /// <summary>
    /// Presenter class
    /// </summary>
    class Presenter : IObserver
    {
        private readonly IView _view;
        private readonly IModel _model;

        public Presenter(IView view, IModel model)
        {
            _view = view;
            _model = model;

            _model.NewProject();


            _model.RegisterObserver(this);
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
        public void Update(List<Figure> figures)
        {

        }
    }
}
