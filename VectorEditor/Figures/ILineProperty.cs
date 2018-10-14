using System.Drawing;

namespace VectorEditor
{
    /// <summary>
    /// Интерфейс для свойств линии
    /// </summary>
    public interface ILineProperty
    {
        /// <summary>
        /// Толщина линии
        /// </summary>
        int LineThickness { get; set;}

        /// <summary>
        /// Цвет линии
        /// </summary>
        Color LineColor { get; set;}

        /// <summary>
        /// Тип линии
        /// </summary>
        LineType LineType { get; set;}
    }
}
