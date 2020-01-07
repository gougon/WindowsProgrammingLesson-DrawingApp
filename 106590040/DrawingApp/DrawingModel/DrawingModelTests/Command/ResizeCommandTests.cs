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
    public class ResizeCommandTests
    {
        Model _model;
        Shape _resizeShape;
        ICommand _resizeCommand;

        // 初始化
        [TestInitialize()]
        public void Initialize()
        {
            _model = new Model();
            _resizeShape = new ShapeFactory().CreateShape(ShapeType.Rectangle);
            _resizeShape.SetStartPoint(1, 1);
            _resizeShape.SetEndPoint(10, 10);
            _model.AddShape(_resizeShape);
            _resizeCommand = new ResizeCommand(_model, _resizeShape, _resizeShape.EndPoint.Clone, new Point(12, 12));
        }

        // 測試 excute
        [TestMethod()]
        public void ExecuteTest()
        {
            _resizeCommand.Execute();
            Assert.AreEqual(true, _resizeShape.EndPoint.IsEqual(new Point(12, 12)));
        }

        // 測試 backExcute
        [TestMethod()]
        public void BackExecuteTest()
        {
            _resizeCommand.Execute();
            _resizeCommand.BackExecute();
            Assert.AreEqual(true, _resizeShape.EndPoint.IsEqual(new Point(10, 10)));
        }
    }
}