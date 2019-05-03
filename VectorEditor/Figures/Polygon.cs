﻿using Newtonsoft.Json;

namespace VectorEditor.Figures
{
    /// <inheritdoc />
    /// <summary>
    /// Класс Полигон
    /// </summary>
    [JsonObject(MemberSerialization.Fields)]
    public class Polygon:FillableFigure
    {
        /// <summary>
        /// Конструктор класса полигона
        /// </summary>
        public Polygon()
        {
            _lineProperties = new LineProperties();
            _points = new Points(0, 3);
            _fillProperty = new FillProperty();
        }
    }
}
