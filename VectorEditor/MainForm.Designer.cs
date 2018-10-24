﻿namespace VectorEditor
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
            this.pbCanvas = new System.Windows.Forms.PictureBox();
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
            this.nudVertexCount = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.pbCanvas)).BeginInit();
            this.gpFigures.SuspendLayout();
            this.gpProps.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLineThickness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVertexCount)).BeginInit();
            this.SuspendLayout();
            // 
            // pbCanvas
            // 
            this.pbCanvas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbCanvas.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pbCanvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbCanvas.Cursor = System.Windows.Forms.Cursors.Default;
            this.pbCanvas.Location = new System.Drawing.Point(3, 11);
            this.pbCanvas.Margin = new System.Windows.Forms.Padding(2);
            this.pbCanvas.Name = "pbCanvas";
            this.pbCanvas.Size = new System.Drawing.Size(648, 521);
            this.pbCanvas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCanvas.TabIndex = 0;
            this.pbCanvas.TabStop = false;
            this.pbCanvas.Click += new System.EventHandler(this.pbCanvas_Click);
            this.pbCanvas.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbCanvas_MouseClick);
            this.pbCanvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbCanvas_MouseDown);
            this.pbCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbCanvas_MouseMove);
            this.pbCanvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbCanvas_MouseUp);
            // 
            // gpFigures
            // 
            this.gpFigures.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gpFigures.Controls.Add(this.nudVertexCount);
            this.gpFigures.Controls.Add(this.buttonClearCanvas);
            this.gpFigures.Controls.Add(this.buttonEllipse);
            this.gpFigures.Controls.Add(this.buttonCircle);
            this.gpFigures.Controls.Add(this.buttonPolygone);
            this.gpFigures.Controls.Add(this.buttonLine);
            this.gpFigures.Controls.Add(this.buttonPolyLine);
            this.gpFigures.Controls.Add(this.buttonCursor);
            this.gpFigures.Location = new System.Drawing.Point(668, 11);
            this.gpFigures.Margin = new System.Windows.Forms.Padding(2);
            this.gpFigures.Name = "gpFigures";
            this.gpFigures.Padding = new System.Windows.Forms.Padding(2);
            this.gpFigures.Size = new System.Drawing.Size(227, 180);
            this.gpFigures.TabIndex = 1;
            this.gpFigures.TabStop = false;
            this.gpFigures.Text = "Инструменты";
            // 
            // buttonClearCanvas
            // 
            this.buttonClearCanvas.Location = new System.Drawing.Point(117, 85);
            this.buttonClearCanvas.Name = "buttonClearCanvas";
            this.buttonClearCanvas.Size = new System.Drawing.Size(107, 31);
            this.buttonClearCanvas.TabIndex = 6;
            this.buttonClearCanvas.Text = "Очистить канву";
            this.buttonClearCanvas.UseVisualStyleBackColor = true;
            this.buttonClearCanvas.Click += new System.EventHandler(this.buttonClearCanvas_Click);
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
            this.buttonEllipse.Click += new System.EventHandler(this.buttonEllipse_Click);
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
            this.buttonCircle.Click += new System.EventHandler(this.buttonCircle_Click);
            // 
            // buttonPolygone
            // 
            this.buttonPolygone.Location = new System.Drawing.Point(4, 120);
            this.buttonPolygone.Margin = new System.Windows.Forms.Padding(2);
            this.buttonPolygone.Name = "buttonPolygone";
            this.buttonPolygone.Size = new System.Drawing.Size(107, 31);
            this.buttonPolygone.TabIndex = 3;
            this.buttonPolygone.Text = "Многоугольник";
            this.buttonPolygone.UseVisualStyleBackColor = true;
            this.buttonPolygone.Click += new System.EventHandler(this.buttonPolygone_Click);
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
            this.buttonLine.Click += new System.EventHandler(this.buttonLine_Click);
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
            this.buttonPolyLine.Click += new System.EventHandler(this.buttonPolyLine_Click);
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
            this.buttonCursor.Click += new System.EventHandler(this.buttonCursor_Click);
            // 
            // gpProps
            // 
            this.gpProps.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gpProps.Controls.Add(this.cbLineType);
            this.gpProps.Controls.Add(this.buttonFillColor);
            this.gpProps.Controls.Add(this.buttonLineColor);
            this.gpProps.Controls.Add(this.nudLineThickness);
            this.gpProps.Controls.Add(this.label4);
            this.gpProps.Controls.Add(this.label3);
            this.gpProps.Controls.Add(this.label2);
            this.gpProps.Controls.Add(this.label1);
            this.gpProps.Location = new System.Drawing.Point(668, 195);
            this.gpProps.Margin = new System.Windows.Forms.Padding(2);
            this.gpProps.Name = "gpProps";
            this.gpProps.Padding = new System.Windows.Forms.Padding(2);
            this.gpProps.Size = new System.Drawing.Size(227, 155);
            this.gpProps.TabIndex = 2;
            this.gpProps.TabStop = false;
            this.gpProps.Text = "Свойства ";
            // 
            // cbLineType
            // 
            this.cbLineType.FormattingEnabled = true;
            this.cbLineType.Items.AddRange(new object[] {
            "Пунктир",
            "Сплошная"});
            this.cbLineType.Location = new System.Drawing.Point(95, 111);
            this.cbLineType.Margin = new System.Windows.Forms.Padding(2);
            this.cbLineType.Name = "cbLineType";
            this.cbLineType.Size = new System.Drawing.Size(79, 21);
            this.cbLineType.TabIndex = 7;
            this.cbLineType.Text = "Выбрать";
            this.cbLineType.SelectedIndexChanged += new System.EventHandler(this.cbLineType_SelectedIndexChanged);
            // 
            // buttonFillColor
            // 
            this.buttonFillColor.BackColor = System.Drawing.SystemColors.Window;
            this.buttonFillColor.Location = new System.Drawing.Point(95, 79);
            this.buttonFillColor.Margin = new System.Windows.Forms.Padding(2);
            this.buttonFillColor.Name = "buttonFillColor";
            this.buttonFillColor.Size = new System.Drawing.Size(78, 23);
            this.buttonFillColor.TabIndex = 6;
            this.buttonFillColor.UseVisualStyleBackColor = false;
            this.buttonFillColor.Click += new System.EventHandler(this.buttonFillColor_Click);
            // 
            // buttonLineColor
            // 
            this.buttonLineColor.BackColor = System.Drawing.Color.Black;
            this.buttonLineColor.Location = new System.Drawing.Point(95, 48);
            this.buttonLineColor.Margin = new System.Windows.Forms.Padding(2);
            this.buttonLineColor.Name = "buttonLineColor";
            this.buttonLineColor.Size = new System.Drawing.Size(78, 23);
            this.buttonLineColor.TabIndex = 5;
            this.buttonLineColor.UseVisualStyleBackColor = false;
            this.buttonLineColor.Click += new System.EventHandler(this.buttonLineColor_Click);
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
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 111);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Тип линии:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 84);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Цвет заливки:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 53);
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
            // nudVertexCount
            // 
            this.nudVertexCount.Location = new System.Drawing.Point(117, 122);
            this.nudVertexCount.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.nudVertexCount.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudVertexCount.Name = "nudVertexCount";
            this.nudVertexCount.Size = new System.Drawing.Size(47, 20);
            this.nudVertexCount.TabIndex = 7;
            this.nudVertexCount.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 543);
            this.Controls.Add(this.gpProps);
            this.Controls.Add(this.gpFigures);
            this.Controls.Add(this.pbCanvas);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "VectorEditor";
            ((System.ComponentModel.ISupportInitialize)(this.pbCanvas)).EndInit();
            this.gpFigures.ResumeLayout(false);
            this.gpProps.ResumeLayout(false);
            this.gpProps.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLineThickness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVertexCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbCanvas;
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
        private System.Windows.Forms.NumericUpDown nudVertexCount;
    }
}

