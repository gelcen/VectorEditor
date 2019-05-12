using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using VectorEditor.Figures;

namespace VectorEditor.Presenter
{
    /// <summary>
    /// Интерфейс для селектора фигур
    /// </summary>
    public interface IFigureSelector
    {
        
        /// <summary>
        /// Получить фигуру под точкой
        /// </summary>
        /// <param name="point">Точка</param>
        /// <param name="figures">Словарь фигур</param>
        /// <returns>Key фигуры</returns>
        int GetFigurePointOn(Point point, Dictionary<int, BaseFigure> figures);

        /// <summary>
        /// Выбор группой
        /// </summary>
        /// <param name="figures">Фигуры на канве</param>
        /// <param name="rectangle">Прямоугольник выбора</param>
        /// <returns>Выбранные фигуры</returns>
        Dictionary<int, BaseFigure> GetFiguresInRect(
                Dictionary<int, BaseFigure> figures, 
                Rectangle rectangle);

        /// <summary>
        /// Определяет находится ли точка на маркере
        /// </summary>
        /// <param name="selectedFigures">Фигуры</param>
        /// <param name="mousePoint">Точка нажатия</param>
        /// <param name="pickedPoint">Точка (маркер)</param>
        /// <param name="pickedFigureIndex">Индекс фигуры</param>
        /// <returns>Индекс маркера (точки)</returns>
        int IsPointOnMarker(Dictionary<int, BaseFigure> selectedFigures, 
                            PointF mousePoint, 
                            out PointF pickedPoint, 
                            out int pickedFigureIndex);
    }
}