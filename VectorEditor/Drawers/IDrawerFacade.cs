using SDK;
using System.Drawing;

namespace VectorEditor.Drawers
{
    public interface IDrawerFacade
    {
        IFactory<BaseDrawer> DrawerFactory
        {
            get;
            set;
        }

        void DrawFigure(BaseFigure figure, Graphics graphics);

        void DrawSelection(BaseFigure figure, Graphics graphics);
    }
}
