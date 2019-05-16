namespace VectorEditor.FileManager
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.gpFigures = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonClearCanvas = new System.Windows.Forms.Button();
            this.cbFigures = new System.Windows.Forms.ComboBox();
            this.buttonEllipse = new System.Windows.Forms.Button();
            this.buttonCircle = new System.Windows.Forms.Button();
            this.buttonPolygone = new System.Windows.Forms.Button();
            this.buttonLine = new System.Windows.Forms.Button();
            this.buttonPolyLine = new System.Windows.Forms.Button();
            this.buttonCursor = new System.Windows.Forms.Button();
            this.gpProps = new System.Windows.Forms.GroupBox();
            this.cbLineType = new System.Windows.Forms.ComboBox();
            this.buttonFillColor = new System.Windows.Forms.Button();
            this.buttonLineColor = new System.Windows.Forms.Button();
            this.nudLineThickness = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.colorDialogLineColor = new System.Windows.Forms.ColorDialog();
            this.btnHeightResize = new System.Windows.Forms.Button();
            this.btnDiagonalResize = new System.Windows.Forms.Button();
            this.btnWidthResize = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToPngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.правкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UndoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RedoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._saveToBitmapDialog = new System.Windows.Forms.SaveFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.pbCanvas = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBoxIsFilled = new System.Windows.Forms.CheckBox();
            this.gpFigures.SuspendLayout();
            this.gpProps.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLineThickness)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCanvas)).BeginInit();
            this.SuspendLayout();
            // 
            // gpFigures
            // 
            this.gpFigures.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.gpFigures.Controls.Add(this.label5);
            this.gpFigures.Controls.Add(this.buttonClearCanvas);
            this.gpFigures.Controls.Add(this.cbFigures);
            this.gpFigures.Controls.Add(this.buttonEllipse);
            this.gpFigures.Controls.Add(this.buttonCircle);
            this.gpFigures.Controls.Add(this.buttonPolygone);
            this.gpFigures.Controls.Add(this.buttonLine);
            this.gpFigures.Controls.Add(this.buttonPolyLine);
            this.gpFigures.Controls.Add(this.buttonCursor);
            this.gpFigures.Location = new System.Drawing.Point(619, 29);
            this.gpFigures.Margin = new System.Windows.Forms.Padding(2);
            this.gpFigures.Name = "gpFigures";
            this.gpFigures.Padding = new System.Windows.Forms.Padding(2);
            this.gpFigures.Size = new System.Drawing.Size(227, 207);
            this.gpFigures.TabIndex = 1;
            this.gpFigures.TabStop = false;
            this.gpFigures.Text = "Инструменты";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(13, 178);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "Тип фигуры:";
            // 
            // buttonClearCanvas
            // 
            this.buttonClearCanvas.Location = new System.Drawing.Point(5, 121);
            this.buttonClearCanvas.Name = "buttonClearCanvas";
            this.buttonClearCanvas.Size = new System.Drawing.Size(106, 31);
            this.buttonClearCanvas.TabIndex = 6;
            this.buttonClearCanvas.Text = "Очистить канву";
            this.buttonClearCanvas.UseVisualStyleBackColor = true;
            this.buttonClearCanvas.Click += new System.EventHandler(this.ButtonClearCanvas_Click);
            // 
            // cbFigures
            // 
            this.cbFigures.FormattingEnabled = true;
            this.cbFigures.Location = new System.Drawing.Point(115, 173);
            this.cbFigures.Name = "cbFigures";
            this.cbFigures.Size = new System.Drawing.Size(107, 21);
            this.cbFigures.TabIndex = 9;
            this.cbFigures.SelectedIndexChanged += new System.EventHandler(this.cbFigures_SelectedIndexChanged);
            // 
            // buttonEllipse
            // 
            this.buttonEllipse.Location = new System.Drawing.Point(117, 51);
            this.buttonEllipse.Margin = new System.Windows.Forms.Padding(2);
            this.buttonEllipse.Name = "buttonEllipse";
            this.buttonEllipse.Size = new System.Drawing.Size(107, 31);
            this.buttonEllipse.TabIndex = 5;
            this.buttonEllipse.Text = "Эллипс";
            this.buttonEllipse.UseVisualStyleBackColor = true;
            this.buttonEllipse.Click += new System.EventHandler(this.ToolButton_Click);
            // 
            // buttonCircle
            // 
            this.buttonCircle.Location = new System.Drawing.Point(4, 85);
            this.buttonCircle.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCircle.Name = "buttonCircle";
            this.buttonCircle.Size = new System.Drawing.Size(107, 31);
            this.buttonCircle.TabIndex = 4;
            this.buttonCircle.Text = "Окружность";
            this.buttonCircle.UseVisualStyleBackColor = true;
            this.buttonCircle.Click += new System.EventHandler(this.ToolButton_Click);
            // 
            // buttonPolygone
            // 
            this.buttonPolygone.Location = new System.Drawing.Point(117, 85);
            this.buttonPolygone.Margin = new System.Windows.Forms.Padding(2);
            this.buttonPolygone.Name = "buttonPolygone";
            this.buttonPolygone.Size = new System.Drawing.Size(105, 31);
            this.buttonPolygone.TabIndex = 3;
            this.buttonPolygone.Text = "Многоугольник";
            this.buttonPolygone.UseVisualStyleBackColor = true;
            this.buttonPolygone.Click += new System.EventHandler(this.ToolButton_Click);
            // 
            // buttonLine
            // 
            this.buttonLine.Location = new System.Drawing.Point(117, 16);
            this.buttonLine.Margin = new System.Windows.Forms.Padding(2);
            this.buttonLine.Name = "buttonLine";
            this.buttonLine.Size = new System.Drawing.Size(107, 31);
            this.buttonLine.TabIndex = 2;
            this.buttonLine.Text = "Линия";
            this.buttonLine.UseVisualStyleBackColor = true;
            this.buttonLine.Click += new System.EventHandler(this.ToolButton_Click);
            // 
            // buttonPolyLine
            // 
            this.buttonPolyLine.Location = new System.Drawing.Point(4, 51);
            this.buttonPolyLine.Margin = new System.Windows.Forms.Padding(2);
            this.buttonPolyLine.Name = "buttonPolyLine";
            this.buttonPolyLine.Size = new System.Drawing.Size(107, 31);
            this.buttonPolyLine.TabIndex = 1;
            this.buttonPolyLine.Text = "Полилиния";
            this.buttonPolyLine.UseVisualStyleBackColor = true;
            this.buttonPolyLine.Click += new System.EventHandler(this.ToolButton_Click);
            // 
            // buttonCursor
            // 
            this.buttonCursor.Location = new System.Drawing.Point(4, 16);
            this.buttonCursor.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCursor.Name = "buttonCursor";
            this.buttonCursor.Size = new System.Drawing.Size(107, 31);
            this.buttonCursor.TabIndex = 0;
            this.buttonCursor.Text = "Указатель";
            this.buttonCursor.UseVisualStyleBackColor = true;
            this.buttonCursor.Click += new System.EventHandler(this.CursorButtonClicked);
            // 
            // gpProps
            // 
            this.gpProps.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.gpProps.Controls.Add(this.checkBoxIsFilled);
            this.gpProps.Controls.Add(this.label6);
            this.gpProps.Controls.Add(this.cbLineType);
            this.gpProps.Controls.Add(this.buttonFillColor);
            this.gpProps.Controls.Add(this.buttonLineColor);
            this.gpProps.Controls.Add(this.nudLineThickness);
            this.gpProps.Controls.Add(this.label4);
            this.gpProps.Controls.Add(this.label3);
            this.gpProps.Controls.Add(this.label2);
            this.gpProps.Controls.Add(this.label1);
            this.gpProps.Location = new System.Drawing.Point(619, 240);
            this.gpProps.Margin = new System.Windows.Forms.Padding(2);
            this.gpProps.Name = "gpProps";
            this.gpProps.Padding = new System.Windows.Forms.Padding(2);
            this.gpProps.Size = new System.Drawing.Size(227, 172);
            this.gpProps.TabIndex = 2;
            this.gpProps.TabStop = false;
            this.gpProps.Text = "Свойства ";
            // 
            // cbLineType
            // 
            this.cbLineType.FormattingEnabled = true;
            this.cbLineType.Items.AddRange(new object[] {
            "Solid",
            "Dash",
            "Dot",
            "DashDot",
            "DashDotDot"});
            this.cbLineType.Location = new System.Drawing.Point(94, 46);
            this.cbLineType.Margin = new System.Windows.Forms.Padding(2);
            this.cbLineType.Name = "cbLineType";
            this.cbLineType.Size = new System.Drawing.Size(79, 21);
            this.cbLineType.TabIndex = 7;
            this.cbLineType.SelectedIndexChanged += new System.EventHandler(this.CbLineType_SelectedIndexChanged);
            // 
            // buttonFillColor
            // 
            this.buttonFillColor.BackColor = System.Drawing.SystemColors.Window;
            this.buttonFillColor.Location = new System.Drawing.Point(94, 102);
            this.buttonFillColor.Margin = new System.Windows.Forms.Padding(2);
            this.buttonFillColor.Name = "buttonFillColor";
            this.buttonFillColor.Size = new System.Drawing.Size(78, 23);
            this.buttonFillColor.TabIndex = 6;
            this.buttonFillColor.UseVisualStyleBackColor = false;
            this.buttonFillColor.Click += new System.EventHandler(this.ButtonFillColor_Click);
            // 
            // buttonLineColor
            // 
            this.buttonLineColor.BackColor = System.Drawing.Color.Black;
            this.buttonLineColor.Location = new System.Drawing.Point(94, 75);
            this.buttonLineColor.Margin = new System.Windows.Forms.Padding(2);
            this.buttonLineColor.Name = "buttonLineColor";
            this.buttonLineColor.Size = new System.Drawing.Size(78, 23);
            this.buttonLineColor.TabIndex = 5;
            this.buttonLineColor.UseVisualStyleBackColor = false;
            this.buttonLineColor.Click += new System.EventHandler(this.ButtonLineColor_Click);
            // 
            // nudLineThickness
            // 
            this.nudLineThickness.Location = new System.Drawing.Point(95, 20);
            this.nudLineThickness.Margin = new System.Windows.Forms.Padding(2);
            this.nudLineThickness.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudLineThickness.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLineThickness.Name = "nudLineThickness";
            this.nudLineThickness.Size = new System.Drawing.Size(49, 20);
            this.nudLineThickness.TabIndex = 4;
            this.nudLineThickness.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLineThickness.ValueChanged += new System.EventHandler(this.NudLineThickness_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 49);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Тип линии:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 107);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Цвет заливки:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 80);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Цвет линии:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Толщина линии:";
            // 
            // btnHeightResize
            // 
            this.btnHeightResize.Location = new System.Drawing.Point(290, 540);
            this.btnHeightResize.Name = "btnHeightResize";
            this.btnHeightResize.Size = new System.Drawing.Size(12, 12);
            this.btnHeightResize.TabIndex = 3;
            this.btnHeightResize.UseVisualStyleBackColor = true;
            this.btnHeightResize.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BtnHeightResize_MouseMove);
            // 
            // btnDiagonalResize
            // 
            this.btnDiagonalResize.Location = new System.Drawing.Point(584, 540);
            this.btnDiagonalResize.Name = "btnDiagonalResize";
            this.btnDiagonalResize.Size = new System.Drawing.Size(12, 12);
            this.btnDiagonalResize.TabIndex = 4;
            this.btnDiagonalResize.UseVisualStyleBackColor = true;
            this.btnDiagonalResize.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BtnDiagonalResize_MouseMove);
            // 
            // btnWidthResize
            // 
            this.btnWidthResize.Location = new System.Drawing.Point(593, 266);
            this.btnWidthResize.Name = "btnWidthResize";
            this.btnWidthResize.Size = new System.Drawing.Size(12, 12);
            this.btnWidthResize.TabIndex = 5;
            this.btnWidthResize.UseVisualStyleBackColor = true;
            this.btnWidthResize.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BtnWidthResize_MouseMove);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.правкаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(857, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newFileToolStripMenuItem,
            this.openFileToolStripMenuItem,
            this.saveToolStripMenuItem1,
            this.exportToPngToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 22);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // newFileToolStripMenuItem
            // 
            this.newFileToolStripMenuItem.Name = "newFileToolStripMenuItem";
            this.newFileToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.newFileToolStripMenuItem.Text = "Создать";
            this.newFileToolStripMenuItem.Click += new System.EventHandler(this.NewFileToolStripMenuItem_Click);
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.openFileToolStripMenuItem.Text = "Открыть...";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.OpenFileToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem1
            // 
            this.saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
            this.saveToolStripMenuItem1.Size = new System.Drawing.Size(162, 22);
            this.saveToolStripMenuItem1.Text = "Сохранить...";
            this.saveToolStripMenuItem1.Click += new System.EventHandler(this.SaveToolStripMenuItem1_Click);
            // 
            // exportToPngToolStripMenuItem
            // 
            this.exportToPngToolStripMenuItem.Name = "exportToPngToolStripMenuItem";
            this.exportToPngToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.exportToPngToolStripMenuItem.Text = "Сохранить как...";
            this.exportToPngToolStripMenuItem.Click += new System.EventHandler(this.ExportToPngToolStripMenuItem_Click);
            // 
            // правкаToolStripMenuItem
            // 
            this.правкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteToolStripMenuItem,
            this.CopyToolStripMenuItem,
            this.UndoToolStripMenuItem,
            this.RedoToolStripMenuItem});
            this.правкаToolStripMenuItem.Name = "правкаToolStripMenuItem";
            this.правкаToolStripMenuItem.Size = new System.Drawing.Size(59, 22);
            this.правкаToolStripMenuItem.Text = "Правка";
            // 
            // DeleteToolStripMenuItem
            // 
            this.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem";
            this.DeleteToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.DeleteToolStripMenuItem.Text = "Удалить";
            this.DeleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItem_Click);
            // 
            // CopyToolStripMenuItem
            // 
            this.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem";
            this.CopyToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.CopyToolStripMenuItem.Text = "Копировать";
            this.CopyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItem_Click);
            // 
            // UndoToolStripMenuItem
            // 
            this.UndoToolStripMenuItem.Name = "UndoToolStripMenuItem";
            this.UndoToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.UndoToolStripMenuItem.Text = "Отменить";
            this.UndoToolStripMenuItem.Click += new System.EventHandler(this.UndoToolStripMenuItem_Click);
            // 
            // RedoToolStripMenuItem
            // 
            this.RedoToolStripMenuItem.Name = "RedoToolStripMenuItem";
            this.RedoToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.RedoToolStripMenuItem.Text = "Вернуть";
            this.RedoToolStripMenuItem.Click += new System.EventHandler(this.RedoToolStripMenuItem_Click);
            // 
            // _saveToBitmapDialog
            // 
            this._saveToBitmapDialog.Filter = "PNG(*.png)|*.png|JPG (*.jpg,*.jpeg)|*.jpg;*.jpeg|BMP(*.bmp)|*.bmp|GIF(*.gif)|*.gi" +
    "f|TIFF (*.tif,*.tiff)|*.tif;*.tiff";
            this._saveToBitmapDialog.ShowHelp = true;
            this._saveToBitmapDialog.Title = "Сохранить в PNG";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Векторный файл (*.vec)|*.vec|Все файлы (*.*)|*.*";
            this.saveFileDialog.ShowHelp = true;
            this.saveFileDialog.Title = "Сохранить проект";
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "vec";
            this.openFileDialog.Filter = "Векторный файл (*.vec)|*.vec|Все файлы (*.*)|*.*";
            // 
            // pbCanvas
            // 
            this.pbCanvas.BackColor = System.Drawing.Color.White;
            this.pbCanvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbCanvas.Location = new System.Drawing.Point(14, 28);
            this.pbCanvas.Name = "pbCanvas";
            this.pbCanvas.Size = new System.Drawing.Size(579, 512);
            this.pbCanvas.TabIndex = 8;
            this.pbCanvas.TabStop = false;
            this.pbCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.PbCanvas_Paint);
            this.pbCanvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PbCanvas_MouseDown);
            this.pbCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PbCanvas_MouseMove);
            this.pbCanvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PbCanvas_MouseUp);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 139);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Заливка:";
            // 
            // checkBoxIsFilled
            // 
            this.checkBoxIsFilled.AutoSize = true;
            this.checkBoxIsFilled.Location = new System.Drawing.Point(66, 139);
            this.checkBoxIsFilled.Name = "checkBoxIsFilled";
            this.checkBoxIsFilled.Size = new System.Drawing.Size(15, 14);
            this.checkBoxIsFilled.TabIndex = 9;
            this.checkBoxIsFilled.UseVisualStyleBackColor = true;
            this.checkBoxIsFilled.CheckedChanged += new System.EventHandler(this.checkBoxIsFilled_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(857, 573);
            this.Controls.Add(this.pbCanvas);
            this.Controls.Add(this.btnWidthResize);
            this.Controls.Add(this.btnDiagonalResize);
            this.Controls.Add(this.btnHeightResize);
            this.Controls.Add(this.gpProps);
            this.Controls.Add(this.gpFigures);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VectorEditor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.gpFigures.ResumeLayout(false);
            this.gpFigures.PerformLayout();
            this.gpProps.ResumeLayout(false);
            this.gpProps.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLineThickness)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCanvas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox gpFigures;
        private System.Windows.Forms.Button buttonEllipse;
        private System.Windows.Forms.Button buttonCircle;
        private System.Windows.Forms.Button buttonPolygone;
        private System.Windows.Forms.Button buttonLine;
        private System.Windows.Forms.Button buttonPolyLine;
        private System.Windows.Forms.Button buttonCursor;
        private System.Windows.Forms.GroupBox gpProps;
        private System.Windows.Forms.NumericUpDown nudLineThickness;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbLineType;
        private System.Windows.Forms.Button buttonFillColor;
        private System.Windows.Forms.Button buttonLineColor;
        private System.Windows.Forms.ColorDialog colorDialogLineColor;
        private System.Windows.Forms.Button buttonClearCanvas;
        private System.Windows.Forms.Button btnHeightResize;
        private System.Windows.Forms.Button btnDiagonalResize;
        private System.Windows.Forms.Button btnWidthResize;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exportToPngToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem правкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CopyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UndoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RedoToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog _saveToBitmapDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.PictureBox pbCanvas;
        private System.Windows.Forms.ComboBox cbFigures;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBoxIsFilled;
        private System.Windows.Forms.Label label6;
    }
}

