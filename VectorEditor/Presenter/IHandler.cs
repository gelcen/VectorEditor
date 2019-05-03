using System;
using System.Drawing;
using System.Windows.Forms;

namespace VectorEditor.Presenter
{
    /// <summary>
    /// Базовый интерфейс для инструментов
    /// </summary>
    public interface IHandler
    {
        /// <summary>
        /// Обновление канвы
        /// </summary>
        Action CanvasRefresh
        {
            set;
        }

        /// <summary>
        /// Делегат для нажатия мышки
        /// </summary>
        Action<object, MouseEventArgs> MouseDown
        {
            get;
            set;
        }

        /// <summary>
        /// Делегат для отпускания мышки
        /// </summary>
        Action<object, MouseEventArgs> MouseUp
        {
            get;
            set;
        }

        /// <summary>
        /// Делегат для движения мышкой
        /// </summary>
        Action<object, MouseEventArgs> MouseMove
        {
            get;
            set;
        }

        /// <summary>
        /// Рисование
        /// </summary>
        /// <param name="g"></param>
        void Draw(Graphics g);
    }
}
