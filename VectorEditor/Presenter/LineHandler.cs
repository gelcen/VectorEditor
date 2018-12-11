using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VectorEditor.Figures;
using VectorEditor.View;

namespace VectorEditor.Presenter
{
    public class LineHandler : IBaseHandler
    {
        private FigureParameters _figureParameters;

        private List<PointF> _points;

        private PictureBox _canvas;

        private BaseFigure _line;
                

        public FigureParameters FigureParameters
        {
            set
            {
                _figureParameters = value;
            }
        }

        public List<PointF> Points
        {
            get
            {
                return _points;
            }
        }

        public PictureBox Canvas
        {
            get
            {
                return _canvas;
            }
            set
            {
                _canvas = value;
            }
        }

        public LineHandler(PictureBox canvas, FigureParameters figureParameters)
        {
            FigureParameters = figureParameters;
            Canvas = canvas;
            _points = new List<PointF>();

            _line = null;
        }

        public event EventHandler<BaseFigure> FigureChanged = delegate { };

        public event EventHandler<BaseFigure> FigureCreated;

        public void OnFigureChanged(BaseFigure e)
        {
            FigureChanged(this, e);
        }

        private void OnFigureCreated(BaseFigure createdFigure)
        {
            EventHandler<BaseFigure> handler = FigureCreated;

            if (handler != null)
            {
                handler(null, createdFigure);
            }
        }

        public void Draw(Graphics g)
        {
            if (_line != null)
            {
                Pen pen = new Pen(_line.LineProperties.Color,
                              _line.LineProperties.Thickness);
                pen.DashStyle = _line.LineProperties.Style;

                g.DrawLine(pen, _line.Points.GetPoints()[0],
                                _line.Points.GetPoints()[1]);
            }
        }

        // Рисовка в OnPaint текущей фигуры, которая передается через интерфейс

        public void MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _line = FigureFactory.CreateFigure(Item.Line);
                _line.LineProperties.Color = _figureParameters.LineColor;
                _line.LineProperties.Style = (DashStyle)_figureParameters.LineType;
                _line.LineProperties.Thickness = _figureParameters.LineThickness;

                _line.Points.AddPoint(new PointF(e.X, e.Y));
                _line.Points.AddPoint(new PointF(e.X, e.Y));

                Canvas.Refresh();
            }
        }

        public void MouseMove(object sender, MouseEventArgs e)
        {
            if (_line == null) return;
            PointF temp = new PointF(e.Location.X, e.Location.Y);
            _line.Points.RemoveLast();
            _line.Points.AddPoint(temp);

            Canvas.Refresh();
        }

        public void MouseUp(object sender, MouseEventArgs e)
        {
            if (_line == null) return;
            OnFigureCreated(_line);
            _line = null;

            Canvas.Refresh();
        }

        public void RemoveHandlers()
        {
            Canvas.MouseDown -= MouseDown;
            Canvas.MouseUp -= MouseUp;
            Canvas.MouseMove -= MouseMove;
        }
    }
}
