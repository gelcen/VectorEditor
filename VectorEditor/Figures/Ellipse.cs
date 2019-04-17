using Newtonsoft.Json;

namespace VectorEditor.Figures
{
    /// <inheritdoc />
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
            LineProperties = new LineProperties();
            Points = new Points(2);
            FillProperty = new FillProperty();
        }
    }
}
