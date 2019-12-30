using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace DrawingModel
{
    class ShapesConverter
    {
        string _text;

        public ShapesConverter(List<Shape> shapes)
        {
            _text = Constant.LEFT_MIDDLE_BRACKET + Constant.CHANGE_LINE;
            foreach (Shape shape in shapes)
            {
                ShapeFormat shapeText = new ShapeFormat(shape.StartPoint, shape.EndPoint, shape.ShapeType);
                _text += Constant.TAB;
                _text += JsonSerializer.Serialize<ShapeFormat>(shapeText);
                _text += (shape != shapes.Last()) ? (Constant.COMMA + Constant.CHANGE_LINE) : Constant.CHANGE_LINE;
            }
            _text += (Constant.RIGHT_MIDDLE_BRACKET + Constant.CHANGE_LINE);
        }

        public string Text
        {
            get
            {
                return _text;
            }
        }
    }
}
