using System.Drawing;
using VectorEditor.Figures;

namespace VectorEditor.Drawers
{
    public abstract class BaseDrawer
    {
        protected const int object_radius = 3;

        public abstract void DrawFigure(BaseFigure figure, Graphics canvas);

        /// <summary>
        /// Рисовка границ и точек
        /// </summary>
        /// <param name="figure"></param>
        /// <param name="canvas"></param>
        public abstract void DrawSelection(BaseFigure figure, Graphics canvas);
    }
}
