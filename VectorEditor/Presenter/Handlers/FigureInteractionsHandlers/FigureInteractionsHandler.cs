using SDK;
using System;
using System.Collections.Generic;
using VectorEditor.Drawers;
using VectorEditor.FileManager;
using VectorEditor.Model;
using VectorEditor.Presenter.Handlers.FigureInteractionsHandler;
using VectorEditor.UndoRedo;

namespace VectorEditor.Presenter.Handlers.FigureInteractionsHandlers
{
    public class FigureInteractionsHandler
    {
        private readonly IView _view;

        private readonly IModel _model;

        private readonly IFileManager _fileManager;

        private readonly IFactory<BaseFigure> _figureFactory;

        private readonly IDrawerFacade _drawerFacade;

        private CursorHandler _cursorHandler;

        public CursorHandler CursorHandler
        {
            get => _cursorHandler;
        }

        private FigureCreatingHandler _figureCreatingHandler;

        private UndoRedoManager _undoRedoManager;

        public FigureInteractionsHandler(IView view,
                                         IModel model, 
                                         IFileManager fileManager,
                                         IFactory<BaseFigure> figureFactory, 
                                         IDrawerFacade drawerFacade,
                                         UndoRedoManager undoRedoManager)
        {
            _view = view;
            _model = model;
            _fileManager = fileManager;
            _figureFactory = figureFactory;
            _drawerFacade = drawerFacade;
            _undoRedoManager = undoRedoManager;

            _view.ToolPicked += ViewToolPicked;
            _view.ParametersChanged += ViewParametersChanged;
            _view.FigureCopied += ViewFigureCopied;
            _view.FiguresDeleted += ViewFiguresDeleted;
            _view.CanvasCleared += ViewCanvasCleared;
        }

        /// <summary>
        /// Обработчик события выбора инструмента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewToolPicked(object sender, string name)
        {
            _view.CurrentHandler = new BaseHandler();

            if (name == "Cursor")
            {
                _cursorHandler = new CursorHandler(_view.CanvasRefresh,
                                                   _model,
                                                   _view.CurrentHandler,
                                                   _drawerFacade);
                _cursorHandler.FiguresMoved += CursorHandlerFiguresMoved;
                _cursorHandler.MarkerMoved += CursorHandlerMarkerMoved;
            }
            else SetHandler(name);
        }

        private void SetHandler(string figureName)
        {
            _figureCreatingHandler = new FigureCreatingHandler(_view.CanvasRefresh,
                                                    _view.FigureParameters,
                                                    _view.CurrentHandler,
                                                    _figureFactory,
                                                    _drawerFacade)
            {
                CurrentFigure = figureName
            };
            _figureCreatingHandler.FigureCreated += _currentHandler_FigureCreated;
        }

        /// <summary>
        /// Обработчик события движения точкой
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CursorHandlerMarkerMoved(Dictionary<int, BaseFigure> oldState,
                                               Dictionary<int, BaseFigure> newState)
        {
            var cmd = new MovePointCommand(_model, oldState, newState);
            _undoRedoManager.Do(cmd);
        }

        /// <summary>
        /// Обработчик события движения фигур(ой)
        /// </summary>
        /// <param name="oldState">Состояние до</param>
        /// <param name="newState">Состояние после</param>
        private void CursorHandlerFiguresMoved(Dictionary<int, BaseFigure> oldState,
                                               Dictionary<int, BaseFigure> newState)
        {
            var cmd = new MoveFigureCommand(_model, oldState, newState);
            _undoRedoManager.Do(cmd);
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
            _undoRedoManager.Do(cmd);
        }

        /// <summary>
        /// Обработчик события изменения параметров фигур(ы)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewParametersChanged(object sender, FigureParameters e)
        {
            if (_cursorHandler.SelectedFigures.Count == 0)
            {
                if (_figureCreatingHandler != null)
                {
                    _figureCreatingHandler.FigureParameters = e;
                }
                return;
            }

            var beforeState = new Dictionary<int, BaseFigure>();

            foreach (var figure in _cursorHandler.SelectedFigures)
            {
                if (!_model.GetFigureList().ContainsKey(figure.Key)) continue;
                var index = figure.Key;
                beforeState.Add(index, (BaseFigure)figure.Value.Clone());
            }

            var cmd = new ChangeParametersCommand(_model, beforeState, e);
            _undoRedoManager.Do(cmd);
            _view.CanvasRefresh?.Invoke();
        }

        /// <summary>
        /// Обработчик события копирования фигур(ы)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewFigureCopied(object sender, EventArgs e)
        {
            foreach (var figure in _cursorHandler.SelectedFigures)
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
        private void ViewFiguresDeleted(object sender, EventArgs e)
        {
            var beforeState = new Dictionary<int, BaseFigure>();

            foreach (var figure in _cursorHandler.SelectedFigures)
            {
                if (!_model.GetFigureList().ContainsKey(figure.Key)) continue;
                var index = figure.Key;
                beforeState.Add(index, figure.Value);
            }

            var cmd = new DeleteFigureCommand(_model, beforeState);
            _undoRedoManager.Do(cmd);
            _cursorHandler.ClearSelectedFigures();
            _view.CanvasRefresh();
        }

        /// <summary>
        /// Обработчик очистки канвы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewCanvasCleared(object sender, EventArgs e)
        {
            var beforeState = new Dictionary<int, BaseFigure>();
            foreach (var figure in _model.GetFigureList())
            {
                if (!_model.GetFigureList().ContainsKey(figure.Key)) continue;
                var index = figure.Key;
                beforeState.Add(index, figure.Value);
            }

            var cmd = new DeleteFigureCommand(_model, beforeState);
            _undoRedoManager.Do(cmd);
            _view.CanvasRefresh?.Invoke();
        }
    }
}
