using System;
using System.Drawing;
using System.Windows.Forms;

namespace VectorEditor.Presenter
{
    /// <summary>
    /// Базовый класс обработчика. 
    /// Используется для композиции в 
    /// классе-обработчике создания фигур 
    /// и в классе-обработчике курсора.
    /// </summary>
    public class BaseHandler:IHandler
    {
        /// <summary>
        /// Обновление канвы
        /// </summary>
        public Action CanvasRefresh { get; set; }

        /// <summary>
        /// Делагат для события Mouse Down
        /// </summary>
        public Action<object, MouseEventArgs> MouseDown { get; set; }

        /// <summary>
        /// Делагат для события Mouse Up
        /// </summary>
        public Action<object, MouseEventArgs> MouseUp { get; set; }

        /// <summary>
        /// Делагат для события Mouse Move
        /// </summary>
        public Action<object, MouseEventArgs> MouseMove { get; set; }

        /// <summary>
        /// Делегат для рисовки
        /// </summary>
        public Action<Graphics> Draw { get; set; }
    }
}
