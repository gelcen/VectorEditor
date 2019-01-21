using System;
using System.Collections.Generic;
using VectorEditor.UndoRedo;
using VectorEditor.Figures;

namespace VectorEditor.View
{
    /// <summary>
    /// Класс для хранения данных события открытия файла
    /// </summary>
    public class FileLoadedEventArgs:EventArgs
    {
        /// <summary>
        /// Фигуры
        /// </summary>
        public List<BaseFigure> Figures
        {
            get;
            set;
        }

        /// <summary>
        /// Стек Undo
        /// </summary>
        public Stack<ICommand> UndoStack
        {
            get;
            set;
        }

        /// <summary>
        /// Стек Redo
        /// </summary>
        public Stack<ICommand> RedoStack
        {
            get;
            set;
        }
    }
}
