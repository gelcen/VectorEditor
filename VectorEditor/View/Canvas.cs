using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VectorEditor.Figures;

namespace VectorEditor.View
{
    public partial class Canvas : UserControl, IObserver
    {
        /// <summary>
        /// Список фигур в представлении
        /// </summary>
        private List<Figure> _figures = new List<Figure>();

        #region Current Parameters
        /// <summary>
        /// Текущий тип линии
        /// </summary>
        private int _currentDashStyle = 0;

        /// <summary>
        /// Текущая толщина линии
        /// </summary>
        private int _currentLineThickness = 1;

        /// <summary>
        /// Текущий цвет линии
        /// </summary>
        private Color _currentLineColor = Color.Black;

        /// <summary>
        /// Текущий цвет заплонения
        /// </summary>
        private Color _currentFillColor = Color.Black;

        public void UpdateLineColor(Color color)
        {
            _currentLineColor = color;
            Invalidate();
        }

        public void UpdateFillColor(Color color)
        {
            _currentFillColor = color;
            Invalidate();
        }

        public void UpdateLineThickness(int thickness)
        {
            _currentLineThickness = thickness;
            Invalidate();
        }

        public void UpdateDashStyle(int style)
        {
            _currentDashStyle = style;
            Invalidate();
        }

        #endregion

        public void Update(List<Figure> figures)
        {
            _figures = figures;
            Refresh();
        }

        /// <summary>
        /// Текущий инструмент
        /// </summary>
        private Item _currentItem = Item.Cursor;

        public Canvas()
        {
            InitializeComponent();
            ResumeLayout();
            DoubleBuffered = true;
            ResizeRedraw = true;

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DrawModel(e.Graphics);
        }

        private void DrawModel(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            if (_figures.Count != 0)
            {
                foreach (var figure in _figures)
                {
                    FigureDrawer.DrawFigure(figure, g);
                }
            }
        }

        public Point GetCanvasPoint(Point point)
        {
            Point resultPoint = point;
            if (resultPoint.X < 0)
                resultPoint.X = 0;
            if (resultPoint.X > Width)
                resultPoint.X = Width;
            if (resultPoint.Y < 0)
                resultPoint.Y = 0;
            if (resultPoint.Y > Height)
                resultPoint.Y = Height;
            return resultPoint;
        }

        private int x, y, lx, ly = 0;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Focus();
            if (e.Button == MouseButtons.Left)
            {
                switch (_currentItem)
                {
                    case Item.Line:

                        break;
                    case Item.Polyline:
                        break;
                    case Item.Polygon:
                        break;
                    case Item.Circle:
                        break;
                    case Item.Ellipse:
                        break;
                }
            }
        }


    }
}
