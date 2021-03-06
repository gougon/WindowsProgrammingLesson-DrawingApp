﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using DrawingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel.Tests
{
    [TestClass()]
    public class LineTests
    {

        Shape _line;

        // 初始化
        [TestInitialize()]
        public void LineTest()
        {
            _line = new Line();
            _line.SetStartPoint(0, 0);
            _line.SetEndPoint(100, 100);
        }

        // 測試 not reverse 的 line 的 Draw
        [TestMethod()]
        public void DrawWithNotReverseTest()
        {
            _line.Draw(new MockGraphics());
        }

        // 測試有 reverse 的 line 的 Draw
        [TestMethod()]
        public void DrawWithReverseTest()
        {
            _line.SetStartPoint(0, 10);
            _line.SetEndPoint(10, 0);
            _line.ArrangePoints();
            _line.Draw(new MockGraphics());
        }

        // 測試 IsPointInShape
        [TestMethod()]
        public void IsPointInShapeTest()
        {
            Point point = new Point(5, 0);
            Assert.AreEqual(true, _line.IsPointInShape(point));
            point = new Point(15.5, 0);
            Assert.AreEqual(false, _line.IsPointInShape(point));
            point = new Point(101, 10);
            Assert.AreEqual(false, _line.IsPointInShape(point));
        }

        // 測試 GetPointToLine 在沒有 reverse 的狀態
        [TestMethod()]
        public void GetPointToLineWithNotReverseTest()
        {
            Point point = new Point(5, 5);
            PrivateObject target = new PrivateObject(_line);
            Assert.AreEqual((double)0, target.Invoke("GetPointToLine", point));
        }

        // 測試 GetPointToLine 在有 reverse 的狀態
        [TestMethod()]
        public void GetPointToLineWithReverseTest()
        {
            _line.SetStartPoint(0, 10);
            _line.SetEndPoint(10, 0);
            _line.ArrangePoints();
            Point point = new Point(5, 5);
            PrivateObject target = new PrivateObject(_line);
            Assert.AreEqual((double)0, target.Invoke("GetPointToLine", point));
        }
    }
}