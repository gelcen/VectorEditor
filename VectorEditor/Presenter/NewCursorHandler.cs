using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VectorEditor.Drawers;
using VectorEditor.Figures;

namespace VectorEditor.Presenter
{
    public enum HandlingState
    {
        Selecting,
        Editing
    }

    public class NewCursorHandler
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

        private SelectionHandler _selectionHandler;

        private EditingHandler _editingHandler;

        public Dictionary<int, BaseFigure> SelectedFigures { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="canvasRefresh">Делегат для обновления канвы</param>
        /// <param name="presenter">Презентер</param>
        /// <param name="handler">Обработчик</param>
        public NewCursorHandler(Action canvasRefresh, 
                                Presenter presenter,
                                IHandler handler)
        {
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
                    FigureDrawer.DrawSelection(figure.Value, g);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        public void SetState(HandlingState state)
        {
            switch (state)
            {
                case HandlingState.Selecting:
                    _selectionHandler = new SelectionHandler(_presenter,
                                                            _handler,
                                                            this);
                    break;
                case HandlingState.Editing:
                    _editingHandler = new EditingHandler(_presenter,
                                                            _handler,
                                                            this);
                    break;
                default:
                    break;
            }
        } 
    }
}
