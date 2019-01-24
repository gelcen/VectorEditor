using System;
using System.Collections.Generic;
using VectorEditor.Figures;
using VectorEditor.Model;
using VectorEditor.Observer;
using VectorEditor.UndoRedo;
using VectorEditor.View;

namespace VectorEditor.Presenter
{
    /// <inheritdoc />
    /// <summary>
    /// Класс представителя
    /// </summary>
    public class Presenter : IObserver
    {
        /// <summary>
        /// Ссылка на представление
        /// </summary>
        private readonly IView _view;

        /// <summary>
        /// Ссылка на модель
        /// </summary>
        private readonly IModel _model;

        /// <summary>
        /// Текущий обработчик инструмента
        /// </summary>
        private IBaseHandler _currentHandler;
        
        /// <summary>
        /// Список фигур
        /// </summary>
        private List<BaseFigure> _figures;

        /// <summary>
        /// Менеджер Undo Redo
        /// </summary>
        private readonly UndoRedoStack _undoRedoStack;

        /// <summary>
        /// Конструктор представителя
        /// </summary>
        /// <param name="view">Представление</param>
        /// <param name="model">Модель</param>
        public Presenter(IView view, IModel model)
        {
            _currentHandler = null;
            _undoRedoStack = new UndoRedoStack();

            _view = view;
            _model = model;

            _model.NewProject();


            _view.ToolPicked += _view_ToolPicked;
            _view.ParametersChanged += _view_ParametersChanged;
            _view.CanvasCleared += _view_CanvasCleared;
            _view.FiguresDeleted += _view_FiguresDeleted;
            _view.FigureCopied += _view_FigureCopied;
            _view.UndoPressed += _view_UndoPressed;
            _view.RedoPressed += _view_RedoPressed;
            _view.CommandStack = _undoRedoStack;
            _view.FileLoaded += _view_FileLoaded;
                            
            _model.RegisterObserver(this);
        }

        /// <summary>
        /// Обработчик события загрузки проекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _view_FileLoaded(object sender, FileLoadedEventArgs e)
        {
            _model.ClearCanvas();
            foreach (var figure in e.Figures)
            {
                _model.AddFigure(figure);
            }
            foreach (var command in e.UndoStack)
            {
                CommandFactory.RestorePointersToModel(command, _model);
            }
            foreach (var command in e.RedoStack)
            {
                CommandFactory.RestorePointersToModel(command, _model);
            }
            //_model.FiguresList = e.Figures;
            _undoRedoStack.Reset();
            foreach (var command in e.UndoStack)
            {
                _undoRedoStack.UndoStack.Push(command);
            }
            foreach (var command in e.RedoStack)
            {
                _undoRedoStack.RedoStack.Push(command);
            }
            //_undoRedoStack.UndoStack = e.UndoStack;
            //_undoRedoStack.RedoStack = e.RedoStack;
            FixCommands();
            _view.Canvas.Refresh();
        }

        /// <summary>
        /// Обработчик события возврата команды
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _view_RedoPressed(object sender, EventArgs e)
        {
            _undoRedoStack.Redo();
            _view.Canvas.Refresh();
        }

        /// <summary>
        /// Обработчик события отмены команды
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _view_UndoPressed(object sender, EventArgs e)
        {
            _undoRedoStack.Undo();
            if (_model.GetFigureList().Count == 0)
            {
                if (_currentHandler.GetType() == typeof(CursorHandler))
                {
                    var handler = _currentHandler as CursorHandler;
                    handler?.ClearSelectedFigures();
                }
            }
            _view.Canvas.Refresh();
        }

        /// <summary>
        /// Обработчик события копирования фигур(ы)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _view_FigureCopied(object sender, EventArgs e)
        {
            if (_currentHandler.GetType() != typeof(CursorHandler)) return;
            var handler = _currentHandler as CursorHandler;
            if (handler == null) return;
            foreach (var figure in handler.SelectedFigures)
            {
                if (_model.GetFigureList().Contains(figure))
                {
                    _model.CopyFigure(figure);
                }
            }
        }

        /// <summary>
        /// Обработчик события удаления фигур(ы)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _view_FiguresDeleted(object sender, EventArgs e)
        {
            if (_currentHandler.GetType() != typeof(CursorHandler)) return;
            var handler = _currentHandler as CursorHandler;
            var beforeState = new Dictionary<int, BaseFigure>();
            if (handler == null) return;
            foreach (var figure in handler.SelectedFigures)
            {
                if (!_model.GetFigureList().Contains(figure)) continue;
                var index = _model.GetFigureList().IndexOf(figure);
                beforeState.Add(index, figure);
            }

            var cmd = new DeleteFigureCommand(_model, beforeState);
            _undoRedoStack.Do(cmd);
            handler.ClearSelectedFigures();
            _view.Canvas.Refresh();
        }

        /// <summary>
        /// Обработчик очистки канвы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _view_CanvasCleared(object sender, EventArgs e)
        {
            var beforeState = new Dictionary<int, BaseFigure>();
            foreach (var figure in _model.GetFigureList())
            {
                if (!_model.GetFigureList().Contains(figure)) continue;
                var index = _model.GetFigureList().IndexOf(figure);
                beforeState.Add(index, figure);
            }

            var cmd = new DeleteFigureCommand(_model, beforeState);
            _undoRedoStack.Do(cmd);
            _view.Canvas.Refresh();
        }

        /// <summary>
        /// Обработчик события изменения параметров фигур(ы)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _view_ParametersChanged(object sender, FigureParameters e)
        {
            var beforeState = new Dictionary<int, BaseFigure>();

            if (_currentHandler.GetType() == typeof(CursorHandler))
            {
                var handler = _currentHandler as CursorHandler;
                if (handler != null)
                    foreach (var figure in handler.SelectedFigures)
                    {
                        if (!_model.GetFigureList().Contains(figure)) continue;
                        var index = _model.GetFigureList().IndexOf(figure);
                        beforeState.Add(index, FigureFactory.CreateCopy(figure));
                    }
            }
            else
            {
                _currentHandler.FigureParameters = e;
            }

            var cmd = new ChangeParametersCommand(_model, beforeState, e);
            _undoRedoStack.Do(cmd);

            _view.Canvas.Invalidate();
        }

        /// <summary>
        /// Обработчик события выбора инструмента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _view_ToolPicked(object sender, Item e)
        {
            switch (e)
            {
                case Item.Line:
                    _currentHandler = new LineHandler(_view.Canvas, _view.FigureParameters);
                    _currentHandler.FigureCreated += _currentHandler_FigureCreated;
                    _view.CurrentHandler = (LineHandler)_currentHandler;
                    break;
                case Item.Polyline:
                    _currentHandler = new PolylineHandler(_view.Canvas, _view.FigureParameters);
                    _currentHandler.FigureCreated += _currentHandler_FigureCreated;
                    _view.CurrentHandler = (PolylineHandler)_currentHandler;
                    break;
                case Item.Circle:
                    _currentHandler = new CircleHandler(_view.Canvas, _view.FigureParameters);
                    _currentHandler.FigureCreated += _currentHandler_FigureCreated;
                    _view.CurrentHandler = (CircleHandler)_currentHandler;
                    break;
                case Item.Ellipse:
                    _currentHandler = new EllipseHandler(_view.Canvas, _view.FigureParameters);
                    _currentHandler.FigureCreated += _currentHandler_FigureCreated;
                    _view.CurrentHandler = (EllipseHandler)_currentHandler;
                    break;
                case Item.Polygon:
                    _currentHandler = new PolygonHandler(_view.Canvas, _view.FigureParameters);
                    _currentHandler.FigureCreated += _currentHandler_FigureCreated;
                    _view.CurrentHandler = (PolygonHandler)_currentHandler;
                    break;
                case Item.Cursor:
                    var cursorHandler = new CursorHandler(_view.Canvas, _view.FigureParameters, this);
                    cursorHandler.FiguresMoved += CursorHandler_FiguresMoved;
                    cursorHandler.PointMoved += CursorHandler_PointMoved;
                    _currentHandler = cursorHandler;
                    _view.CurrentHandler = (CursorHandler)_currentHandler;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(e), e, null);
            }
        }

        /// <summary>
        /// Обработчик события движения точкой
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CursorHandler_PointMoved(object sender, Dictionary<int, BaseFigure> e)
        {
            if (_currentHandler.GetType() != typeof(CursorHandler)) return;
            var handler = _currentHandler as CursorHandler;
            if (handler == null) return;
            var cmd = new MovePointCommand(_model, handler.BeforePointState, e);
            _undoRedoStack.Do(cmd);
        }

        /// <summary>
        /// Обработчик события движения фигур(ой)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CursorHandler_FiguresMoved(object sender, Dictionary<int, BaseFigure> e)
        {
            if (_currentHandler.GetType() != typeof(CursorHandler)) return;
            var handler = _currentHandler as CursorHandler;
            if (handler == null) return;
            var cmd = new MoveFigureCommand(_model, handler.BeforeState, e);
            _undoRedoStack.Do(cmd);
        }

        /// <summary>
        /// Позволяет исправить команды после чтения. 
        /// </summary>
        private void FixCommands()
        {
            int count = _undoRedoStack.UndoCount;
            for (int i = 0; i < count; i++)
            {
                _undoRedoStack.Undo();
            }
            for (int i = 0; i < count; i++)
            {
                _undoRedoStack.Redo();
            }
        }

        /// <summary>
        /// Обработчик события создания фигуры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _currentHandler_FigureCreated(object sender, BaseFigure e)
        {
            var index = _model.GetFigureList().Count;
            var cmd = new AddFigureCommand(_model, e, index);
            _undoRedoStack.Do(cmd);
        }

        
        /// <inheritdoc />
        /// <summary>
        /// Обновление состояния представления
        /// </summary>
        /// <param name="figures"></param>
        public void Update(List<BaseFigure> figures)
        {
            _figures = figures;
            _view.Figures = figures;
        }

        /// <summary>
        /// Получить список фигур
        /// </summary>
        /// <returns></returns>
        public List<BaseFigure> GetFigures()
        {
            return _figures;
        }
    }
}
