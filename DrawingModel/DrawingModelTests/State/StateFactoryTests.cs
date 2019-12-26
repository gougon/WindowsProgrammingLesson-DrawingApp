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
    public class StateFactoryTests
    {
        ShapeFactory _shapeFactory = new ShapeFactory();

        // 測試 createState
        [TestMethod()]
        public void CreateStateTest()
        {
            Shape shape = _shapeFactory.CreateShape(ShapeType.Line);
            Assert.AreEqual(ShapeType.Line, shape.ShapeType);

            shape = _shapeFactory.CreateShape(ShapeType.Rectangle);
            Assert.AreEqual(ShapeType.Rectangle, shape.ShapeType);

            shape = _shapeFactory.CreateShape(ShapeType.SixSide);
            Assert.AreEqual(ShapeType.SixSide, shape.ShapeType);
        }
    }
}