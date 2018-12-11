using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using VectorEditor.Drawers;
using VectorEditor.Figures;
using VectorEditor.View;

namespace VectorEditor.Presenter
{
    public class PolygonHandler : IBaseHandler
    {
        private FigureParameters _figureParameters;

        private PictureBox _canvas;

        private BaseFigure _polygon;


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

        public PolygonHandler(PictureBox canvas, FigureParameters figureParameters)
        {
            FigureParameters = figureParameters;
            Canvas = canvas;

            _polygon = null;
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
            if (_polygon != null)
            {
                FigureDrawer.DrawFigure(_polygon, g);
            }
        }

        public void MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (_polygon == null)
                {
                    var Polygon = FigureFactory.CreateFigure(Item.Polygon) as FillableFigure;

                    Polygon.LineProperties.Color = _figureParameters.LineColor;
                    Polygon.LineProperties.Style = (DashStyle)_figureParameters.LineType;
                    Polygon.LineProperties.Thickness = _figureParameters.LineThickness;
                    Polygon.FillProperty.FillColor = _figureParameters.FillColor;

                    _polygon = Polygon;

                    _polygon.Points.AddPoint(new PointF(e.X, e.Y));
                    _polygon.Points.AddPoint(new PointF(e.X, e.Y));

                    Canvas.Refresh();
                }
                else
                {
                    _polygon.Points.AddPoint(new PointF(e.X, e.Y));

                    Canvas.Refresh();
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (_polygon == null) return;
                OnFigureCreated(_polygon);
                _polygon = null;
            }
        }

        public void MouseMove(object sender, MouseEventArgs e)
        {
            if (_polygon == null) return;
            PointF temp = new PointF(e.Location.X, e.Location.Y);
            _polygon.Points.RemoveLast();
            _polygon.Points.AddPoint(temp);

            Canvas.Refresh();
        }

        public void MouseUp(object sender, MouseEventArgs e)
        {
            if (_polygon == null) return;
            PointF temp = new PointF(e.Location.X, e.Location.Y);
            _polygon.Points.RemoveLast();
            _polygon.Points.AddPoint(temp);

            Canvas.Refresh();
        }
    }
}
