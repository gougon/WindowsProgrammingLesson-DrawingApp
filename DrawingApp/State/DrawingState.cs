using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrawingModel;

namespace DrawingApp
{
    public class DrawingState : State
    {
        public DrawingState(Model model) : base(model)
        {
            _stateType = StateType.Drawing;
        }

        // 處理按下指標事件
        public override void PressPointer(ShapeType shapeType, double left, double top)
        {
            _model.PressPointer(shapeType, left, top);
        }

        // 處理移動指標事件
        public override void MovePointer(double left, double top)
        {
            _model.MovePointer(left, top);
        }

        // 處理放開指標事件
        public override void ReleasePointer(double left, double top)
        {
            _model.ReleasePointerWithDrawingState(left, top);
        }

        // 處理繪圖事件
        public override void Draw(IGraphics graphics)
        {
            _model.Draw(graphics);
        }
    }
}
