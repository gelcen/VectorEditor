using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VectorEditor.Figures;
using VectorEditor.View;

namespace VectorEditor.Model
{
    /// <summary>
    /// Интерфейс для Представления
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Канва
        /// </summary>
        PictureBox Canvas
        {
            get;
        }

        /// <summary>
        /// Параметры
        /// </summary>
        FigureParameters FigureParameters
        {
            get;
        }

        /// <summary>
        /// Установка текущего инструмента
        /// </summary>
        /// <param name="tool"></param>
        void SetTool(Item tool);

    }
}
