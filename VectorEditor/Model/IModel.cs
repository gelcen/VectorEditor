﻿using System.Collections.Generic;
using VectorEditor.Figures;
using VectorEditor.View;

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
        void AddFigure(BaseFigure figure);

        /// <summary>
        /// Изменение флага IsChanged
        /// </summary>
        void HasChanged();

        void CopyFigure(BaseFigure figure);

        void CutFigure(BaseFigure figure);

        void DeleteFigure(BaseFigure figure);

        void ChangeFigureParameters(int index, FigureParameters parameters);

        void MoveFigure(int index, BaseFigure figure);

        void Save(string filename);

        void Load();

        void NewProject();

        void ClearCanvas();

        /// <summary>
        /// Получение списка фигур
        /// </summary>
        /// <returns></returns>
        List<BaseFigure> getFigureList();

        //void registerObserver(Observer observer);

        //void removeObserver(IObserver observer);        
    }
}
