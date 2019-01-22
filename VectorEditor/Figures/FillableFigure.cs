using Newtonsoft.Json;

namespace VectorEditor.Figures
{
    /// <inheritdoc />
    /// <summary>
    /// Базовый класс для фигур с заполнением
    /// </summary>
    [JsonObject(MemberSerialization.Fields)]
    public class FillableFigure: BaseFigure
    {
        /// <summary>
        /// Цвет заполнения
        /// </summary>
        protected FillProperty _fillProperty;

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
