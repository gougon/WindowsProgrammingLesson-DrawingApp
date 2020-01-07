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
    public class RectangleTests
    {
        Shape _rectangle;

        // 初始化
        [TestInitialize()]
        public void Initialize()
        {
            _rectangle = new Rectangle();
            _rectangle.SetStartPoint(0, 0);
            _rectangle.SetEndPoint(100, 100);
        }

        // 測試 Draw
        [TestMethod()]
        public void DrawTest()
        {
            _rectangle.Draw(new MockGraphics());
        }

        // 測試 IsPointInShape
        [TestMethod()]
        public void IsPointInShapeTest()
        {
            Point point = new Point(50, 50);
            Assert.AreEqual(true, _rectangle.IsPointInShape(point));
            point = new Point(103, 50);
            Assert.AreEqual(false, _rectangle.IsPointInShape(point));
        }
    }
}