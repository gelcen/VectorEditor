using Newtonsoft.Json;

namespace VectorEditor.Figures
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
