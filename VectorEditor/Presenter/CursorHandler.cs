using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using VectorEditor.Drawers;
using VectorEditor.Figures;
using VectorEditor.View;

namespace VectorEditor.Presenter
{
    public class CursorHandler : IBaseHandler, IPropertyChanged
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
                if (_selectedFigure != null)
                {
                    _selectedFigure.LineProperties.Color = _figureParameters.LineColor;
                    _selectedFigure.LineProperties.Style = (DashStyle)_figureParameters.LineType;
                    _selectedFigure.LineProperties.Thickness = _figureParameters.LineThickness;
                    if (_selectedFigure.GetType() == typeof(FillableFigure))
                    {
                        var figure = _selectedFigure as FillableFigure;
                        if (figure == null) return;
                        figure.FillProperty.FillColor = _figureParameters.FillColor;
                    }
                }
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

        public event EventHandler<FigureParameters> ParametersChanged;

        public void Draw(Graphics g)
        {
            Pen pen= new Pen(Color.FromArgb(0, 120, 215), 1);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;

            Brush brush = new SolidBrush(Color.FromArgb(80, 0, 102, 204));

            g.FillRectangle(brush, _selectionRect);
            g.DrawRectangle(pen, _selectionRect);
            pen.Dispose();
            brush.Dispose();
            if (_selectedFigure != null && _isFigurePicked)
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

        private bool _isFigurePicked = false;

        private List<BaseFigure> _selectedFigures;

        private Rectangle _selectionRect;

        private float _offsetX;

        private float _offsetY;

        public void MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isMouseDown = true;
                if (IsPointOnFigure(e.Location))
                {
                    if ((_selectedFigure != null) &&
                        _selectedFigure == GetFigurePointOn(e.Location))
                    {
                        _isMouseDownOnFigure = true;
                        _offsetX = _selectedFigure.Points.GetPoints()[0].X - e.X;
                        _offsetY = _selectedFigure.Points.GetPoints()[0].Y - e.Y;
                    }
                }
                _originalMouseDownPoint = e.Location;
                
            }
        }

        public void MouseMove(object sender, MouseEventArgs e)
        {
            //if (_isDraggingSelectionRect)
            //{
            //    PointF currentMouseDownPoint = e.Location;
            //    UpdateDragSelectionRect(_originalMouseDownPoint,
            //                            currentMouseDownPoint);
            //}
            //else if (_isMouseDown)
            //{
            //    if (_isMouseDownOnFigure)
            //    {
            //        float newX1 = e.X + _offsetX;
            //        float newY1 = e.Y + _offsetY;

            //        float dx = newX1 - _selectedFigure.Points.GetPoints()[0].X;
            //        float dy = newY1 - _selectedFigure.Points.GetPoints()[0].Y;

            //        if (dx == 0 && dy == 0) return;

            //        _isDraggingFigure = true;
            //        PointF tmpPt0 = new PointF(newX1, newY1);
            //        _selectedFigure.Points.Replace(0, tmpPt0);
            //        PointF tempPoint1 = new PointF(
            //                _selectedFigure.Points.GetPoints()[1].X + dx,
            //                _selectedFigure.Points.GetPoints()[1].Y + dy);
            //        _selectedFigure.Points.Replace(1, tempPoint1);                       
            //    }
            //    else
            //    {
            //        PointF currentMouseDownPoint = e.Location;
            //        float deltaX = Math.Abs(
            //                       currentMouseDownPoint.X - _originalMouseDownPoint.X);
            //        float deltaY = Math.Abs(
            //                       currentMouseDownPoint.Y - _originalMouseDownPoint.Y);
            //        double distance = Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));
            //        if (distance > _dragTreshold)
            //        {
            //            _isDraggingSelectionRect = true;
            //            InitDragSelectionRect(_originalMouseDownPoint,
            //                                  currentMouseDownPoint);
            //        }
            //    }                
            //}
            //else
            //{
            if (_isMouseDownOnFigure)
            {
                float newX1 = e.X + _offsetX;
                float newY1 = e.Y + _offsetY;

                float dx = newX1 - _selectedFigure.Points.GetPoints()[0].X;
                float dy = newY1 - _selectedFigure.Points.GetPoints()[0].Y;

                if (dx == 0 && dy == 0) return;

                _isDraggingFigure = true;
                PointF tmpPt0 = new PointF(newX1, newY1);
                _selectedFigure.Points.Replace(0, tmpPt0);
                PointF tempPoint1 = new PointF(
                        _selectedFigure.Points.GetPoints()[1].X + dx,
                        _selectedFigure.Points.GetPoints()[1].Y + dy);
                _selectedFigure.Points.Replace(1, tempPoint1);
            }
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
            //}
            Canvas.Refresh();
        }

        public void MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ////Был создан прямоугольник выборки
                //bool wasDragSelectionApplied = false;

                //if (_isDraggingSelectionRect)
                //{
                //    //Выборка закончилась
                //    _isDraggingSelectionRect = false;
                //    SelectFiguresInRect();

                //    wasDragSelectionApplied = true;
                //}

                //if (_isMouseDown)
                //{
                //    _isMouseDown = false;
                //    if (_isDraggingFigure)
                //    {
                //        _isDraggingFigure = false;
                //    }
                //    if (_isMouseDownOnFigure)
                //    {
                //        PointF currentMouseDownPoint = e.Location;
                //        float deltaX = Math.Abs(
                //                       currentMouseDownPoint.X - _originalMouseDownPoint.X);
                //        float deltaY = Math.Abs(
                //                       currentMouseDownPoint.Y - _originalMouseDownPoint.Y);
                //        double distance = Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));
                //        if (distance < _dragTreshold)
                //        {
                //            _isFigurePicked = true;
                //            _selectedFigure = GetFigurePointOn(_originalMouseDownPoint);
                //        }
                //        _isMouseDownOnFigure = false;
                //    }                    
                //}

                //if(!wasDragSelectionApplied)
                //{
                //    //Клик по пустому месту
                //    _selectedFigures.Clear();
                //    _selectedFigure = null;
                //}
                if (_isMouseDown)
                {
                    if (_isMouseDownOnFigure)
                    {
                        _isMouseDownOnFigure = false;
                    }
                    else
                    {
                        PointF currentMouseDownPoint = e.Location;
                        float deltaX = Math.Abs(
                                       currentMouseDownPoint.X - _originalMouseDownPoint.X);
                        float deltaY = Math.Abs(
                                       currentMouseDownPoint.Y - _originalMouseDownPoint.Y);
                        double distance = Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));
                        if (distance < _dragTreshold)
                        {
                            if (IsPointOnFigure(e.Location))
                            {
                                _selectedFigure = GetFigurePointOn(e.Location);
                                _isFigurePicked = true;
                            }
                            else
                            {
                                _selectedFigure = null;
                                _isFigurePicked = false;
                            }
                        }
                        _isMouseDown = false;
                    }
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
            if (_presenter.GetFigures() != null)
            {
                var figures = _presenter.GetFigures();
                GraphicsPath path = new GraphicsPath();
                foreach (var figure in figures)
                {
                    var points = figure.Points.GetPoints();
                    Pen pickPen = new Pen(Color.Transparent, 3);

                    //path.AddRectangle(GetRect(points));
                    path.AddLine(points[0], points[1]);
                    
                    result = path.IsOutlineVisible(point, pickPen);                    
                    path.Reset();
                    if (result) break;                    
                }
            }
            return result;
        }

        private BaseFigure GetFigurePointOn(PointF point)
        {
            if (_presenter.GetFigures() != null)
            {
                var figures = _presenter.GetFigures();
                foreach (var figure in figures)
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
