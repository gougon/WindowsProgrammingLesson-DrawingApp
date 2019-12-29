using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    public class Point
    {
        private double _left;
        private double _top;

        public Point()
        {
            _left = 0;
            _top = 0;
        }

        // 帶有 left 和 top 的 constructor
        public Point(double left, double top)
        {
            _left = left;
            _top = top;
        }

        // 判斷 2 Point 是否相等
        public bool IsEqual(Point point)
        {
            return _left == point.Left && _top == point.Top;
        }

        // 判斷是否在 2 point 的 range 內
        public bool IsInRange(Point startPoint, Point endPoint)
        {
            double left = startPoint.GetSmallLeft(endPoint);
            double top = startPoint.GetSmallTop(endPoint);
            double width = startPoint.GetLeftDifference(endPoint);
            double height = startPoint.GetTopDifference(endPoint);
            return _left >= left && _left <= left + width &&
                _top >= top && _top <= top + height;
        }

        // 判斷是否為 left or top 其中一個小於另外一點
        public bool IsLeftExclusiveOrTopToPoint(Point point)
        {
            return (GetSmallLeft(point) != _left) !=
                (GetSmallTop(point) != _top);
        }

        // 判斷是否在一個 circle 的 range 內
        public bool IsInCircleRange(int radius, Point point)
        {
            return GetLeftDifference(point) <= radius &&
                GetTopDifference(point) <= radius;
        }

        // 與 endPoint 相比，將位置重整
        public void ArrangePoints(ref Point endPoint)
        {
            float left = (float)GetSmallLeft(endPoint);
            float top = (float)GetSmallTop(endPoint);
            float width = (float)GetLeftDifference(endPoint);
            float height = (float)GetTopDifference(endPoint);
            _left = left;
            _top = top;
            endPoint.Left = left + width;
            endPoint.Top = top + height;
        }

        // 判斷 left 有沒有比另外一個 point 小
        public bool IsLeftSmallThan(Point point)
        {
            return _left < point._left;
        }

        // 判斷 top 有沒有比另外一個 point 小
        public bool IsTopSmallThan(Point point)
        {
            return _top < point._top;
        }

        // 取得較小的 left
        public double GetSmallLeft(Point point)
        {
            return _left > point.Left ? point.Left : _left;
        }

        // 取得較小的 top
        public double GetSmallTop(Point point)
        {
            return _top > point.Top ? point.Top : _top;
        }

        // 取得較大的 left
        public double GetBigLeft(Point point)
        {
            return _left > point.Left ? _left : point.Left;
        }

        // 取得較大的 top
        public double GetBigTop(Point point)
        {
            return _top > point.Top ? _top : point.Top;
        }

        // 取得 left 的差
        public double GetLeftDifference(Point point)
        {
            return Math.Abs(_left - point.Left);
        }

        // 取得 top 的差
        public double GetTopDifference(Point point)
        {
            return Math.Abs(_top - point.Top);
        }

        // 取得 point to line distance
        public double GetPointToLineDistance(Point startPoint, Point endPoint)
        {
            double slope = startPoint.GetSlope(endPoint);
            double offset = startPoint.GetOffset(endPoint);
            double distance = (_left * slope) - _top + offset;
            distance /= (Math.Sqrt((Math.Pow(slope, Constant.TWO) + 1)));
            return distance;
        }

        // 取得 slope
        private double GetSlope(Point point)
        {
            return (_top - point.Top) / (_left - point.Left);
        }

        // 取得 offset
        private double GetOffset(Point point)
        {
            return _top - this.GetSlope(point) * _left;
        }

        // _left 的 getter & setter
        public double Left
        {
            set
            {
                _left = value;
            }
            get
            {
                return _left;
            }
        }

        // _top 的 getter & setter
        public double Top
        {
            set
            {
                _top = value;
            }
            get
            {
                return _top;
            }
        }
    }
}
