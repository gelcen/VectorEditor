﻿using System;
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
        private Dictionary<int, BaseFigure> _figures;

        /// <summary>
        /// Менеджер Undo Redo
        /// </summary>
        private readonly UndoRedoStack _undoRedoStack;

        /// <summary>
        /// Тип сохранения 
        /// </summary>
        private SaveState _saveState;

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
            _saveState = SaveState.NewFile;
            _view.SaveType = _saveState;

            _view.ToolPicked += _view_ToolPicked;
            _view.ParametersChanged += _view_ParametersChanged;
            _view.CanvasCleared += _view_CanvasCleared;
            _view.FiguresDeleted += _view_FiguresDeleted;
            _view.FigureCopied += _view_FigureCopied;
            _view.UndoPressed += _view_UndoPressed;
            _view.RedoPressed += _view_RedoPressed;
            _view.CommandStack = _undoRedoStack;
            _view.FileLoaded += _view_FileLoaded;
            _view.NewProjectCreated += _view_NewProjectCreated;                

            _model.RegisterObserver(this);
        }

        /// <summary>
        /// Создание нового файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _view_NewProjectCreated(object sender, EventArgs e)
        {
            _model.ClearCanvas();
            _undoRedoStack.Reset();
        }

        /// <summary>
        /// Обработчик события загрузки проекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _view_FileLoaded(object sender, FileLoadedEventArgs e)
        {
            _model.ClearCanvas();
            foreach (var command in e.RedoList)
            {
                CommandFactory.RestorePointersToModel(command, _model);
            }
            _undoRedoStack.Reset();
            _saveState = SaveState.OpenedFile;
            FixCommands(e.UndoCount, e.RedoList);
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
                if (_model.GetFigureList().ContainsKey(figure.Key))
                {
                    _model.CopyFigure(figure.Key, figure.Value);
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
                if (!_model.GetFigureList().ContainsKey(figure.Key)) continue;
                var index = figure.Key;
                beforeState.Add(index, figure.Value);
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
                if (!_model.GetFigureList().ContainsKey(figure.Key)) continue;
                var index = figure.Key;
                beforeState.Add(index, figure.Value);
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
                        if (!_model.GetFigureList().ContainsKey(figure.Key)) continue;
                        var index = figure.Key;
                        beforeState.Add(index, FigureFactory.CreateCopy(figure.Value));
                    }
            }
            else
            {
                var handler = _currentHandler as FigureCreatingHandler;
                if (handler != null)
                {
                    handler.FigureParameters = e;
                }
                //_currentHandler.FigureParameters = e;
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
        private void _view_ToolPicked(object sender, ToolType e)
        {
            switch (e)
            {
                case ToolType.Line:
                    _currentHandler = new LineHandler(_view.Canvas, _view.FigureParameters);
                    SetFigureCreatedHandler(_currentHandler);
                    _view.CurrentHandler = _currentHandler;
                    break;
                case ToolType.Polyline:
                    _currentHandler = new PolylineHandler(_view.Canvas, _view.FigureParameters);
                    SetFigureCreatedHandler(_currentHandler);
                    _view.CurrentHandler = _currentHandler;
                    break;
                case ToolType.Circle:
                    _currentHandler = new CircleHandler(_view.Canvas, _view.FigureParameters);
                    SetFigureCreatedHandler(_currentHandler);
                    _view.CurrentHandler = _currentHandler;
                    break;
                case ToolType.Ellipse:
                    _currentHandler = new EllipseHandler(_view.Canvas, _view.FigureParameters);
                    SetFigureCreatedHandler(_currentHandler);
                    _view.CurrentHandler = _currentHandler;
                    break;
                case ToolType.Polygon:
                    _currentHandler = new PolygonHandler(_view.Canvas, _view.FigureParameters);
                    SetFigureCreatedHandler(_currentHandler);
                    _view.CurrentHandler = _currentHandler;
                    break;
                case ToolType.Cursor:
                    var cursorHandler = new CursorHandler(_view.Canvas,  this);
                    cursorHandler.FiguresMoved += CursorHandler_FiguresMoved;
                    cursorHandler.PointMoved += CursorHandler_PointMoved;
                    _currentHandler = cursorHandler;
                    _view.CurrentHandler = _currentHandler;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(e), e, null);
            }
        }

        /// <summary>
        /// Подписка обработчика события создания фигуры
        /// </summary>
        /// <param name="currentHandler"></param>
        private void SetFigureCreatedHandler(IBaseHandler currentHandler)
        {
            var handler = currentHandler as FigureCreatingHandler;
            if (handler != null)
            {
                handler.FigureCreated += _currentHandler_FigureCreated;
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
        private void FixCommands(int undoCount, List<ICommand> redoList)
        {
            foreach (var command in redoList)
            {
                _undoRedoStack.Do(command);
            }
            for (int i = 0; i < undoCount; i++)
            {
                _undoRedoStack.Undo();
            }
        }

        /// <summary>
        /// Обработчик события создания фигуры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _currentHandler_FigureCreated(object sender, BaseFigure e)
        {
            _model.CurrentIndex += 1;
            var index = _model.CurrentIndex;
            var cmd = new AddFigureCommand(_model, e, index);
            _undoRedoStack.Do(cmd);
        }

        
        /// <inheritdoc />
        /// <summary>
        /// Обновление состояния представления
        /// </summary>
        /// <param name="figures"></param>
        public void Update(Dictionary<int, BaseFigure> figures)
        {
            _figures = figures;
            _view.Figures = figures;
            _view.IsChanged = _model.IsChanged;
        }

        /// <summary>
        /// Получить список фигур
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, BaseFigure> GetFigures()
        {
            return _figures;
        }
    }
}
