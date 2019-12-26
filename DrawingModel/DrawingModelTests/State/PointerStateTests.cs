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
        CommandManager _manager;
        Point _startPoint;
        bool _isPressed;

        // 初始化
        [TestInitialize()]
        public void PointerStateTest()
        {
            Shape shape = new ShapeFactory().CreateShape(ShapeType.Rectangle);
            shape.SetStartPoint(5, 5);
            shape.SetEndPoint(15, 15);
            _model = new Model();
            _shapes = new List<Shape>();
            _shapes.Add(shape);
            _state = new PointerState(_model, _shapes);
            _target = new PrivateObject(_state);
            PrivateObject _modelTarger = new PrivateObject(_model);
            _manager = (CommandManager)_modelTarger.GetField("_commandManager");
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

        // 測試 GetSelectShape
        [TestMethod()]
        public void GetSelectShapeTest()
        {
            _startPoint = (Point)_target.GetField("_startPoint");
            _startPoint.Left = 10;
            _startPoint.Top = 10;
            Shape selectShape = _state.GetSelectShape();
            Assert.AreEqual(ShapeType.Rectangle, selectShape.ShapeType);
        }

        // mock notify
        private void Notify()
        {
            _isNotify = true;
        }
    }
}