﻿using System;
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
                //_figureParameters = value;
                //if (_selectedFigure != null)
                //{
                //    SetParameters(_selectedFigure, _figureParameters);
                //}
                //if (_selectedFigures != null)
                //{
                //    SetParameters(_selectedFigures, _figureParameters);
                //}
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

        public CursorHandler(PictureBox canvas,
            FigureParameters figureParameters,
            Presenter presenter)
        {
            FigureParameters = figureParameters;
            Canvas = canvas;
            _presenter = presenter;

            _selectedFigure = null;
            _selectedFigures = new List<BaseFigure>();
            _originalFigures = new List<BaseFigure>();
            _beforeState = new Dictionary<int, BaseFigure>();
            _beforePointState = new Dictionary<int, BaseFigure>();

            MouseDownDelegate += MouseDown;
            MouseUpDelegate += MouseUp;
            MouseMoveDelegate += MouseMoveSelecting;
        }


        public event EventHandler<BaseFigure> FigureCreated;

        public event EventHandler<FigureParameters> ParametersChanged;
        

        public void Draw(Graphics g)
        {
            Pen pen = new Pen(Color.FromArgb(0, 120, 215), 1);
            pen.DashStyle = DashStyle.Solid;

            Brush brush = new SolidBrush(Color.FromArgb(80, 0, 102, 204));

            g.FillRectangle(brush, _selectionRect);
            g.DrawRectangle(pen, _selectionRect);
            pen.Dispose();
            brush.Dispose();

            if (_selectedFigures.Count != 0)
            {
                foreach (var figure in _selectedFigures)
                {
                    FigureDrawer.DrawSelection(figure, g);
                }
            }
            if (_selectedFigure != null && _isFigurePicked)
            {
                FigureDrawer.DrawSelection(_selectedFigure, g);

            }

        }

        public void ClearSelectedFigures()
        {
            _originalFigures.Clear();
            _selectedFigures.Clear();
            _selectedFigure = null;
        }

        /// <summary>
        /// Расстояние для клика
        /// </summary>
        private const int object_radius = 3;

        // We're over an object if the distance squared
        // between the mouse and the object is less than this.
        private const int over_dist_squared = object_radius * object_radius;

        private bool _isMouseDown = false;

        private bool _isDraggingSelectionRect = false;

        private PointF _originalMouseDownPoint;

        private static readonly double _dragTreshold = 5;

        private bool _isFigurePicked = false;

        private List<BaseFigure> _selectedFigures;

        private List<BaseFigure> _originalFigures;

        private Rectangle _selectionRect;

        private float _offsetX;

        private float _offsetY;

        private PointF _pickedPoint;

        private int _pickedPointIndex;

        private int _pickedFigureIndex;

        private bool _isSelectionEmpty = false;

        private Dictionary<int, BaseFigure> _beforeState;

        public Dictionary<int, BaseFigure> BeforeState
        {
            get
            {
                return _beforeState;
            }
        }

        private Dictionary<int, BaseFigure> _beforePointState;

        public Dictionary<int, BaseFigure> BeforePointState
        {
            get
            {
                return _beforePointState;
            }

            set
            {
                _beforePointState = value;
            }
        }

        private int _oldFigureIndex;

        //Мышка нажата
        public void MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (_isFigurePicked != false &&
                    _selectedFigures != null)
                {
                    if (IsPointOnFigure(e.Location))
                    {
                        if ((_selectedFigures != null) &&
                            _selectedFigures.Contains(GetFigurePointOn(e.Location)))
                        {
                            _selectedFigure = GetFigurePointOn(e.Location);

                            //Сохраняем предыдущее состояние
                            if (_beforeState != null) _beforeState.Clear();

                            foreach (var figure in _selectedFigures)
                            {                                
                                if (_presenter.GetFigures().Contains(figure))
                                {
                                    int index = _presenter.GetFigures().IndexOf(figure);
                                    _beforeState.Add(index, FigureFactory.CreateCopy(figure));
                                }
                            }

                            MouseMoveDelegate -= MouseMoveSelecting;
                            MouseMoveDelegate += MouseMoveFigure;
                            MouseUpDelegate += MouseUpFigure;

                            _offsetX = _selectedFigure.Points.GetPoints()[0].X - e.X;
                            _offsetY = _selectedFigure.Points.GetPoints()[0].Y - e.Y;
                        }
                    }
                    else if (IsPointOnMarker(e.Location, out _pickedPoint))
                    {
                        MouseMoveDelegate -= MouseMoveSelecting;
                        MouseMoveDelegate += MouseMoveMarker;
                        MouseUpDelegate += MouseUpMarker;

                        if (_beforePointState != null) _beforePointState.Clear();

                        foreach (var figure in _presenter.GetFigures())
                        {
                            if (figure == _selectedFigures[_pickedFigureIndex])
                            {
                                _oldFigureIndex=_presenter.GetFigures().IndexOf(figure);
                                _beforePointState.Add(_oldFigureIndex, 
                                                      FigureFactory.CreateCopy(figure));

                            }
                        }

                        _offsetX = _pickedPoint.X - e.X;
                        _offsetY = _pickedPoint.Y - e.Y;
                    }
                    else
                    {
                        _isMouseDown = true;
                    }
                }
                else
                {
                    _isMouseDown = true;
                }
                _originalMouseDownPoint = e.Location;

            }
        }

        public event EventHandler<Dictionary<int, BaseFigure>> FiguresMoved;

        private void OnFiguresMoved(Dictionary<int, BaseFigure> newState)
        {
            EventHandler< Dictionary < int, BaseFigure >> handler = FiguresMoved;

            if (handler != null)
            {
                handler(this, newState);
            }
        }

        //Отпускание фигуры
        private void MouseUpFigure(object obj, MouseEventArgs e)
        {
            MouseMoveDelegate += MouseMoveSelecting;
            MouseMoveDelegate -= MouseMoveFigure;
            MouseUpDelegate -= MouseUpFigure;

            if (_reallyMoved)
            {
                Dictionary<int, BaseFigure> newState = new Dictionary<int, BaseFigure>();
                foreach (var figure in _selectedFigures)
                {
                    if (_presenter.GetFigures().Contains(figure))
                    {
                        int index = _presenter.GetFigures().IndexOf(figure);
                        newState.Add(index, FigureFactory.CreateCopy(figure));
                    }
                }

                OnFiguresMoved(newState);
                _reallyMoved = false;
            }

            Canvas.Refresh();
        }

        private bool _reallyMoved = false;
        private bool _isPointMoved = false;

        //Двигание фигурой
        private void MouseMoveFigure(object obj, MouseEventArgs e)
        {
            PointF currentMouseDownPoint = e.Location;
            float deltaX = Math.Abs(
                           currentMouseDownPoint.X - _originalMouseDownPoint.X);
            float deltaY = Math.Abs(
                           currentMouseDownPoint.Y - _originalMouseDownPoint.Y);
            double distance = Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));
            if (distance >= _dragTreshold)
            {
                _reallyMoved = true;
            }

            if (_selectedFigures == null) return;
            if (_selectedFigures.Count == 1)
            {
                float newX1 = e.X + _offsetX;
                float newY1 = e.Y + _offsetY;

                float dx = newX1 - _selectedFigure.Points.GetPoints()[0].X;
                float dy = newY1 - _selectedFigure.Points.GetPoints()[0].Y;

                if (dx == 0 && dy == 0) return;

                PointF tmpPt0 = new PointF(newX1, newY1);
                _selectedFigure.Points.Replace(0, tmpPt0);
                int count = _selectedFigure.Points.GetPoints().Count;
                for (int i = 1; i < count; i++)
                {
                    PointF tempPoint1 = new PointF(
                        _selectedFigure.Points.GetPoints()[i].X + dx,
                        _selectedFigure.Points.GetPoints()[i].Y + dy);
                    _selectedFigure.Points.Replace(i, tempPoint1);
                }
            }
            else
            {
                float newX1 = e.X + _offsetX;
                float newY1 = e.Y + _offsetY;

                float dx = newX1 - _selectedFigure.Points.GetPoints()[0].X;
                float dy = newY1 - _selectedFigure.Points.GetPoints()[0].Y;

                if (dx == 0 && dy == 0) return;

                PointF tmpPt0 = new PointF(newX1, newY1);
                _selectedFigure.Points.Replace(0, tmpPt0);
                int count = _selectedFigure.Points.GetPoints().Count;
                for (int i = 1; i < count; i++)    
                {
                    PointF tempPoint1 = new PointF(
                        _selectedFigure.Points.GetPoints()[i].X + dx,
                        _selectedFigure.Points.GetPoints()[i].Y + dy);
                    _selectedFigure.Points.Replace(i, tempPoint1);
                }
                
                foreach (var figure in _selectedFigures)
                {
                    if (figure != _selectedFigure)
                    {
                        for (int i = 0; i < figure.Points.GetPoints().Count; i++)
                        {
                            figure.Points.Replace(i,
                                new PointF(
                                    figure.Points.GetPoints()[i].X + dx,
                                    figure.Points.GetPoints()[i].Y + dy));
                        }
                    }
                }
            }

            Canvas.Refresh();
        }

        public event EventHandler<Dictionary<int, BaseFigure>> PointMoved;

        private void OnPointMoved(Dictionary<int, BaseFigure> newPointState)
        {
            EventHandler<Dictionary<int, BaseFigure>> handler = PointMoved;

            if (handler != null)
            {
                handler(this, newPointState);
            }
        }

        //Отпускание точки-маркера
        private void MouseUpMarker(object obj, MouseEventArgs e)
        {
            MouseMoveDelegate += MouseMoveSelecting;
            MouseMoveDelegate -= MouseMoveMarker;
            MouseUpDelegate -= MouseUpMarker;

            Dictionary<int, BaseFigure> newPointState = new Dictionary<int, BaseFigure>();
            if (_isPointMoved)
            {
                newPointState.Add(_pickedFigureIndex, 
                                    FigureFactory.CreateCopy(_selectedFigures[_pickedFigureIndex]));

                OnPointMoved(newPointState);

                _isPointMoved = false;
            }            

            Canvas.Refresh();
        }

        //Двигание точки - маркера
        private void MouseMoveMarker(object obj, MouseEventArgs e)
        {
            PointF currentMouseDownPoint = e.Location;
            float deltaX = Math.Abs(
                           currentMouseDownPoint.X - _originalMouseDownPoint.X);
            float deltaY = Math.Abs(
                           currentMouseDownPoint.Y - _originalMouseDownPoint.Y);
            double distance = Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));
            if (distance >= _dragTreshold)
            {
                _isPointMoved = true;
            }
            _selectedFigures[_pickedFigureIndex].Points.Replace(
                             _pickedPointIndex,
                             new PointF(e.X + _offsetX, e.Y + _offsetY));

            Canvas.Refresh();
        }

        public void MouseMove(object sender, MouseEventArgs e)
        {

        }

        //Выборка или просто движение
        public void MouseMoveSelecting(object sender, MouseEventArgs e)
        {
            if (_isDraggingSelectionRect)
            {
                PointF currentMouseDownPoint = e.Location;
                UpdateDragSelectionRect(_originalMouseDownPoint,
                                        currentMouseDownPoint);
            }
            else if (_isMouseDown)
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
            Cursor newCursor;
            if (IsPointOnFigure(e.Location))
            {
                newCursor = Cursors.Hand;
            }
            else if (IsPointOnMarker(e.Location, out _pickedPoint))
            {
                newCursor = Cursors.Cross;
            }
            else
            {
                newCursor = Cursors.Default;
            }

            if (Canvas.Cursor != newCursor)
            {
                Canvas.Cursor = newCursor;
            }


            Canvas.Refresh();
        }

        //Отпускание мыши после выборки или выбор одной фигуры
        public void MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                if (_isDraggingSelectionRect)
                {
                    //Выборка закончилась
                    _isDraggingSelectionRect = false;
                    SelectFiguresInRect();
                    _isFigurePicked = true;
                    if (_isSelectionEmpty)
                    {
                        _originalFigures.Clear();
                        _isFigurePicked = false;
                        _selectedFigures.Clear();
                        _selectedFigure = null;
                        _isSelectionEmpty = false;
                    }
                }

                if (_isMouseDown)
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
                            _selectedFigures.Clear();
                            _selectedFigure = GetFigurePointOn(e.Location);
                            _selectedFigures.Add(_selectedFigure);
                            _originalFigures.Add(FigureFactory.CreateCopy(_selectedFigure));           
                            _isFigurePicked = true;

                        }
                        else
                        {
                            _originalFigures.Clear();
                            _selectedFigures.Clear();
                            _selectedFigure = null;
                            _isFigurePicked = false;
                        }
                    }
                    _isMouseDown = false;
                }
            }
            Canvas.Refresh();
        }

        private void SelectFiguresInRect()
        {
            if (_presenter.GetFigures() == null)
            {
                _selectionRect = new Rectangle();
                return;
            }
            int count = _presenter.GetFigures().Count;
            List<BaseFigure> selectedFigures = new List<BaseFigure>();
            for (int i = 0; i < count; i++)
            {
                var points = _presenter.GetFigures()[i].Points.GetPoints();
                Rectangle figureRect = GetRect(points);
                if (_selectionRect.IntersectsWith(figureRect))
                {
                    selectedFigures.Add(_presenter.GetFigures()[i]);
                }
            }
            if (selectedFigures.Count == 0)
            {
                _isSelectionEmpty = true;
            }
            else
            {
                _originalFigures.Clear();
                _selectedFigures.Clear();
                _selectedFigures = selectedFigures;
                foreach (var figure in selectedFigures)
                {
                    _originalFigures.Add(FigureFactory.CreateCopy(figure));
                }
            }
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

                    //path.AddLine(points[0], points[1]);
                    AddFigureToGraphicsPath(path, figure);

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

                    AddFigureToGraphicsPath(path, figure);


                    if (path.IsOutlineVisible(point, pickPen))
                    {
                        return figure;
                    }
                }
            }
            return null;
        }

        //For selected figure
        private bool IsPointOnMarker(PointF mousePoint,
                                     out PointF pickedPoint)
        {
            if (_selectedFigures != null)
            {
                if (_selectedFigures.Count == 1)
                {
                    var points = _selectedFigures[0].Points.GetPoints();
                    int count = _selectedFigures[0].Points.GetPoints().Count;
                    for (int i = 0; i < count; i++)
                    {
                        if (FindDistanceToPointSquared(mousePoint,
                            _selectedFigures[0].Points.GetPoints()[i]) < over_dist_squared)
                        {
                            pickedPoint = _selectedFigures[0].Points.GetPoints()[i];
                            _pickedPointIndex = i;
                            _pickedFigureIndex = 0;
                            return true;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < _selectedFigures.Count; i++)
                    {
                        var points = _selectedFigures[i].Points.GetPoints();
                        int count = _selectedFigures[i].Points.GetPoints().Count;
                        for (int j = 0; j < count; j++)
                        {
                            if (FindDistanceToPointSquared(mousePoint,
                                _selectedFigures[i].Points.GetPoints()[j]) < over_dist_squared)
                            {
                                pickedPoint = _selectedFigures[i].Points.GetPoints()[j];
                                _pickedPointIndex = j;
                                _pickedFigureIndex = i;
                                return true;
                            }
                        }
                    }
                }
            }
            _pickedPointIndex = -1;
            _pickedFigureIndex = -1;
            pickedPoint = new PointF(-1, -1);
            return false;
        }

        private float FindDistanceToPointSquared(PointF pt1, PointF pt2)
        {
            float dx = pt1.X - pt2.X;
            float dy = pt1.Y - pt2.Y;
            return dx * dx + dy * dy;
        }

        private Rectangle GetRect(IReadOnlyCollection<PointF> points)
        {
            int minX = (int)points.Min(x => x.X);
            int minY = (int)points.Min(y => y.Y);
            int maxX = (int)points.Max(x => x.X);
            int maxY = (int)points.Max(y => y.Y);
            return new Rectangle(minX, minY, Math.Abs(maxX - minX), Math.Abs(maxY - minY));
        }

        private BaseFigure SetParameters(BaseFigure figure, FigureParameters parameters)
        {
            figure.LineProperties.Color = parameters.LineColor;
            figure.LineProperties.Style = (DashStyle)parameters.LineType;
            figure.LineProperties.Thickness = parameters.LineThickness;
            if (figure.GetType() == typeof(Circle) ||
                figure.GetType() == typeof(Ellipse) ||
                figure.GetType() == typeof(Polygon))
            {
                var tempFigure = figure as FillableFigure;
                if (tempFigure == null) return null;
                tempFigure.FillProperty.FillColor = parameters.FillColor;
                figure = tempFigure;
            }
            return figure;
        }

        private List<BaseFigure> SetParameters(List<BaseFigure> figures,
                                               FigureParameters parameters)
        {
            foreach (var figure in figures)
            {
                figure.LineProperties.Color = parameters.LineColor;
                figure.LineProperties.Style = (DashStyle)parameters.LineType;
                figure.LineProperties.Thickness = parameters.LineThickness;
                if (figure.GetType() == typeof(Circle) ||
                    figure.GetType() == typeof(Ellipse) ||
                    figure.GetType() == typeof(Polygon))
                {
                    //FIX: сделать темп фигуру до проверки с as и проверить на null
                    var tempFigure = figure as FillableFigure;
                    if (tempFigure == null) return null;
                    tempFigure.FillProperty.FillColor = parameters.FillColor;
                }
            }
            return figures;
        }

        private FigureParameters GetParameters(BaseFigure figure, FigureParameters parameters)
        {
            parameters.LineColor = figure.LineProperties.Color;
            parameters.LineType = (int)figure.LineProperties.Style;
            parameters.LineThickness = figure.LineProperties.Thickness;
            if (figure.GetType() == typeof(Circle) ||
                figure.GetType() == typeof(Ellipse) ||
                figure.GetType() == typeof(Polygon))
            {
                var tempFigure = figure as FillableFigure;
                //if (tempFigure == null) return;
                parameters.FillColor = tempFigure.FillProperty.FillColor;
            }
            return parameters;
        }

        public List<BaseFigure> SelectedFigures
        {
            get
            {
                return _selectedFigures;
            }
        }

        public List<BaseFigure> OriginalFigures
        {
            get
            {
                return _originalFigures;
            }
        }
       

        private void AddFigureToGraphicsPath(GraphicsPath path, BaseFigure figure)
        {
            var points = figure.Points.GetPoints();

            if (figure.GetType() == typeof(Line))
            {
                path.AddLine(points[0], points[1]);
            }
            else if (figure.GetType() == typeof(Circle))
            {
                int width = (int)Math.Abs(points[0].X - points[1].X);
                int height = (int)Math.Abs(points[0].Y - points[1].Y);

                int radius = Math.Max(width, height);

                int x = (int)Math.Min(points[0].X, points[1].X);
                int y = (int)Math.Min(points[0].Y, points[1].Y);

                Rectangle circleRect = new Rectangle(x, y, radius, radius);
                path.AddEllipse(circleRect);
            }
            else if (figure.GetType() == typeof(Ellipse))
            {
                int width = (int)Math.Abs(points[0].X - points[1].X);
                int height = (int)Math.Abs(points[0].Y - points[1].Y);

                int x = (int)Math.Min(points[0].X, points[1].X);
                int y = (int)Math.Min(points[0].Y, points[1].Y);

                Rectangle ellipseRect = new Rectangle(x, y, width, height);
                path.AddEllipse(ellipseRect);
            }
            else if (figure.GetType() == typeof(Polyline))
            {
                path.AddLines(points.ToArray());
            }
            else if (figure.GetType() == typeof(Polygon))
            {
                path.AddPolygon(points.ToArray());
            }
        }
    }
}
