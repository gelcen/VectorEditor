using Newtonsoft.Json;

namespace SDK
{
    /// <summary>
    /// Базовая сущность для фигуры
    /// </summary>
    [JsonObject(MemberSerialization.Fields)]
    public abstract class BaseFigure
    {
        /// <summary>
        /// Параметры линии
        /// </summary>
        protected LineProperties _lineProperties;

        /// <summary>
        /// Точки, по которым строится фигура
        /// </summary>
        protected Points _points;

        /// <summary>
        /// Свойства для параметров линии
        /// </summary>
        public LineProperties LineProperties
        {
            get
            {
                return _lineProperties;
            }
            set
            {
                _lineProperties = value;
            }
        }

        /// <summary>
        /// Свойства для точек
        /// </summary>
        public Points Points
        {
            get
            {
                return _points;
            }
            set
            {
                _points = value;
            }
        }
    }
}
