using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace DrawingModel
{
    public class ShapesConverter
    {
        string _text;
        List<Shape> _shapes;

        // 將 shapes 轉換為 json 字串
        public void ConvertToText(List<Shape> shapes)
        {
            List<ShapeFormat> shapeFormats = new List<ShapeFormat>();
            foreach (Shape shape in shapes)
            {
                ShapeFormat shapeText = new ShapeFormat(shape.StartPoint, shape.EndPoint, shape.ShapeType);
                shapeFormats.Add(shapeText);
            }
            _text = JsonSerializer.Serialize<List<ShapeFormat>>(shapeFormats);
        }

        // 將 json 字串轉為 shapes
        public void ConvertToShapes(string text)
        {
            _shapes = new List<Shape>();
            List<ShapeFormat> shapeFormats = JsonSerializer.Deserialize<List<ShapeFormat>>(text);
            foreach (ShapeFormat shapeFormat in shapeFormats)
            {
                Shape shape = new ShapeFactory().CreateShape((ShapeType)shapeFormat.ShapeType);
                shape.SetStartPoint(shapeFormat.StartPointLeft, shapeFormat.StartPointTop);
                shape.SetEndPoint(shapeFormat.EndPointLeft, shapeFormat.EndPointTop);
                _shapes.Add(shape);
            }
        }

        public string Text
        {
            get
            {
                return _text;
            }
        }

        public List<Shape> Shapes
        {
            get
            {
                return _shapes;
            }
        }
    }
}
