﻿using Newtonsoft.Json;
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
    }
}
