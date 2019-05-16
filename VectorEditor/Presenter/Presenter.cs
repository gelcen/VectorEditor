﻿using System;
using System.Collections.Generic;
using VectorEditor.Figures;
using VectorEditor.Model;
using VectorEditor.Observer;
using VectorEditor.UndoRedo;
using VectorEditor.FileManager;
using System.Linq;
using SDK;
using VectorEditor.Drawers;

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
        /// Файловый менеджер
        /// </summary>
        private readonly IFileManager _fileManager;

        /// <summary>
        /// Обработчик курсора
        /// </summary>
        private CursorHandler _cursorHandler;

        /// <summary>
        /// Список фигур
        /// </summary>
        private Dictionary<int, BaseFigure> _figures;

        /// <summary>
        /// Менеджер Undo Redo
        /// </summary>
        private readonly UndoRedoManager _undoRedoManager;

        /// <summary>
        /// Тип сохранения 
        /// </summary>
        private SaveState _saveState;

        public IDrawerFacade DrawerFacade
        {
            get;
            set;
        }

        /// <summary>
        /// Конструктор представителя
        /// </summary>
        /// <param name="view">Представление</param>
        /// <param name="model">Модель</param>
        /// <param name="fileManager">File Manager</param>
        public Presenter(IView view, IModel model, IFileManager fileManager, 
                         IFactory<BaseFigure> figureFactory, IFactory<BaseDrawer> drawerFactory)
        {
            _undoRedoManager = new UndoRedoManager();

            _view = view;
            _model = model;
            _fileManager = fileManager;            

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
            _view.FileSaved += ViewFileSaved;
            _view.FileOpened += ViewFileOpened;
            _view.NewProjectCreated += _view_NewProjectCreated;                

            _model.RegisterObserver(this);
        }

        /// <summary>
        /// Обработчик события сохранения проекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="filename"></param>
        private void ViewFileSaved(object sender, string filename)
        {

            var redoList = _undoRedoManager.RedoStack.ToList();
            var undoList = _undoRedoManager.UndoStack.ToList();
            redoList.AddRange(undoList);

            _fileManager.SaveToFile(filename,
                                    _model.GetFigureList(),
                                    redoList,
                                    _undoRedoManager.RedoCount);
        }

        /// <summary>
        /// Обработчик открытия проекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="filename"></param>
        private void ViewFileOpened(object sender, string filename)
        {
            _model.NewProject();
            Dictionary<int, BaseFigure> figures =
                _fileManager.OpenFromFile(filename,
                                          out List<ICommand> commands,
                                          out int redoCount);
            foreach (var figure in figures)
            {
                _model.AddFigure(figure.Key, figure.Value);
            }
            foreach (var command in commands)
            {
                CommandFactory.RestorePointersToModel(command, _model);
            }
            commands.Reverse();
            _undoRedoManager.Reset();
            for (int i=0; i < commands.Count; i++)
            {
                if (i < commands.Count - redoCount)
                {
                    _undoRedoManager.UndoStack.Push(commands[i]);
                }
                else
                {
                    _undoRedoManager.RedoStack.Push(commands[i]);
                }                
            }
            _view.CanvasRefresh();
        }

        /// <summary>
        /// Создание нового файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _view_NewProjectCreated(object sender, EventArgs e)
        {
            _model.ClearCanvas();
            _undoRedoManager.Reset();
        }

        /// <summary>
        /// Обработчик события возврата команды
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _view_RedoPressed(object sender, EventArgs e)
        {
            _undoRedoManager.Redo();
            _view.CanvasRefresh();
        }

        /// <summary>
        /// Обработчик события отмены команды
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _view_UndoPressed(object sender, EventArgs e)
        {
            _undoRedoManager.Undo();
            if (_model.GetFigureList().Count == 0)
            {
                _cursorHandler?.ClearSelectedFigures();
            }
            _view.CanvasRefresh();
        }

        /// <summary>
        /// Обработчик события копирования фигур(ы)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _view_FigureCopied(object sender, EventArgs e)
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
        private void _view_FiguresDeleted(object sender, EventArgs e)
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
            _undoRedoManager.Do(cmd);
            _view.CanvasRefresh?.Invoke();
        }

        /// <summary>
        /// Обработчик события изменения параметров фигур(ы)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _view_ParametersChanged(object sender, FigureParameters e)
        {
            if (_cursorHandler.SelectedFigures.Count == 0) return;

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
        /// Обработчик события выбора инструмента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _view_ToolPicked(object sender, string name)
        {
            _view.CurrentHandler = new BaseHandler();

            if (name == "Cursor")
            {
                _cursorHandler = new CursorHandler(_view.CanvasRefresh, 
                                                   this,
                                                   _view.CurrentHandler,
                                                   DrawerFacade);
                _cursorHandler.FiguresMoved += CursorHandlerFiguresMoved;
                _cursorHandler.MarkerMoved += CursorHandlerMarkerMoved;
            }
            else SetHandler(name);            
        }

        private void SetHandler(string figureName)
        {
            var handler = new FigureCreatingHandler(_view.CanvasRefresh, 
                                                    _view.FigureParameters, 
                                                    _view.CurrentHandler,
                                                    DrawerFacade)
            {
                CurrentFigure = figureName
            };           
            handler.FigureCreated += _currentHandler_FigureCreated;
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
