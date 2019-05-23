using System;
using VectorEditor.FileManager;
using VectorEditor.Model;
using VectorEditor.Presenter.Handlers.FigureInteractionsHandler;
using VectorEditor.UndoRedo;

namespace VectorEditor.Presenter.Handlers
{
    public class UndoRedoActionsHandler
    {
        private UndoRedoManager _undoRedoManager;

        private IView _view;

        private readonly IModel _model;
        private CursorHandler _cursorHandler;

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
