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
    public class ShapeTests
    {
        Shape _shape;
        PrivateObject _target;

        // 初始化
        [TestInitialize()]
        public void Initialize()
        {
            _shape = new ShapeFactory().CreateShape(ShapeType.Line);
            _target = new PrivateObject(_shape);
            _shape.SetStartPoint(1, 1);
            _shape.SetEndPoint(5, 5);
        }

        // 測試 MarkOutlines
        [TestMethod()]
        public void MarkOutlinesTest()
        {
            _shape.MarkOutlines(new MockGraphics());
        }

        // 測試 SetStartPoint
        [TestMethod()]
        public void SetStartPointTest()
        {
            _shape.SetStartPoint(1, 2);
            Point point = (Point)_target.GetField("_startPoint");
            Assert.AreEqual(1, point.Left);
            Assert.AreEqual(2, point.Top);
        }

        // 測試 SetEndPoint
        [TestMethod()]
        public void SetEndPointTest()
        {
            _shape.SetEndPoint(1, 2);
            Point point = (Point)_target.GetField("_endPoint");
            Assert.AreEqual(1, point.Left);
            Assert.AreEqual(2, point.Top);
        }

        // 測試 Information
        [TestMethod()]
        public void InformationTest()
        {
            _shape.SetStartPoint(1, 1);
            _shape.SetEndPoint(10, 10);
            Assert.AreEqual("Line (1, 1, 9, 9)", _shape.Information);
        }

        // 測試 Height
        [TestMethod()]
        public void HeightTest()
        {
            Assert.AreEqual(4, _shape.Height);
        }

        // 測試 Width
        [TestMethod()]
        public void WidthtTest()
        {
            Assert.AreEqual(4, _shape.Width);
        }

        // 測試 Left
        [TestMethod()]
        public void LeftTest()
        {
            Assert.AreEqual(1, _shape.Left);
        }

        // 測試 Top
        [TestMethod()]
        public void TopTest()
        {
            Assert.AreEqual(1, _shape.Top);
        }

        // 測試 ShapeType
        [TestMethod()]
        public void ShapeTypeTest()
        {
            Assert.AreEqual(ShapeType.Line, _shape.ShapeType);
        }
    }
}