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
    /// Класс для обработки инструмента полилиния
    /// </summary>
    public class PolylineHandler : FigureCreatingHandler
    {
        /// <summary>
        /// Параметры полилинии
        /// </summary>
        private FigureParameters _figureParameters;

        /// <summary>
        /// Полилиния
        /// </summary>
        private BaseFigure _polyline;

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
        /// Конструктор класса обработчика для полилинии
        /// </summary>
        /// <param name="canvas">Канва</param>
        /// <param name="figureParameters">Параметры фигуры</param>
        public PolylineHandler(PictureBox canvas, FigureParameters figureParameters)
        {
            FigureParameters = figureParameters;
            Canvas = canvas;

            _polyline = null;

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
            if (_polyline != null)
            {
                FigureDrawer.DrawFigure(_polyline, g);
            }
        }

        /// <summary>
        /// Обработчик нажатия мышки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (_polyline == null)
                    {
                        _polyline = FigureFactory.CreateFigure(ToolType.Polyline);

                        _polyline.LineProperties.Color = _figureParameters.LineColor;
                        _polyline.LineProperties.Style = (DashStyle)
                            _figureParameters.LineStyle;
                        _polyline.LineProperties.Thickness = _figureParameters.LineThickness;

                        _polyline.Points.AddPoint(new PointF(e.X, e.Y));
                        _polyline.Points.AddPoint(new PointF(e.X, e.Y));

                        Canvas.Refresh();
                    }
                    else
                    {
                        _polyline.Points.AddPoint(new PointF(e.X, e.Y));

                        Canvas.Refresh();
                    }

                    break;
                case MouseButtons.Right:
                    if (_polyline == null) return;
                    OnFigureCreated(_polyline);
                    _polyline = null;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Обработчик движения мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MouseMove(object sender, MouseEventArgs e)
        {
            PolygonHandler.AddPointToFigure(_polyline, e);

            Canvas.Refresh();
        }

        /// <summary>
        /// Обработчик отпускания мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MouseUp(object sender, MouseEventArgs e)
        {
            PolygonHandler.AddPointToFigure(_polyline, e);

            Canvas.Refresh();
        }
    }
}
