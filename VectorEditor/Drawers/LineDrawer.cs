using System.Drawing;
using System.Windows.Forms;

namespace VectorEditor
{
    class LineDrawer:Drawer
    {
        // Неизвестно, понадобится ли изменять линию после конструктора
        public Line line;

        public LineDrawer(Line line, PictureBox canvas)
        {
            SetCanvas(canvas);
            this.line = line;            
        }

        public override void Draw()
        {
            Graphics g = Canvas.CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Pen pen = new Pen(line.LineColor, line.LineThickness);
            PickLineType(line.LineType, pen);
            g.DrawLine(pen, new PointF(line.A.X, line.A.Y), 
                            new PointF(line.B.X, line.B.Y));
            g.Dispose();
        }
    }
}
