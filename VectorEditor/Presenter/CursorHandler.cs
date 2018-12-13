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
    public class CursorHandler : IBaseHandler
    {
        private FigureParameters _figureParameters;

        private PictureBox _canvas;

        private BaseFigure _selectedFigure;

        private Presenter _presenter;

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

        public CursorHandler(PictureBox canvas, FigureParameters figureParameters, Presenter presenter)
        {
            FigureParameters = figureParameters;
            Canvas = canvas;
            _presenter = presenter;
            
            _selectedFigure = null;
            _selectedFigures = new List<BaseFigure>();
        }

        public event EventHandler<BaseFigure> FigureCreated;

        public void Draw(Graphics g)
        {
            Pen pen= new Pen(Color.FromArgb(0, 120, 215), 1);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;

            Brush brush = new SolidBrush(Color.FromArgb(80, 0, 102, 204));

            g.FillRectangle(brush, _selectionRect);
            g.DrawRectangle(pen, _selectionRect);
            pen.Dispose();
            brush.Dispose();
            if (_selectedFigure != null)
            {
                FigureDrawer.DrawSelection(_selectedFigure, g);
            }
            
        }

        /// <summary>
        /// Расстояние для клика
        /// </summary>
        private const int object_radius = 3;

        private bool _isMouseDown = false;

        private bool _isDraggingSelectionRect = false;

        private PointF _originalMouseDownPoint;

        private static readonly double _dragTreshold = 5;

        private bool _isMouseDownOnFigure = false;

        private bool _isDraggingFigure = false;

        private List<BaseFigure> _selectedFigures;

        private Rectangle _selectionRect;

        public void MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _selectedFigures.Clear();

                _isMouseDown = true;
                if (IsPointOnFigure(e.Location))
                {
                    _isMouseDownOnFigure = true;
                    _selectedFigure = GetFigurePointOn(e.Location);
                }
                
                _originalMouseDownPoint = e.Location;
            }
        }

        public void MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDraggingSelectionRect)
            {
                PointF currentMouseDownPoint = e.Location;
                UpdateDragSelectionRect(_originalMouseDownPoint,
                                        currentMouseDownPoint);
            }
            else if (_isMouseDown)
            {
                if (_isMouseDownOnFigure)
                {
                    PointF currentMouseDownPoint = e.Location;
                    float deltaX = currentMouseDownPoint.X - _originalMouseDownPoint.X;
                    float deltaY = currentMouseDownPoint.Y - _originalMouseDownPoint.Y;
                    float distance = (float)Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));

                    if (distance > _dragTreshold)
                    {
                        _isDraggingFigure = true;
                        PointF tempPoint0 = new PointF();
                        tempPoint0.X = _selectedFigure.Points.GetPoints()[0].X;
                        tempPoint0.Y = _selectedFigure.Points.GetPoints()[0].Y;
                        PointF tempPoint1 = new PointF();
                        tempPoint1.X = _selectedFigure.Points.GetPoints()[1].X;
                        tempPoint1.Y = _selectedFigure.Points.GetPoints()[1].Y;
                        tempPoint0.X += deltaX;
                        tempPoint0.Y += deltaY;
                        tempPoint1.X += deltaX;
                        tempPoint1.Y += deltaY;
                        _selectedFigure.Points.Replace(0, tempPoint0);
                        _selectedFigure.Points.Replace(1, tempPoint1);
                    }

                }
                else
                {
                    PointF currentMouseDownPoint = e.Location;
                    float deltaX = Math.Abs(
                                   currentMouseDownPoint.X - _originalMouseDownPoint.X);
                    float deltaY = Math.Abs(
                                   currentMouseDownPoint.Y - _originalMouseDownPoint.Y);
                    double distance = Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));
                    if (distance > _dragTreshold)
                    {
                        _isDraggingSelectionRect = true;
                        InitDragSelectionRect(_originalMouseDownPoint,
                                              currentMouseDownPoint);
                    }
                }                
            }
            else
            {
                Cursor newCursor;
                if (IsPointOnFigure(e.Location))
                {
                    newCursor = Cursors.Hand;
                }
                else
                {
                    newCursor = Cursors.Default;
                }

                if (Canvas.Cursor != newCursor)
                {
                    Canvas.Cursor = newCursor;
                }
            }
            Canvas.Refresh();
        }

        public void MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //Была создан прямоугольник выборки
                bool wasDragSelectionApplied = false;

                if (_isDraggingSelectionRect)
                {
                    //Выборка закончилась
                    _isDraggingSelectionRect = false;
                    SelectFiguresInRect();

                    wasDragSelectionApplied = true;
                }

                if (_isMouseDown)
                {
                    _isMouseDown = false;
                    if (_isDraggingFigure)
                    {
                        _isDraggingFigure = false;
                    }
                }

                if(!wasDragSelectionApplied)
                {
                    //Клик по пустому месту
                    _selectedFigures.Clear();
                }
            }
            Canvas.Refresh();
        }

        private void SelectFiguresInRect()
        {
            _selectionRect = new Rectangle();
        }

        private void InitDragSelectionRect(PointF pt1, PointF pt2)
        {
            UpdateDragSelectionRect(pt1, pt2);
        }

        private void UpdateDragSelectionRect(PointF pt1, PointF pt2)
        {
            double x, y, width, height;

            if (pt2.X < pt1.X)
            {
                x = pt2.X;
                width = pt1.X - pt2.X;
            }
            else
            {
                x = pt1.X;
                width = pt2.X - pt1.X;
            }

            if (pt2.Y < pt1.Y)
            {
                y = pt2.Y;
                height = pt1.Y - pt2.Y;
            }
            else
            {
                y = pt1.Y;
                height = pt2.Y - pt1.Y;
            }

            _selectionRect = new Rectangle((int)x, (int)y, 
                                           (int)width, (int)height);
        }

        private bool IsPointOnFigure(PointF point)
        {
            bool result = false;
            foreach (var figure in _presenter.GetFigures())
            {
                var points = figure.Points.GetPoints();
                Pen pickPen = new Pen(Color.Transparent, 3);

                GraphicsPath path = new GraphicsPath();

                //path.AddRectangle(GetRect(points));
                path.AddLine(points[0], points[1]);

                result = path.IsOutlineVisible(point, pickPen);
            }
            return result;
        }

        private BaseFigure GetFigurePointOn(PointF point)
        {
            foreach (var figure in _presenter.GetFigures())
            {
                var points = figure.Points.GetPoints();
                Pen pickPen = new Pen(Color.Transparent, 3);

                GraphicsPath path = new GraphicsPath();

                //path.AddRectangle(GetRect(points));
                path.AddLine(points[0], points[1]);

                if (path.IsOutlineVisible(point, pickPen))
                {
                    return figure;
                }
            }
            return null;
        }

        private RectangleF GetRect(IReadOnlyCollection<PointF> points)
        {
            float minX = points.Min(x => x.X);
            float minY = points.Min(y => y.Y);
            float maxX = points.Max(x => x.X);
            float maxY = points.Max(y => y.Y);
            return new RectangleF(minX, minY, Math.Abs(maxX - minX), Math.Abs(maxY - minY));
        }
    }
}
