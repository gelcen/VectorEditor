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
    /// <summary>
    /// Класс для рисования многоугольника
    /// </summary>
    class PolygoneDrawer:Drawer
    {
        /// <summary>
        /// Поле с параметрами многоугольника
        /// </summary>
        public Polygone polygone;

        /// <summary>
        /// Конструктор класса PolygoneDrawer
        /// </summary>
        /// <param name="polygone"></param>
        /// <param name="canvas"></param>
        public PolygoneDrawer(Polygone polygone, PictureBox canvas)
        {
            this.polygone = polygone;
            SetCanvas(canvas);
        }

        /// <summary>
        /// Функция для преобразования списка точек в массив
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        private PointF[] ConvertPoints(List<PointF> points)
        {
            PointF[] pointsArray = new PointF[points.Count];
            for (int i=0; i<points.Count; i++)
            {
                pointsArray[i] = points.ElementAt(i);
            }
            return pointsArray;
        }

        /// <summary>
        /// Функция рисования
        /// </summary>
        public override void Draw()
        {
            if (polygone.points.Count >= 3)
            {
                Graphics g = Canvas.CreateGraphics();
                Pen pen = new Pen(polygone.LineColor, polygone.LineThickness);
                PickLineType(polygone.LineType, pen);
                PointF[] pointsArray = ConvertPoints(polygone.points);
                g.DrawPolygon(pen, pointsArray);
                g.Dispose();
            }
        }
    }
}
