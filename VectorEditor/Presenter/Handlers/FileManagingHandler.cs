using SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using VectorEditor.FileManager;
using VectorEditor.Model;
using VectorEditor.UndoRedo;

namespace VectorEditor.Presenter.Handlers
{
    public class FileManagingHandler
    {
        private readonly IFileManager _fileManager;

        private readonly Model.IModel _model;

        private readonly UndoRedoManager _undoRedoManager;

        private readonly IView _view;


        public FileManagingHandler(IFileManager fileManager,
                                   Model.IModel model,
                                   UndoRedoManager undoRedoManager,
                                   IView view)
        {
            _fileManager = fileManager;
            _model = model;
            _undoRedoManager = undoRedoManager;
            _view = view;

            _view.FileOpened += ViewFileOpened;
            _view.FileSaved += ViewFileSaved;
            _view.NewProjectCreated += ViewNewProjectCreated;
        }

        /// <summary>
        /// Создание нового файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewNewProjectCreated(object sender, EventArgs e)
        {
            _model.ClearCanvas();
            _undoRedoManager.Reset();
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
            for (int i = 0; i < commands.Count; i++)
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
            _view.CanvasRefresh?.Invoke();
        }
    }
}
