using SDK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using VectorEditor.Model;

namespace VectorEditor.Presenter.Handlers.FigureInteractionsHandler
{
    /// <summary>
    /// Класс-обработчик для 
    /// обработки выбора фигур
    /// </summary>
    public class CursorSelectingHandler
    {
        /// <summary>
        /// Ссылка на обработчик
        /// </summary>
        private IHandler _handler;

        /// <summary>
        /// Ссылка на Презентер
        /// </summary>
        private IModel _model;

        /// <summary>
        /// Селектор
        /// </summary>
        private IFigureSelector _selector;

        /// <summary>
        /// Точка нажатия курсора
        /// </summary>
        private Point _beginPoint;

        /// <summary>
        /// Точка разжатия
        /// </summary>
        private Point _endPoint;

        /// <summary>
        /// Прямоугольник выбора
        /// </summary>
        private Rectangle _selectionRectangle;

        /// <summary>
        /// Выбранные фигуры
        /// </summary>
        private Dictionary<int, BaseFigure> _selectedFigures;

        /// <summary>
        /// Нажата ли мышка
        /// </summary>
        private bool _isMousePressed = false;

        /// <summary>
        /// Ссылка на обработчик курсора
        /// </summary>
        private CursorHandler _cursorHandler;

        /// <summary>
        /// Расстояние для клика
        /// </summary>
        private const int _objectRadius = 3;

        /// <summary>
        /// Мышка над фигурой или маркером, 
        /// если расстояние между мышкой и объектом меньше этой.
        /// </summary>
        private const int _overDistSquared = _objectRadius * _objectRadius;

        /// <summary>
        /// Количество пикселей. 
        /// Операция считается за движение - если двинули на большее
        /// расстояние, чем это. 
        /// </summary>
        private const double _dragTreshold = 5;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="model">Презентер</param>
        /// <param name="handler">Обработчик</param>
        /// <param name="cursorHandler">Обработчик курсора</param>
        public CursorSelectingHandler(IModel model,
                                IHandler handler,
                                CursorHandler cursorHandler,
                                IFigureSelector selector)
        {
            _selector = selector;
            _model = model;
            _handler = handler;
            _cursorHandler = cursorHandler;

            _selectedFigures = _cursorHandler.SelectedFigures;

            _handler.Draw += DrawHandler;
            _handler.MouseDown += MouseDownSelecting;
            _handler.MouseUp += MouseUpSelecting;
            _handler.MouseMove += MouseMoveSelecting;            
        }

        /// <summary>
        /// Рисовка 
        /// </summary>
        /// <param name="g"></param>
        private void DrawHandler(Graphics g)
        {
            var pen = new Pen(Color.FromArgb(0, 120, 215), 1)
            { DashStyle = DashStyle.Solid };

            Brush brush = new SolidBrush(Color.FromArgb(80, 0, 102, 204));

            g.FillRectangle(brush, _selectionRectangle);
            g.DrawRectangle(pen, _selectionRectangle);
            pen.Dispose();
            brush.Dispose();            
        }

        /// <summary>
        /// Обработчик нажатия курсора
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void MouseDownSelecting(object o, MouseEventArgs e)
        {
            _beginPoint = new Point(e.X, e.Y);
            _isMousePressed = true;
            _selectionRectangle = new Rectangle()
            {
                X = e.X,
                Y = e.Y
            };
        }

        /// <summary>
        /// Обработчик отжатия курсора
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void MouseUpSelecting(object o, MouseEventArgs e)
        {
            _endPoint = new Point(e.X, e.Y);
            if (WasItMove(_beginPoint, _endPoint))
            {
                GroupSelect();
                Unsubscribe();
            }
            else
            {
                SingleSelect(_endPoint);
                Unsubscribe();
            }
            _isMousePressed = false;

            _handler.CanvasRefresh?.Invoke();
        }

        /// <summary>
        /// Отписка обработчиков и
        /// передача выбранных фигур 
        /// в обработчик курсора
        /// </summary>
        private void Unsubscribe()
        {
            if (_selectedFigures.Count != 0)
            {
                _handler.Draw -= DrawHandler;
                _handler.MouseDown -= MouseDownSelecting;
                _handler.MouseUp -= MouseUpSelecting;
                _handler.MouseMove -= MouseMoveSelecting;

                _cursorHandler.SelectedFigures = _selectedFigures;
                _cursorHandler.SetState(HandlingState.Editing);
            }
        }

        /// <summary>
        /// Обработчик движения курсора
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void MouseMoveSelecting(object o, MouseEventArgs e)
        {
            if (!_isMousePressed) return;

            if (WasItMove(_beginPoint, new PointF(e.X, e.Y)))
            {
                UpdateRectangle(_beginPoint, new Point(e.X, e.Y));
            }

            _handler.CanvasRefresh?.Invoke();
        }



        /// <summary>
        /// Выбор одной фигуры
        /// </summary>
        /// <param name="point">Точка выбора</param>
        private void SingleSelect(Point point)
        {
            if (_model.GetFigureList() == null) return;

            var figures = _model.GetFigureList();

            int key = _selector.GetFigurePointOn(point, figures);

            if (figures.ContainsKey(key))
            {
                _selectedFigures.Add(key, figures[key]);
            }
        }

        /// <summary>
        /// Выбор по площади
        /// </summary>
        private void GroupSelect()
        {
            if (_model.GetFigureList() == null)
            {
                _selectionRectangle = new Rectangle();
                return;
            }

            _selectedFigures = _selector.GetFiguresInRect(_model.GetFigureList(),
                                                    _selectionRectangle);

            _selectionRectangle = new Rectangle();

        }

        /// <summary>
        /// Обновить прямоугольник выбора
        /// </summary>
        /// <param name="p1">Старая точка</param>
        /// <param name="p2">Новая точка</param>
        private void UpdateRectangle(Point p1, Point p2)
        {
            _selectionRectangle.X = Math.Min(p1.X, p2.X);
            _selectionRectangle.Y = Math.Min(p1.Y, p2.Y);
            _selectionRectangle.Width = Math.Max(p1.X, p2.X) - _selectionRectangle.X;
            _selectionRectangle.Height = Math.Max(p1.Y, p2.Y) - _selectionRectangle.Y;
        }

        /// <summary>
        /// Считает, прошла ли мышка расстояние больше порога движения DragTreshold.
        /// </summary>
        /// <param name="beginPoint">Точка нажатия</param>
        /// <param name="endPoint">Текущая точка</param>
        /// <returns></returns>
        private bool WasItMove(PointF beginPoint, PointF endPoint)
        {
            var deltaX = Math.Abs(
                endPoint.X - beginPoint.X);
            var deltaY = Math.Abs(
                endPoint.Y - beginPoint.Y);
            var distance = Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));
            return distance >= _dragTreshold;
        }
    }
}
