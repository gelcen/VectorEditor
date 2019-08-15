using System.Drawing;

namespace VectorEditorCore.View
{
    /// <summary>
    /// Структура для параметров фигуры
    /// </summary>
    public struct FigureParameters
    {
        /// <summary>
        /// Толщина линии
        /// </summary>
        public int LineThickness { get; set; }

        /// <summary>
        /// Стиль линии
        /// </summary>
        public int LineStyle { get; set; }

        /// <summary>
        /// Цвет линии
        /// </summary>
        public Color LineColor { get; set; }

        /// <summary>
        /// Цвет заливки фигур
        /// </summary>
        public Color FillColor { get; set; }

        /// <summary>
        /// Фигура залита?
        /// </summary>
        public bool IsFilled { get; set; }
    }
}
