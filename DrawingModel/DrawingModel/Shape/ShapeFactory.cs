using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    public class ShapeFactory
    {
        private const string ERROR_MESSAGE = "未知的 Shape 型別";

        // 依據不同的 ShapeType 創造 Shape
        public Shape CreateShape(ShapeType type)
        {
            Shape shape;
            switch (type)
            {
                case ShapeType.Line:
                    shape = new Line();
                    break;
                case ShapeType.Rectangle:
                    shape = new Rectangle();
                    break;
                case ShapeType.SixSide:
                    shape = new SixSide();
                    break;
                default:
                    throw new ArgumentException(ERROR_MESSAGE);
            }
            return shape;
        }
    }
}
