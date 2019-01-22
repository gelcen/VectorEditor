using System.Collections.Generic;
using VectorEditor.Figures;
using VectorEditor.Observer;
using VectorEditor.View;

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
        void AddFigure(BaseFigure figure);

        /// <summary>
        /// Изменение флага IsChanged
        /// </summary>
        void HasChanged();

        /// <summary>
        /// Копирование объекта
        /// </summary>
        /// <param name="figure"></param>
        void CopyFigure(BaseFigure figure);

        /// <summary>
        /// Вырезание объекта
        /// </summary>
        /// <param name="figure"></param>
        void CutFigure(BaseFigure figure);

        /// <summary>
        /// Удаление фигуры по индексу
        /// </summary>
        /// <param name="index">Индекс удаляемой фигуры</param>
        /// <param name="figure">Фигурa</param>
        void DeleteFigureAt(int index, BaseFigure figure);

        /// <summary>
        /// Удаление фигуры по ссылке
        /// </summary>
        /// <param name="figure"></param>
        void DeleteFigure(BaseFigure figure);

        /// <summary>
        /// Вставка фигуры
        /// </summary>
        /// <param name="index">Индекс вставки</param>
        /// <param name="figure">Вставляемая фигур</param>
        void InsertAt(int index, BaseFigure figure);

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
        /// Изменение фигуры
        /// </summary>
        /// <param name="index">Индекс фигуры</param>
        /// <param name="figure">Новая фигуры</param>
        void ChangeFigure(int index, BaseFigure figure);

        /// <summary>
        /// Сохранение проекта
        /// </summary>
        /// <param name="filename">Имя файла</param>
        void Save(string filename);

        /// <summary>
        /// Загрузка проекта
        /// </summary>
        void Load();

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
        List<BaseFigure> GetFigureList();       
    }
}
