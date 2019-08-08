using System;
using VectorEditor.FileManager;
using VectorEditor.Models;
using VectorEditor.Presenter.Handlers.FigureInteractionsHandler;
using VectorEditor.UndoRedo;

namespace VectorEditor.Presenter.Handlers
{
    /// <summary>
    /// Класс для обработки 
    /// событий нажатий на кнопки
    /// Undo, Redo
    /// </summary>
    public class UndoRedoActionsHandler
    {
        /// <summary>
        /// Ссылка на менеджер отмены и 
        /// возврата операций
        /// </summary>
        private UndoRedoManager _undoRedoManager;

        /// <summary>
        /// Ссылка на представление
        /// </summary>
        private IView _view;

        /// <summary>
        /// Ссылка на модель
        /// </summary>
        private readonly IModel _model;

        /// <summary>
        /// Ссылка на обработчик курсора
        /// </summary>
        private CursorHandler _cursorHandler;

        /// <summary>
        /// Конструктор класса для обработки 
        /// событий нажатий на кнопки
        /// Undo, Redo
        /// </summary>
        /// <param name="undoRedoMangaer">менеджер отмены и 
        /// возврата операций</param>
        /// <param name="view">Представление</param>
        /// <param name="model">Модель</param>
        /// <param name="cursorHandler">Обработчик курсора</param>
        public UndoRedoActionsHandler(UndoRedoManager undoRedoMangaer,
                                      IView view,
                                      IModel model,
                                      CursorHandler cursorHandler)
        {
            _undoRedoManager = undoRedoMangaer;
            _view = view;
            _model = model;
            _cursorHandler = cursorHandler;

            _view.UndoPressed += ViewUndoPressed;
            _view.RedoPressed += ViewRedoPressed;
        }

        /// <summary>
        /// Обработчик события возврата команды
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewRedoPressed(object sender, EventArgs e)
        {
            _undoRedoManager.Redo();
            _view.CanvasRefresh?.Invoke();
        }

        /// <summary>
        /// Обработчик события отмены команды
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewUndoPressed(object sender, EventArgs e)
        {
            _undoRedoManager.Undo();
            if (_model.GetFigureList().Count == 0)
            {
                _cursorHandler?.ClearSelectedFigures();
            }
            _view.CanvasRefresh?.Invoke();
        }
    }
}
