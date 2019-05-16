using SDK;

namespace PolylineFigure
{
    /// <inheritdoc />
    /// <summary>
    /// Класс полилинии 
    /// </summary>
    public class PolylineFigure: BaseFigure
    {
        /// <summary>
        /// Конструктор класса полилинии 
        /// </summary>
        public PolylineFigure()
        {
            _lineProperties = new LineProperties();
            _points = new Points();
        }        

    }
}
