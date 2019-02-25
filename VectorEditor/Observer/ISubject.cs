namespace VectorEditor.Observer
{
    /// <summary>
    /// Интерфейс объекта
    /// </summary>
    public interface ISubject
    {
        /// <summary>
        /// Регистрация наблюдателей
        /// </summary>
        /// <param name="observer">Наблюдатель</param>
        void RegisterObserver(IObserver observer);

        /// <summary>
        /// Удаление наблюдателей
        /// </summary>
        /// <param name="observer">Наблюдатель</param>
        void RemoveObserver(IObserver observer);

        /// <summary>
        /// Оповещение наблюдателей
        /// </summary>
        void NotifyObservers();

    }
}
