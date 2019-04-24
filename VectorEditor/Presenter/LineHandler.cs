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
    /// Класс для обработчика инструмента линии
    /// </summary>
    public class LineHandler : BaseFigureCreatingHandler
    {
        /// <summary>
        /// Параметры линии
        /// </summary>
        private FigureParameters _figureParameters;

        /// <summary>
        /// Линия
        /// </summary>
        private BaseFigure _line;

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
        /// Конструктор класса обработчика для линии
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="figureParameters"></param>
        public LineHandler(PictureBox canvas, FigureParameters figureParameters)
        {
            FigureParameters = figureParameters;
            Canvas = canvas;

            _line = null;

            MouseDownDelegate += MouseDown;
            MouseUpDelegate += MouseUp;
            MouseMoveDelegate += MouseMove;
        }

        /// <inheritdoc />
        /// <summary>
        /// Событие создания фигуры-линии
        /// </summary>
        public override event EventHandler<BaseFigure> FigureCreated;

        /// <summary>
        /// Вызов события создания фигуры
        /// </summary>
        /// <param name="createdFigure">Созданная фигура</param>
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
        public override void Draw(Graphics g)
        {
            if (_line != null)
            {
                FigureDrawer.DrawFigure(_line, g);
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
            _line = FigureFactory.CreateFigure(ToolType.Line);
            _line.LineProperties.Color = _figureParameters.LineColor;
            _line.LineProperties.Style = (DashStyle)_figureParameters.LineStyle;
            _line.LineProperties.Thickness = _figureParameters.LineThickness;
            
            _line.Points.AddPoint(new PointF(e.X, e.Y));
            _line.Points.AddPoint(new PointF(e.X, e.Y));

            Canvas.Refresh();
        }

        /// <summary>
        /// Обработчик движения мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MouseMove(object sender, MouseEventArgs e)
        {
            if (_line == null) return;
            var temp = new PointF(e.Location.X, e.Location.Y);
            _line.Points.RemoveLast();
            _line.Points.AddPoint(temp);

            Canvas.Refresh();
        }

        /// <summary>
        /// Обработчик отпускания мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MouseUp(object sender, MouseEventArgs e)
        {
            if (_line == null) return;
            OnFigureCreated(_line);
            _line = null;

            Canvas.Refresh();
        }
    }
}
