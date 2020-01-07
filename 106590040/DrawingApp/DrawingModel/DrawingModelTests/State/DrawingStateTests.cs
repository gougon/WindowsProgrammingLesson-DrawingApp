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
    public class DrawingStateTests
    {
        Model _model;
        List<Shape> _shapes;
        State _state;
        bool _isNotify;
        PrivateObject _target;
        PrivateObject _modelTarget;
        CommandManager _manager;
        Shape _hint;
        Point _startPoint;
        bool _isPressed;

        // 初始化
        [TestInitialize()]
        public void DrawingStateTest()
        {
            Shape shape = new ShapeFactory().CreateShape(ShapeType.Rectangle);
            shape.SetStartPoint(5, 5);
            shape.SetEndPoint(15, 15);
            _model = new Model();
            _modelTarget = new PrivateObject(_model);
            _manager = (CommandManager)_modelTarget.GetField("_commandManager");
            _shapes = (List<Shape>)_modelTarget.GetField("_shapes");
            _state = new DrawingState(_model, _shapes);
            _target = new PrivateObject(_state);
            _isNotify = false;
            _model._modelChanged += Notify;
            Assert.AreEqual(StateType.Drawing, _state.StateType);
        }

        // 測試 PressPointer
        [TestMethod()]
        public void PressPointerTest()
        {
            _isPressed = (bool)_target.GetField("_isPressed");
            _state.PressPointer(ShapeType.Line, 10, -10);
            Assert.AreEqual(false, _isPressed);
            _state.PressPointer(ShapeType.Line, -10, 10);
            Assert.AreEqual(false, _isPressed);
            _state.PressPointer(ShapeType.Line, -10, -10);
            Assert.AreEqual(false, _isPressed);
            _state.PressPointer(ShapeType.Line, 10, 10);
            _startPoint = (Point)_target.GetField("_startPoint");
            _hint = (Shape)_target.GetField("_hint");
            _isPressed = (bool)_target.GetField("_isPressed");
            Assert.AreEqual(true, _startPoint.IsEqual(new Point(10, 10)));
            Assert.AreEqual(ShapeType.Line, _hint.ShapeType);
            Assert.AreEqual(true, _isPressed);
        }

        // 測試 MovePointer
        [TestMethod()]
        public void MovePointerTest()
        {
            _state.PressPointer(ShapeType.Line, 10, 10);
            _state.MovePointer(11, 11);
            _hint = (Shape)_target.GetField("_hint");
            PrivateObject hintTarget = new PrivateObject(_hint);
            Point endPoint = (Point)hintTarget.GetField("_endPoint");
            Assert.AreEqual(11, endPoint.Left);
            Assert.AreEqual(11, endPoint.Top);
        }

        // 測試 ReleasePointer
        [TestMethod()]
        public void ReleasePointerTest()
        {
            _state.PressPointer(ShapeType.Line, 10, 10);
            _state.ReleasePointer(_manager, 11, 11);
            _isPressed = (bool)_target.GetField("_isPressed");
            _shapes = (List<Shape>)_modelTarget.GetField("_shapes");
            Assert.AreEqual(false, _isPressed);
            Assert.AreEqual(ShapeType.Line, _shapes[0].ShapeType);
            Assert.AreEqual(true, _isNotify);
        }

        // 測試 SetShapePoints
        [TestMethod()]
        public void SetShapePoints()
        {
            Shape testShape = new ShapeFactory().CreateShape(ShapeType.Line);
            _target.Invoke("SetShapePoints", new object[] { testShape, new Point(1, 1), new Point(3, 4) });
            Assert.AreEqual(1, testShape.Left);
            Assert.AreEqual(1, testShape.Top);
            Assert.AreEqual(2, testShape.Width);
            Assert.AreEqual(3, testShape.Height);
        }

        // 測試 Draw
        [TestMethod()]
        public void DrawTest()
        {
            _shapes = (List<Shape>)_modelTarget.GetField("_shapes");
            _state.PressPointer(ShapeType.Line, 10, 10);
            _state.Draw(new MockGraphics());
        }

        // 測試 GetSelectShape
        [TestMethod()]
        public void GetSelectShapeTest()
        {
            Assert.AreEqual(null, _state.GetSelectShape());
        }

        // mock notify
        private void Notify()
        {
            _isNotify = true;
        }
    }
}