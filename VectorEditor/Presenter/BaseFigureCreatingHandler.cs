using System;
using System.Drawing;
using System.Windows.Forms;
using VectorEditor.Figures;
using VectorEditor.FileManager;

namespace VectorEditor.Presenter
{
    /// <summary>
    /// Базовый класс для инструментов, создающих фигуры
    /// </summary>
    public abstract class BaseFigureCreatingHandler:IBaseHandler
    {
        /// <summary>
        /// Канва
        /// </summary>
        public abstract PictureBox Canvas { get; set; }

        /// <summary>
        /// Делегат для нажатия мышки
        /// </summary>
        public abstract MouseOperation MouseDownDelegate { get; set; }

        /// <summary>
        /// Делегат для отпускания мышки
        /// </summary>
        public abstract MouseOperation MouseUpDelegate { get; set; }

        /// <summary>
        /// Делегат для движения мышкой
        /// </summary>
        public abstract MouseOperation MouseMoveDelegate { get; set; }

        /// <summary>
        /// Параметры фигуры
        /// </summary>
        public abstract FigureParameters FigureParameters
        {
            set;
        }

        /// <summary>
        /// Событие создания фигуры
        /// </summary>
        public abstract event EventHandler<BaseFigure> FigureCreated;

        /// <summary>
        /// Рисование
        /// </summary>
        /// <param name="g"></param>
        public abstract void Draw(Graphics g);
    }
}
