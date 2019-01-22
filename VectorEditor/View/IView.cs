using System;
using System.Collections.Generic;
using System.Windows.Forms;
using VectorEditor.Figures;
using VectorEditor.Presenter;
using VectorEditor.UndoRedo;

namespace VectorEditor.View
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

        /// <summary>
        /// Список фигур
        /// </summary>
        List<BaseFigure> Figures
        {
            set;
        }

        /// <summary>
        /// Стек команд
        /// </summary>
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

        /// <summary>
        /// Событие загрузки файла
        /// </summary>
        event EventHandler<FileLoadedEventArgs> FileLoaded;

        /// <summary>
        /// Событие изменения параметров фигуры
        /// </summary>
        event EventHandler<FigureParameters> ParametersChanged;

        /// <summary>
        /// Событие очистки канвы
        /// </summary>
        event EventHandler CanvasCleared;

        /// <summary>
        /// События удаления фигур(ы)
        /// </summary>
        event EventHandler FiguresDeleted;

        /// <summary>
        /// События копирования фигур(ы)
        /// </summary>
        event EventHandler FigureCopied;

        /// <summary>
        /// События нажатия на отмену команды
        /// </summary>
        event EventHandler UndoPressed;

        /// <summary>
        /// События нажатия на возврат команды
        /// </summary>
        event EventHandler RedoPressed;

        /// <summary>
        /// События выбора инструмента
        /// </summary>
        event EventHandler<Item> ToolPicked;

        /// <summary>
        /// Свойство для текущего инструмента
        /// </summary>
        IBaseHandler CurrentHandler
        {
            get;
            set;
        }              
    }
}
