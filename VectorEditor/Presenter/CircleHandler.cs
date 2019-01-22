using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using VectorEditor.Drawers;
using VectorEditor.Figures;
using VectorEditor.View;

namespace VectorEditor.Presenter
{
    /// <inheritdoc />
    /// <summary>
    /// Класс для окружности
    /// </summary>
    public class CircleHandler : IBaseHandler
    {
        /// <summary>
        /// Параметры фигуры
        /// </summary>
        private FigureParameters _figureParameters;

        /// <summary>
        /// Поле для окружности
        /// </summary>
        private BaseFigure _circle;

        /// <inheritdoc />
        /// <summary>
        /// Свойство для поля
        /// </summary>
        public FigureParameters FigureParameters
        {
            set
            {
                _figureParameters = value;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Свойство для канвы
        /// </summary>
        public PictureBox Canvas { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Свойство для делегата нажатия на мышку
        /// </summary>
        public MouseOperation MouseDownDelegate
        {
            set;
            get;
        }

        /// <inheritdoc />
        /// <summary>
        /// Свойство для делегата отпускания мышки
        /// </summary>
        public MouseOperation MouseUpDelegate
        {
            set;
            get;
        }

        /// <inheritdoc />
        /// <summary>
        /// Свойство для делегата движения мышкой
        /// </summary>
        public MouseOperation MouseMoveDelegate
        {
            set;
            get;
        }

        /// <summary>
        /// Конструктор класса обработчика для окружности
        /// </summary>
        /// <param name="canvas">Канва</param>
        /// <param name="figureParameters">Параметры</param>
        public CircleHandler(PictureBox canvas, FigureParameters figureParameters)
        {
            FigureParameters = figureParameters;
            Canvas = canvas;

            _circle = null;

            MouseDownDelegate += MouseDown;
            MouseUpDelegate += MouseUp;
            MouseMoveDelegate += MouseMove;
        }

        /// <inheritdoc />
        /// <summary>
        /// Событие создания фигуры-окружности
        /// </summary>
        public event EventHandler<BaseFigure> FigureCreated;

        /// <summary>
        /// Вызов события создания фигуры
        /// </summary>
        /// <param name="createdFigure"></param>
        private void OnFigureCreated(BaseFigure createdFigure)
        {
            var handler = FigureCreated;

            handler?.Invoke(null, createdFigure);
        }

        /// <inheritdoc />
        /// <summary>
        /// Рисовка создаваемой фигуры
        /// </summary>
        /// <param name="g"></param>
        public void Draw(Graphics g)
        {
            if (_circle != null)
            {
                FigureDrawer.DrawFigure(_circle, g);
            }
        }

        /// <summary>
        /// Обработчик нажатия мышки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            var circle = FigureFactory.CreateFillableFigure(Item.Circle) as FillableFigure;

            if (circle != null)
            {
                circle.LineProperties.Color = _figureParameters.LineColor;
                circle.LineProperties.Style = (DashStyle) _figureParameters.LineStyle;
                circle.LineProperties.Thickness = _figureParameters.LineThickness;
                circle.FillProperty.FillColor = _figureParameters.FillColor;

                _circle = circle;
            }

            _circle.Points.AddPoint(new PointF(e.X, e.Y));
            _circle.Points.AddPoint(new PointF(e.X, e.Y));

            Canvas.Refresh();
        }

        /// <summary>
        /// Обработчик движения мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MouseMove(object sender, MouseEventArgs e)
        {
            if (_circle == null) return;
            var temp = new PointF(e.Location.X, e.Location.Y);
            _circle.Points.RemoveLast();
            _circle.Points.AddPoint(temp);

            Canvas.Refresh();
        }

        /// <summary>
        /// Обработчик отпускания мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MouseUp(object sender, MouseEventArgs e)
        {
            if (_circle == null) return;
            OnFigureCreated(_circle);
            _circle = null;

            Canvas.Refresh();
        }
    }
}
