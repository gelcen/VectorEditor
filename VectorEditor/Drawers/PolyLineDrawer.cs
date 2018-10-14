using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VectorEditor
{
    class PolyLineDrawer:CanvasDrawer
    {
        public PolyLine polyLine;

        public PolyLineDrawer(PolyLine polyLine, PictureBox canvas)
        {
            this.polyLine = polyLine;
            SetCanvas(canvas);
        }

        public override void Draw()
        {
            if (polyLine.points.Count > 1)
            {
                Graphics g = Canvas.CreateGraphics();
                Pen pen = new Pen(polyLine.LineColor, polyLine.LineThickness);
                PickLineType(polyLine.LineType, pen);
                for (int i=0; i < polyLine.points.Count; i++)
                {
                    if (i == polyLine.points.Count-2)
                    {
                        g.DrawLine(pen, new Point(polyLine.points[i].X,
                                    polyLine.points[i].Y),
                            new Point(polyLine.points[i + 1].X,
                                    polyLine.points[i + 1].Y));
                        g.Dispose();
                    }                    
                }
                //foreach (var Point in polyLine.points)
                //{
                //    if 
                //}
            }
        }
    }
}
