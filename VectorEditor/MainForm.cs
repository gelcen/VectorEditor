using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VectorEditor.Drawers;
using VectorEditor.Figures;

namespace VectorEditor
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Переменная для хранения текущего цвета линии
        /// </summary>
        Color currentLineColor;

        /// <summary>
        /// Переменная для хранения текущего цвета заполнения
        /// </summary>
        Color currentFillColor;

        /// <summary>
        /// Переменная для хранения текущего типа линии
        /// </summary>
        LineType currentLineType;

        bool drawing;

        /// <summary>
        /// Начальные и конечные координаты
        /// </summary>
        int x, y, lx, ly = 0;

        /// <summary>
        /// Текущий элемент
        /// </summary>
        Item currentItem;

        /// <summary>
        /// Полилиния
        /// </summary>
        PolyLine polyLine;

        /// <summary>
        /// Многоугольник
        /// </summary>
        Polygone polygone;

        List<PointF> points;

        Figure figure;

        Control draggedPiece = null;
        bool resizing = false;
        private Point startDraggingPoint;
        private Size startSize;
        Rectangle rectProposedSize = Rectangle.Empty;
        int resizingMargin = 5;

        public MainForm()
        {
            InitializeComponent();
            currentLineColor = Color.Black;
            currentFillColor = Color.White;
            currentLineType = LineType.Solid;
            currentItem = Item.Cursor;
            polyLine = new PolyLine();
            drawing = false;

        }

        /// <summary>
        /// Обработчик события в момент нажатия кнопки мыши, когда указатель над канвой
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            draggedPiece = sender as Control;

            if (((e.X <= resizingMargin) || (e.X >= draggedPiece.Width - resizingMargin) ||
                (e.Y <= resizingMargin) || (e.Y >= draggedPiece.Height - resizingMargin)) 
                && (currentItem == Item.Cursor))
            {
                resizing = true;

                this.Cursor = Cursors.SizeNWSE;

                this.startSize = new Size(e.X, e.Y);
                Point pt = this.PointToScreen(draggedPiece.Location);
                rectProposedSize = new Rectangle(pt, startSize);

                ControlPaint.DrawReversibleFrame(rectProposedSize, this.ForeColor, FrameStyle.Dashed);
            }
            else
            {
                resizing = false;
                drawing = true;
                x = e.X;
                y = e.Y;
                pbCanvas.Cursor = Cursors.Cross;
            }

            this.startDraggingPoint = e.Location;                      
        }

        /// <summary>
        /// Обработчик события в момента отпускания кнопки мышки, когда указатель над канвой
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            drawing = false;
            lx = e.X;
            ly = e.Y;
            if (resizing)
            {
                if (rectProposedSize.Width > 0 && rectProposedSize.Height > 0)
                {
                    ControlPaint.DrawReversibleFrame(rectProposedSize, this.ForeColor, FrameStyle.Dashed);
                }
                if (rectProposedSize.Width > 10 && rectProposedSize.Height > 10)
                {
                    this.draggedPiece.Size = rectProposedSize.Size;
                }
                else
                {
                    this.draggedPiece.Size = new Size((int)Math.Max(10, rectProposedSize.Width), 
                        Math.Max(10, rectProposedSize.Height));
                }
            }            
            switch (currentItem)
            {
                case Item.Line:
                    Figure line = FigureFactory.CreateFigure(Item.Line);
                    line = FigureFactory.SetParameters(line, x, y, lx, ly, 
                                        Convert.ToInt32(nudLineThickness.Value), 
                                        currentLineColor, currentLineType);
                    LineDrawer lineDrawer = new LineDrawer((Line)line, pbCanvas);
                    lineDrawer.Draw();
                    break;
                case Item.Polyline:
                    break;
                case Item.Polygon:
                    break;
                case Item.Circle:
                    float rad = (float)Math.Sqrt(Math.Pow((lx - x), 2) + Math.Pow((ly - y), 2));
                    Figure circle = FigureFactory.CreateFigure(Item.Circle);
                    circle = FigureFactory.SetParameters(circle, x, y, rad, currentLineColor,
                                              Convert.ToInt32(nudLineThickness.Value),
                                              currentFillColor, currentLineType);
                    CircleDrawer circleDrawer = new CircleDrawer((Circle)circle, pbCanvas);
                    circleDrawer.Draw();
                    break;
                case Item.Ellipse:
                    Figure ellipse = FigureFactory.CreateFigure(Item.Ellipse);
                    ellipse = FigureFactory.SetParameters(ellipse, x, y, lx - x, ly - y,
                                                  currentLineColor,
                                                  currentFillColor,
                                                  Convert.ToInt32(nudLineThickness.Value),
                                                  currentLineType);
                    EllipseDrawer ellipseDrawer = new EllipseDrawer((Ellipse)ellipse, pbCanvas);
                    ellipseDrawer.Draw();
                    break;
                default:
                    break;
            }
            this.draggedPiece = null;
            this.startDraggingPoint = Point.Empty;
            pbCanvas.Cursor = Cursors.Default;
            this.Cursor = Cursors.Default;
        }

        private void pbCanvas_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonLine_Click(object sender, EventArgs e)
        {
            currentItem = Item.Line;
        }

        private void pbCanvas_MouseClick(object sender, MouseEventArgs e)
        {
            if (currentItem == Item.Polyline)
            {
                x = e.X;
                y = e.Y;
                points.Add(new PointF(x, y));
                figure = FigureFactory.SetParameters(figure, points,
                                                    Convert.ToInt32(nudLineThickness.Value),
                                                    currentLineColor, currentLineType);                
                PolyLineDrawer drawer = new PolyLineDrawer((PolyLine)figure, pbCanvas);
                drawer.Draw();
            }
            else if (currentItem == Item.Polygon)
            {
                x = e.X;
                y = e.Y;

                //polygone = new Polygone(Convert.ToInt32(nudVertexCount.Value),
                //                    Convert.ToInt32(nudVertexCount.Value),
                //                    currentLineColor,
                //                    currentLineType,
                //                    currentFillColor);                              
                if (polygone.points.Count < polygone.PointsCount)
                {
                    polygone.Add(x, y);
                }
                else if (polygone.points.Count == polygone.PointsCount)
                {
                    PolygoneDrawer polygoneDrawer = new PolygoneDrawer(polygone, pbCanvas);
                    polygoneDrawer.Draw();
                    currentItem = Item.Cursor;
                }
            }
            else
            {
                polyLine = new PolyLine();
                figure = null;
                points.Clear();
            }
        }

        private void buttonPolyLine_Click(object sender, EventArgs e)
        {
            currentItem = Item.Polyline;
            figure = FigureFactory.CreateFigure(Item.Polyline);
            points = new List<PointF>();
        }

        private void cbLineType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbLineType.Text)
            {
                case "Сплошная":
                    currentLineType = LineType.Solid;
                    break;
                case "Пунктир":
                    currentLineType = LineType.Dashed;
                    break;
                default:
                    throw new Exception("Неверное значение типа линии.");
            }
        }

        private void buttonCursor_Click(object sender, EventArgs e)
        {
            currentItem = Item.Cursor;
        }

        private void buttonCircle_Click(object sender, EventArgs e)
        {
            currentItem = Item.Circle;
        }

        private void buttonEllipse_Click(object sender, EventArgs e)
        {
            currentItem = Item.Ellipse;
        }

        private void buttonClearCanvas_Click(object sender, EventArgs e)
        {
            currentItem = Item.Cursor;
            pbCanvas.Image = null;
        }

        private void pbCanvas_SizeChanged(object sender, EventArgs e)
        {
            pbCanvas.Invalidate();
        }

        private void pbCanvas_Paint(object sender, PaintEventArgs e)
        {
            pbCanvas.Invalidate();
        }

        private void buttonPolygone_Click(object sender, EventArgs e)
        {
            currentItem = Item.Polygon;
            

        }

        private void pbCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (draggedPiece != null)
            {
                if (resizing)
                {
                    if (rectProposedSize.Width > 0 && rectProposedSize.Height > 0)
                        ControlPaint.DrawReversibleFrame(rectProposedSize, this.ForeColor, FrameStyle.Dashed);
                    rectProposedSize.Width = e.X - this.startDraggingPoint.X + this.startSize.Width;
                    rectProposedSize.Height = e.Y - this.startDraggingPoint.Y + this.startSize.Height;
                    if (rectProposedSize.Width > 0 && rectProposedSize.Height > 0)
                        ControlPaint.DrawReversibleFrame(rectProposedSize, this.ForeColor, FrameStyle.Dashed);
                }

            }
        }

        private void buttonLineColor_Click(object sender, EventArgs e)
        {
            if (colorDialogLineColor.ShowDialog()==DialogResult.OK)
            {
                buttonLineColor.BackColor = colorDialogLineColor.Color;
                currentLineColor = colorDialogLineColor.Color;
            }
        }

        private void buttonFillColor_Click(object sender, EventArgs e)
        {
            if(colorDialogLineColor.ShowDialog()==DialogResult.OK)
            {
                buttonFillColor.BackColor = colorDialogLineColor.Color;
                currentFillColor = colorDialogLineColor.Color;
            }
        }
    }
}
