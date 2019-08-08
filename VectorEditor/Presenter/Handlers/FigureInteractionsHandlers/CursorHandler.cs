using SDK;
using System;
using System.Collections.Generic;
using System.Drawing;
using VectorEditor.Drawers;
using VectorEditor.Models;

namespace VectorEditor.Presenter.Handlers.FigureInteractionsHandler
{
    /// <summary>
    /// Перечисление состояний
    /// обработчика курсора
    /// </summary>
    public enum HandlingState
    {
        Selecting,
        Editing
    }

    /// <summary>
    /// Класс для обработки 
    /// действий с курсором
    /// </summary>
    public class CursorHandler
    {
        /// <summary>
        /// Состояние обработчика курсора
        /// </summary>
        private HandlingState _state;

        /// <summary>
        /// Ссылка на обработчик
        /// </summary>
        private IHandler _handler;

        /// <summary>
        /// Ссылка на модель
        /// </summary>
        private IModel _model;

        /// <summary>
        /// Ссылка на обработчик
        /// выбора фигур
        /// </summary>
        private CursorSelectingHandler _selectionHandler;

        /// <summary>
        /// Ссылка на обработчик
        /// редактирования фигур
        /// </summary>
        private CursorEditingHandler _editingHandler;

        /// <summary>
        /// Выбранные фигуры
        /// </summary>
        public Dictionary<int, BaseFigure> SelectedFigures { get; set; }

        /// <summary>
        /// Ссылка на фасад рисования фигур
        /// </summary>
        private IDrawerFacade _drawerFacade;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="canvasRefresh">Делегат для обновления канвы</param>
        /// <param name="model">Презентер</param>
        /// <param name="handler">Обработчик</param>
        public CursorHandler(Action canvasRefresh, 
                             IModel model,
                             IHandler handler,
                             IDrawerFacade drawerFacade)
        {
            _drawerFacade = drawerFacade;

            _state = HandlingState.Selecting;

            SelectedFigures = new Dictionary<int, BaseFigure>();

            _model = model;

            _handler = handler;
            _handler.CanvasRefresh = canvasRefresh;
            _handler.Draw += DrawHandler;
            SetState(_state);
        }

        /// <summary>
        /// Рисовка маркеров выбранных фигур
        /// </summary>
        /// <param name="g">Поверхность рисования</param>
        private void DrawHandler(Graphics g)
        {
            if (SelectedFigures.Count != 0)
            {
                foreach (var figure in SelectedFigures)
                {
                    _drawerFacade.DrawSelection(figure.Value, g);
                }
            }
        }

        /// <summary>
        /// Установка текущего состояния
        /// </summary>
        /// <param name="state">Состояние для установки</param>
        public void SetState(HandlingState state)
        {
            _state = state;
            switch (state)
            {
                case HandlingState.Selecting:
                    _selectionHandler = new CursorSelectingHandler(_model,
                                                            _handler,
                                                            this, 
                                                            new Selector());
                    break;
                case HandlingState.Editing:
                    _editingHandler = new CursorEditingHandler(_model,
                                                            _handler,
                                                            this,
                                                            new Selector());
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Событие перемещения фигур
        /// </summary>
        public Action<Dictionary<int, BaseFigure>,
                      Dictionary<int, BaseFigure>> FiguresMoved;

        /// <summary>
        /// Событие перемещения маркера
        /// </summary>
        public Action<Dictionary<int, BaseFigure>,
                      Dictionary<int, BaseFigure>> MarkerMoved;

        /// <summary>
        /// Очистить список
        /// выбранных фигур
        /// </summary>
        public void ClearSelectedFigures()
        {
            SelectedFigures.Clear();
        }
    }
}
