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

    public abstract class Shape
    {
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

        // 取得 ShapeType 對應的文字
        private string GetShapeText(ShapeType shapeType)
        {
            switch (shapeType)
            {
                case ShapeType.Line:
                    return Constant.LINE_TEXT;
                case ShapeType.Rectangle:
                    return Constant.RECTANGLE_TEXT;
                case ShapeType.SixSide:
                    return Constant.SIX_SIDE_TEXT;
            }
            return null;
        }

        // 取得 information
        public string Information
        {
            get
            {
                string information = (GetShapeText(this.ShapeType) + Constant.SPACE + Constant.LEFT_BRACKET);
                information += (this.Left + Constant.COMMA + Constant.SPACE);
                information += (this.Top + Constant.COMMA + Constant.SPACE);
                information += (this.Width + Constant.COMMA + Constant.SPACE);
                information += (this.Height + Constant.RIGHT_BRACKET);
                return information;
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
