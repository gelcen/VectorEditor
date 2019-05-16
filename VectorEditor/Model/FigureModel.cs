using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using VectorEditor.Figures;
using VectorEditor.Observer;
using VectorEditor.FileManager;
using SDK;

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

        /// <summary>
        /// Текущий индекс
        /// </summary>
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
        public bool IsChanged { get; private set; }

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
            var addingFigure = (BaseFigure)figure.Clone(); 
            CurrentIndex += 1;
            _figures.Add(CurrentIndex, addingFigure);
            NotifyObservers();      
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
            HasChanged();
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

            if (figure is FillableFigure fillable)
            {
                fillable.FillProperty.FillColor = newParameters.FillColor;
            }
            
            HasChanged();
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
            HasChanged();
            NotifyObservers();
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
            HasChanged();
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
