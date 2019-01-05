namespace VectorEditor
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
            this.buttonClearCanvas = new System.Windows.Forms.Button();
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
            this.pbCanvas = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.соToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьКакToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.правкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.копироватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отменитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вернутьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gpFigures.SuspendLayout();
            this.gpProps.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLineThickness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCanvas)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpFigures
            // 
            this.gpFigures.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.gpFigures.Controls.Add(this.buttonClearCanvas);
            this.gpFigures.Controls.Add(this.buttonEllipse);
            this.gpFigures.Controls.Add(this.buttonCircle);
            this.gpFigures.Controls.Add(this.buttonPolygone);
            this.gpFigures.Controls.Add(this.buttonLine);
            this.gpFigures.Controls.Add(this.buttonPolyLine);
            this.gpFigures.Controls.Add(this.buttonCursor);
            this.gpFigures.Location = new System.Drawing.Point(680, 20);
            this.gpFigures.Name = "gpFigures";
            this.gpFigures.Size = new System.Drawing.Size(340, 277);
            this.gpFigures.TabIndex = 1;
            this.gpFigures.TabStop = false;
            this.gpFigures.Text = "Инструменты";
            // 
            // buttonClearCanvas
            // 
            this.buttonClearCanvas.Location = new System.Drawing.Point(176, 131);
            this.buttonClearCanvas.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonClearCanvas.Name = "buttonClearCanvas";
            this.buttonClearCanvas.Size = new System.Drawing.Size(160, 48);
            this.buttonClearCanvas.TabIndex = 6;
            this.buttonClearCanvas.Text = "Очистить канву";
            this.buttonClearCanvas.UseVisualStyleBackColor = true;
            this.buttonClearCanvas.Click += new System.EventHandler(this.buttonClearCanvas_Click);
            // 
            // buttonEllipse
            // 
            this.buttonEllipse.Location = new System.Drawing.Point(176, 78);
            this.buttonEllipse.Name = "buttonEllipse";
            this.buttonEllipse.Size = new System.Drawing.Size(160, 48);
            this.buttonEllipse.TabIndex = 5;
            this.buttonEllipse.Text = "Эллипс";
            this.buttonEllipse.UseVisualStyleBackColor = true;
            this.buttonEllipse.Click += new System.EventHandler(this.buttonEllipse_Click);
            // 
            // buttonCircle
            // 
            this.buttonCircle.Location = new System.Drawing.Point(6, 131);
            this.buttonCircle.Name = "buttonCircle";
            this.buttonCircle.Size = new System.Drawing.Size(160, 48);
            this.buttonCircle.TabIndex = 4;
            this.buttonCircle.Text = "Окружность";
            this.buttonCircle.UseVisualStyleBackColor = true;
            this.buttonCircle.Click += new System.EventHandler(this.buttonCircle_Click);
            // 
            // buttonPolygone
            // 
            this.buttonPolygone.Location = new System.Drawing.Point(6, 185);
            this.buttonPolygone.Name = "buttonPolygone";
            this.buttonPolygone.Size = new System.Drawing.Size(160, 48);
            this.buttonPolygone.TabIndex = 3;
            this.buttonPolygone.Text = "Многоугольник";
            this.buttonPolygone.UseVisualStyleBackColor = true;
            this.buttonPolygone.Click += new System.EventHandler(this.buttonPolygone_Click);
            // 
            // buttonLine
            // 
            this.buttonLine.Location = new System.Drawing.Point(176, 25);
            this.buttonLine.Name = "buttonLine";
            this.buttonLine.Size = new System.Drawing.Size(160, 48);
            this.buttonLine.TabIndex = 2;
            this.buttonLine.Text = "Линия";
            this.buttonLine.UseVisualStyleBackColor = true;
            this.buttonLine.Click += new System.EventHandler(this.buttonLine_Click);
            // 
            // buttonPolyLine
            // 
            this.buttonPolyLine.Location = new System.Drawing.Point(6, 78);
            this.buttonPolyLine.Name = "buttonPolyLine";
            this.buttonPolyLine.Size = new System.Drawing.Size(160, 48);
            this.buttonPolyLine.TabIndex = 1;
            this.buttonPolyLine.Text = "Полилиния";
            this.buttonPolyLine.UseVisualStyleBackColor = true;
            this.buttonPolyLine.Click += new System.EventHandler(this.buttonPolyLine_Click);
            // 
            // buttonCursor
            // 
            this.buttonCursor.Location = new System.Drawing.Point(6, 25);
            this.buttonCursor.Name = "buttonCursor";
            this.buttonCursor.Size = new System.Drawing.Size(160, 48);
            this.buttonCursor.TabIndex = 0;
            this.buttonCursor.Text = "Указатель";
            this.buttonCursor.UseVisualStyleBackColor = true;
            this.buttonCursor.Click += new System.EventHandler(this.buttonCursor_Click);
            // 
            // gpProps
            // 
            this.gpProps.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.gpProps.Controls.Add(this.cbLineType);
            this.gpProps.Controls.Add(this.buttonFillColor);
            this.gpProps.Controls.Add(this.buttonLineColor);
            this.gpProps.Controls.Add(this.nudLineThickness);
            this.gpProps.Controls.Add(this.label4);
            this.gpProps.Controls.Add(this.label3);
            this.gpProps.Controls.Add(this.label2);
            this.gpProps.Controls.Add(this.label1);
            this.gpProps.Location = new System.Drawing.Point(686, 303);
            this.gpProps.Name = "gpProps";
            this.gpProps.Size = new System.Drawing.Size(340, 238);
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
            this.cbLineType.Location = new System.Drawing.Point(142, 171);
            this.cbLineType.Name = "cbLineType";
            this.cbLineType.Size = new System.Drawing.Size(116, 28);
            this.cbLineType.TabIndex = 7;
            this.cbLineType.SelectedIndexChanged += new System.EventHandler(this.cbLineType_SelectedIndexChanged);
            // 
            // buttonFillColor
            // 
            this.buttonFillColor.BackColor = System.Drawing.SystemColors.Window;
            this.buttonFillColor.Location = new System.Drawing.Point(142, 122);
            this.buttonFillColor.Name = "buttonFillColor";
            this.buttonFillColor.Size = new System.Drawing.Size(117, 35);
            this.buttonFillColor.TabIndex = 6;
            this.buttonFillColor.UseVisualStyleBackColor = false;
            this.buttonFillColor.Click += new System.EventHandler(this.buttonFillColor_Click);
            // 
            // buttonLineColor
            // 
            this.buttonLineColor.BackColor = System.Drawing.Color.Black;
            this.buttonLineColor.Location = new System.Drawing.Point(142, 74);
            this.buttonLineColor.Name = "buttonLineColor";
            this.buttonLineColor.Size = new System.Drawing.Size(117, 35);
            this.buttonLineColor.TabIndex = 5;
            this.buttonLineColor.UseVisualStyleBackColor = false;
            this.buttonLineColor.Click += new System.EventHandler(this.buttonLineColor_Click);
            // 
            // nudLineThickness
            // 
            this.nudLineThickness.Location = new System.Drawing.Point(142, 31);
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
            this.nudLineThickness.Size = new System.Drawing.Size(74, 26);
            this.nudLineThickness.TabIndex = 4;
            this.nudLineThickness.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLineThickness.ValueChanged += new System.EventHandler(this.nudLineThickness_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 171);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Тип линии:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Цвет заливки:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Цвет линии:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Толщина линии:";
            // 
            // btnHeightResize
            // 
            this.btnHeightResize.Location = new System.Drawing.Point(315, 537);
            this.btnHeightResize.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnHeightResize.Name = "btnHeightResize";
            this.btnHeightResize.Size = new System.Drawing.Size(18, 18);
            this.btnHeightResize.TabIndex = 3;
            this.btnHeightResize.UseVisualStyleBackColor = true;
            this.btnHeightResize.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnHeightResize_MouseMove);
            // 
            // btnDiagonalResize
            // 
            this.btnDiagonalResize.Location = new System.Drawing.Point(655, 537);
            this.btnDiagonalResize.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDiagonalResize.Name = "btnDiagonalResize";
            this.btnDiagonalResize.Size = new System.Drawing.Size(18, 18);
            this.btnDiagonalResize.TabIndex = 4;
            this.btnDiagonalResize.UseVisualStyleBackColor = true;
            this.btnDiagonalResize.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnDiagonalResize_MouseMove);
            // 
            // btnWidthResize
            // 
            this.btnWidthResize.Location = new System.Drawing.Point(655, 263);
            this.btnWidthResize.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnWidthResize.Name = "btnWidthResize";
            this.btnWidthResize.Size = new System.Drawing.Size(18, 18);
            this.btnWidthResize.TabIndex = 5;
            this.btnWidthResize.UseVisualStyleBackColor = true;
            this.btnWidthResize.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnWidthResize_MouseMove);
            // 
            // pbCanvas
            // 
            this.pbCanvas.BackColor = System.Drawing.SystemColors.HighlightText;
            this.pbCanvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbCanvas.Location = new System.Drawing.Point(13, 45);
            this.pbCanvas.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pbCanvas.Name = "pbCanvas";
            this.pbCanvas.Size = new System.Drawing.Size(643, 496);
            this.pbCanvas.TabIndex = 6;
            this.pbCanvas.TabStop = false;
            this.pbCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.pbCanvas_Paint);
            this.pbCanvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbCanvas_MouseDown);
            this.pbCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbCanvas_MouseMove);
            this.pbCanvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbCanvas_MouseUp);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.правкаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1284, 33);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.соToolStripMenuItem,
            this.сохранитьToolStripMenuItem,
            this.сохранитьToolStripMenuItem1,
            this.сохранитьКакToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(65, 29);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // соToolStripMenuItem
            // 
            this.соToolStripMenuItem.Name = "соToolStripMenuItem";
            this.соToolStripMenuItem.Size = new System.Drawing.Size(214, 30);
            this.соToolStripMenuItem.Text = "Создать";
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(214, 30);
            this.сохранитьToolStripMenuItem.Text = "Открыть";
            // 
            // сохранитьToolStripMenuItem1
            // 
            this.сохранитьToolStripMenuItem1.Name = "сохранитьToolStripMenuItem1";
            this.сохранитьToolStripMenuItem1.Size = new System.Drawing.Size(214, 30);
            this.сохранитьToolStripMenuItem1.Text = "Сохранить";
            // 
            // сохранитьКакToolStripMenuItem
            // 
            this.сохранитьКакToolStripMenuItem.Name = "сохранитьКакToolStripMenuItem";
            this.сохранитьКакToolStripMenuItem.Size = new System.Drawing.Size(214, 30);
            this.сохранитьКакToolStripMenuItem.Text = "Сохранить как";
            // 
            // правкаToolStripMenuItem
            // 
            this.правкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteToolStripMenuItem,
            this.копироватьToolStripMenuItem,
            this.отменитьToolStripMenuItem,
            this.вернутьToolStripMenuItem});
            this.правкаToolStripMenuItem.Name = "правкаToolStripMenuItem";
            this.правкаToolStripMenuItem.Size = new System.Drawing.Size(85, 29);
            this.правкаToolStripMenuItem.Text = "Правка";
            // 
            // DeleteToolStripMenuItem
            // 
            this.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem";
            this.DeleteToolStripMenuItem.Size = new System.Drawing.Size(210, 30);
            this.DeleteToolStripMenuItem.Text = "Удалить";
            this.DeleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItem_Click);
            // 
            // копироватьToolStripMenuItem
            // 
            this.копироватьToolStripMenuItem.Name = "копироватьToolStripMenuItem";
            this.копироватьToolStripMenuItem.Size = new System.Drawing.Size(210, 30);
            this.копироватьToolStripMenuItem.Text = "Копировать";
            // 
            // отменитьToolStripMenuItem
            // 
            this.отменитьToolStripMenuItem.Name = "отменитьToolStripMenuItem";
            this.отменитьToolStripMenuItem.Size = new System.Drawing.Size(210, 30);
            this.отменитьToolStripMenuItem.Text = "Отменить";
            // 
            // вернутьToolStripMenuItem
            // 
            this.вернутьToolStripMenuItem.Name = "вернутьToolStripMenuItem";
            this.вернутьToolStripMenuItem.Size = new System.Drawing.Size(210, 30);
            this.вернутьToolStripMenuItem.Text = "Вернуть";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 944);
            this.Controls.Add(this.pbCanvas);
            this.Controls.Add(this.btnWidthResize);
            this.Controls.Add(this.btnDiagonalResize);
            this.Controls.Add(this.btnHeightResize);
            this.Controls.Add(this.gpProps);
            this.Controls.Add(this.gpFigures);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VectorEditor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.gpFigures.ResumeLayout(false);
            this.gpProps.ResumeLayout(false);
            this.gpProps.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLineThickness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCanvas)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
        private System.Windows.Forms.PictureBox pbCanvas;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem соToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem сохранитьКакToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem правкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem копироватьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отменитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вернутьToolStripMenuItem;
    }
}

