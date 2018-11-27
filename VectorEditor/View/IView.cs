using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorEditor.Figures;

namespace VectorEditor.Model
{
    /// <summary>
    /// Интерфейс для Представления
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Событие - Фигура создана в представлении
        /// </summary>
        event EventHandler<FigureCreatedEventArgs> FigureCreated;


        /// <summary>
        /// Событие изменения модели
        /// </summary>
        void DrawModel(List<Figure> figures);
    }
}
