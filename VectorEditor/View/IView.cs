using System;
using System.Windows.Forms;
using VectorEditor.Presenter;
using VectorEditor.UndoRedo;
using VectorEditor.View;

namespace VectorEditor.Model
{
    /// <summary>
    /// Интерфейс для Представления
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Канва
        /// </summary>
        PictureBox Canvas
        {
            get;
        }

        UndoRedoStack CommandStack
        {
            set;
        }

        /// <summary>
        /// Параметры
        /// </summary>
        FigureParameters FigureParameters
        {
            get;
            set;
        }

        event EventHandler<FigureParameters> ParametersChanged;

        event EventHandler CanvasCleared;

        event EventHandler FiguresDeleted;

        event EventHandler FigureCopied;

        event EventHandler UndoPressed;

        event EventHandler RedoPressed;

        event EventHandler<Item> ToolPicked;

        IBaseHandler CurrentHandler
        {
            get;
            set;
        }              
    }
}
