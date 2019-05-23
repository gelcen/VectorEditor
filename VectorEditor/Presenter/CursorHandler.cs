﻿using SDK;
using System;
using System.Collections.Generic;
using System.Drawing;
using VectorEditor.Drawers;

namespace VectorEditor.Presenter
{
    public enum HandlingState
    {
        Selecting,
        Editing
    }

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
        /// 
        /// </summary>
        private Presenter _presenter;

        private CursorSelectingHandler _selectionHandler;

        private CursorEditingHandler _editingHandler;

        public Dictionary<int, BaseFigure> SelectedFigures { get; set; }

        public IDrawerFacade DrawerFacade { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="canvasRefresh">Делегат для обновления канвы</param>
        /// <param name="presenter">Презентер</param>
        /// <param name="handler">Обработчик</param>
        public CursorHandler(Action canvasRefresh, 
                             Presenter presenter,
                             IHandler handler,
                             IDrawerFacade drawerFacade)
        {
            DrawerFacade = drawerFacade;

            _state = HandlingState.Selecting;

            SelectedFigures = new Dictionary<int, BaseFigure>();

            _presenter = presenter;

            _handler = handler;
            _handler.CanvasRefresh = canvasRefresh;
            _handler.Draw += DrawHandler;
            SetState(_state);
        }

        private void DrawHandler(Graphics g)
        {
            if (SelectedFigures.Count != 0)
            {
                foreach (var figure in SelectedFigures)
                {
                    DrawerFacade.DrawSelection(figure.Value, g);
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
                    _selectionHandler = new CursorSelectingHandler(_presenter,
                                                            _handler,
                                                            this, 
                                                            new Selector());
                    break;
                case HandlingState.Editing:
                    _editingHandler = new CursorEditingHandler(_presenter,
                                                            _handler,
                                                            this,
                                                            new Selector());
                    break;
                default:
                    break;
            }
        }

        public Action<Dictionary<int, BaseFigure>,
                      Dictionary<int, BaseFigure>> FiguresMoved;

        public Action<Dictionary<int, BaseFigure>,
                      Dictionary<int, BaseFigure>> MarkerMoved;

        /// <summary>
        /// Очистить выборку фигур
        /// </summary>
        public void ClearSelectedFigures()
        {
            SelectedFigures.Clear();
            //_selectedFigure = null;
        }
    }
}
