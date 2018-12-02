using System.Collections.Generic;
using VectorEditor.Figures;

namespace VectorEditor.Model
{
    /// <summary>
    /// Интерфейс для взаимодействия модели и представления
    /// </summary>
    public interface IModel: ISubject
    {
        /// <summary>
        /// Добавление фигуры
        /// </summary>
        void AddFigure(Figure figure);

        /// <summary>
        /// Изменение флага IsChanged
        /// </summary>
        void HasChanged();

        void CopyFigure(Figure figure);

        void CutFigure(Figure figure);

        void DeleteFigure(Figure figure);

        void ChangeFigureSize(Figure figure);

        void MoveFigure(Figure figure);

        void Save(string filename);

        void Load();

        void NewProject();

        void ClearCanvas();

        /// <summary>
        /// Получение списка фигур
        /// </summary>
        /// <returns></returns>
        List<Figure> getFigureList();

        //void registerObserver(Observer observer);

        //void removeObserver(IObserver observer);        
    }
}
