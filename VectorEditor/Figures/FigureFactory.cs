using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorEditor.Figures
{
    public static class FigureFactory
    {
        public static BaseFigure CreateFigure(Item type)
        {
            switch (type)
            {
                case Item.Line:
                    return new Line();
                case Item.Polyline:
                    return new Polyline();
                case Item.Polygon:
                    return new Polygon();
                case Item.Circle:
                    return new Circle();
                case Item.Ellipse:
                    return new Ellipse();
                default:
                    return null;
            }
        }

        public static BaseFigure CreateFillableFigure(Item type)
        {
            switch (type)
            {
                case Item.Polygon:
                    return new Polygon();
                case Item.Circle:
                    return new Circle();
                case Item.Ellipse:
                    return new Ellipse();
                default:
                    return null;
            }
        }
    }
}
