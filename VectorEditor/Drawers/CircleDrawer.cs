using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VectorEditor.Figures;

namespace VectorEditor.Drawers
{
    class CircleDrawer:Drawer
    {
        Circle circle;

        public CircleDrawer(Circle circle, PictureBox canvas)
        {
            SetCanvas(canvas);
            this.circle = circle;
        }

        public override void Draw()
        {
            float x = circle.CenterPoint.X;
            float y = circle.CenterPoint.Y;
            float rad = circle.Radius;
            
            Graphics g = Canvas.CreateGraphics();
            Pen pen = new Pen(circle.LineColor, circle.LineThickness);
            PickLineType(circle.LineType, pen);
            g.DrawEllipse(pen, x - rad, y - rad, rad + rad, rad + rad);
            if (circle.FillColor != Color.White)
                {
                    SolidBrush brush = new SolidBrush(circle.FillColor);
                    g.FillEllipse(brush, x - rad, y - rad, rad + rad, rad + rad);
                }
            g.Dispose();
        }

    }
}
