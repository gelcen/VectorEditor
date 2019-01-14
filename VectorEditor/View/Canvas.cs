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
using VectorEditor.Presenter;

namespace VectorEditor.View
{
    public partial class Canvas : UserControl
    {
        /// <summary>
        /// Список фигур в представлении
        /// </summary>
        private List<BaseFigure> _figures = new List<BaseFigure>();

        public LineHandler CurrentHandler;

        #region Current Parameters
        /// <summary>
        /// Текущий тип линии
        /// </summary>
        private int _currentDashStyle = 1;

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
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (CurrentHandler != null)
            {
                CurrentHandler.Draw(g);
            }
        }

        private void DrawModel(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            if (_figures.Count != 0)
            {
                foreach (var figure in _figures)
                {
                    //FigureDrawer.DrawFigure(figure, g);
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (CurrentHandler != null)
            {
                CurrentHandler.MouseDown(null, e);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (CurrentHandler != null)
            {
                CurrentHandler.MouseMove(null, e);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (CurrentHandler != null)
            {
                CurrentHandler.MouseUp(null, e);
            }
        }

    }
}
