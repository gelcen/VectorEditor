using System.Collections.Generic;
using System.Drawing;

namespace VectorEditor.Figures
{
    public class Points
    {
        private List<PointF> _points = new List<PointF>();

        private readonly int _count;

        public Points(int count = 0)
        {
            _count = count;
        }

        public IReadOnlyList<PointF> GetPoints()
        {
            return _points;
        }

        public void AddPoint(PointF point)
        {
            if (IsInRange())
            {
                _points.Add(point);
            }
        }

        public void RemoveLast()
        {
            _points.RemoveAt(_points.Count - 1);
        }

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
