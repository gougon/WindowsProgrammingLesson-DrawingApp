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
    public class PointTests
    {
        // 測試 constructor
        [TestMethod()]
        public void ConstructorTest()
        {
            Point point = new Point();
            Assert.AreEqual(point.Left, 0);
            Assert.AreEqual(point.Top, 0);
        }

        // 測試帶有參數的constructor
        [TestMethod()]
        public void ConstructorWithParameterTest()
        {
            Point point = new Point(3, -1);
            Assert.AreEqual(point.Left, 3);
            Assert.AreEqual(point.Top, -1);
        }

        // 測試 IsEqual
        [TestMethod()]
        public void IsEqualTest()
        {
            Point point1 = new Point(2.2, 3.3);
            Point point2 = new Point(2.2, 3.3);
            Assert.AreEqual(point1.IsEqual(point2), true);

            point2.Left = 3;
            Assert.AreEqual(point1.IsEqual(point2), false);
        }

        // 測試 IsInRange
        [TestMethod()]
        public void IsInRangeTest()
        {
            Point point = new Point(0, 0);
            Point startPoint = new Point(0, 0);
            Point endPoint = new Point(10, 10);
            Assert.AreEqual(true, point.IsInRange(startPoint, endPoint));
            point = new Point(11, 0);
            Assert.AreEqual(false, point.IsInRange(startPoint, endPoint));
        }

        // 測試 IsInCircleRange
        [TestMethod()]
        public void IsInCircleRangeTest()
        {
            Point centerPoint = new Point(5, 5);
            Assert.AreEqual(true, centerPoint.IsInCircleRange(Constant.MARK_CIRCLE_RADIUS, new Point(2, 2)));
            Assert.AreEqual(false, centerPoint.IsInCircleRange(Constant.MARK_CIRCLE_RADIUS, new Point(1, 2)));
            Assert.AreEqual(false, centerPoint.IsInCircleRange(Constant.MARK_CIRCLE_RADIUS, new Point(2, 1)));
            Assert.AreEqual(false, centerPoint.IsInCircleRange(Constant.MARK_CIRCLE_RADIUS, new Point(1, 1)));
        }

        // 測試 ArrangePoints
        [TestMethod()]
        public void ArrangePointsTest()
        {
            Point startPoint = new Point(1, 1);
            Point endPoint = new Point(2, 2);
            TestIsArrange(startPoint, endPoint);

            startPoint = new Point(2, 1);
            endPoint = new Point(1, 2);
            TestIsArrange(startPoint, endPoint);

            startPoint = new Point(2, 2);
            endPoint = new Point(1, 1);
            TestIsArrange(startPoint, endPoint);
        }

        // 測試是否有交換
        private void TestIsArrange(Point startPoint, Point endPoint)
        {
            startPoint.ArrangePoints(ref endPoint);
            Assert.AreEqual(1, startPoint.Left);
            Assert.AreEqual(1, startPoint.Top);
            Assert.AreEqual(2, endPoint.Left);
            Assert.AreEqual(2, endPoint.Top);
        }

        // 測試 IsLeftTopXorToPoint
        [TestMethod()]
        public void IsLeftTopXorToPointTest()
        {
            Point startPoint = new Point(2, 2);
            Point endPoint = new Point(1, 1);
            Assert.AreEqual(false, startPoint.IsLeftExclusiveOrTopToPoint(endPoint));
            endPoint = new Point(2, 1);
            Assert.AreEqual(true, startPoint.IsLeftExclusiveOrTopToPoint(endPoint));
            endPoint = new Point(1, 2);
            Assert.AreEqual(true, startPoint.IsLeftExclusiveOrTopToPoint(endPoint));
            endPoint = new Point(2, 2);
            Assert.AreEqual(false, startPoint.IsLeftExclusiveOrTopToPoint(endPoint));
        }

        // 測試 GetBiggerLeft
        [TestMethod()]
        public void GetSmallerLeftTest()
        {
            Point point1 = new Point(1.1, 2.2);
            Point point2 = new Point(3.3, 0);
            Assert.AreEqual(point1.GetSmallLeft(point2), 1.1);
            Assert.AreEqual(point2.GetSmallLeft(point1), 1.1);
        }

        // 測試 GetSmallerTop
        [TestMethod()]
        public void GetSmallerTopTest()
        {
            Point point1 = new Point(1.1, 2.2);
            Point point2 = new Point(3.3, 0);
            Assert.AreEqual(point1.GetSmallTop(point2), 0);
            Assert.AreEqual(point2.GetSmallTop(point1), 0);
        }

        // 測試 GetBiggerLeft
        [TestMethod()]
        public void GetBiggerLeftTest()
        {
            Point point1 = new Point(1.1, 2.2);
            Point point2 = new Point(3.3, 0);
            Assert.AreEqual(point1.GetBigLeft(point2), 3.3);
            Assert.AreEqual(point2.GetBigLeft(point1), 3.3);
        }

        // 測試 GetBiggerTop
        [TestMethod()]
        public void GetBiggerTopTest()
        {
            Point point1 = new Point(1.1, 2.2);
            Point point2 = new Point(3.3, 0);
            Assert.AreEqual(point1.GetBigTop(point2), 2.2);
            Assert.AreEqual(point2.GetBigTop(point1), 2.2);
        }

        // 測試 GetLeftDifference
        [TestMethod()]
        public void GetLeftDifferenceTest()
        {
            Point point1 = new Point(1, 1.2);
            Point point2 = new Point(3, 0);
            Assert.AreEqual(point1.GetLeftDifference(point2), 2);
        }

        // 測試 GetTopDifference
        [TestMethod()]
        public void GetTopDifferenceTest()
        {
            Point point1 = new Point(1.1, 1.2);
            Point point2 = new Point(3.3, 0);
            Assert.AreEqual(point1.GetTopDifference(point2), 1.2);
        }

        // 測試 GetSlope
        [TestMethod()]
        public void GetSlopeTest()
        {
            Point point1 = new Point(1, 2);
            Point point2 = new Point(2, 1);
            PrivateObject target = new PrivateObject(point1);
            Assert.AreEqual((double)-1, target.Invoke("GetSlope", point2));
        }

        // 測試 GetDistanceToLineDistance
        [TestMethod()]
        public void GetDistanceToLineDistanceTest()
        {
            Point startPoint = new Point(0, 0);
            Point endPoint = new Point(10, 10);
            Point point = new Point(5, 5);
            Assert.AreEqual(0, point.GetPointToLineDistance(startPoint, endPoint));
        }

        // 測試 GetOffset
        [TestMethod()]
        public void GetOffset()
        {
            Point point1 = new Point(1, 2);
            Point point2 = new Point(2, 1);
            PrivateObject target = new PrivateObject(point1);
            Assert.AreEqual((double)3, target.Invoke("GetOffset", point2));
        }

        // 測試 Left 的 getter & setter
        [TestMethod()]
        public void LeftPropertyTopTest()
        {
            Point point = new Point();
            point.Left = 3.1;
            Assert.AreEqual(point.Left, 3.1);
        }

        // 測試 Top 的 getter & setter
        [TestMethod()]
        public void TopPropertyTopTest()
        {
            Point point = new Point();
            point.Top = 3.2;
            Assert.AreEqual(point.Top, 3.2);
        }
    }
}