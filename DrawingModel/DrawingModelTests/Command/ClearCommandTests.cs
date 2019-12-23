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
    public class ClearCommandTests
    {
        ClearCommand _command;
        Model _model;
        List<Shape> _shapes;
        PrivateObject _target;

        // 初始化
        [TestInitialize()]
        public void Initialize()
        {
            _model = new Model();
            _model.AddShape(new ShapeFactory().CreateShape(ShapeType.Line));
            _model.AddShape(new ShapeFactory().CreateShape(ShapeType.SixSide));
            _target = new PrivateObject(_model);
            _shapes = (List<Shape>)_target.GetField("_shapes");
            _command = new ClearCommand(_model, _shapes);
            
        }

        // 測試 Execute
        [TestMethod()]
        public void ExecuteTest()
        {
            _command.Execute();
            Assert.AreEqual(0, _shapes.Count);
        }

        // 測試 UnExecute
        [TestMethod()]
        public void UnExecuteTest()
        {
            _command.Execute();
            _command.BackExecute();
            Assert.AreEqual(2, _shapes.Count);
        }
    }
}