using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using VectorEditor.Drawers;
using VectorEditor.Figures;
using VectorEditor.View;

namespace VectorEditor.Presenter
{
    public class LineHandler : IBaseHandler
    {
        private FigureParameters _figureParameters;

        private PictureBox _canvas;

        private BaseFigure _line;
                

        public FigureParameters FigureParameters
        {
            set
            {
                _figureParameters = value;
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

        public MouseOperation MouseDownDelegate
        {
            set;
            get;
        }

        public MouseOperation MouseUpDelegate
        {
            set;
            get;
        }

        public MouseOperation MouseMoveDelegate
        {
            set;
            get;
        }

        public LineHandler(PictureBox canvas, FigureParameters figureParameters)
        {
            FigureParameters = figureParameters;
            Canvas = canvas;

            _line = null;

            MouseDownDelegate += MouseDown;
            MouseUpDelegate += MouseUp;
            MouseMoveDelegate += MouseMove;
        }

        public event EventHandler<BaseFigure> FigureCreated;

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
                FigureDrawer.DrawFigure(_line, g);
            }
        }

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
    }
}
