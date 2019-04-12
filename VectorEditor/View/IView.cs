using System;
using System.Collections.Generic;
using System.Windows.Forms;
using VectorEditor.Figures;
using VectorEditor.Presenter;
using VectorEditor.UndoRedo;

namespace VectorEditor.FileManager
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
        Dictionary<int, BaseFigure> Figures
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
        /// Флаг изменения файла
        /// </summary>
        bool IsChanged
        {
            get;
            set;
        }

        /// <summary>
        /// Тип сохранения
        /// </summary>
        SaveState SaveType
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
        /// События нажатия на кнопку отмену команды
        /// </summary>
        event EventHandler UndoPressed;

        /// <summary>
        /// События нажатия на возврат команды
        /// </summary>
        event EventHandler RedoPressed;

        /// <summary>
        /// События выбора инструмента
        /// </summary>
        event EventHandler<ToolType> ToolPicked;

        /// <summary>
        /// События создания нового файла
        /// </summary>
        event EventHandler NewProjectCreated;

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
