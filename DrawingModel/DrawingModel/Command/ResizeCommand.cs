using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    public class ResizeCommand : ICommand
    {
        Model _model;
        Shape _shape;
        Point _originPoint;
        Point _cursorPoint;

        public ResizeCommand(Model model, Shape shape, Point originPoint, Point cursorPoint)
        {
            _model = model;
            _shape = shape;
            _originPoint = originPoint;
            _cursorPoint = cursorPoint;
        }

        // Resize
        public void Execute()
        {
            _model.Resize(_shape, _cursorPoint);
        }

        // 還原 Resize
        public void BackExecute()
        {
            _shape.SetEndPoint(_originPoint.Left, _originPoint.Top);
        }
    }
}
