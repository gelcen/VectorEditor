using System.Drawing;

using SDK;

namespace VectorEditorCore.Drawers
{
    /// <summary>
    /// Интерфейс, используемый
    /// для рисования фигур и маркеров
    /// </summary>
    interface IDrawerFacade
    {
        /// <summary>
        /// Нарисовать фигуру
        /// </summary>
        /// <param name="figure">Фигура</param>
        /// <param name="graphics">Поврхность рисования</param>
        void DrawFigure(BaseFigure figure, Graphics graphics);

        /// <summary>
        /// Нарисовать маркеры
        /// </summary>
        /// <param name="figure">Выделенная фигура</param>
        /// <param name="graphics">Поверхность рисования</param>
        void DrawMarkers(BaseFigure figure, Graphics graphics);
    }
}
