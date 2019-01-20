using Newtonsoft.Json;

namespace VectorEditor.Figures
{
    /// <summary>
    /// Класс для линии
    /// </summary>
    [JsonObject(MemberSerialization.Fields)]
    public class Line:BaseFigure
    {
        /// <summary>
        /// Конструктор класса для линии
        /// </summary>
        public Line()
        {
            _lineProperties = new LineProperties();
            _points = new Points(2);
        }
    }
}
