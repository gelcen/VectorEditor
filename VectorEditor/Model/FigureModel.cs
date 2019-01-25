﻿using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using VectorEditor.Figures;
using VectorEditor.Observer;
using VectorEditor.View;

namespace VectorEditor.Model
{
    /// <inheritdoc />
    /// <summary>
    /// Класс для модели
    /// </summary>
    public class FigureModel:IModel
    {
        /// <summary>
        /// Список подписчиков
        /// </summary>
        private readonly List<IObserver> _observers;

        /// <summary>
        /// Текущий индекс
        /// </summary>
        private int _currentIndex;

        public int CurrentIndex
        {
            get { return _currentIndex; }
            set { _currentIndex = value; }
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public FigureModel()
        {
            _observers = new List<IObserver>();
        }

        /// <summary>
        /// Список фигур
        /// </summary>
        private Dictionary<int, BaseFigure> _figures;

        /// <summary>
        /// Свойство для флага изменения проекта
        /// </summary>
        public bool IsChanged { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Добавление фигуры в список фигур
        /// </summary>
        /// <param name="index">Индекс фигуры</param>
        /// <param name="figure">Добавляемая фигура</param>
        public void AddFigure(int index, BaseFigure figure)
        {
            _figures.Add(index, figure);
            HasChanged();
            NotifyObservers();
        }

        /// <inheritdoc />
        /// <summary>
        /// Есть изменение
        /// </summary>
        public void HasChanged()
        {
            IsChanged = true;
        }

        /// <inheritdoc />
        /// <summary>
        /// Копирование объекта
        /// </summary>
        /// <param name="index">Индек фигуры</param>
        /// <param name="figure">Копируемая фигура</param>
        public void CopyFigure(int index, BaseFigure figure)
        {
            var addingFigure = FigureFactory.CreateCopy(figure, FigureFactory.CopyType.CopyWithOffset);
            CurrentIndex += 1;
            _figures.Add(CurrentIndex, addingFigure);
            NotifyObservers();      
        }

        /// <inheritdoc />
        /// <summary>
        /// Вырезание объекта
        /// </summary>
        /// <param name="figure"></param>
        public void CutFigure(BaseFigure figure)
        {

        }

        /// <inheritdoc />
        /// <summary>
        /// Удаление фигуры по индексу
        /// </summary>
        /// <param name="index">Индекс удаляемой фигуры</param>
        /// <param name="figure">Фигурa</param>
        public void DeleteFigureAt(int index, BaseFigure figure)
        {
            _figures.Remove(index);
            NotifyObservers();
        }

        /// <inheritdoc />
        /// <summary>
        /// Изменения параметров фигуры
        /// </summary>
        /// <param name="index">Индекс фигуры, у которой менятся параметры</param>
        /// <param name="newParameters">Новые параметрвы</param>
        public void ChangeFigureParameters(int index, FigureParameters newParameters)
        {
            var figure = _figures[index];
            figure.LineProperties.Color = newParameters.LineColor;
            figure.LineProperties.Style = (DashStyle)newParameters.LineStyle;
            figure.LineProperties.Thickness = newParameters.LineThickness;
            if (figure.GetType() == typeof(Circle) ||
                figure.GetType() == typeof(Ellipse) ||
                figure.GetType() == typeof(Polygon))
            {
                var tempFigure = figure as FillableFigure;
                if (tempFigure != null) tempFigure.FillProperty.FillColor = newParameters.FillColor;
            }
            NotifyObservers();
        }

        /// <inheritdoc />
        /// <summary>
        /// Изменение фигуры
        /// </summary>
        /// <param name="index">Индекс фигуры</param>
        /// <param name="newFigure">Новая фигуры</param>
        public void ChangeFigure(int index, BaseFigure newFigure)
        {
            var figure = _figures[index];
            figure.LineProperties.Color = newFigure.LineProperties.Color;
            figure.LineProperties.Style = newFigure.LineProperties.Style;
            figure.LineProperties.Thickness = newFigure.LineProperties.Thickness;
            if (newFigure.GetType() == typeof(Circle) ||
                newFigure.GetType() == typeof(Ellipse) ||
                newFigure.GetType() == typeof(Polygon))
            {
                var tempFigure = newFigure as FillableFigure;
                var changedFigure = figure as FillableFigure;
                if (changedFigure != null)
                {
                    if (tempFigure != null) changedFigure.FillProperty.FillColor = tempFigure.FillProperty.FillColor;
                    figure = changedFigure;
                }
            }
            var count = figure.Points.GetPoints().Count;
            for (var i = 0; i < count; i++)
            {
                figure.Points.Replace(i, new PointF(newFigure.Points.GetPoints()[i].X,
                                                    newFigure.Points.GetPoints()[i].Y));
            }
            NotifyObservers();
        }

        /// <inheritdoc />
        /// <summary>
        /// Перемещение объекта
        /// </summary>
        /// <param name="index">Индекс</param>
        /// <param name="figure">Фигура с новыми точками</param>
        public void MoveFigure(int index, BaseFigure figure)
        {
            var count = _figures[index].Points.GetPoints().Count;
            for (var i = 0; i < count; i++)
            {
                _figures[index].Points.Replace(i, new PointF(figure.Points.GetPoints()[i].X,
                                                             figure.Points.GetPoints()[i].Y));
            }
            NotifyObservers();
        }

        /// <inheritdoc />
        /// <summary>
        /// Сохранение проекта
        /// </summary>
        /// <param name="filename">Имя файла</param>
        public void Save(string filename)
        {

        }

        /// <inheritdoc />
        /// <summary>
        /// Загрузка проекта
        /// </summary>
        public  void Load()
        {

        }

        /// <inheritdoc />
        /// <summary>
        /// Создание нового проекта
        /// </summary>
        public void NewProject()
        {
            _figures = new Dictionary<int, BaseFigure>();
            IsChanged = false;
            _currentIndex = 0;
        }

        /// <inheritdoc />
        /// <summary>
        /// Очистка канвы
        /// </summary>
        public void ClearCanvas()
        {
            _figures.Clear();

            NotifyObservers();
        }

        /// <inheritdoc />
        /// <summary>
        /// Получить список фигур
        /// </summary>
        /// <returns>Список фигур</returns>
        public Dictionary<int, BaseFigure> GetFigureList()
        {
            return _figures;
        }

        /// <inheritdoc />
        /// <summary>
        /// Регистрация наблюдателей
        /// </summary>
        /// <param name="observer">Регистрируемый наблюдатель</param>
        public void RegisterObserver(IObserver observer)
        {
            _observers.Add(observer);
        }

        /// <inheritdoc />
        /// <summary>
        /// Удаление наблюдателей
        /// </summary>
        /// <param name="observer">Удаляемый наблюдатель</param>
        public void RemoveObserver(IObserver observer)
        {
            _observers.Remove(observer);
        }

        /// <inheritdoc />
        /// <summary>
        /// Оповещение наблюдателей
        /// </summary>
        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update(_figures);
            }
        }
    }
}
