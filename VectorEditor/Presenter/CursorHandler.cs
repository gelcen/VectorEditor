using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VectorEditor.Figures;
using VectorEditor.View;

namespace VectorEditor.Presenter
{
    public class CursorHandler : IBaseHandler
    {
        private FigureParameters _figureParameters;

        private PictureBox _canvas;

        private BaseFigure _selectedFigure;

        private Presenter _presenter;

        public FigureParameters FigureParameters
        {
            set
            {
                _figureParameters = value;
            }
        }

        public PictureBox Canvas
        {
            get
            {
                return _canvas;
            }
            set
            {
                _canvas = value;
            }
        }

        public CursorHandler(PictureBox canvas, FigureParameters figureParameters, Presenter presenter)
        {
            FigureParameters = figureParameters;
            Canvas = canvas;
            _presenter = presenter;
            
            _selectedFigure = null;
            _selectedFigures = new List<BaseFigure>();
        }

        public event EventHandler<BaseFigure> FigureCreated;

        public void Draw(Graphics g)
        {
            Pen pen= new Pen(Color.FromArgb(0, 120, 215), 1);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;

            Brush brush = new SolidBrush(Color.FromArgb(80, 0, 102, 204));

            g.FillRectangle(brush, _selectionRect);
            g.DrawRectangle(pen, _selectionRect);
            pen.Dispose();
            brush.Dispose();
        }

        /// <summary>
        /// Расстояние для клика
        /// </summary>
        private const int object_radius = 3;

        private bool _isMouseDown = false;

        private bool _isDraggingSelectionRect = false;

        private PointF _originalMouseDownPoint;

        private static readonly double _dragTreshold = 5;

        private bool _isMouseDownOnFigure = false;

        private bool _isDraggingFigure = false;

        private List<BaseFigure> _selectedFigures;

        private Rectangle _selectionRect;

        public void MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _selectedFigures.Clear();

                _isMouseDown = true;
                _originalMouseDownPoint = e.Location;
            }
        }

        public void MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDraggingSelectionRect)
            {
                PointF currentMouseDownPoint = e.Location;
                UpdateDragSelectionRect(_originalMouseDownPoint,
                                        currentMouseDownPoint);
            }
            else if (_isMouseDown)
            {
                PointF currentMouseDownPoint = e.Location;
                float deltaX = Math.Abs(
                               currentMouseDownPoint.X - _originalMouseDownPoint.X);
                float deltaY = Math.Abs(
                               currentMouseDownPoint.Y - _originalMouseDownPoint.Y);
                double distance = Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));
                if (distance > _dragTreshold)
                {
                    _isDraggingSelectionRect = true;
                    InitDragSelectionRect(_originalMouseDownPoint, 
                                          currentMouseDownPoint);
                }
            }
            Canvas.Refresh();
        }

        public void MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //Была создан прямоугольник выборки
                bool wasDragSelectionApplied = false;

                if (_isDraggingSelectionRect)
                {
                    //Выборка закончилась
                    _isDraggingSelectionRect = false;
                    SelectFiguresInRect();

                    wasDragSelectionApplied = true;
                }

                if (_isMouseDown)
                {
                    _isMouseDown = false;
                }

                if(!wasDragSelectionApplied)
                {
                    //Клик по пустому месту
                    _selectedFigures.Clear();
                }
            }
            Canvas.Refresh();
        }

        private void SelectFiguresInRect()
        {
            _selectionRect = new Rectangle();
        }

        private void InitDragSelectionRect(PointF pt1, PointF pt2)
        {
            UpdateDragSelectionRect(pt1, pt2);
        }

        private void UpdateDragSelectionRect(PointF pt1, PointF pt2)
        {
            double x, y, width, height;

            if (pt2.X < pt1.X)
            {
                x = pt2.X;
                width = pt1.X - pt2.X;
            }
            else
            {
                x = pt1.X;
                width = pt2.X - pt1.X;
            }

            if (pt2.Y < pt1.Y)
            {
                y = pt2.Y;
                height = pt1.Y - pt2.Y;
            }
            else
            {
                y = pt1.Y;
                height = pt2.Y - pt1.Y;
            }

            _selectionRect = new Rectangle((int)x, (int)y, 
                                           (int)width, (int)height);

        }
    }
}
