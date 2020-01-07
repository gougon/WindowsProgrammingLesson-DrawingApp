using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    public class DrawCommand : ICommand
    {
        Model _model;
        Shape _shape;

        public DrawCommand(Model model, Shape shape)
        {
            _model = model;
            _shape = shape;
        }

        // 加入 shape
        public void Execute()
        {
            _model.AddShape(_shape);
        }

        // 刪除 shape
        public void BackExecute()
        {
            _model.DeleteShape();
        }
    }
}
