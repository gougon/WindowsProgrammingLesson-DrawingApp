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
    public class GoogleDriveHelperTests
    {
        GoogleDriveHelper _helper;
        List<Shape> _shapes;

        // 初始化
        [TestInitialize()]
        public void Initialize()
        {
            _helper = new GoogleDriveHelper();

            _shapes = new List<Shape>();

            Shape shape = new ShapeFactory().CreateShape(ShapeType.Line);
            shape.SetStartPoint(1, 1);
            shape.SetEndPoint(10, 10);
            _shapes.Add(shape);

            shape = new ShapeFactory().CreateShape(ShapeType.Rectangle);
            shape.SetStartPoint(2, 2);
            shape.SetEndPoint(9, 9);
            _shapes.Add(shape);
        }

        // 測試 save
        [TestMethod()]
        public void SaveTest()
        {
            _helper.Save(_shapes);
        }

        // 測試 load
        [TestMethod()]
        public void LoadTest()
        {
            _helper.Load(_shapes);
        }
    }
}