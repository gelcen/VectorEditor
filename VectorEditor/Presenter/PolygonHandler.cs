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
    /// Класс обработчика для инструмента полигона
    /// </summary>
    public class PolygonHandler : IBaseHandler
    {
        /// <summary>
        /// Параметры полигона
        /// </summary>
        private FigureParameters _figureParameters;

        /// <summary>
        /// Полигон
        /// </summary>
        private BaseFigure _polygon;

        /// <inheritdoc />
        /// <summary>
        /// Свойство для параметров
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
        /// Конструктор класса обработчика для полигона
        /// </summary>
        /// <param name="canvas">Канва</param>
        /// <param name="figureParameters">Параметры фигуры</param>
        public PolygonHandler(PictureBox canvas, FigureParameters figureParameters)
        {
            FigureParameters = figureParameters;
            Canvas = canvas;

            _polygon = null;

            MouseDownDelegate += MouseDown;
            MouseUpDelegate += MouseUp;
            MouseMoveDelegate += MouseMove;
        }

        /// <inheritdoc />
        /// <summary>
        /// Событие создания фигуры-полигона
        /// </summary>
        public event EventHandler<BaseFigure> FigureCreated;

        /// <summary>
        ///  Вызов события создания фигуры
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
        public void Draw(Graphics g)
        {
            if (_polygon != null)
            {
                FigureDrawer.DrawFigure(_polygon, g);
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
                    if (_polygon == null)
                    {
                        var polygon = 
                            FigureFactory.CreateFigure(ToolType.Polygon) as FillableFigure;

                        if (polygon != null)
                        {
                            polygon.LineProperties.Color = _figureParameters.LineColor;
                            polygon.LineProperties.Style = (DashStyle) 
                                _figureParameters.LineStyle;
                            polygon.LineProperties.Thickness = _figureParameters.LineThickness;
                            polygon.FillProperty.FillColor = _figureParameters.FillColor;

                            _polygon = polygon;
                        }

                        if (_polygon != null)
                        {
                            _polygon.Points.AddPoint(new PointF(e.X, e.Y));
                            _polygon.Points.AddPoint(new PointF(e.X, e.Y));
                        }

                        Canvas.Refresh();
                    }
                    else
                    {
                        _polygon.Points.AddPoint(new PointF(e.X, e.Y));

                        Canvas.Refresh();
                    }

                    break;
                case MouseButtons.Right:
                    if (_polygon == null) return;
                    OnFigureCreated(_polygon);
                    _polygon = null;
                    break;
            }
        }

        /// <summary>
        /// Обработчик движения мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MouseMove(object sender, MouseEventArgs e)
        {
            AddPointToFigure(_polygon, e);

            Canvas.Refresh();
        }

        /// <summary>
        /// Добавление точки в полигон, полилинию
        /// </summary>
        /// <param name="polygon">Полигон</param>
        /// <param name="e">Точка</param>
        public static void AddPointToFigure(BaseFigure polygon, MouseEventArgs e)
        {
            if (polygon == null) return;
            var temp = new PointF(e.Location.X, e.Location.Y);
            polygon.Points.RemoveLast();
            polygon.Points.AddPoint(temp);
        }

        /// <summary>
        /// Обработчик отпускания мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MouseUp(object sender, MouseEventArgs e)
        {
            AddPointToFigure(_polygon, e);

            Canvas.Refresh();
        }
    }
}
