using Newtonsoft.Json;

namespace SDK
{
    /// <inheritdoc />
    /// <summary>
    /// Базовый класс для фигур с заполнением
    /// </summary>
    [JsonObject(MemberSerialization.Fields)]
    public abstract class FillableFigure: BaseFigure
    {
        /// <summary>
        /// Цвет заполнения
        /// </summary>
        protected FillProperty _fillProperty;

        /// <summary>
        /// Свойство для цвета заполнения
        /// </summary>
        public FillProperty FillProperty
        {
            get
            {
                return _fillProperty;
            }
            set
            {
                _fillProperty = value;
            }
        }
    }
}
