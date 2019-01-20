using Newtonsoft.Json;

namespace VectorEditor.Figures
{
    /// <summary>
    /// Класс полилинии 
    /// </summary>
    [JsonObject(MemberSerialization.Fields)]
    public class Polyline: BaseFigure
    {
        /// <summary>
        /// Конструктор класса полилинии 
        /// </summary>
        public Polyline()
        {
            _lineProperties = new LineProperties();
            _points = new Points();
        }        

    }
}
