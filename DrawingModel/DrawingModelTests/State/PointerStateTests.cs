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
    public class PointerStateTests
    {
        Model _model;
        List<Shape> _shapes;
        State _state;
        bool _isNotify;
        PrivateObject _target;
        PrivateObject _modelTarget;
        CommandManager _manager;
        Point _startPoint;
        bool _isPressed;

        // 初始化
        [TestInitialize()]
        public void PointerStateTest()
        {
            _model = new Model();
            _modelTarget = new PrivateObject(_model);
            _shapes = (List<Shape>)_modelTarget.GetField("_shapes");
            _state = new PointerState(_model, _shapes);
            _target = new PrivateObject(_state);
            _manager = (CommandManager)_modelTarget.GetField("_commandManager");
            _isNotify = false;
            _model._modelChanged += Notify;
            Assert.AreEqual(StateType.Pointer, _state.StateType);
        }

        // 測試 PressPointer
        [TestMethod()]
        public void PressPointerTest()
        {
            _state.PressPointer(ShapeType.Line, 10, 10);
            _startPoint = (Point)_target.GetField("_startPoint");
            _isPressed = (bool)_target.GetField("_isPressed");
            Assert.AreEqual(true, _startPoint.IsEqual(new Point(10, 10)));
            Assert.AreEqual(true, _isPressed);
        }

        // 測試 MovePointer
        [TestMethod()]
        public void MovePointerTest()
        {
            AppendShapeIntoShapes();
            _state.PressPointer(ShapeType.Rectangle, 13, 13);
            _state.ReleasePointer(_manager, 13, 13);
            _state.PressPointer(ShapeType.Rectangle, 16, 16);
            _state.MovePointer(17, 17);
            _shapes = (List<Shape>)_modelTarget.GetField("_shapes");
            Assert.AreEqual(12, _shapes[0].Width);
            Assert.AreEqual(12, _shapes[0].Height);
        }

        // 測試 ReleasePointer
        [TestMethod()]
        public void ReleasePointerTest()
        {
            _state.PressPointer(ShapeType.Line, 10, 10);
            _state.ReleasePointer(_manager, 10, 10);
            _isPressed = (bool)_target.GetField("_isPressed");
            Assert.AreEqual(false, _isPressed);
            Assert.AreEqual(true, _isNotify);
        }

        // 測試 Draw
        [TestMethod()]
        public void DrawTest()
        {
            _startPoint = (Point)_target.GetField("_startPoint");
            _startPoint.Left = 10;
            _startPoint.Top = 10;
            _state.Draw(new MockGraphics());
        }

        // 測試 SetSelectShape
        [TestMethod()]
        public void SetSelectShapeTest()
        {
            AppendShapeIntoShapes();
            _startPoint = (Point)_target.GetField("_startPoint");
            _startPoint.Left = 10;
            _startPoint.Top = 10;
            _target.Invoke("SetSelectShape");
            Shape selectShape = _state.GetSelectShape();
            Assert.AreEqual(ShapeType.Rectangle, selectShape.ShapeType);
        }

        // mock notify
        private void Notify()
        {
            _isNotify = true;
        }

        // 將 _shapes 加入一個 shape
        private void AppendShapeIntoShapes()
        {
            Shape shape = new ShapeFactory().CreateShape(ShapeType.Rectangle);
            shape.SetStartPoint(5, 5);
            shape.SetEndPoint(15, 15);
            _shapes = (List<Shape>)_modelTarget.GetField("_shapes");
            _shapes.Add(shape);
        }
    }
}