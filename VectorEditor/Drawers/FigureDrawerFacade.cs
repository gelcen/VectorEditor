using SDK;
using System.Drawing;



namespace VectorEditor.Drawers
{
    /// <summary>
    /// Фабрика для рисовальщиков. Также рисует фигуру
    /// </summary>
    public class FigureDrawerFacade: IDrawerFacade
    {
        /// <summary>
        /// Ссылка на DrawerFactory
        /// </summary>
        public IFactory<BaseDrawer> DrawerFactory { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="drawerFactory">Reference to drawer factory</param>
        public FigureDrawerFacade(IFactory<BaseDrawer> drawerFactory)
        {
            DrawerFactory = drawerFactory;
        }

        /// <summary>
        /// Создать рисовальщик и нарисовать фигуру
        /// </summary>
        /// <param name="figure">Фигура для рисования</param>
        /// <param name="canvas">Канва</param>
        public void DrawFigure(BaseFigure figure, Graphics canvas)
        {
            if (figure == null) return;

            var drawer = DrawerFactory
                .CreateInstance(figure.GetFigureName());

            drawer?.DrawFigure(figure, canvas);
        }

        /// <summary>
        /// Рисовка маркеров фигуры
        /// </summary>
        /// <param name="figure"></param>
        /// <param name="canvas"></param>
        public void DrawSelection(BaseFigure figure, Graphics canvas)
        {
            if (figure == null) return;

            var drawer = DrawerFactory
                .CreateInstance(figure.GetFigureName());

            drawer?.DrawSelection(figure, canvas);
        }
    }
}
