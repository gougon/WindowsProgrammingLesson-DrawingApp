using Microsoft.VisualStudio.TestTools.UnitTesting;
using DrawingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel.Tests
{
    [TestClass()]
    public class ShapesConverterTests
    {
        ShapesConverter _converter;

        // 初始化
        [TestInitialize()]
        public void Initialize()
        {
            _converter = new ShapesConverter();
        }

        // 測試 ConvertToText
        [TestMethod()]
        public void ConvertToTextTest()
        {
            List<Shape> shapes = new List<Shape>();

            Shape shape = new ShapeFactory().CreateShape(ShapeType.Line);
            shape.SetStartPoint(1, 1);
            shape.SetEndPoint(10, 10);
            shapes.Add(shape);

            shape = new ShapeFactory().CreateShape(ShapeType.Rectangle);
            shape.SetStartPoint(2, 2);
            shape.SetEndPoint(9, 9);
            shapes.Add(shape);

            _converter.ConvertToText(shapes);

            string json = "[{\"StartPointLeft\":1,\"StartPointTop\":1,\"EndPointLeft\":10,\"EndPointTop\":10,\"ShapeType\":0},{\"StartPointLeft\":2,\"StartPointTop\":2,\"EndPointLeft\":9,\"EndPointTop\":9,\"ShapeType\":1}]";

            Assert.AreEqual(json, _converter.Text);
        }

        [TestMethod()]
        public void ConvertToShapesTest()
        {
            string json = "[{\"StartPointLeft\":1,\"StartPointTop\":1,\"EndPointLeft\":10,\"EndPointTop\":10,\"ShapeType\":0},{\"StartPointLeft\":2,\"StartPointTop\":2,\"EndPointLeft\":9,\"EndPointTop\":9,\"ShapeType\":1}]";

            _converter.ConvertToShapes(json);

            List<Shape> shapes = _converter.Shapes;

            Assert.AreEqual(1, shapes[0].StartPoint.Left);
            Assert.AreEqual(1, shapes[0].StartPoint.Top);
            Assert.AreEqual(10, shapes[0].EndPoint.Left);
            Assert.AreEqual(10, shapes[0].EndPoint.Top);
            Assert.AreEqual(ShapeType.Line, shapes[0].ShapeType);

            Assert.AreEqual(2, shapes[1].StartPoint.Left);
            Assert.AreEqual(2, shapes[1].StartPoint.Top);
            Assert.AreEqual(9, shapes[1].EndPoint.Left);
            Assert.AreEqual(9, shapes[1].EndPoint.Top);
            Assert.AreEqual(ShapeType.Rectangle, shapes[1].ShapeType);
        }
    }
}