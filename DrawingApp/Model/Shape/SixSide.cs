using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    public class SixSide : Shape
    {
        public SixSide()
        {
            _shapeType = DrawingModel.ShapeType.SixSide;
        }

        // 實作 shape 的 Draw method
        public override void Draw(IGraphics graphics)
        {
            graphics.DrawSixSide(_startPoint, _endPoint);
        }

        // 實作 shape 的 IsPointInShape method
        public override bool IsPointInShape(Point point)
        {
            return point.IsInRange(_startPoint, _endPoint);
        }
    }
}
