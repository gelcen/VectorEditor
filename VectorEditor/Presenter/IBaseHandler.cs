using System;
using System.Drawing;
using System.Windows.Forms;
using VectorEditor.Figures;
using VectorEditor.View;

namespace VectorEditor.Presenter
{
    /// <summary>
    /// Делегат для операций с мышкой
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="e"></param>
    public delegate void MouseOperation(object obj, MouseEventArgs e);

    /// <summary>
    /// Базовый интерфейс для инструментов
    /// </summary>
    public interface IBaseHandler
    {
        /// <summary>
        /// Канва
        /// </summary>
        PictureBox Canvas
        {
            get;
            set;
        }

        /// <summary>
        /// Делегат для нажатия мышки
        /// </summary>
        MouseOperation MouseDownDelegate
        {
            set;
            get;
        }

        /// <summary>
        /// Делегат для отпускания мышки
        /// </summary>
        MouseOperation MouseUpDelegate
        {
            set;
            get;
        }

        /// <summary>
        /// Делегат для движения мышкой
        /// </summary>
        MouseOperation MouseMoveDelegate
        {
            set;
            get;
        }

        /// <summary>
        /// Рисование
        /// </summary>
        /// <param name="g"></param>
        void Draw(Graphics g);
    }
}
