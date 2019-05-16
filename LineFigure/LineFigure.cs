using Newtonsoft.Json;
using SDK;

namespace LineFigure
{
    /// <inheritdoc />
    /// <summary>
    /// Класс для линии
    /// </summary>
    [JsonObject(MemberSerialization.Fields)]
    public class LineFigure:BaseFigure
    {
        /// <summary>
        /// Конструктор класса для линии
        /// </summary>
        public LineFigure()
        {
            _lineProperties = new LineProperties();
            _points = new Points(2);
        }
    }
}
