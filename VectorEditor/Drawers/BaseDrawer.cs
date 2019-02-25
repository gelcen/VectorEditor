using System.Drawing;
using VectorEditor.Figures;

namespace VectorEditor.Drawers
{
    /// <summary>
    /// Базовый класс для рисовальщиков
    /// </summary>
    public abstract class BaseDrawer
    {
        /// <summary>
        /// Константа для размера маркеров
        /// </summary>
        protected const int ObjectRadius = 3;

        /// <summary>
        /// Нарисовать фигуру
        /// </summary>
        /// <param name="figure">Рисуемая фигура</param>
        /// <param name="canvas">Канва</param>
        public abstract void DrawFigure(BaseFigure figure, Graphics canvas);

        /// <summary>
        /// Рисовка границ и точек
        /// </summary>
        /// <param name="figure">Фигура</param>
        /// <param name="canvas">Канва</param>
        public abstract void DrawSelection(BaseFigure figure, Graphics canvas);
    }
}
