using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;

namespace VectorEditor.Figures
{
    /// <summary>
    /// Класс для точек фигуры
    /// </summary>
    [JsonObject(MemberSerialization.Fields)]
    public class Points
    {
        /// <summary>
        /// Список точек
        /// </summary>
        private readonly List<PointF> _points = new List<PointF>();

        /// <summary>
        /// Количество точек
        /// </summary>
        private readonly int _count;

        /// <summary>
        /// Конструктор класса для точек
        /// </summary>
        /// <param name="count"></param>
        public Points(int count = 0)
        {
            _count = count;
        }

        /// <summary>
        /// Получить точки
        /// </summary>
        /// <returns>Возвращает PointF точки</returns>
        public IReadOnlyList<PointF> GetPoints()
        {
            return _points;
        }

        /// <summary>
        /// Добавление точки в список
        /// </summary>
        /// <param name="point"></param>
        public void AddPoint(PointF point)
        {
            if (IsInRange())
            {
                _points.Add(point);
            }
        }

        /// <summary>
        /// Удалить последнюю точку
        /// </summary>
        public void RemoveLast()
        {
            _points.RemoveAt(_points.Count - 1);
        }

        /// <summary>
        /// Заменить точку
        /// </summary>
        /// <param name="index">Индекс</param>
        /// <param name="point">Замена</param>
        public void Replace(int index, PointF point)
        {
            _points[index] = point;
        }

        /// <summary>
        /// Проверят вхождение в лимит
        /// </summary>
        /// <returns></returns>
        private bool IsInRange()
        {
            if (_count == 0)
            {
                return true;
            }

            if (_points.Count < _count)
            {
                return true;
            }

            return false;
        }
    }
}
