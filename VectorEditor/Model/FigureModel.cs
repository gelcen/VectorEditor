using System;
using System.Collections.Generic;
using VectorEditor.Figures;

namespace VectorEditor.Model
{
    /// <summary>
    /// Класс для модели
    /// </summary>
    class FigureModel:IModel, ISubject
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
            var addingFigure = FigureFactory.CreateCopy(figure);
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
        public void DeleteFigure(BaseFigure figure)
        {
            _figures.Remove(figure);
        }

        /// <summary>
        /// Изменение размеров фигуры
        /// </summary>
        /// <param name="figure"></param>
        public void ChangeFigureSize(BaseFigure figure)
        {

        }

        /// <summary>
        /// Перемещение объекта
        /// </summary>
        /// <param name="figure"></param>
        public void MoveFigure(BaseFigure figure)
        {

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
