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

        /// <summary>
        /// Список точек
        /// </summary>
        List<PointF> points;

        /// <summary>
        /// Буффер для канваса, используется при изменении размеров формы
        /// </summary>
        PictureBox bufferCanvas;

        /// <summary>
        /// События прорисовки многоугольника
        /// </summary>
        event EventHandler polygoneDrawed;

        /// <summary>
        /// Текущий обработчик нажатия мышкой по канве
        /// </summary>
        MouseEventHandler currentMouseClickHandler;

        /// <summary>
        /// Отписка обработчика события нажатия на канву
        /// </summary>
        private void RemoveMouseClickHandler()
        {
            pbCanvas.MouseClick -= currentMouseClickHandler;            
        }

        /// <summary>
        /// Текущий обработчик при отпускании кнопки мышки
        /// </summary>
        MouseEventHandler currentMouseUpHandler;

        /// <summary>
        /// Отписка обработчика события отпускания кнопки мышки от канвы
        /// </summary>
        private void RemoveMouseUpHandler()
        {
            pbCanvas.MouseUp -= currentMouseUpHandler;
        }

        /// <summary>
        /// Фигура
        /// </summary>
        Figure figure;

        /// <summary>
        /// TODO: Вынести обработчики в отдельный класс с возможностью расширения
        /// </summary>

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
            bufferCanvas = new PictureBox();

            currentMouseClickHandler = MouseClickCursor;         
            pbCanvas.MouseClick += MouseClickCursor;

            currentMouseUpHandler = MouseUpCursor;
            pbCanvas.MouseUp += MouseUpCursor;

            polygoneDrawed += buttonPolygone_Click;

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
        /// Обработчик события отпускания кнопки мышки для "Курсор"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseUpCursor(object sender, MouseEventArgs e)
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
            this.draggedPiece = null;
            this.startDraggingPoint = Point.Empty;
            pbCanvas.Cursor = Cursors.Default;
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Обработчик нажатия по канве для "Курсор"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseClickCursor(object sender, MouseEventArgs e)
        {
            
        }

        /// <summary>
        /// Обработчик события отпускания кнопки мышки для "Курсор"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        /// <summary>
        /// Обработчик события нажатия на кнопку "Линия"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLine_Click(object sender, EventArgs e)
        {
            RemoveMouseUpHandler();
            pbCanvas.MouseUp += MouseUpLine;
            currentMouseUpHandler = MouseUpLine;
            currentItem = Item.Line;
        }

        /// <summary>
        /// Обработчик события отпускания кнопки мышки для инструмента "Линия"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseUpLine(object sender, MouseEventArgs e)
        {
            lx = e.X;
            ly = e.Y;
            Figure line = FigureFactory.CreateFigure(Item.Line);
            line = FigureFactory.SetParameters(line, x, y, lx, ly,
                                Convert.ToInt32(nudLineThickness.Value),
                                currentLineColor, currentLineType);
            LineDrawer lineDrawer = new LineDrawer((Line)line, pbCanvas);
            lineDrawer.Draw();
            CopyCanvas();
        }

        /// <summary>
        /// Обработчик события нажатия по канве для инструмента "Полилиния"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseClickPolyline(object sender, MouseEventArgs e)
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

        /// <summary>
        /// Обработчик события нажатия по канве для инструмента "Полигон"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseClickPolygone(object sender, MouseEventArgs e)
        {
            x = e.X;
            y = e.Y;
                           
            if (polygone.points.Count < polygone.PointsCount)
            {
                polygone.Add(x, y);
            }
            else if (polygone.points.Count == polygone.PointsCount)
            {
                PolygoneDrawer polygoneDrawer = new PolygoneDrawer(polygone, pbCanvas);
                polygoneDrawer.Draw();
                if (polygoneDrawed != null)
                {
                    polygoneDrawed(sender, e);
                }
                CopyCanvas();
                //currentItem = Item.Cursor;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
    
                polygone = new Polygone();
                polygone.SetParameters(Convert.ToInt32(nudVertexCount.Value),
                    Convert.ToInt32(nudLineThickness.Value), currentLineColor,
                    currentLineType, currentFillColor);
                if (polygone.points.Count < polygone.PointsCount)
                {
                    polygone.Add(x, y);
                }
                else if (polygone.points.Count == polygone.PointsCount)
                {
                    PolygoneDrawer polygoneDrawer = new PolygoneDrawer(polygone, pbCanvas);
                    polygoneDrawer.Draw();
                    //currentItem = Item.Cursor;
                }
            }
            else
            {
                polyLine = new PolyLine();
                figure = null;
                if (points != null)
                {
                    points.Clear();
                }
            }
        }

        /// <summary>
        /// Обработчик нажатия по кнопке "Полилиния"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPolyLine_Click(object sender, EventArgs e)
        {
            RemoveMouseClickHandler();
            RemoveMouseUpHandler();
            currentItem = Item.Polyline;

            figure = FigureFactory.CreateFigure(Item.Polyline);
            points = new List<PointF>();  
                      
            pbCanvas.MouseClick += MouseClickPolyline;
            currentMouseClickHandler = MouseClickPolyline;
        }

        /// <summary>
        /// Обработчик нажатия по кнопке "Многоугольник"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPolygone_Click(object sender, EventArgs e)
        {
            RemoveMouseClickHandler();
            RemoveMouseUpHandler();
            currentItem = Item.Polygon;

            polygone = new Polygone();
            polygone.SetParameters(Convert.ToInt32(nudVertexCount.Value),
                Convert.ToInt32(nudLineThickness.Value), currentLineColor,
                currentLineType, currentFillColor);

            pbCanvas.MouseClick += MouseClickPolygone;
            currentMouseClickHandler = MouseClickPolygone;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Обработчик события нажатия на кнопку "Указатель"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCursor_Click(object sender, EventArgs e)
        {
            RemoveMouseClickHandler();
            pbCanvas.MouseClick += MouseClickCursor;
            currentMouseClickHandler = MouseClickCursor;


            RemoveMouseUpHandler();
            pbCanvas.MouseUp += MouseUpCursor;
            currentMouseUpHandler = MouseUpCursor;

            pbCanvas.Cursor = Cursors.Default;
            currentItem = Item.Cursor;
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку "Окружность"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCircle_Click(object sender, EventArgs e)
        {
            RemoveMouseUpHandler();
            RemoveMouseClickHandler();
            pbCanvas.MouseUp += MouseUpCircle;
            currentMouseUpHandler = MouseUpCircle;
            currentItem = Item.Circle;
        }

        /// <summary>
        /// Обработчик события отпускания кнопки мышки для инструмента "Окружность"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseUpCircle(object sender, MouseEventArgs e)
        {
            lx = e.X;
            ly = e.Y;
            float rad = (float)Math.Sqrt(Math.Pow((lx - x), 2) + Math.Pow((ly - y), 2));
            Figure circle = FigureFactory.CreateFigure(Item.Circle);
            circle = FigureFactory.SetParameters(circle, x, y, rad, currentLineColor,
                                      Convert.ToInt32(nudLineThickness.Value),
                                      currentFillColor, currentLineType);
            CircleDrawer circleDrawer = new CircleDrawer((Circle)circle, pbCanvas);
            circleDrawer.Draw();
            CopyCanvas();
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку "Эллипс"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEllipse_Click(object sender, EventArgs e)
        {
            RemoveMouseUpHandler();
            RemoveMouseClickHandler();
            pbCanvas.MouseUp += MouseUpEllipse;
            currentMouseUpHandler = MouseUpEllipse;
            currentItem = Item.Ellipse;
        }

        /// <summary>
        /// Обработчик события отпускания кнопки мышки для инструмента "Эллипс"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseUpEllipse(object sender, MouseEventArgs e)
        {
            lx = e.X;
            ly = e.Y;
            Figure ellipse = FigureFactory.CreateFigure(Item.Ellipse);
            ellipse = FigureFactory.SetParameters(ellipse, x, y, lx - x, ly - y,
                                          currentLineColor,
                                          currentFillColor,
                                          Convert.ToInt32(nudLineThickness.Value),
                                          currentLineType);
            EllipseDrawer ellipseDrawer = new EllipseDrawer((Ellipse)ellipse, pbCanvas);
            ellipseDrawer.Draw();
            CopyCanvas();
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку очистки канвы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClearCanvas_Click(object sender, EventArgs e)
        {
            currentItem = Item.Cursor;
            pbCanvas.Image = null;
        }

        /// <summary>
        /// Обработчик события движения указателя мышки над канвой
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Обработчик события изменения значения количества вершин многоугольника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nudVertexCount_ValueChanged(object sender, EventArgs e)
        {
            if (polygoneDrawed != null)
            {
                polygoneDrawed(sender, e);
            }
        }

        /// <summary>
        /// Копирования изображения канваса, нужна для MainForm_SizeChanged
        /// </summary>
        private void CopyCanvas()
        {
            bufferCanvas.Image = pbCanvas.Image;
        }

        /// <summary>
        /// Обработчик события выбора цвета линии
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLineColor_Click(object sender, EventArgs e)
        {
            if (colorDialogLineColor.ShowDialog()==DialogResult.OK)
            {
                buttonLineColor.BackColor = colorDialogLineColor.Color;
                currentLineColor = colorDialogLineColor.Color;
            }
        }

        /// <summary>
        /// Обработчик события выбора цвета заливки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
