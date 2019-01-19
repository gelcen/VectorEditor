using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using VectorEditor.Figures;
using VectorEditor.View;

namespace VectorEditor.Model
{
    /// <summary>
    /// Класс для модели
    /// </summary>
    public class FigureModel:IModel, ISubject
    {
        /// <summary>
        /// Список подписчиков
        /// </summary>
        private List<IObserver> _observers;

        /// <summary>
        /// Конструктор
        /// </summary>
        public FigureModel()
        {
            _observers = new List<IObserver>();
        }

        /// <summary>
        /// Флаг для обозначения внесения изменений
        /// </summary>
        private bool _isChanged;

        /// <summary>
        /// Список фигур
        /// </summary>
        private List<BaseFigure> _figures;

        /// <summary>
        /// Добавление фигуры в список фигур
        /// </summary>
        /// <param name="figure">Добавляемая фигура</param>
        public void AddFigure(BaseFigure figure)
        {
            _figures.Add(figure);
            HasChanged();
            NotifyObservers();
        }

        /// <summary>
        /// Есть изменение
        /// </summary>
        public void HasChanged()
        {
            _isChanged = true;
        }

        /// <summary>
        /// Копирование объекта
        /// </summary>
        /// <param name="figure"></param>
        public void CopyFigure(BaseFigure figure)
        {
            var addingFigure = FigureFactory.CreateCopyWithOffset(figure);
            _figures.Add(addingFigure);
            NotifyObservers();      
        }

        /// <summary>
        /// Вырезание объекта
        /// </summary>
        /// <param name="figure"></param>
        public void CutFigure(BaseFigure figure)
        {

        }

        /// <summary>
        /// Удаление объекта
        /// </summary>
        /// <param name="figure"></param>
        public void DeleteFigureAt(int index, BaseFigure figure)
        {
            _figures.RemoveAt(index);
            NotifyObservers();
        }

        public void DeleteFigure(BaseFigure figure)
        {
            _figures.Remove(figure);
            NotifyObservers();
        }

        public void InsertAt(int index, BaseFigure figure)
        {
            _figures.Insert(index, figure);
            NotifyObservers();
        }

        /// <summary>
        /// Изменение размеров фигуры
        /// </summary>
        /// <param name="figure"></param>
        public void ChangeFigureParameters(int index, FigureParameters parameters)
        {
            var figure = _figures[index];
            figure.LineProperties.Color = parameters.LineColor;
            figure.LineProperties.Style = (DashStyle)parameters.LineType;
            figure.LineProperties.Thickness = parameters.LineThickness;
            if (figure.GetType() == typeof(Circle) ||
                figure.GetType() == typeof(Ellipse) ||
                figure.GetType() == typeof(Polygon))
            {
                //FIX: сделать темп фигуру до проверки с as и проверить на null
                var tempFigure = figure as FillableFigure;
                tempFigure.FillProperty.FillColor = parameters.FillColor;
            }
            NotifyObservers();
        }


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
                changedFigure.FillProperty.FillColor = tempFigure.FillProperty.FillColor;
                figure = changedFigure;
            }
            int count = figure.Points.GetPoints().Count;
            for (int i = 0; i < count; i++)
            {
                figure.Points.Replace(i, new PointF(newFigure.Points.GetPoints()[i].X,
                                                    newFigure.Points.GetPoints()[i].Y));
            }
            NotifyObservers();
        }

        /// <summary>
        /// Перемещение объекта
        /// </summary>
        /// <param name="figure"></param>
        public void MoveFigure(int index, BaseFigure figure)
        {
            int count = _figures[index].Points.GetPoints().Count;
            for (int i = 0; i < count; i++)
            {
                _figures[index].Points.Replace(i, new PointF(figure.Points.GetPoints()[i].X,
                                                             figure.Points.GetPoints()[i].Y));
            }
            NotifyObservers();
        }

        /// <summary>
        /// Сохранение проекта
        /// </summary>
        /// <param name="filename"></param>
        public void Save(string filename)
        {

        }

        /// <summary>
        /// Загрузка проекта
        /// </summary>
        public  void Load()
        {

        }

        /// <summary>
        /// Создание нового проекта
        /// </summary>
        public void NewProject()
        {
            _figures = new List<BaseFigure>();
            _isChanged = false;
        }

        /// <summary>
        /// Очистка канвы
        /// </summary>
        public void ClearCanvas()
        {
            _figures.Clear();
            NotifyObservers();
        }

        public List<BaseFigure> getFigureList()
        {
            return _figures;
        }

        public void RegisterObserver(IObserver o)
        {
            _observers.Add(o);
        }

        public void RemoveObserver(IObserver o)
        {
            _observers.Remove(o);
        }

        public void NotifyObservers()
        {
            for (int i = 0; i < _observers.Count; i++)
            {
                IObserver observer = _observers[i];
                observer.Update(_figures);
            }
        }

        //public void registerObserver(Observer observer)
        //{
        //    throw new NotImplementedException();
        //}

        //public void removeObserver(System.IObserver observer)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
