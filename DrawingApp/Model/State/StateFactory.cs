using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    public class StateFactory
    {
        // 創造 state
        public State CreateState(StateType stateType, Model model, List<Shape> shapes)
        {
            switch (stateType)
            {
                case StateType.Pointer:
                    return new PointerState(model, shapes);
                case StateType.Drawing:
                    return new DrawingState(model, shapes);
                default:
                    throw new Exception(Constant.CREATE_STATE_ERROR_MESSAGE);
            }
        }
    }
}
