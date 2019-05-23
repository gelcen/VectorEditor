using SDK;
using System.Collections.Generic;
using System.Windows.Forms;
using VectorEditor.Drawers;
using VectorEditor.FileManager;
using VectorEditor.Observer;
using VectorEditor.Presenter.Handlers;
using VectorEditor.Presenter.Handlers.FigureInteractionsHandlers;
using VectorEditor.UndoRedo;

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
        private readonly Model.IModel _model;

        /// <summary>
        /// Файловый менеджер
        /// </summary>
        private readonly IFileManager _fileManager;

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

        private FileManagingHandler _fileManagingHandler;

        private UndoRedoActionsHandler _undoRedoActionsHandler;

        private FigureInteractionsHandler _figureInteractionsHandler;


        /// <summary>
        /// Конструктор представителя
        /// </summary>
        /// <param name="view">Представление</param>
        /// <param name="model">Модель</param>
        /// <param name="fileManager">File Manager</param>
        public Presenter(IView view, Model.IModel model, IFileManager fileManager, 
                         IFactory<BaseFigure> figureFactory, IDrawerFacade drawerFacade)
        {
            _undoRedoManager = new UndoRedoManager();

            _view = view;
            _model = model;
            _fileManager = fileManager;

            _fileManagingHandler = new FileManagingHandler(
                                   fileManager,
                                   model,
                                   _undoRedoManager,
                                   view);

            _figureInteractionsHandler = new FigureInteractionsHandler(
                                    view,
                                    model,
                                    fileManager,
                                    figureFactory,
                                    drawerFacade,
                                    _undoRedoManager);

            _undoRedoActionsHandler = new UndoRedoActionsHandler(
                                    _undoRedoManager,
                                    view,
                                    model,
                                    _figureInteractionsHandler.CursorHandler);

            _model.NewProject();
            _saveState = SaveState.NewFile;
            _view.SaveType = _saveState;                         

            _model.RegisterObserver(this);
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
        /// Запуск приложения
        /// </summary>
        public void Run()
        {
            Application.Run((Form)_view);
        }
    }
}
