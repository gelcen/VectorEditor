using Newtonsoft.Json;

namespace VectorEditor.Figures
{
    /// <summary>
    /// Класс для эллипса
    /// </summary>
    [JsonObject(MemberSerialization.Fields)]
    public class Ellipse:FillableFigure
    {
        /// <summary>
        /// Конструктор класса эллипс
        /// </summary>
        public Ellipse()
        {
            _lineProperties = new LineProperties();
            _points = new Points(2);
            _fillProperty = new FillProperty();
        }

    }
}
