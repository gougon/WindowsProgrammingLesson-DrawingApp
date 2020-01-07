using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    public class ClearCommand : ICommand
    {
        Model _model;
        List<Shape> _shapes;

        public ClearCommand(Model model, List<Shape> shapes)
        {
            _model = model;
            _shapes = new List<Shape>();
            foreach (Shape shape in shapes)
            {
                _shapes.Add(shape);
            }
        }

        // 重新加入所有 shapes
        public void Execute()
        {
            int length = _shapes.Count;
            for (int count = 0; count < length; count++)
            {
                _model.DeleteShape();
            }
        }

        // 刪除所有 shapes
        public void BackExecute()
        {
            int length = _shapes.Count;
            for (int count = 0; count < length; count++)
            {
                _model.AddShape(_shapes[count]);
            }
        }
    }
}
