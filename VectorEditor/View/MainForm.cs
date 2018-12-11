using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using VectorEditor.Drawers;
using VectorEditor.Figures;
using VectorEditor.Model;
using VectorEditor.Presenter;
using VectorEditor.View;

namespace VectorEditor
{
    public partial class MainForm : Form, IView
    {


        /// <summary>
        /// События прорисовки многоугольника
        /// </summary>
        event EventHandler polygoneDrawed;

        public event EventHandler<FigureCreatedEventArgs> FigureCreated;       

        private IBaseHandler _currentHandler;

        private List<BaseFigure> _figures;

        /// <summary>
        /// Текущие параметры
        /// </summary>
        private FigureParameters _figureParameters;

        #region Реализация IView
        public PictureBox Canvas
        {
            get
            {
                return pbCanvas;
            }
        }

        public FigureParameters FigureParameters
        {
            get
            {
                return _figureParameters;
            }
        }

        public void SetTool(Item tool)
        {
            throw new NotImplementedException();
        }

        public IBaseHandler CurrentHandler
        {
            get
            {
                return _currentHandler;
            }
            set
            {
                _currentHandler = value;
            }
        }

        public List<BaseFigure> Figures
        {
            get
            {
                return _figures;
            }
            set
            {
                _figures = value;
            }
        }

        /// <summary>
        /// Событие выбора инструмента
        /// </summary>
        public event EventHandler<Item> ToolPicked;

        public event EventHandler<FigureParameters> ParametersChanged;

        #endregion

        private void OnToolPicked(Item pickedItem)
        {
            EventHandler<Item> handler = ToolPicked;

            if (handler != null)
            {
                handler(this, pickedItem);
            }
        }

        private void OnParametersChanged(FigureParameters figureParameters)
        {
            EventHandler<FigureParameters> handler = ParametersChanged;
            if(handler !=null)
            {
                handler(this, figureParameters);
            }
        }

        public MainForm()
        {
            InitializeComponent();

            _figureParameters = new FigureParameters();

            this.DoubleBuffered = true;

            _figureParameters.LineColor = Color.Black;
            _figureParameters.FillColor = Color.White;
            _figureParameters.LineThickness = 1;
            _figureParameters.LineType = 0;


            pbCanvas.Parent = this;

        }

        /// <summary>
        /// Обертка для вызова обработчика события создания фигуры
        /// </summary>
        /// <param name="e"></param>
        private void OnFigureCreated(FigureCreatedEventArgs e)
        {
            EventHandler<FigureCreatedEventArgs> handler = FigureCreated;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Обработчик события изменения значения типа линии
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbLineType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _figureParameters.LineType = cbLineType.SelectedIndex;
            OnParametersChanged(_figureParameters);
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку очистки канвы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClearCanvas_Click(object sender, EventArgs e)
        {
            //pbCanvas.Image = null;
        }

        #region Изменение размера канвы
        /// <summary>
        /// Изменение размера по высоте
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHeightResize_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                btnHeightResize.Location = new Point(btnHeightResize.Location.X,
                                                     PointToClient(Cursor.Position).Y);
                pbCanvas.SetBounds(pbCanvas.Location.X, pbCanvas.Location.Y,
                                   pbCanvas.Width,
                                   btnHeightResize.Location.Y - pbCanvas.Location.Y);
                btnDiagonalResize.Location = new Point(pbCanvas.Location.X + pbCanvas.Width,
                                                       pbCanvas.Location.Y + pbCanvas.Height);
                btnWidthResize.Location = new Point(btnWidthResize.Location.X, 
                                                    pbCanvas.Location.Y + pbCanvas.Height / 2);
            }
        }

        /// <summary>
        /// Изменение размера канвы по ширине
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWidthResize_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                btnWidthResize.Location = new Point(PointToClient(Cursor.Position).X,
                                                    btnWidthResize.Location.Y);
                pbCanvas.SetBounds(pbCanvas.Location.X, pbCanvas.Location.Y,
                                   btnWidthResize.Location.X - pbCanvas.Location.X,
                                   pbCanvas.Height);
                btnDiagonalResize.Location = new Point(pbCanvas.Location.X + pbCanvas.Width,
                                                       pbCanvas.Location.Y + pbCanvas.Height);
                btnHeightResize.Location = new Point(pbCanvas.Location.X + pbCanvas.Width / 2,
                                                     btnHeightResize.Location.Y);
            }
        }

        /// <summary>
        /// Изменение размера по диагонали
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDiagonalResize_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                btnDiagonalResize.Location = PointToClient(Cursor.Position);
                btnHeightResize.Location = new Point(pbCanvas.Location.X + pbCanvas.Width / 2,
                                                     btnDiagonalResize.Location.Y);
                btnWidthResize.Location = new Point(btnDiagonalResize.Location.X, 
                                                    pbCanvas.Location.Y + pbCanvas.Height / 2);
                pbCanvas.SetBounds(pbCanvas.Location.X, pbCanvas.Location.Y,
                                   btnDiagonalResize.Location.X - pbCanvas.Location.X,
                                   btnDiagonalResize.Location.Y - pbCanvas.Location.Y);
                
            }
        }
        #endregion

        private void buttonLine_Click(object sender, EventArgs e)
        {
            OnToolPicked(Item.Line);
        }

        private void buttonPolyLine_Click(object sender, EventArgs e)
        {
            OnToolPicked(Item.Polyline);
        }

        #region Изменение параметров фигуры
        private void nudLineThickness_ValueChanged(object sender, EventArgs e)
        {
            _figureParameters.LineThickness = Convert.ToInt32(nudLineThickness.Value);
            OnParametersChanged(_figureParameters);
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
        /// Обработчик события выбора цвета линии
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLineColor_Click(object sender, EventArgs e)
        {
            if (colorDialogLineColor.ShowDialog()==DialogResult.OK)
            {
                buttonLineColor.BackColor = colorDialogLineColor.Color;
                _figureParameters.LineColor = colorDialogLineColor.Color;
                OnParametersChanged(_figureParameters);
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
                _figureParameters.FillColor = colorDialogLineColor.Color;
                OnParametersChanged(_figureParameters);
            }
        }
        #endregion

        #region Обработчики событий MouseDown, MouseMove, MouseUp, Paint

        private void pbCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (CurrentHandler != null)
            {
                CurrentHandler.MouseDown(null, e);
            }
        }

        private void pbCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (CurrentHandler != null)
            {
                CurrentHandler.MouseMove(null, e);
            }
        }

        private void pbCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (CurrentHandler != null)
            {
                CurrentHandler.MouseUp(null, e);
            }
        }

        private void pbCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            if (_figures != null)
            {
                foreach (BaseFigure figure in _figures)
                {
                    FigureDrawer.DrawFigure(figure, g);
                }
            }

            if (CurrentHandler != null)
            {
                CurrentHandler.Draw(g);
            }
            //Сделать фабрику для рисования разных фигур
        }

        #endregion

        
    }
}
