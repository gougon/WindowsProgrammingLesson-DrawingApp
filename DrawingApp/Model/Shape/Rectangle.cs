using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    public class Rectangle : Shape
    {
        public Rectangle()
        {
            _shapeType = DrawingModel.ShapeType.Rectangle;
        }

        // 實作 shape 的 Draw method
        public override void Draw(IGraphics graphics)
        {
            graphics.DrawRectangle(_startPoint, _endPoint);
        }

        // 實作 shape 的 IsPointInShape method
        public override bool IsPointInShape(Point point)
        {
            return point.IsInRange(_startPoint, _endPoint);
        }
    }
}
