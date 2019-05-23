using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SDK
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
        /// Максимальное количество точек
        /// </summary>
        private readonly int _maxCount;

        /// <summary>
        /// Минимальное количество точек
        /// </summary>
        private readonly int _minCount;

        /// <summary>
        /// Конструктор класса для точек
        /// </summary>
        /// <param name="count"></param>
        public Points(int maxCount = 0, int minCount = 0)
        {
            _maxCount = maxCount;
            _minCount = minCount;
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
        public bool IsInRange()
        {
            if (_maxCount == 0)
            {
                return true;
            }

            if (_points.Count < _maxCount)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Проверят достаточно ли точек
        /// </summary>
        /// <returns></returns>
        public bool IsEnough()
        {
            if (_minCount == 0)
            {
                return true;
            }

            if (_points.Count < _minCount)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Найти расстояние между точками
        /// </summary>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        /// <returns></returns>
        public static double FindDistanceBetween(PointF pt1, PointF pt2)
        {
            var dx = pt1.X - pt2.X;
            var dy = pt1.Y - pt2.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        /// <summary>
        /// Получить прямоугольник вокруг фигуры
        /// </summary>
        /// <param name="points">Точки фигуры</param>
        /// <returns>Прямоугольник</returns>
        public Rectangle GetRectangle(IReadOnlyCollection<PointF> points)
        {
            int minX = (int)points.Min(x => x.X);
            int minY = (int)points.Min(y => y.Y);
            int maxX = (int)points.Max(x => x.X);
            int maxY = (int)points.Max(y => y.Y);
            return new Rectangle(minX, minY, Math.Abs(maxX - minX), Math.Abs(maxY - minY));
        }

        /// <summary>
        /// Получить прямоугольник вокруг фигуры
        /// </summary>
        /// <returns></returns>
        public Rectangle GetRectangle()
        {
            return GetRectangle(_points);
        }
    }
}
