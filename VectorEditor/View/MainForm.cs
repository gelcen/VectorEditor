﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using VectorEditor.Drawers;
using VectorEditor.Figures;
using VectorEditor.Presenter;
using VectorEditor.UndoRedo;

namespace VectorEditor.View
{
    /// <inheritdoc cref="MainForm" />
    /// <summary>
    /// Класс главной формы
    /// </summary>
    public partial class MainForm : Form, IView
    {
        /// <summary>
        /// Список фигур
        /// </summary>
        private Dictionary<int, BaseFigure> _figures;

        /// <summary>
        /// Словарь для кнопок инструментов
        /// </summary>
        private readonly Dictionary<Control, ToolType> _toolsDictionary;

        /// <summary>
        /// Текущие параметры
        /// </summary>
        private FigureParameters _figureParameters;

        #region Реализация IView

        /// <summary>
        /// Флаг изменения
        /// </summary>
        public bool IsChanged
        {
            get;
            set;
        }

        /// <summary>
        /// Тип сохранения
        /// </summary>
        public SaveState SaveType
        {
            get;
            set;
        }

        /// <inheritdoc />
        /// <summary>
        /// Свойство для канвы
        /// </summary>
        public PictureBox Canvas
        {
            get { return pbCanvas; }
        }

        /// <inheritdoc />
        /// <summary>
        /// Список фигур
        /// </summary>
        public Dictionary<int, BaseFigure> Figures
        {
            set
            {
                _figures = value;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Параметры фигур
        /// </summary>
        public FigureParameters FigureParameters
        {
            get
            {
                return _figureParameters;
            }
            set
            {
                _figureParameters = value;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Текущий инструмент
        /// </summary>
        public IBaseHandler CurrentHandler { get; set; }

        /// <summary>
        /// Стек команд
        /// </summary>
        private UndoRedoStack _undoRedoStack;

        /// <inheritdoc />
        /// <summary>
        /// Свойство для стека команд
        /// </summary>
        public UndoRedoStack CommandStack
        {
            set
            {
                _undoRedoStack = value;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Событие выбора инструмента
        /// </summary>
        public event EventHandler<ToolType> ToolPicked;
        
        /// <inheritdoc />
        /// <summary>
        /// Событие изменения параметров фигуры
        /// </summary>
        public event EventHandler<FigureParameters> ParametersChanged;

        /// <inheritdoc />
        /// <summary>
        /// Событие нажатия на кнопку очистки канвы
        /// </summary>
        public event EventHandler CanvasCleared;

        /// <inheritdoc />
        /// <summary>
        /// Событие нажатия на кнопку удаления
        /// </summary>
        public event EventHandler FiguresDeleted;

        /// <inheritdoc />
        /// <summary>
        /// События нажатия на кнопку копирования 
        /// </summary>
        public event EventHandler FigureCopied;

        /// <inheritdoc />
        /// <summary>
        /// Событие нажатия на отмену операции
        /// </summary>
        public event EventHandler UndoPressed;

        /// <inheritdoc />
        /// <summary>
        /// События нажатия на возврат операции
        /// </summary>
        public event EventHandler RedoPressed;

        /// <inheritdoc />
        /// <summary>
        /// Событие загрузки проекта
        /// </summary>
        public event EventHandler<FileLoadedEventArgs> FileLoaded;

        /// <inheritdoc />
        /// <summary>
        /// События создания нового файла
        /// </summary>
        public event EventHandler NewProjectCreated;

        #endregion

        /// <summary>
        /// Вызов события выбора инструмента
        /// </summary>
        /// <param name="pickedToolType"></param>
        private void OnToolPicked(ToolType pickedToolType)
        {
            var handler = ToolPicked;

            handler?.Invoke(this, pickedToolType);
        }

        /// <inheritdoc />
        /// <summary>
        /// Конструктор класса формы
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            _figureParameters = new FigureParameters
            {
                LineColor = Color.Black,
                FillColor = Color.Transparent,
                LineThickness = 1,
                LineStyle = 0
            };

            pbCanvas.Parent = this;

            _toolsDictionary = new Dictionary<Control, ToolType>();
            InitTools();            
        }

        /// <summary>
        /// Инициализация словаря кнопок инструментов
        /// </summary>
        private void InitTools()
        {
            _toolsDictionary.Add(buttonCursor, ToolType.Cursor);
            _toolsDictionary.Add(buttonLine, ToolType.Line);
            _toolsDictionary.Add(buttonPolyLine, ToolType.Polyline);
            _toolsDictionary.Add(buttonCircle, ToolType.Circle);
            _toolsDictionary.Add(buttonEllipse, ToolType.Ellipse);
            _toolsDictionary.Add(buttonPolygone, ToolType.Polygon);
        }

        /// <summary>
        /// Обработчик нажатия на кнопку инструмента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolButton_Click(object sender, EventArgs e)
        {
            OnToolPicked(_toolsDictionary[(Control)sender]);
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку очистки канвы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClearCanvas_Click(object sender, EventArgs e)
        {
            CanvasCleared?.Invoke(this, e);
        }

        #region Изменение размера канвы
        /// <summary>
        /// Изменение размера по высоте
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHeightResize_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            btnHeightResize.Location = new Point(btnHeightResize.Location.X,
                PointToClient(Cursor.Position).Y);
            Canvas.SetBounds(Canvas.Location.X, Canvas.Location.Y,
                Canvas.Width,
                btnHeightResize.Location.Y - Canvas.Location.Y);
            btnDiagonalResize.Location = new Point(Canvas.Location.X + Canvas.Width,
                Canvas.Location.Y + Canvas.Height);
            btnWidthResize.Location = new Point(btnWidthResize.Location.X, 
                Canvas.Location.Y + Canvas.Height / 2);
        }

        /// <summary>
        /// Изменение размера канвы по ширине
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWidthResize_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            btnWidthResize.Location = new Point(PointToClient(Cursor.Position).X,
                btnWidthResize.Location.Y);
            Canvas.SetBounds(Canvas.Location.X, Canvas.Location.Y,
                btnWidthResize.Location.X - Canvas.Location.X,
                Canvas.Height);
            btnDiagonalResize.Location = new Point(Canvas.Location.X + Canvas.Width,
                Canvas.Location.Y + Canvas.Height);
            btnHeightResize.Location = new Point(Canvas.Location.X + Canvas.Width / 2,
                btnHeightResize.Location.Y);
        }

        /// <summary>
        /// Изменение размера по диагонали
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDiagonalResize_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            btnDiagonalResize.Location = PointToClient(Cursor.Position);
            btnHeightResize.Location = new Point(Canvas.Location.X + Canvas.Width / 2,
                btnDiagonalResize.Location.Y);
            btnWidthResize.Location = new Point(btnDiagonalResize.Location.X, 
                Canvas.Location.Y + Canvas.Height / 2);
            Canvas.SetBounds(Canvas.Location.X, Canvas.Location.Y,
                btnDiagonalResize.Location.X - Canvas.Location.X,
                btnDiagonalResize.Location.Y - Canvas.Location.Y);
        }
        #endregion

        #region Изменение параметров фигуры

        /// <summary>
        /// Обработчик изменения толщины линии
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nudLineThickness_ValueChanged(object sender, EventArgs e)
        {
            _figureParameters.LineThickness = Convert.ToInt32(nudLineThickness.Value);
            ParametersChanged?.Invoke(this, _figureParameters);
        }

        /// <summary>
        /// Обработчик события изменения значения типа линии
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbLineType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _figureParameters.LineStyle = cbLineType.SelectedIndex;
            ParametersChanged?.Invoke(this, _figureParameters);
        }

        /// <summary>
        /// Обработчик события выбора цвета линии
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLineColor_Click(object sender, EventArgs e)
        {
            if (colorDialogLineColor.ShowDialog() != DialogResult.OK) return;
            buttonLineColor.BackColor = colorDialogLineColor.Color;
            _figureParameters.LineColor = colorDialogLineColor.Color;
            ParametersChanged?.Invoke(this, _figureParameters);
        }

        /// <summary>
        /// Обработчик события выбора цвета заливки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonFillColor_Click(object sender, EventArgs e)
        {
            if (colorDialogLineColor.ShowDialog() != DialogResult.OK) return;
            buttonFillColor.BackColor = colorDialogLineColor.Color;
            _figureParameters.FillColor = colorDialogLineColor.Color;
            ParametersChanged?.Invoke(this, _figureParameters);
        }
        #endregion

        #region Обработчики событий MouseDown, MouseMove, MouseUp, Paint

        /// <summary>
        /// Обработчик события нажатия мышки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            OnMouseDown(e);
            CurrentHandler?.MouseDownDelegate(sender, e);
        }

        /// <summary>
        /// Обработчик движения мышкой
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            OnMouseMove(e);
            CurrentHandler?.MouseMoveDelegate(sender, e);
        }

        /// <summary>
        /// Обработчик отпускания нажатой мышки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            OnMouseUp(e);
            CurrentHandler?.MouseUpDelegate(sender, e);
        }

        /// <summary>
        /// Обработчик события рисования PictureBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbCanvas_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            if (_figures != null)
            {
                foreach (var figure in _figures)
                {
                    FigureDrawer.DrawFigure(figure.Value, g);
                }
            }

            CurrentHandler?.Draw(g);
        }

        #endregion        

        /// <summary>
        /// Обработчик события загрузки главной формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            KeyPreview = true;
            OnToolPicked(ToolType.Cursor);
        }

        #region Обработчики нажатий по пунктам меню

        /// <summary>
        /// Обработчик нажатия на пункт меню "Удалить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FiguresDeleted?.Invoke(this, e);
        }

        /// <summary>
        /// Обработчик нажатия на пункт меню "Копировать"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FigureCopied?.Invoke(this, e);
        }

        /// <summary>
        /// Обработчик нажатия на пункт меню "Отменить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UndoPressed?.Invoke(this, e);
        }

        /// <summary>
        /// Обработчик нажатия на пункт меню "Вернуть"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RedoPressed?.Invoke(this, e);
        }

        #endregion

        /// <summary>
        /// Обработчик события нажатия на пункт меню "Сохранить в PNG"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exportToPngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var pictureBox = new PictureBox {Size = Canvas.Size};
            var bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            var bitmapGraphics = Graphics.FromImage(bitmap);
            
            foreach (var figure in _figures)
            {
                FigureDrawer.DrawFigure(figure.Value, bitmapGraphics);
            }
            pictureBox.BackColor = Color.White;
            pictureBox.Image = bitmap;            
            if (_saveToPNGDialog.ShowDialog() != DialogResult.OK) return;
            try
            {
                pictureBox.Image.Save(_saveToPNGDialog.FileName);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Метод для сохранения
        /// </summary>
        private void SaveToFile()
        {
            var saver = new Saver();
            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;
            try
            {
                saver.SaveToFile(_undoRedoStack,
                    saveFileDialog.FileName);
                Canvas.Refresh();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Обработчик события нажатия на пункт меню "Сохранить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveToFile();
        }

        /// <summary>
        /// Обработчик события открытия файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var saver = new Saver();
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            try
            {
                var fileLoadedEventArgs = saver.OpenFromFile(openFileDialog.FileName);
                FileLoaded?.Invoke(this, fileLoadedEventArgs);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Обработчик нажатия горячих клавиш
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z)
            {
                UndoPressed?.Invoke(this, e);
            }
            if (e.Control && e.KeyCode == Keys.Y)
            {                
                RedoPressed?.Invoke(this, e);
            }
            if (e.Control && e.KeyCode == Keys.C)
            {                
                FigureCopied?.Invoke(this, e);
            }
            if (e.KeyCode == Keys.Delete)
            {
                FiguresDeleted?.Invoke(this, e);
            }

        }

        /// <summary>
        /// Обработчик нажатия на пункт создания нового файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewProjectCreated?.Invoke(this, e);
        }

        /// <summary>
        /// Обработчик выхода из программы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!IsChanged) return;
            var result = MessageBox.Show(@"Сохранить изменения?", @"Внимание",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            switch (result)
            {
                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;
                case DialogResult.Yes:
                    SaveToFile();
                    break;
                case DialogResult.No:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
