using System;
using System.Collections.Generic;
using VectorEditor.UndoRedo;
using VectorEditor.Figures;

namespace VectorEditor.FileManager
{
    /// <inheritdoc />
    /// <summary>
    /// Класс для хранения данных события открытия файла
    /// </summary>
    public class FileLoadedEventArgs:EventArgs
    {
        /// <inheritdoc />
        /// <summary>
        /// Конструктор класса для аргументов
        /// события открытия файла.
        /// </summary>
        public FileLoadedEventArgs()
        {
            Figures = new List<BaseFigure>();
            UndoStack = new Stack<ICommand>();
            RedoStack = new Stack<ICommand>();
            RedoList = new List<ICommand>();
        }

        /// <summary>
        /// Список операций
        /// </summary>
        public List<ICommand> RedoList
        {
            get;
            set;
        }

        /// <summary>
        /// Количество Undo 
        /// </summary>
        public int UndoCount
        {
            get;
            set;
        }

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
