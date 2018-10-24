using System.Drawing;
using System.Windows.Forms;
using VectorEditor.Figures;

namespace VectorEditor.Drawers
{
    /// <summary>
    /// Класс для рисовки эллипса
    /// </summary>
    class EllipseDrawer:Drawer
    {
        /// <summary>
        /// Эллипс
        /// </summary>
        public Ellipse ellipse;

        /// <summary>
        /// Конструктор класса EllipseDrawer
        /// </summary>
        /// <param name="ellipse">Класс, содержащий параметры эллипса</param>
        /// <param name="canvas">Канва для рисования</param>
        public EllipseDrawer(Ellipse ellipse, PictureBox canvas)
        {
            this.ellipse = ellipse;
            SetCanvas(canvas);
        }

        /// <summary>
        /// Функция для рисовки эллипса
        /// </summary>
        public override void Draw()
        {
            Graphics g = Canvas.CreateGraphics();
            Pen pen = new Pen(ellipse.LineColor, ellipse.LineThickness);
            PickLineType(ellipse.LineType, pen);
            g.DrawEllipse(pen, ellipse.A.X, ellipse.A.Y, 
                          ellipse.Width, ellipse.Height);
            if (ellipse.FillColor != Color.White)
            {
                SolidBrush brush = new SolidBrush(ellipse.FillColor);
                g.FillEllipse(brush, ellipse.A.X, ellipse.A.Y,
                          ellipse.Width, ellipse.Height);
            }
            g.Dispose();
        }
    }
}
