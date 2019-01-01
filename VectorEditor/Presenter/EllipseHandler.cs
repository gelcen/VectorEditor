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
    public class EllipseHandler : IBaseHandler
    {
        private FigureParameters _figureParameters;

        private PictureBox _canvas;

        private BaseFigure _ellipse;


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

        public EllipseHandler(PictureBox canvas, FigureParameters figureParameters)
        {
            FigureParameters = figureParameters;
            Canvas = canvas;

            _ellipse = null;

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
            if (_ellipse != null)
            {
                FigureDrawer.DrawFigure(_ellipse, g);
            }
        }

        public void MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var ellipse = FigureFactory.CreateFillableFigure(Item.Ellipse) as FillableFigure;

                ellipse.LineProperties.Color = _figureParameters.LineColor;
                ellipse.LineProperties.Style = (DashStyle)_figureParameters.LineType;
                ellipse.LineProperties.Thickness = _figureParameters.LineThickness;
                ellipse.FillProperty.FillColor = _figureParameters.FillColor;

                _ellipse = ellipse;

                _ellipse.Points.AddPoint(new PointF(e.X, e.Y));
                _ellipse.Points.AddPoint(new PointF(e.X, e.Y));

                Canvas.Refresh();
            }
        }

        public void MouseMove(object sender, MouseEventArgs e)
        {
            if (_ellipse == null) return;
            PointF temp = new PointF(e.Location.X, e.Location.Y);
            _ellipse.Points.RemoveLast();
            _ellipse.Points.AddPoint(temp);

            Canvas.Refresh();
        }

        public void MouseUp(object sender, MouseEventArgs e)
        {
            if (_ellipse == null) return;
            OnFigureCreated(_ellipse);
            _ellipse = null;

            Canvas.Refresh();
        }
    }
}
