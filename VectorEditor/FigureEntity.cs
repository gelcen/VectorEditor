using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VectorEditor.View;

namespace VectorEditor
{
    public abstract class FigureEntity : ICloneable
    {
        protected Pen _pen;

        protected List<PointF> _points = new List<PointF>();

        public Color fillColor
        {
            get;
            set;
        }
        public Color lineColor
        {
            get
            {
                return _pen.Color;
            }
            set
            {
                _pen.Color = value;
            }
        }
        public DashStyle lineType
        {
            get
            {
                return _pen.DashStyle;
            }
            set
            {
                _pen.DashStyle = value;
            }
        }
        public float lineThickness
        {
            get
            {
                return _pen.Width;
            }
            set
            {
                _pen.Width = value;
            }
        }

        public abstract FigureEntity Clone();

        public Canvas Canvas
        {
            get;
            protected set;
        }

        /// <summary>
        /// Функция для преобразования списка точек в массив
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        protected PointF[] ConvertPoints(List<PointF> points)
        {
            PointF[] pointsArray = new PointF[points.Count];
            for (int i = 0; i < points.Count; i++)
            {
                pointsArray[i] = points.ElementAt(i);
            }
            return pointsArray;
        }

        protected abstract void OnCanvasMouseDown(object sender, MouseEventArgs e);
        protected abstract void OnCanvasMouseUp(object sender, MouseEventArgs e);
        protected abstract void OnCanvasMouseMove(object sender, MouseEventArgs e);
        protected abstract void OnCanvasMouseStartDrag(object sender, MouseEventArgs e);
        protected abstract void OnCanvasMouseDrag(object sender, MouseEventArgs e);
        protected abstract void OnCanvasMouseEndDrag(object sender, MouseEventArgs e);
        public abstract void Draw(Graphics gr);
        public abstract bool IsInsidePoint(Point p);
        public abstract RectangleF GetRect();

        object ICloneable.Clone()
        {
            return Clone();
        }
    }
}
