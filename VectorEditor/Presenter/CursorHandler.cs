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
    /// <summary>
    /// Класс для инструмента "Указатель"
    /// </summary>
    public class CursorHandler : IBaseHandler, IPropertyChanged
    {
        /// <summary>
        /// Ссылка на канву
        /// </summary>
        private PictureBox _canvas;

        /// <summary>
        /// Выбранная фигура
        /// </summary>
        private BaseFigure _selectedFigure;

        /// <summary>
        /// Ссылка на презентер
        /// </summary>
        private Presenter _presenter;

        /// <summary>
        /// Параметры фигуры
        /// </summary>
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

        /// <summary>
        /// Свойство для канвы
        /// </summary>
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

        /// <summary>
        /// Свойство для делегата нажатия мышки
        /// </summary>
        public MouseOperation MouseDownDelegate
        {
            set;
            get;
        }

        /// <summary>
        /// Свойство для делегата отпускания мышки
        /// </summary>
        public MouseOperation MouseUpDelegate
        {
            set;
            get;
        }

        /// <summary>
        /// Свойство для делегата двигания мышкой
        /// </summary>
        public MouseOperation MouseMoveDelegate
        {
            set;
            get;
        }

        /// <summary>
        /// Конструктор класса инструмента "Указатель"
        /// </summary>
        /// <param name="canvas">Канва</param>
        /// <param name="figureParameters">Параметры фигуры</param>
        /// <param name="presenter">Презентер</param>
        public CursorHandler(PictureBox canvas,
            FigureParameters figureParameters,
            Presenter presenter)
        {
            FigureParameters = figureParameters;
            Canvas = canvas;
            _presenter = presenter;

            _selectedFigure = null;
            _selectedFigures = new List<BaseFigure>();
            _beforeState = new Dictionary<int, BaseFigure>();
            _beforePointState = new Dictionary<int, BaseFigure>();

            MouseDownDelegate += MouseDown;
            MouseUpDelegate += MouseUp;
            MouseMoveDelegate += MouseMoveSelecting;
        }

        /// <summary>
        /// События создания фигуры
        /// </summary>
        public event EventHandler<BaseFigure> FigureCreated;

        /// <summary>
        /// Событие изменения параметров фигуры
        /// </summary>
        public event EventHandler<FigureParameters> ParametersChanged;
        
        /// <summary>
        /// Рисование выбранных фигур
        /// </summary>
        /// <param name="g"></param>
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

        /// <summary>
        /// Очистить выборку фигур
        /// </summary>
        public void ClearSelectedFigures()
        {
            _selectedFigures.Clear();
            _selectedFigure = null;
        }

        /// <summary>
        /// Расстояние для клика
        /// </summary>
        private const int object_radius = 3;

        /// <summary>
        /// Мышка над фигурой или маркером, 
        /// если расстояние между мышкой и объектом меньше этой.
        /// </summary>
        private const int over_dist_squared = object_radius * object_radius;

        /// <summary>
        /// Флаг. Нажата ли мышка
        /// </summary>
        private bool _isMouseDown = false;

        /// <summary>
        /// Флаг. Появился ли прямоугольник выборки.
        /// </summary>
        private bool _isDraggingSelectionRect = false;

        /// <summary>
        /// Первоначальная точка нажатия
        /// </summary>
        private PointF _originalMouseDownPoint;

        /// <summary>
        /// Количество пикселей. 
        /// Операция считается за движение - если двинули на большее
        /// расстояние, чем это. 
        /// </summary>
        private static readonly double _dragTreshold = 5;

        /// <summary>
        /// Флаг для выбора фигур
        /// </summary>
        private bool _isFigurePicked = false;

        /// <summary>
        /// Список выбранных фигур
        /// </summary>
        private List<BaseFigure> _selectedFigures;

        /// <summary>
        /// Прямоугольник выборки
        /// </summary>
        private Rectangle _selectionRect;

        /// <summary>
        /// Отступ по X
        /// </summary>
        private float _offsetX;

        /// <summary>
        /// Отступ по Y
        /// </summary>
        private float _offsetY;

        /// <summary>
        /// Выбранный маркер
        /// </summary>
        private PointF _pickedPoint;

        /// <summary>
        /// Индекс выбранного маркера
        /// </summary>
        private int _pickedPointIndex;

        /// <summary>
        /// Индекс выбранной фигуры
        /// </summary>
        private int _pickedFigureIndex;

        /// <summary>
        /// Если выборка пуста. 
        /// Прямоугольник пуст или тыкнули в пустую точку
        /// </summary>
        private bool _isSelectionEmpty = false;

        /// <summary>
        /// Поле-словарь для состояния до передвигания фигур.
        /// Хранит индексы в главном списке и фигуры.
        /// </summary>
        private Dictionary<int, BaseFigure> _beforeState;

        /// <summary>
        /// Свойство для состояния до передвигания фигур
        /// </summary>
        public Dictionary<int, BaseFigure> BeforeState
        {
            get
            {
                return _beforeState;
            }
        }

        /// <summary>
        /// Поле-словарь для состояния до передвигания маркера.
        /// Хранит индексы в главном списке и фигуры.
        /// </summary>
        private Dictionary<int, BaseFigure> _beforePointState;

        /// <summary>
        /// Свойство для состояния до передвигания маркера 
        /// </summary>
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

        /// <summary>
        /// Индекс фигуры, у которой двигают маркером
        /// </summary>
        private int _oldFigureIndex;

        //Мышка нажата
        /// <summary>
        /// Обработчик события нажатия мышки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// События передвигания фигур
        /// </summary>
        public event EventHandler<Dictionary<int, BaseFigure>> FiguresMoved;

        /// <summary>
        /// Вызов обработчика события движения фигур
        /// </summary>
        /// <param name="newState"></param>
        private void OnFiguresMoved(Dictionary<int, BaseFigure> newState)
        {
            EventHandler< Dictionary < int, BaseFigure >> handler = FiguresMoved;

            if (handler != null)
            {
                handler(this, newState);
            }
        }

        //Отпускание фигуры
        /// <summary>
        /// Обработчик отпускания фигур
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Флаг для движения фигур. Становится true,
        /// если двинули фигурой(ами) на 5 пикселей и больше.
        /// </summary>
        private bool _reallyMoved = false;

        /// <summary>
        /// Флаг для движения маркером. Становится true,
        /// если двинули маркером на 5 пикселей и больше.
        /// </summary>
        private bool _isPointMoved = false;

        //Двигание фигурой
        /// <summary>
        /// Обработчик движения фигур
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Событие движения маркером
        /// </summary>
        public event EventHandler<Dictionary<int, BaseFigure>> PointMoved;

        /// <summary>
        /// Вызов обработчика события движения маркера
        /// </summary>
        /// <param name="newPointState"></param>
        private void OnPointMoved(Dictionary<int, BaseFigure> newPointState)
        {
            EventHandler<Dictionary<int, BaseFigure>> handler = PointMoved;

            if (handler != null)
            {
                handler(this, newPointState);
            }
        }

        /// <summary>
        /// Обработчик отпускания маркера
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Обработчик движения маркера
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Обработчик движения мышки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Обработки отпускания мышки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                            _isFigurePicked = true;

                        }
                        else
                        {
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

        /// <summary>
        /// Выбрать фигуры, находящиеся в прямоугольнике выборки 
        /// </summary>
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
                _selectedFigures.Clear();
                _selectedFigures = selectedFigures;
            }
            _selectionRect = new Rectangle();
        }

        /// <summary>
        /// Инициализировать прямоугольник выборки
        /// </summary>
        /// <param name="pt1">Верхняя левая точка</param>
        /// <param name="pt2">Правая нижняя точка</param>
        private void InitDragSelectionRect(PointF pt1, PointF pt2)
        {
            UpdateDragSelectionRect(pt1, pt2);
        }

        /// <summary>
        /// Обновить прямоугольник выборки
        /// </summary>
        /// <param name="pt1">Верхняя левая точка</param>
        /// <param name="pt2">Правая нижняя точка</param>
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

        /// <summary>
        /// Определят:находится ли мышка над фигурой
        /// </summary>
        /// <param name="point">Точка мышки</param>
        /// <returns></returns>
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

        /// <summary>
        /// Получить фигуру, на которую указывает мышка
        /// </summary>
        /// <param name="point">Точка мышки</param>
        /// <returns>Фигура</returns>
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
        /// <summary>
        /// Определяет находится ли точка на маркере
        /// </summary>
        /// <param name="mousePoint">Точка</param>
        /// <param name="pickedPoint">Маркер, на котором точка</param>
        /// <returns></returns>
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

        /// <summary>
        /// Найти расстояние от точки до точки
        /// </summary>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        /// <returns></returns>
        private float FindDistanceToPointSquared(PointF pt1, PointF pt2)
        {
            float dx = pt1.X - pt2.X;
            float dy = pt1.Y - pt2.Y;
            return dx * dx + dy * dy;
        }

        /// <summary>
        /// Получить прямоугольник вокруг фигуры
        /// </summary>
        /// <param name="points">Точки фигуры</param>
        /// <returns>Прямоугольник</returns>
        private Rectangle GetRect(IReadOnlyCollection<PointF> points)
        {
            int minX = (int)points.Min(x => x.X);
            int minY = (int)points.Min(y => y.Y);
            int maxX = (int)points.Max(x => x.X);
            int maxY = (int)points.Max(y => y.Y);
            return new Rectangle(minX, minY, Math.Abs(maxX - minX), Math.Abs(maxY - minY));
        }

        /// <summary>
        /// Свойство для получения выбранных фигур
        /// </summary>
        public List<BaseFigure> SelectedFigures
        {
            get
            {
                return _selectedFigures;
            }
        }
       
        /// <summary>
        /// Добавить фигуру в GraphicsPath
        /// </summary>
        /// <param name="path">GraphicsPath</param>
        /// <param name="figure">Добавляемая фигура</param>
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
