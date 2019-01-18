using System;
using System.Collections.Generic;
using System.Drawing;
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

        public static BaseFigure CreateCopyWithOffset(BaseFigure figure)
        {
            BaseFigure resultFigure = null;
            if (figure.GetType() == typeof(FillableFigure))
            {
                var fillableFigure = figure as FillableFigure;
                FillableFigure newFigure = new FillableFigure();

                newFigure.LineProperties.Color = fillableFigure.LineProperties.Color;
                newFigure.LineProperties.Thickness = fillableFigure.LineProperties.Thickness;
                newFigure.LineProperties.Style = fillableFigure.LineProperties.Style;

                newFigure.FillProperty.FillColor = fillableFigure.FillProperty.FillColor;

                foreach (var point in fillableFigure.Points.GetPoints())
                {
                    newFigure.Points.AddPoint(new PointF(point.X + 15, point.Y));
                }

                resultFigure = newFigure;
            }
            else
            {
                if (figure.GetType() == typeof(Line))
                {
                    resultFigure = new Line();
                    resultFigure.LineProperties.Color = figure.LineProperties.Color;
                    resultFigure.LineProperties.Thickness = figure.LineProperties.Thickness;
                    resultFigure.LineProperties.Style = figure.LineProperties.Style;

                    foreach (var point in figure.Points.GetPoints())
                    {
                        resultFigure.Points.AddPoint(new PointF(point.X + 15, point.Y));
                    }
                }
                else if (figure.GetType() == typeof(Polyline))
                {
                    resultFigure = new Polyline();
                    resultFigure.LineProperties.Color = figure.LineProperties.Color;
                    resultFigure.LineProperties.Thickness = figure.LineProperties.Thickness;
                    resultFigure.LineProperties.Style = figure.LineProperties.Style;

                    foreach (var point in figure.Points.GetPoints())
                    {
                        resultFigure.Points.AddPoint(new PointF(point.X + 15, point.Y));
                    }
                }
                else if (figure.GetType() == typeof(Circle))
                {
                    Circle circle = new Circle();
                    circle.LineProperties.Color = figure.LineProperties.Color;
                    circle.LineProperties.Thickness = figure.LineProperties.Thickness;
                    circle.LineProperties.Style = figure.LineProperties.Style;

                    var temp = figure as FillableFigure;
                    circle.FillProperty.FillColor = temp.FillProperty.FillColor;

                    foreach (var point in figure.Points.GetPoints())
                    {
                        circle.Points.AddPoint(new PointF(point.X + 15, point.Y));
                    }

                    resultFigure = circle;
                }
                else if (figure.GetType() == typeof(Ellipse))
                {
                    Ellipse circle = new Ellipse();
                    circle.LineProperties.Color = figure.LineProperties.Color;
                    circle.LineProperties.Thickness = figure.LineProperties.Thickness;
                    circle.LineProperties.Style = figure.LineProperties.Style;

                    var temp = figure as FillableFigure;
                    circle.FillProperty.FillColor = temp.FillProperty.FillColor;

                    foreach (var point in figure.Points.GetPoints())
                    {
                        circle.Points.AddPoint(new PointF(point.X + 15, point.Y));
                    }

                    resultFigure = circle;
                }
                else if (figure.GetType() == typeof(Polygon))
                {
                    Polygon circle = new Polygon();
                    circle.LineProperties.Color = figure.LineProperties.Color;
                    circle.LineProperties.Thickness = figure.LineProperties.Thickness;
                    circle.LineProperties.Style = figure.LineProperties.Style;

                    var temp = figure as FillableFigure;
                    circle.FillProperty.FillColor = temp.FillProperty.FillColor;

                    foreach (var point in figure.Points.GetPoints())
                    {
                        circle.Points.AddPoint(new PointF(point.X + 15, point.Y));
                    }

                    resultFigure = circle;
                }
            }
            return resultFigure;
        }

        public static BaseFigure CreateCopy(BaseFigure figure)
        {
            BaseFigure resultFigure = null;
            if (figure.GetType() == typeof(FillableFigure))
            {
                var fillableFigure = figure as FillableFigure;
                FillableFigure newFigure = new FillableFigure();

                newFigure.LineProperties.Color = fillableFigure.LineProperties.Color;
                newFigure.LineProperties.Thickness = fillableFigure.LineProperties.Thickness;
                newFigure.LineProperties.Style = fillableFigure.LineProperties.Style;

                newFigure.FillProperty.FillColor = fillableFigure.FillProperty.FillColor;

                foreach (var point in fillableFigure.Points.GetPoints())
                {
                    newFigure.Points.AddPoint(new PointF(point.X, point.Y));
                }

                resultFigure = newFigure;
            }
            else
            {
                if (figure.GetType() == typeof(Line))
                {
                    resultFigure = new Line();
                    resultFigure.LineProperties.Color = figure.LineProperties.Color;
                    resultFigure.LineProperties.Thickness = figure.LineProperties.Thickness;
                    resultFigure.LineProperties.Style = figure.LineProperties.Style;

                    foreach (var point in figure.Points.GetPoints())
                    {
                        resultFigure.Points.AddPoint(new PointF(point.X, point.Y));
                    }
                }
                else if (figure.GetType() == typeof(Polyline))
                {
                    resultFigure = new Polyline();
                    resultFigure.LineProperties.Color = figure.LineProperties.Color;
                    resultFigure.LineProperties.Thickness = figure.LineProperties.Thickness;
                    resultFigure.LineProperties.Style = figure.LineProperties.Style;

                    foreach (var point in figure.Points.GetPoints())
                    {
                        resultFigure.Points.AddPoint(new PointF(point.X, point.Y));
                    }
                }
                else if (figure.GetType() == typeof(Circle))
                {
                    Circle circle = new Circle();
                    circle.LineProperties.Color = figure.LineProperties.Color;
                    circle.LineProperties.Thickness = figure.LineProperties.Thickness;
                    circle.LineProperties.Style = figure.LineProperties.Style;

                    var temp = figure as FillableFigure;
                    circle.FillProperty.FillColor = temp.FillProperty.FillColor;

                    foreach (var point in figure.Points.GetPoints())
                    {
                        circle.Points.AddPoint(new PointF(point.X, point.Y));
                    }

                    resultFigure = circle;
                }
                else if (figure.GetType() == typeof(Ellipse))
                {
                    Ellipse circle = new Ellipse();
                    circle.LineProperties.Color = figure.LineProperties.Color;
                    circle.LineProperties.Thickness = figure.LineProperties.Thickness;
                    circle.LineProperties.Style = figure.LineProperties.Style;

                    var temp = figure as FillableFigure;
                    circle.FillProperty.FillColor = temp.FillProperty.FillColor;

                    foreach (var point in figure.Points.GetPoints())
                    {
                        circle.Points.AddPoint(new PointF(point.X, point.Y));
                    }

                    resultFigure = circle;
                }
                else if (figure.GetType() == typeof(Polygon))
                {
                    Polygon circle = new Polygon();
                    circle.LineProperties.Color = figure.LineProperties.Color;
                    circle.LineProperties.Thickness = figure.LineProperties.Thickness;
                    circle.LineProperties.Style = figure.LineProperties.Style;

                    var temp = figure as FillableFigure;
                    circle.FillProperty.FillColor = temp.FillProperty.FillColor;

                    foreach (var point in figure.Points.GetPoints())
                    {
                        circle.Points.AddPoint(new PointF(point.X, point.Y));
                    }

                    resultFigure = circle;
                }
            }
            return resultFigure;
        }
    }
}
