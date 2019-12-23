using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    public class Line : Shape
    {
        public Line()
        {
            _shapeType = DrawingModel.ShapeType.Line;
        }

        // 實作 shape 的 Draw method
        public override void Draw(IGraphics graphics)
        {
            graphics.DrawLine(_startPoint, _endPoint);
        }

        // 實作 shape 的 IsPointInShape method
        public override bool IsPointInShape(Point point)
        {
            if (!point.IsInRange(_startPoint, _endPoint))
                return false;
            double pointToLineDistance = GetPointToLine(point);
            return pointToLineDistance <= Constant.POINTER_ERROR;
        }

        // 計算點到職線的距離;
        private double GetPointToLine(Point point)
        {
            double pointToLineDistance = point.GetPointToLineDistance(_startPoint, _endPoint);
            return Math.Abs(pointToLineDistance);
        }
    }
}
