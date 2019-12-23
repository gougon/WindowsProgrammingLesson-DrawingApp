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
    public class ShapeFactoryTests
    {
        ShapeFactory _factory;

        // 初始化 _factory
        [TestInitialize()]
        public void Initialize()
        {
            _factory = new ShapeFactory();
        }

        // 測試是否可以創建每一種 ShapeType
        [TestMethod()]
        public void CreateShapeTest()
        {
            Shape shape = _factory.CreateShape(ShapeType.Line);
            Assert.AreEqual(shape.GetType().Name, "Line");
            shape = _factory.CreateShape(ShapeType.Rectangle);
            Assert.AreEqual(shape.GetType().Name, "Rectangle");
            shape = _factory.CreateShape(ShapeType.SixSide);
            Assert.AreEqual(shape.GetType().Name, "SixSide");
        }

        // 測試不在 ShapeType 內的值是否會 throw error
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateShapeErrorTest()
        {
            Shape shape = _factory.CreateShape((ShapeType)(-1));
        }
    }
}