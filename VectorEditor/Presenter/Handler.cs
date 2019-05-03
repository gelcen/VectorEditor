using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using VectorEditor.Drawers;
using VectorEditor.Figures;
using VectorEditor.FileManager;

namespace VectorEditor.Presenter
{
    public class Handler:IHandler
    {
        private BaseFigure _createdFigure;

        public FigureParameters FigureParameters
        {
            get;
            set;
        }

        public ToolType CurrentTool { get; set; }

        public Action CanvasRefresh { get; set; }
        
        public Action<object, MouseEventArgs> MouseDown { get; set; }
        public Action<object, MouseEventArgs> MouseUp { get; set; }
        public Action<object, MouseEventArgs> MouseMove { get; set; }

        public Handler(Action canvasRefresh, FigureParameters figureParameters)
        {
            CanvasRefresh = canvasRefresh;
            FigureParameters = figureParameters;

            _createdFigure = null;
            CurrentTool = ToolType.Cursor;

            MouseDown += MouseDownHandler;
            MouseUp += MouseUpHandler;
            MouseMove += MouseMoveHandler;
        }

        public void MouseDownHandler(object sender, MouseEventArgs e)
        {
            if (_createdFigure == null)
            {
                _createdFigure = FigureFactory.CreateFigure(CurrentTool);
                if (_createdFigure is FillableFigure fillable)
                {
                    fillable.FillProperty.FillColor = FigureParameters.FillColor;
                    SetLineProperties(fillable);
                }
                else
                {
                    SetLineProperties(_createdFigure);
                }
            }
            CanvasRefresh();
        }

        public event EventHandler<BaseFigure> FigureCreated;

        private void OnFigureCreated(BaseFigure createdFigure)
        {
            var handler = FigureCreated;

            handler?.Invoke(null, createdFigure);
        }

        private void SetLineProperties(BaseFigure figure)
        {
            figure.LineProperties.Color = FigureParameters.LineColor;
            figure.LineProperties.Thickness = FigureParameters.LineThickness;
            figure.LineProperties.Style = (DashStyle)FigureParameters.LineStyle;
        }

        public void MouseUpHandler(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {                
                if (_createdFigure.Points.GetPoints().Count == 0)
                {
                    _createdFigure.Points.AddPoint(new PointF(e.X, e.Y));
                    _createdFigure.Points.AddPoint(new PointF(e.X, e.Y));
                }
                else if (_createdFigure.Points.IsInRange())
                {
                    _createdFigure.Points.AddPoint(new PointF(e.X, e.Y));
                }
                else
                {
                    OnFigureCreated(_createdFigure);
                    _createdFigure = null;
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (_createdFigure == null) return;
                if (_createdFigure.Points.IsEnough())
                {
                    OnFigureCreated(_createdFigure);
                    _createdFigure = null;
                }
                else
                {
                    _createdFigure = null;
                }                                
            }
            CanvasRefresh();
        }

        public void MouseMoveHandler(object sender, MouseEventArgs e)
        {
            if (_createdFigure == null || 
                _createdFigure.Points.GetPoints().Count < 2) return;

            var temp = new PointF(e.Location.X, e.Location.Y);
            _createdFigure.Points.RemoveLast();
            _createdFigure.Points.AddPoint(temp);

            CanvasRefresh();
        }

        public void Draw(Graphics g)
        {
            if (_createdFigure != null)
            {
                FigureDrawer.DrawFigure(_createdFigure, g);
            }
        }
    }
}
