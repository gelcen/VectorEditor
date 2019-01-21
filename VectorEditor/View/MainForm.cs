using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using VectorEditor.Drawers;
using VectorEditor.Figures;
using VectorEditor.Model;
using VectorEditor.Presenter;
using VectorEditor.UndoRedo;
using VectorEditor.View;

namespace VectorEditor
{
    public partial class MainForm : Form, IView, IObserver
    {
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
            set
            {
                _figureParameters = value;
                nudLineThickness.Value = _figureParameters.LineThickness;
                buttonLineColor.BackColor = _figureParameters.LineColor;
                buttonFillColor.BackColor = _figureParameters.FillColor;
                cbLineType.SelectedIndex = _figureParameters.LineType;
            }
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

        private UndoRedoStack _undoRedoStack;

        public UndoRedoStack CommandStack
        {
            set
            {
                _undoRedoStack = value;
            }
        }

        /// <summary>
        /// Событие выбора инструмента
        /// </summary>
        public event EventHandler<Item> ToolPicked;

        public event EventHandler<FigureParameters> ParametersChanged;

        public event EventHandler CanvasCleared;

        public event EventHandler FiguresDeleted;

        public event EventHandler FigureCopied;

        public event EventHandler UndoPressed;

        public event EventHandler RedoPressed;

        public event EventHandler<FileLoadedEventArgs> FileLoaded;

        #endregion

        private void OnFileLoaded(FileLoadedEventArgs e)
        {
            EventHandler<FileLoadedEventArgs> handler = FileLoaded;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void OnRedoPressed()
        {
            EventHandler handler = RedoPressed;

            if (handler != null)
            {
                handler(this, null);
            }
        }

        private void OnUndoPressed()
        {
            EventHandler handler = UndoPressed;

            if (handler != null)
            {
                handler(this, null);
            }
        }

        private void OnFigureCopied()
        {
            EventHandler handler = FigureCopied;

            if (handler != null)
            {
                handler(this, null);
            }
        }

        private void OnFiguresDeleted()
        {
            EventHandler handler = FiguresDeleted;

            if (handler != null)
            {
                handler(this, null);
            }
        }

        private void OnCanvasCleared()
        {
            EventHandler handler = CanvasCleared;

            if (handler != null)
            {
                handler(this, null);
            }
        }

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
            if(handler != null)
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
            _figureParameters.FillColor = Color.Transparent;
            _figureParameters.LineThickness = 1;
            _figureParameters.LineType = 0;

            
            pbCanvas.Parent = this;

        }

        /// <summary>
        /// Обработчик события нажатия на кнопку очистки канвы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClearCanvas_Click(object sender, EventArgs e)
        {
            //pbCanvas.Image = null;
            OnCanvasCleared();
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

        private void buttonCursor_Click(object sender, EventArgs e)
        {
            OnToolPicked(Item.Cursor);
        }

        private void buttonLine_Click(object sender, EventArgs e)
        {
            OnToolPicked(Item.Line);
        }

        private void buttonPolyLine_Click(object sender, EventArgs e)
        {
            OnToolPicked(Item.Polyline);
        }

        private void buttonCircle_Click(object sender, EventArgs e)
        {
            OnToolPicked(Item.Circle);
        }

        private void buttonEllipse_Click(object sender, EventArgs e)
        {
            OnToolPicked(Item.Ellipse);
        }

        private void buttonPolygone_Click(object sender, EventArgs e)
        {
            OnToolPicked(Item.Polygon);
        }

        #region Изменение параметров фигуры

        private void nudLineThickness_ValueChanged(object sender, EventArgs e)
        {
            _figureParameters.LineThickness = Convert.ToInt32(nudLineThickness.Value);
            OnParametersChanged(_figureParameters);
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
                CurrentHandler.MouseDownDelegate(sender, e);                
            }
        }

        private void pbCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (CurrentHandler != null)
            {
                CurrentHandler.MouseMoveDelegate(sender, e);
            }
        }

        private void pbCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (CurrentHandler != null)
            {
                CurrentHandler.MouseUpDelegate(sender, e);
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
        }

        #endregion        

        //IObserver
        public void Update(List<BaseFigure> figures)
        {
            _figures = figures;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            OnToolPicked(Item.Cursor);
        }

        #region Обработчики нажатий по пунктам меню

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnFiguresDeleted();
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnFigureCopied();
        }

        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnUndoPressed();
        }

        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnRedoPressed();
        }

        #endregion

        /// <summary>
        /// Обработчик события нажатия на пункт меню "Сохранить в PNG"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exportToPngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _saveToPNGDialog = new SaveFileDialog();
            _saveToPNGDialog.Title = "Сохранить в PNG";
            _saveToPNGDialog.OverwritePrompt = true;
            _saveToPNGDialog.CheckPathExists = true;
            _saveToPNGDialog.Filter = "Image Files(*.PNG)|*.PNG";
            _saveToPNGDialog.ShowHelp = true;

            PictureBox pictureBox = new PictureBox();
            pictureBox.Size = pbCanvas.Size;
            Bitmap bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            Graphics bitmapGraphics = Graphics.FromImage(bitmap);

            foreach (var figure in _figures)
            {
                FigureDrawer.DrawFigure(figure, bitmapGraphics);
            }

            pictureBox.Image = bitmap;
            if (_saveToPNGDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox.Image.Save(_saveToPNGDialog.FileName);
                }
                catch (Exception)
                {
                    throw new Exception();
                }
            }
        }

        /// <summary>
        /// Обработчик события нажатия на пункт меню "Сохранить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Saver saver = new Saver();
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    saver.SaveToFile(_figures, 
                                     _undoRedoStack.UndoStack,
                                     _undoRedoStack.RedoStack,
                                     saveFileDialog.FileName);
                }
                catch (Exception)
                {
                    throw new Exception();
                }
            }
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Saver saver = new Saver();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileLoadedEventArgs fileLoadedEventArgs = new FileLoadedEventArgs();
                    fileLoadedEventArgs = saver.OpenFromFile(openFileDialog.FileName);
                    OnFileLoaded(fileLoadedEventArgs);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true && e.KeyCode == Keys.Z)
            {
                OnUndoPressed();
            }
            if (e.Control == true && e.KeyCode == Keys.Y)
            {
                OnRedoPressed();
            }
            if (e.Control == true && e.KeyCode == Keys.C)
            {
                OnFigureCopied();
            }
            if (e.KeyCode == Keys.Delete)
            {
                OnFiguresDeleted();
            }

        }
    }
}
