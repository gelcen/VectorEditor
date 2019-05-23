using SDK;
using System;
using System.Collections.Generic;
using VectorEditor.Drawers;
using VectorEditor.Presenter;

namespace VectorEditor.FileManager
{
    /// <summary>
    /// Интерфейс для Представления
    /// </summary>
    public interface IView
    {
        IFactory<BaseFigure> FigureFactory
        {
            set;
            get;
        }

        IDrawerFacade DrawerFacade
        {
            get;
            set;
        }

        /// <summary>
        /// Обновление канвы
        /// </summary>
        Action CanvasRefresh
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
        event EventHandler<string> ToolPicked;

        /// <summary>
        /// Событие открытия файла проекта
        /// </summary>
        event EventHandler<string> FileOpened;

        /// <summary>
        /// Событие сохранения проекта
        /// </summary>
        event EventHandler<string> FileSaved;

        /// <summary>
        /// События создания нового файла
        /// </summary>
        event EventHandler NewProjectCreated;

        /// <summary>
        /// Свойство для текущего инструмента
        /// </summary>
        IHandler CurrentHandler
        {
            get;
            set;
        }              
    }
}
