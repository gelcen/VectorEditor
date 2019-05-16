using SDK;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using VectorEditor.Drawers;
using VectorEditor.Figures;
using VectorEditor.FileManager;

namespace VectorEditor.Presenter
{
    /// <summary>
    /// Класс-обработчик создания фигур
    /// </summary>
    public class FigureCreatingHandler
    {
        /// <summary>
        /// Созданная фигура
        /// </summary>
        private BaseFigure _createdFigure;

        /// <summary>
        /// Поле для хранения обработчика
        /// </summary>
        private IHandler _handler;        

        /// <summary>
        /// Параметры фигуры
        /// </summary>
        public FigureParameters FigureParameters
        {
            get;
            set;
        }

        /// <summary>
        /// Текущий инструмент(фигура)
        /// </summary>
        public string CurrentFigure { get; set; }

        public IDrawerFacade DrawerFacade
        {
            get;
            set;
        }

        public IFactory<BaseFigure> FigureFactory
        {
            get;
            set;
        }

        /// <summary>
        /// Конструктор класса 
        /// </summary>
        /// <param name="canvasRefresh">Ссылка на делегат обновления канвы</param>
        /// <param name="figureParameters">Параметры фигуры</param>
        /// <param name="handler">Ссылка на обработчик</param>
        public FigureCreatingHandler(Action canvasRefresh, 
                                     FigureParameters figureParameters, 
                                     IHandler handler,
                                     IFactory<BaseFigure> figureFactory,
                                     IDrawerFacade drawerFacade)
        {
            FigureFactory = figureFactory;
            DrawerFacade = drawerFacade;
            _handler = handler;
            _handler.CanvasRefresh = canvasRefresh;
            FigureParameters = figureParameters;            

            _createdFigure = null;

            _handler.MouseDown += MouseDownHandler;
            _handler.MouseUp += MouseUpHandler;
            _handler.MouseMove += MouseMoveHandler;
            _handler.Draw += DrawHandler;
        }

        /// <summary>
        /// Обработчик события Mouse Down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MouseDownHandler(object sender, MouseEventArgs e)
        {
            if (_createdFigure == null)
            {
                _createdFigure = FigureFactory.CreateInstance(CurrentFigure);
                if (_createdFigure is FillableFigure fillable)
                {
                    fillable.FillProperty.FillColor = FigureParameters.FillColor;
                    fillable.FillProperty.IsFilled = FigureParameters.IsFilled;
                    SetLineProperties(fillable);
                }
                else
                {
                    SetLineProperties(_createdFigure);
                }
            }

            _handler.CanvasRefresh?.Invoke();
        }

        /// <summary>
        /// Событие создания фигуры
        /// </summary>
        public event EventHandler<BaseFigure> FigureCreated;

        /// <summary>
        /// Вызов обработчика события создания фигуры
        /// </summary>
        /// <param name="createdFigure"></param>
        private void OnFigureCreated(BaseFigure createdFigure)
        {
            var handler = FigureCreated;

            handler?.Invoke(null, createdFigure);
        }

        /// <summary>
        /// Установить для фигуры 
        /// параметры линии
        /// </summary>
        /// <param name="figure">Фигура</param>
        private void SetLineProperties(BaseFigure figure)
        {
            figure.LineProperties.Color = FigureParameters.LineColor;
            figure.LineProperties.Thickness = FigureParameters.LineThickness;
            figure.LineProperties.Style = (DashStyle)FigureParameters.LineStyle;
        }

        /// <summary>
        /// Обработчик события Mouse Up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MouseUpHandler(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {                
                if (_createdFigure.Points.GetPoints().Count == 0)
                {
                    _createdFigure.Points.AddPoint(new PointF(e.X, e.Y));
                    _createdFigure.Points.AddPoint(new PointF(e.X, e.Y));
                }
                else if (_createdFigure.Points.IsInRange())
                {
                    _createdFigure.Points.AddPoint(new PointF(e.X, e.Y));
                }
                else
                {
                    OnFigureCreated(_createdFigure);
                    _createdFigure = null;
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (_createdFigure == null) return;
                if (_createdFigure.Points.IsEnough())
                {
                    OnFigureCreated(_createdFigure);
                    _createdFigure = null;
                }
                else
                {
                    _createdFigure = null;
                }                                
            }

            _handler.CanvasRefresh?.Invoke();
        }

        /// <summary>
        /// Обработчик события Mouse Move
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MouseMoveHandler(object sender, MouseEventArgs e)
        {
            if (_createdFigure == null || 
                _createdFigure.Points.GetPoints().Count < 2) return;

            var temp = new PointF(e.Location.X, e.Location.Y);
            _createdFigure.Points.RemoveLast();
            _createdFigure.Points.AddPoint(temp);

            _handler.CanvasRefresh?.Invoke();
        }

        /// <summary>
        /// Обработчик рисовки
        /// </summary>
        /// <param name="g"></param>
        public void DrawHandler(Graphics g)
        {
            if (_createdFigure != null)
            {                
                DrawerFacade.DrawFigure(_createdFigure, g);
            }
        }

    }
}
