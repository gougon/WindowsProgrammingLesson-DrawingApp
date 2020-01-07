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
    public class ShapeFormatTests
    {
        ShapeFormat _shapeFormat;

        // 初始化
        [TestInitialize()]
        public void Initialize()
        {
            Point startPoint = new Point(1, 1);
            Point endPoint = new Point(10, 10);
            _shapeFormat = new ShapeFormat(startPoint, endPoint, ShapeType.Rectangle, false);
            Assert.AreEqual(1, _shapeFormat.StartPointLeft);
            Assert.AreEqual(1, _shapeFormat.StartPointTop);
            Assert.AreEqual(10, _shapeFormat.EndPointLeft);
            Assert.AreEqual(10, _shapeFormat.EndPointTop);
            Assert.AreEqual(1, _shapeFormat.ShapeType);
        }

        // 測試 StartPointLeft
        [TestMethod()]
        public void StartPointLeftTest()
        {
            _shapeFormat.StartPointLeft = 2;
            Assert.AreEqual(2, _shapeFormat.StartPointLeft);
        }

        // 測試 StartPointTop
        [TestMethod()]
        public void StartPointTopTest()
        {
            _shapeFormat.StartPointTop = 2;
            Assert.AreEqual(2, _shapeFormat.StartPointTop);
        }

        // 測試 EndPointLeft
        [TestMethod()]
        public void EndPointLeftTest()
        {
            _shapeFormat.EndPointLeft = 2;
            Assert.AreEqual(2, _shapeFormat.EndPointLeft);
        }

        // 測試 EndPointTop
        [TestMethod()]
        public void EndPointTopTest()
        {
            _shapeFormat.EndPointLeft = 2;
            Assert.AreEqual(2, _shapeFormat.EndPointLeft);
        }

        // 測試 ShapeType
        [TestMethod()]
        public void ShapetTypeTest()
        {
            _shapeFormat.ShapeType = 0;
            Assert.AreEqual(0, _shapeFormat.ShapeType);
        }

        // 測試 IsReverse
        [TestMethod()]
        public void IsReverseTest()
        {
            _shapeFormat.IsReverse = true;
            Assert.AreEqual(true, _shapeFormat.IsReverse);
        }
    }
}