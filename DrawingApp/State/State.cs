using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrawingModel;

namespace DrawingApp
{
    public enum StateType
    {
        Drawing,
        Pointer
    }

    public abstract class State
    {
        protected Model _model;
        protected StateType _stateType;

        public State(Model model)
        {
            _model = model;
        }

        // 處理按下指標的事件
        public abstract void PressPointer(ShapeType shapeType, double left, double top);

        // 處理移動指標的事件
        public abstract void MovePointer(double left, double top);

        // 處理放開指標的事件
        public abstract void ReleasePointer(double left, double top);

        // 處理繪畫事件
        public abstract void Draw(IGraphics graphics);

        // _stateType 的 getter
        public StateType StateType
        {
            get
            {
                return _stateType;
            }
        }
    }
}
