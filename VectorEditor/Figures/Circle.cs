using Newtonsoft.Json;

namespace VectorEditor.Figures
{
    /// <inheritdoc />
    /// <summary>
    /// Класс для окружности
    /// </summary>
    [JsonObject(MemberSerialization.Fields)]
    public class Circle:FillableFigure
    {
        /// <summary>
        /// Конструктор класса окружности
        /// </summary>
        public Circle()
        {
            _lineProperties = new LineProperties();
            _points = new Points(2);
            _fillProperty = new FillProperty();
        }
    }
}
