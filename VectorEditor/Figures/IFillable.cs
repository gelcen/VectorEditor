using System.Drawing;

namespace VectorEditor
{
    /// <summary>
    /// Интерфейс для фигур с заливкой
    /// </summary>
    public interface IFillable
    {
        /// <summary>
        /// Цвет заливки
        /// </summary>
        Color FillColor { get; set; }
    }
}
