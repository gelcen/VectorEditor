using SDK;
using System.Collections.Generic;
using VectorEditor.FileManager;
using VectorEditor.Observer;

namespace VectorEditor.Model
{
    /// <inheritdoc />
    /// <summary>
    /// Интерфейс для взаимодействия модели и представления
    /// </summary>
    public interface IModel: ISubject
    {
        /// <summary>
        /// Добавление фигуры
        /// </summary>
        void AddFigure(int index, BaseFigure figure);

        /// <summary>
        /// Текущий индекс
        /// </summary>
        int CurrentIndex { get; set; }

        /// <summary>
        /// Флаг IsChanged
        /// </summary>
        bool IsChanged { get; }

        /// <summary>
        /// Копирование объекта
        /// </summary>
        /// <param name="index">Индекс</param>
        /// <param name="figure">Фигурa</param>
        void CopyFigure(int index, BaseFigure figure);

        /// <summary>
        /// Удаление фигуры по индексу
        /// </summary>
        /// <param name="index">Индекс удаляемой фигуры</param>
        /// <param name="figure">Фигурa</param>
        void DeleteFigureAt(int index, BaseFigure figure);

        /// <summary>
        /// Изменения параметров фигуры
        /// </summary>
        /// <param name="index">Индекс фигуры, у которой менятся параметры</param>
        /// <param name="newParameters">Новые параметрвы</param>
        void ChangeFigureParameters(int index, FigureParameters newParameters);

        /// <summary>
        /// Перемещение объекта
        /// </summary>
        /// <param name="index">Индекс</param>
        /// <param name="figure">Фигура с новыми точками</param>
        void MoveFigure(int index, BaseFigure figure);

        /// <summary>
        ///  Создание нового проекта
        /// </summary>
        void NewProject();

        /// <summary>
        /// Очистка канвы
        /// </summary>
        void ClearCanvas();

        /// <summary>
        /// Получение списка фигур
        /// </summary>
        /// <returns></returns>
        Dictionary<int, BaseFigure> GetFigureList();       
    }
}
