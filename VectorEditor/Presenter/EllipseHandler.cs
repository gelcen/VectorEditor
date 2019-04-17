using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using VectorEditor.Drawers;
using VectorEditor.Figures;
using VectorEditor.FileManager;

namespace VectorEditor.Presenter
{
    /// <inheritdoc />
    /// <summary>
    /// Класс для обработки инструмента эллипса
    /// </summary>
    public class EllipseHandler : BaseFigureCreatingHandler
    {
        /// <summary>
        /// Параметры эллипса
        /// </summary>
        private FigureParameters _figureParameters;

        /// <summary>
        /// Эллипс
        /// </summary>
        private BaseFigure _ellipse;

        /// <inheritdoc />
        /// <summary>
        /// Свойство для параметров
        /// </summary>
        public override FigureParameters FigureParameters
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
        public override PictureBox Canvas { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Свойство для делегата нажатия на мышку
        /// </summary>
        public override MouseOperation MouseDownDelegate
        {
            set;
            get;
        }

        /// <inheritdoc />
        /// <summary>
        /// Свойство для делегата отпускания мышки
        /// </summary>
        public override MouseOperation MouseUpDelegate
        {
            set;
            get;
        }

        /// <inheritdoc />
        /// <summary>
        /// Свойство для делегата движения мышкой
        /// </summary>
        public override MouseOperation MouseMoveDelegate
        {
            set;
            get;
        }

        /// <summary>
        ///  Конструктор класса обработчика для эллипса
        /// </summary>
        /// <param name="canvas">Канва</param>
        /// <param name="figureParameters">Параметры</param>
        public EllipseHandler(PictureBox canvas, FigureParameters figureParameters)
        {
            FigureParameters = figureParameters;
            Canvas = canvas;

            _ellipse = null;

            MouseDownDelegate += MouseDown;
            MouseUpDelegate += MouseUp;
            MouseMoveDelegate += MouseMove;
        }

        /// <inheritdoc />
        /// <summary>
        /// Событие создания фигуры-полилинии
        /// </summary>
        public override event EventHandler<BaseFigure> FigureCreated;

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
        ///  Рисовка создаваемой фигуры
        /// </summary>
        /// <param name="g">Graphics</param>
        public override void Draw(Graphics g)
        {
            if (_ellipse != null)
            {
                FigureDrawer.DrawFigure(_ellipse, g);
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
            var ellipse = FigureFactory.CreateFillableFigure(ToolType.Ellipse) as FillableFigure;

            if (ellipse != null)
            {
                ellipse.LineProperties.Color = _figureParameters.LineColor;
                ellipse.LineProperties.Style = (DashStyle) _figureParameters.LineStyle;
                ellipse.LineProperties.Thickness = _figureParameters.LineThickness;
                ellipse.FillProperty.FillColor = _figureParameters.FillColor;

                _ellipse = ellipse;
            }

            _ellipse.Points.AddPoint(new PointF(e.X, e.Y));
            _ellipse.Points.AddPoint(new PointF(e.X, e.Y));

            Canvas.Refresh();
        }

        /// <summary>
        /// Обработчик движения мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MouseMove(object sender, MouseEventArgs e)
        {
            if (_ellipse == null) return;
            var temp = new PointF(e.Location.X, e.Location.Y);
            _ellipse.Points.RemoveLast();
            _ellipse.Points.AddPoint(temp);

            Canvas.Refresh();
        }

        /// <summary>
        /// Обработчик отпускания мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MouseUp(object sender, MouseEventArgs e)
        {
            if (_ellipse == null) return;
            OnFigureCreated(_ellipse);
            _ellipse = null;

            Canvas.Refresh();
        }
    }
}
