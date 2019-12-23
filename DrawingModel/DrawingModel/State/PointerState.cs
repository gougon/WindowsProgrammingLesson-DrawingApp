using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    public class PointerState : State
    {
        // Constructor
        public PointerState(Model model, List<Shape> shapes) : base(model, shapes)
        {
            // do nothing
        }

        // 處理按下指標事件
        public override void PressPointer(ShapeType shapeType, double left, double top)
        {
            if (left > 0 && top > 0)
            {
                _startPoint.Left = left;
                _startPoint.Top = top;
                _isPressed = true;
            }
        }

        // 處理移動指標事件
        public override void MovePointer(double left, double top)
        {
            // Do nothing
        }

        // 處理放開指標事件
        public override void ReleasePointer(CommandManager commandManager, double left, double top)
        {
            if (_isPressed)
            {
                _isPressed = false;
                HandleModelChanged();
            }
        }

        // 處理繪圖事件
        public override void Draw(IGraphics graphics)
        {
            RefreshShapes(graphics);
            Shape selectShape = GetSelectShape();
            if (selectShape != null)
            {
                selectShape.MarkOutlines(graphics);
            }
        }

        // 尋找 select shape
        public override Shape GetSelectShape()
        {
            double left = _startPoint.Left;
            double top = _startPoint.Top;
            int shapeQuantity = _shapes.Count;
            for (int order = shapeQuantity - 1; order >= 0; order--)
            {
                Shape shape = _shapes[order];
                if (shape.IsPointInShape(new Point(left, top)))
                {
                    return shape;
                }
            }
            return null;
        }
    }
}
