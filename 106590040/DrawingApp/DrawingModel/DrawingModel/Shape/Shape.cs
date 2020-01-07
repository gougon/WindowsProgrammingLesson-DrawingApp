using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    public enum ShapeType
    {
        Line, 
        Rectangle,
        SixSide
    };

    public abstract class Shape : ICloneable
    {
        protected bool _isReverse = false;
        protected Point _startPoint;
        protected Point _endPoint;
        protected ShapeType _shapeType;

        // 將 Shape 畫出來的 abstract method
        public abstract void Draw(IGraphics graphics);

        // 判斷 point 是否在 shape 內的 abstract method
        public abstract bool IsPointInShape(Point point);

        // 若是 point 在 range 內則將外框飆出來
        public void MarkOutlines(IGraphics graphics)
        {
            graphics.MarkOutlines(_startPoint, _endPoint);
        }

        // _startPoint 的 setter
        public void SetStartPoint(double left, double top)
        {
            _startPoint = new Point(left, top);
        }

        // _endPoint 的 setter
        public void SetEndPoint(double left, double top)
        {
            _endPoint = new Point(left, top);
        }

        // 重整 startPoint 和 endPoint
        public void ArrangePoints()
        {
            if (_startPoint.IsLeftExclusiveOrTopToPoint(_endPoint))
            {
                _isReverse = true;
            }
            else
            {
                _isReverse = false;
            }
            _startPoint.ArrangePoints(ref _endPoint);
        }

        // 判斷 point 有沒有在 shape 的右下角
        public bool IsPointInResizeRange(Point point)
        {
            return LowerRightPoint.IsInCircleRange(Constant.MARK_CIRCLE_RADIUS, point);
        }

        // 取得 information
        public string Information
        {
            get
            {
                string information = (GetShapeText(this.ShapeType) + Constant.SPACE + Constant.LEFT_SMALL_BRACKET);
                information += (UpperLeftPoint.Left + Constant.COMMA + Constant.SPACE);
                information += (UpperLeftPoint.Top + Constant.COMMA + Constant.SPACE);
                information += (UpperLeftPoint.GetLeftDifference(LowerRightPoint) + Constant.COMMA + Constant.SPACE);
                information += (UpperLeftPoint.GetTopDifference(LowerRightPoint) + Constant.RIGHT_SMALL_BRACKET);
                return information;
            }
        }

        // 取得 ShapeType 對應的文字
        public string GetShapeText(ShapeType shapeType)
        {
            switch (shapeType)
            {
                case ShapeType.Line:
                    return Constant.LINE_TEXT;
                case ShapeType.Rectangle:
                    return Constant.RECTANGLE_TEXT;
                default:
                    return Constant.SIX_SIDE_TEXT;
            }
        }

        // 複製
        public object Clone()
        {
            Shape shape = new ShapeFactory().CreateShape(_shapeType);
            shape.SetStartPoint(_startPoint.Left, _startPoint.Top);
            shape.SetEndPoint(_endPoint.Left, _endPoint.Top);
            shape.IsReverse = _isReverse;
            return shape;
        }

        // 取得 startPoint
        public Point StartPoint
        {
            get
            {
                return _startPoint;
            }
        }

        // 取得 endPoint
        public Point EndPoint
        {
            get
            {
                return _endPoint;
            }
        }

        // 判斷有沒有 reverse
        public bool IsReverse
        {
            get
            {
                return _isReverse;
            }
            set
            {
                _isReverse = value;
            }
        }

        // 取得右上角 point
        public Point UpperRightPoint
        {
            get
            {
                double left = _startPoint.GetBigLeft(_endPoint);
                double top = _startPoint.GetSmallTop(_endPoint);
                return new Point(left, top);
            }
        }

        // 取得右下角 point
        public Point LowerRightPoint
        {
            get
            {
                double left = _startPoint.GetBigLeft(_endPoint);
                double top = _startPoint.GetBigTop(_endPoint);
                return new Point(left, top);
            }
        }

        // 取得左上角 point
        public Point UpperLeftPoint
        {
            get
            {
                double left = _startPoint.GetSmallLeft(_endPoint);
                double top = _startPoint.GetSmallTop(_endPoint);
                return new Point(left, top);
            }
        }

        // 取得左下角 point
        public Point LowerLeftPoint
        {
            get
            {
                double left = _startPoint.GetSmallLeft(_endPoint);
                double top = _startPoint.GetBigTop(_endPoint);
                return new Point(left, top);
            }
        }

        // 取得 _shapeType
        public ShapeType ShapeType
        {
            get
            {
                return _shapeType;
            }
        }

        // 取得左上角的 left
        public double Left
        {
            get
            {
                return _startPoint.GetSmallLeft(_endPoint);
            }
        }

        // 取得左上角的 top
        public double Top
        {
            get
            {
                return _startPoint.GetSmallTop(_endPoint);
            }
        }

        // 取得 width
        public double Width
        {
            get
            {
                return _startPoint.GetLeftDifference(_endPoint);
            }
        }

        // 取得 height
        public double Height
        {
            get
            {
                return _startPoint.GetTopDifference(_endPoint);
            }
        }
    }
}
