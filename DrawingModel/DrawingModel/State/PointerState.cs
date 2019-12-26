using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    public class PointerState : State
    {
        Shape _selectShape = null;
        bool _isInResizeState = false;

        // Constructor
        public PointerState(Model model, List<Shape> shapes) : base(model, shapes)
        {
            _stateType = StateType.Pointer;
        }

        // 處理按下指標事件
        public override void PressPointer(ShapeType shapeType, double left, double top)
        {
            if (left > 0 && top > 0)
            {
                _startPoint.Left = left;
                _startPoint.Top = top;
                _isPressed = true;
                DetermineIsInResizeState(new Point(left, top));
                SetSelectShape();
            }
        }

        // 判斷是否在 resize state
        private void DetermineIsInResizeState(Point cursorPoint)
        {
            if (_selectShape != null && 
                _selectShape.IsPointInResizeRange(cursorPoint))
            {
                _isInResizeState = true;
            }
        }

        // 處理移動指標事件
        public override void MovePointer(double left, double top)
        {
            if (_isPressed && left > 0 && top > 0)
            {
                Point cursorPoint = new Point(left, top);
                HandleResize(cursorPoint);
                HandleModelChanged();
            }
        }

        // 處理 resize
        private void HandleResize(Point cursorPoint)
        {
            if (_isInResizeState)
            {
                ResizeShape(_selectShape, cursorPoint);
            }
        }

        // resize
        private void ResizeShape(Shape resizeShape, Point cursorPoint)
        {
            resizeShape.SetEndPoint(cursorPoint.Left, cursorPoint.Top);
        }

        // 處理放開指標事件
        public override void ReleasePointer(CommandManager commandManager, double left, double top)
        {
            if (_isPressed)
            {
                _isPressed = false;
                _isInResizeState = false;
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
            return _selectShape;
        }

        // 尋找 select shape
        private void SetSelectShape()
        {
            double left = _startPoint.Left;
            double top = _startPoint.Top;
            int shapeQuantity = _shapes.Count;
            _selectShape = FindSelectShape(shapeQuantity, left, top);
        }

        // 找到 select shape
        private Shape FindSelectShape(int shapeQuantity, double left, double top)
        {
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

        // override clear
        public override void Clear()
        {
            _startPoint.Left = -1;
            _startPoint.Top = -1;
            _selectShape = null;
        }
    }
}
