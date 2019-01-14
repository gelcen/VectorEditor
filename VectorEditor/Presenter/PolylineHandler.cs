using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using VectorEditor.Drawers;
using VectorEditor.Figures;
using VectorEditor.View;

namespace VectorEditor.Presenter
{
    public class PolylineHandler : IBaseHandler
    {
        private FigureParameters _figureParameters;

        private PictureBox _canvas;

        private BaseFigure _polyline;


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

        public PolylineHandler(PictureBox canvas, FigureParameters figureParameters)
        {
            FigureParameters = figureParameters;
            Canvas = canvas;

            _polyline = null;

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
            if (_polyline != null)
            {
                FigureDrawer.DrawFigure(_polyline, g);
            }
        }

        public void MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (_polyline == null)
                {
                    _polyline = FigureFactory.CreateFigure(Item.Polyline);

                    _polyline.LineProperties.Color = _figureParameters.LineColor;
                    _polyline.LineProperties.Style = (DashStyle)_figureParameters.LineType;
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
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (_polyline == null) return;
                OnFigureCreated(_polyline);
                _polyline = null;
            }
        }

        public void MouseMove(object sender, MouseEventArgs e)
        {
            if (_polyline == null) return;
            PointF temp = new PointF(e.Location.X, e.Location.Y);
            _polyline.Points.RemoveLast();
            _polyline.Points.AddPoint(temp);

            Canvas.Refresh();
        }

        public void MouseUp(object sender, MouseEventArgs e)
        {

            if (_polyline == null) return;
            PointF temp = new PointF(e.Location.X, e.Location.Y);
            _polyline.Points.RemoveLast();
            _polyline.Points.AddPoint(temp);

            Canvas.Refresh();
        }
    }
}
