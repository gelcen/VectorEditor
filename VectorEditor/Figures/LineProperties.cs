using Newtonsoft.Json;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace VectorEditor.Figures
{
    /// <summary>
    /// Класс для хранения параметров линии
    /// </summary>
    [JsonObject(MemberSerialization.Fields)]
    public class LineProperties
    {
        public Color Color
        {
            get;
            set;
        }

        public int Thickness
        {
            get;
            set;
        }

        public DashStyle Style
        {
            get;
            set;
        }

        /// <summary>
        /// Конструктор без аргументов
        /// </summary>
        public LineProperties()
        {
            Color = Color.Black;
            Thickness = 1;
            Style = DashStyle.Solid;
        }

        /// <summary>
        /// Конструктор с аргументами
        /// </summary>
        /// <param name="lineColor">Цвет линии</param>
        /// <param name="thickness">Толщина линии</param>
        /// <param name="style">Стиль линии</param>
        public LineProperties(Color lineColor, int thickness, DashStyle style)
        {
            Color = lineColor;
            Thickness = thickness;
            Style = style;
        }
    }
}
