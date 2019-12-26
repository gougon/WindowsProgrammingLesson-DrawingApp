using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    public enum StateType
    {
        Drawing,
        Pointer
    }

    public abstract class State
    {
        protected Model _model;

        protected List<Shape> _shapes;
        protected Point _startPoint = new Point();
        protected StateType _stateType;
        protected bool _isPressed = false;

        public State(Model model, List<Shape> shapes)
        {
            _model = model;
            _shapes = shapes;
        }

        // 處理按下指標的事件
        public abstract void PressPointer(ShapeType shapeType, double left, double top);

        // 處理移動指標的事件
        public abstract void MovePointer(double left, double top);

        // 處理放開指標的事件
        public abstract void ReleasePointer(CommandManager commandManager, double left, double top);

        // 處理繪畫事件
        public abstract void Draw(IGraphics graphics);

        // 尋找 select shape
        public abstract Shape GetSelectShape();

        // 清空所有狀態
        public abstract void Clear();

        // 將所有 shape 重繪
        protected void RefreshShapes(IGraphics graphics)
        {
            graphics.ClearAll();
            foreach (Shape shape in _shapes)
            {
                shape.Draw(graphics);
            }
        }

        // _stateType 的 getter
        public StateType StateType
        {
            get
            {
                return _stateType;
            }
        }

        // observer model changed
        public void HandleModelChanged()
        {
            _model.NotifyModelChanged();
        }
    }
}
