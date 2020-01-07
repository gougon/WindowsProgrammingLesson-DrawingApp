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
    public class DrawCommandTests
    {
        DrawCommand _command;
        Model _model;
        Shape _shape;
        PrivateObject _target;

        // 初始化
        [TestInitialize()]
        public void Initialize()
        {
            _model = new Model();
            _shape = new ShapeFactory().CreateShape(ShapeType.Rectangle);
            _command = new DrawCommand(_model, _shape);
            _target = new PrivateObject(_model);
        }

        // 測試 Execute
        [TestMethod()]
        public void ExecuteTest()
        {
            _command.Execute();
            List<Shape> list = (List<Shape>)_target.GetField("_shapes");
            Assert.AreEqual(1, list.Count);
        }

        // 測試 UnExecute
        [TestMethod()]
        public void UnExecuteTest()
        {
            _command.Execute();
            _command.BackExecute();
            List<Shape> list = (List<Shape>)_target.GetField("_shapes");
            Assert.AreEqual(0, list.Count);
        }
    }
}