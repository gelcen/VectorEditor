﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VectorEditor.Figures;

namespace VectorEditor.Presenter
{
    public class EditingHandler
    {
        private Presenter _presenter;
        private IHandler _handler;
        private NewCursorHandler _cursorHandler;
        private Selector _selector;

        /// <summary>
        /// Точка нажатия курсора
        /// </summary>
        private Point _beginPoint;

        /// <summary>
        /// Точка разжатия
        /// </summary>
        private Point _endPoint;

        /// <summary>
        /// Поле для состояния до передвижения фигур
        /// </summary>
        private Dictionary<int, BaseFigure> _oldFiguresState;

        /// <summary>
        /// Свойство для состояния до передвигания маркера 
        /// </summary>        
        private Dictionary<int, BaseFigure> _oldMarkerState;

        /// <summary>
        /// Id фигуры, у которой двигают
        /// маркером
        /// </summary>
        private int _pickedFigureId;

        /// <summary>
        /// Id маркера, которым двигают
        /// </summary>
        private int _pickedMarkerId;

        private float _offsetX;

        private float _offsetY;

        private PointF _pickedPoint;

        private BaseFigure _selectedFigure;

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

        private bool _isMouseDown;

        public EditingHandler(Presenter presenter,
                                IHandler handler,
                                NewCursorHandler cursorHandler)
        {
            _selector = new Selector();
            _oldFiguresState = new Dictionary<int, BaseFigure>();
            _oldMarkerState = new Dictionary<int, BaseFigure>();

            _presenter = presenter;
            _handler = handler;
            _cursorHandler = cursorHandler;

            _handler.Draw += DrawHandler;
            _handler.MouseDown += MouseDown;
            _handler.MouseUp += MouseUp;
            _handler.MouseMove += MouseMove;
        }

        private void MouseDown(object sender, MouseEventArgs e)
        {            
            _beginPoint = new Point(e.X, e.Y);

            if (e.Button != MouseButtons.Left) return;
            
            if (_selector.GetFigurePointOn(_beginPoint,
                                           _cursorHandler.SelectedFigures) != -1)
            {
                _selectedFigure =
                    _cursorHandler.SelectedFigures
                    [_selector.GetFigurePointOn(_beginPoint,
                     _cursorHandler.SelectedFigures)];

                _oldFiguresState?.Clear();

                foreach (var figure in _cursorHandler.SelectedFigures)
                {
                    var key = figure.Key;
                    _oldFiguresState?.Add(key, FigureFactory.CreateCopy(figure.Value));
                }

                _handler.MouseMove -= MouseMove;
                _handler.MouseMove += MouseMoveFigure;
                _handler.MouseUp += MouseUpFigure;

                int id = _selector.GetFigurePointOn(_beginPoint,
                    _cursorHandler.SelectedFigures);

                var selectedFigure = _cursorHandler.SelectedFigures[id];

                _offsetX = selectedFigure.Points.GetPoints()[0].X - e.X;
                _offsetY = selectedFigure.Points.GetPoints()[0].Y - e.Y;

            }
            else if (_selector.IsPointOnMarker(_cursorHandler.SelectedFigures,
                                              e.Location,
                                              out _pickedPoint,
                                              out _pickedFigureId) != -1)
            {
                _pickedMarkerId = _selector.IsPointOnMarker(_cursorHandler.SelectedFigures,
                                              e.Location,
                                              out _pickedPoint,
                                              out _pickedFigureId);

                _handler.MouseMove -= MouseMove;
                _handler.MouseMove += MouseMoveMarker;
                _handler.MouseUp += MouseUpMarker;

                _oldMarkerState?.Clear();

                _oldMarkerState.Add(_pickedFigureId,
                    FigureFactory.CreateCopy(
                        _presenter.GetFigures()[_pickedFigureId]));

                _offsetX = _pickedPoint.X - e.X;
                _offsetY = _pickedPoint.Y - e.Y;
            }
            else
            {
                _isMouseDown = true;
            }
        }

        private void MouseUp(object sender, MouseEventArgs e)
        {
            if (_isMouseDown)
            {
                _endPoint = new Point(e.X, e.Y);
                if ((_beginPoint.X == _endPoint.X &&
                    _beginPoint.Y == _endPoint.Y) ||
                    !WasItMove(_beginPoint, _endPoint))
                {
                    Unsubscribe();
                }
                _isMouseDown = false;
            }
                       
        }

        /// <summary>
        /// Отписка обработчиков и
        /// передача выбранных фигур 
        /// в обработчик курсора
        /// </summary>
        private void Unsubscribe()
        {
            _handler.Draw -= DrawHandler;
            _handler.MouseDown -= MouseDown;
            _handler.MouseUp -= MouseUp;
            _handler.MouseMove -= MouseMove;

            _cursorHandler.SelectedFigures = 
                new Dictionary<int, BaseFigure>();
            _cursorHandler.SetState(HandlingState.Selecting);
            _handler.CanvasRefresh?.Invoke();
        }

        private void MouseMove(object sender, MouseEventArgs e)
        {
            Cursor newCursor;

            if (_selector.GetFigurePointOn(_beginPoint,
                                           _cursorHandler.SelectedFigures) != -1)
            {
                newCursor = Cursors.Hand;
            }
            else if (_selector.IsPointOnMarker(_cursorHandler.SelectedFigures,
                                              e.Location,
                                              out _pickedPoint,
                                              out _pickedFigureId) != -1)
            {
                newCursor = Cursors.Cross;
            }
            else
            {
                newCursor = Cursors.Default;
            }

            if (sender is PictureBox form)
            {
                if (form.Cursor != newCursor)
                {
                    form.Cursor = newCursor;
                }
            }
        }                

        private void MouseMoveFigure(object sender, MouseEventArgs e)
        {
            if (!WasItMove(_beginPoint, e.Location)) return;

            if (_cursorHandler.SelectedFigures.Count == 1)
            {
                MoveFigurePoints(_selectedFigure, 
                                e.X, e.Y, _offsetX, _offsetY);
            }
            else
            {               
                var firstPoint = _selectedFigure.Points.GetPoints()[0];

                CountDelta(e.X, e.Y,
                           _offsetX, _offsetY,
                           firstPoint,
                           out float dx, out float dy);

                MoveFigurePoints(_selectedFigure,
                                e.X, e.Y, _offsetX, _offsetY);

                foreach (var figure in _cursorHandler.SelectedFigures)
                {
                    if (figure.Value == _selectedFigure) continue;

                    int count = figure.Value.Points.GetPoints().Count;

                    for (int i=0; i < count; i++)
                    {
                        figure.Value.Points.Replace(i,
                            new PointF(
                                figure.Value.Points.GetPoints()[i].X + dx,
                                figure.Value.Points.GetPoints()[i].Y + dy));
                    }
                }
            }

            _handler.CanvasRefresh?.Invoke();
        }

        private void MouseUpFigure(object sender, MouseEventArgs e)
        {
            _handler.MouseMove += MouseMove;
            _handler.MouseMove -= MouseMoveFigure;
            _handler.MouseUp -= MouseUpFigure;

            if (WasItMove(_beginPoint, new PointF(e.X, e.Y)))
            {
                var newState = new Dictionary<int, BaseFigure>();

                foreach (var figure in _cursorHandler.SelectedFigures)
                {                                        
                    newState.Add(figure.Key, 
                        FigureFactory.CreateCopy(figure.Value));
                }

                _cursorHandler.FiguresMoved?.Invoke(_oldFiguresState,
                                                    newState);
            }

            _handler.CanvasRefresh?.Invoke();
        }

        private void MouseMoveMarker(object sender, MouseEventArgs e)
        {
            if (!WasItMove(_beginPoint, e.Location)) return;

            _cursorHandler.SelectedFigures[_pickedFigureId]
                .Points.Replace(_pickedMarkerId,
                new PointF(e.X + _offsetX, e.Y + _offsetY));

            _handler.CanvasRefresh?.Invoke();
        }

        private void MouseUpMarker(object sender, MouseEventArgs e)
        {
            _handler.MouseMove += MouseMove;
            _handler.MouseMove -= MouseMoveMarker;
            _handler.MouseUp -= MouseUpMarker;

            var newMarkerState = new Dictionary<int, BaseFigure>();

            if (WasItMove(_beginPoint, e.Location))
            {
                newMarkerState.Add(_pickedFigureId,
                    FigureFactory.CreateCopy(
                        _cursorHandler.SelectedFigures[_pickedFigureId]));

                _cursorHandler.MarkerMoved?.Invoke(
                    _oldMarkerState, newMarkerState);
            }

            _handler.CanvasRefresh?.Invoke();
        }

        private void DrawHandler(Graphics obj)
        {
            
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

        /// <summary>
        /// Сдвиг точек фигуры
        /// </summary>
        /// <param name="figure">Фигура</param>
        /// <param name="eX">Текущие координата Х</param>
        /// <param name="eY">Текущая координата Y</param>
        /// <param name="offsetX">Смещение по X</param>
        /// <param name="offsetY">Смещение по Y</param>
        private static void MoveFigurePoints(BaseFigure figure,
            int eX, int eY,
            float offsetX, float offsetY)
        {

            CountDelta(eX, eY,
                       offsetX, offsetY,
                       figure.Points.GetPoints()[0],
                       out float dx, out float dy);

            if (Math.Abs(dx) < 0.000000001 && Math.Abs(dy) < 0.000000001) return;

            var tempPoint = new PointF(eX + offsetX, eY + offsetY);
            figure.Points.Replace(0, tempPoint);
            var count = figure.Points.GetPoints().Count;
            for (var i = 1; i < count; i++)
            {
                var tempPoint1 = new PointF(
                    figure.Points.GetPoints()[i].X + dx,
                    figure.Points.GetPoints()[i].Y + dy);
                figure.Points.Replace(i, tempPoint1);
            }
        }

        /// <summary>
        /// Функция для расчета смещения
        /// </summary>
        /// <param name="eX">Текущие координата Х</param>
        /// <param name="eY">Текущая координата Y</param>
        /// <param name="offsetX">Смещение по X</param>
        /// <param name="offsetY">Смещение по Y</param>
        /// <param name="point">Точка</param>
        /// <param name="dx">Смещение</param>
        /// <param name="dy">Смещение</param>
        private static void CountDelta(int eX, int eY, float offsetX,
                                                       float offsetY,
                                                       PointF point,
                                                       out float dx, out float dy)
        {
            var newX = eX + offsetX;
            var newY = eY + offsetY;
            dx = newX - point.X;
            dy = newY - point.Y;
        }


    }
}