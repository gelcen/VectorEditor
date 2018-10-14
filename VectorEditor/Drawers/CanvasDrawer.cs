using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VectorEditor
{
    /// <summary>
    /// Класс для рисовки
    /// </summary>
    class CanvasDrawer
    {
        public PictureBox Canvas
        {
            get;
            set;
        }
        
        public void SetCanvas(PictureBox canvas)
        {
            Canvas = canvas;
        }

        public void PickLineType(LineType lineType, Pen pen)
        {
            switch (lineType)
            {
                case LineType.Solid:
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                    break;
                case LineType.Dashed:
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    break;
                default:
                    break;
            }
        }

        public virtual void Draw()
        {

        }       
    }
}
