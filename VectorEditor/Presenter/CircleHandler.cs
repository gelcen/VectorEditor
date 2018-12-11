using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VectorEditor.Drawers;
using VectorEditor.Figures;
using VectorEditor.View;

namespace VectorEditor.Presenter
{
    public class CircleHandler : IBaseHandler
    {
        private FigureParameters _figureParameters;

        private PictureBox _canvas;

        private BaseFigure _circle;


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

        public CircleHandler(PictureBox canvas, FigureParameters figureParameters)
        {
            FigureParameters = figureParameters;
            Canvas = canvas;

            _circle = null;
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
            if (_circle != null)
            {
                FigureDrawer.DrawFigure(_circle, g);
            }
        }

        public void MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var circle = FigureFactory.CreateFillableFigure(Item.Circle) as FillableFigure;

                circle.LineProperties.Color = _figureParameters.LineColor;
                circle.LineProperties.Style = (DashStyle)_figureParameters.LineType;
                circle.LineProperties.Thickness = _figureParameters.LineThickness;
                circle.FillProperty.FillColor = _figureParameters.FillColor;

                _circle = circle;

                _circle.Points.AddPoint(new PointF(e.X, e.Y));
                _circle.Points.AddPoint(new PointF(e.X, e.Y));

                Canvas.Refresh();
            }
        }

        public void MouseMove(object sender, MouseEventArgs e)
        {
            if (_circle == null) return;
            PointF temp = new PointF(e.Location.X, e.Location.Y);
            _circle.Points.RemoveLast();
            _circle.Points.AddPoint(temp);

            Canvas.Refresh();
        }

        public void MouseUp(object sender, MouseEventArgs e)
        {
            if (_circle == null) return;
            OnFigureCreated(_circle);
            _circle = null;

            Canvas.Refresh();
        }
    }
}
