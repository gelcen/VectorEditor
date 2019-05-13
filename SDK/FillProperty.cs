using Newtonsoft.Json;
using System.Drawing;

namespace SDK
{
    /// <summary>
    /// Класс для свойства "Цвет заполнения"
    /// </summary>
    [JsonObject(MemberSerialization.Fields)]
    public class FillProperty
    {
        /// <summary>
        /// Свойство для цвета заполнения
        /// </summary>
        public Color FillColor
        {
            get;
            set;
        }

        /// <summary>
        /// Заполнять фигуру?
        /// </summary>
        public bool IsFilled
        {
            get;
            set;
        }

        /// <summary>
        /// Конструктор без аргументов
        /// </summary>
        public FillProperty()
        {
            FillColor = Color.Transparent;
            IsFilled = false;
        }

        /// <summary>
        /// Конструктор с аргументами
        /// </summary>
        /// <param name="fillColor">Цвет заливки</param>
        public FillProperty(Color fillColor, bool isFilled)
        {
            FillColor = fillColor;
            IsFilled = IsFilled;
        }
    }
}
