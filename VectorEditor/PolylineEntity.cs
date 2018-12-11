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
    class PolylineEntity : FigureEntity
    {
        public PolylineEntity(Canvas canvas, PointF pointOne, float lineThickness, 
                              Color lineColor, DashStyle dashStyle = DashStyle.Solid)
        {
            Canvas = canvas;
            _points.Add(pointOne);
            _points.Add(pointOne);
            _pen = new Pen(lineColor, lineThickness);
            _pen.DashStyle = dashStyle;
            Canvas.MouseDown += OnCanvasMouseDown;
            Canvas.MouseUp += OnCanvasMouseUp;
            Canvas.MouseMove += OnCanvasMouseMove;
        }

        public override FigureEntity Clone()
        {
            throw new NotImplementedException();
        }

        public override void Draw(Graphics g)
        {
            g.DrawLines(_pen, ConvertPoints(_points));
        }

        public override RectangleF GetRect()
        {
            throw new NotImplementedException();
        }

        public override bool IsInsidePoint(Point p)
        {
            throw new NotImplementedException();
        }

        protected override void OnCanvasMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //_points.Add(Canvas.GetCanvasPoint(e.Location));
            }
        }

        protected override void OnCanvasMouseDrag(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void OnCanvasMouseEndDrag(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void OnCanvasMouseMove(object sender, MouseEventArgs e)
        {
           // _points[_points.Count - 1] = Canvas.GetCanvasPoint(e.Location);
            Canvas.Refresh();
        }

        protected override void OnCanvasMouseStartDrag(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void OnCanvasMouseUp(object sender, MouseEventArgs e)
        {
           // _points[_points.Count - 1] = Canvas.GetCanvasPoint(e.Location);
            Canvas.Refresh();
        }
    }
}
