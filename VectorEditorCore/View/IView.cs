using System;
using System.Collections.Generic;

using SDK;

namespace VectorEditorCore.View
{
    /// <summary>
    /// Интерфейс, который содержит все 
    /// действия пользователя и 
    /// вспомогательные свойства
    /// </summary>
    public interface IView
    {
        #region Взаимодействия с файлами

        /// <summary>
        /// Событие открытия нового файла. 
        /// Аргумент - путь к файлу
        /// </summary>
        event EventHandler<string> FileOpened;

        /// <summary>
        /// Событие сохранения файла.
        /// Аргумент - путь к файлу
        /// </summary>
        event EventHandler<string> FileSaved;

        /// <summary>
        /// Событие создания нового файла
        /// </summary>
        event EventHandler NewFileCreated;

        /// <summary>
        /// Событие закрытия редактора
        /// </summary>
        event EventHandler EditorClosed;

        /// <summary>
        /// Событие сохранения 
        /// файла в расстровом формате.
        /// Аргумент - путь к файлу
        /// </summary>
        event EventHandler<string> FileSavedAsBitmap;

        #endregion

        #region Взаимодействия с фигурами

        /// <summary>
        /// Параметры 
        /// </summary>
        FigureParameters FigureParameters
        {
            get;
            set;
        }

        /// <summary>
        /// Список фигур
        /// </summary>
        Dictionary<int, BaseFigure> Figures
        {
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

        #endregion

        /// <summary>
        /// События выбора инструмента.
        /// Аргумент - название инструмента
        /// </summary>
        event EventHandler<string> ToolPicked;

        /// <summary>
        /// Обновление канвы
        /// </summary>
        Action CanvasRefresh
        {
            get;
        }

        /// <summary>
        /// Фабрика для создания фигур
        /// </summary>
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
    }
}
