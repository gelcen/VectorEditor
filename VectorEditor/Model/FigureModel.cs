using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorEditor.Figures;
using VectorEditor.Observer;

namespace VectorEditor.Model
{
    /// <summary>
    /// Класс для модели
    /// </summary>
    class FigureModel:IModel, ISubject
    {

        private List<IObserver> _observers;

        /// <summary>
        /// Флаг для обозначения внесения изменений
        /// </summary>
        private bool _isChanged;

        /// <summary>
        /// Список фигур
        /// </summary>
        private List<Figure> _figures;

        /// <summary>
        /// Добавление фигуры в список фигур
        /// </summary>
        /// <param name="figure">Добавляемая фигура</param>
        public void AddFigure(Figure figure)
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
        public void CopyFigure(Figure figure)
        {

        }

        /// <summary>
        /// Вырезание объекта
        /// </summary>
        /// <param name="figure"></param>
        public void CutFigure(Figure figure)
        {

        }

        /// <summary>
        /// Удаление объекта
        /// </summary>
        /// <param name="figure"></param>
        public void DeleteFigure(Figure figure)
        {

        }

        /// <summary>
        /// Изменение размеров фигуры
        /// </summary>
        /// <param name="figure"></param>
        public void ChangeFigureSize(Figure figure)
        {

        }

        /// <summary>
        /// Перемещение объекта
        /// </summary>
        /// <param name="figure"></param>
        public void MoveFigure(Figure figure)
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
            _figures = new List<Figure>();
            _isChanged = false;
        }

        /// <summary>
        /// Очистка канвы
        /// </summary>
        public void ClearCanvas()
        {
            _figures.Clear();
        }

        public List<Figure> getFigureList()
        {
            throw new NotImplementedException();
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
