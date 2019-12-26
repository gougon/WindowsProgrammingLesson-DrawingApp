using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    public class DrawingState : State
    {
        private ShapeFactory _shapeFactory = new ShapeFactory();

        private Shape _hint;
        private ShapeType _type;

        // Constructor
        public DrawingState(Model model, List<Shape> shapes) : base(model, shapes)
        {
            _stateType = StateType.Drawing;
        }

        // 處理按下指標事件
        public override void PressPointer(ShapeType shapeType, double left, double top)
        {
            if (left > 0 && top > 0)
            {
                _type = shapeType;
                _startPoint.Left = left;
                _startPoint.Top = top;
                _hint = _shapeFactory.CreateShape(_type);
                SetShapePoints(_hint, _startPoint, _startPoint);
                _isPressed = true;
            }
        }

        // 處理移動指標事件
        public override void MovePointer(double left, double top)
        {
            if (_isPressed)
            {
                SetShapePoints(_hint, _startPoint, new Point(left, top));
                HandleModelChanged();
            }
        }

        // 處理放開指標事件
        public override void ReleasePointer(CommandManager commandManager, double left, double top)
        {
            if (_isPressed)
            {
                _isPressed = false;
                Shape shape = _shapeFactory.CreateShape(_type);
                Point endPoint = new Point(left, top);
                SetShapePoints(shape, _startPoint, endPoint);
                shape.ArrangePoints();
                _startPoint.Left = -1;
                _startPoint.Top = -1;
                commandManager.Execute(new DrawCommand(_model, shape));
                HandleModelChanged();
            }
        }

        // 設定 point 的 startPoint 和 endPoint
        private void SetShapePoints(Shape shape, Point startPoint, Point endPoint)
        {
            
            shape.SetStartPoint(startPoint.Left, startPoint.Top);
            shape.SetEndPoint(endPoint.Left, endPoint.Top);
        }

        // 處理繪圖事件
        public override void Draw(IGraphics graphics)
        {
            RefreshShapes(graphics);
            if (_isPressed)
            {
                _hint.Draw(graphics);
            }
        }

        // 尋找 select shape
        public override Shape GetSelectShape()
        {
            return null;
        }

        // override clear
        public override void Clear()
        {
            _startPoint.Left = -1;
            _startPoint.Top = -1;
        }
    }
}
