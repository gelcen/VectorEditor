using System.Collections.Generic;
using VectorEditor.Figures;

namespace VectorEditor.Observer
{
    /// <summary>
    /// Интефейс наблюдателя
    /// </summary>
    public interface IObserver
    {
        /// <summary>
        /// Обновление состояния предмета
        /// </summary>
        /// <param name="figures">Фигуры</param>
        void Update(Dictionary<int, BaseFigure> figures);
    }
}
