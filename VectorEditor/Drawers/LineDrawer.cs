using System.Drawing;
using System.Windows.Forms;

namespace VectorEditor
{
    class LineDrawer:CanvasDrawer
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
            Pen pen = new Pen(line.LineColor, line.LineThickness);
            PickLineType(line.LineType, pen);
            g.DrawLine(pen, new Point(line.A.X, line.A.Y), 
                            new Point(line.B.X, line.B.Y));
            g.Dispose();
        }
    }
}
