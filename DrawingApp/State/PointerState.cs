using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrawingModel;

namespace DrawingApp
{
    public class PointerState : State
    {
        public PointerState(Model model) : base(model)
        {
            _stateType = StateType.Pointer;
        }

        // 處理按下指標事件
        public override void PressPointer(ShapeType shapeType, double left, double top)
        {
            _model.PressPointer(shapeType, left, top);
        }

        // 處理移動指標事件
        public override void MovePointer(double left, double top)
        {
            // Do nothing
        }

        // 處理放開指標事件
        public override void ReleasePointer(double left, double top)
        {
            _model.ReleasePointerWithPointerState(left, top);
        }

        // 處理繪圖事件
        public override void Draw(IGraphics graphics)
        {
            _model.MarkOutlines(graphics);
        }
    }
}
