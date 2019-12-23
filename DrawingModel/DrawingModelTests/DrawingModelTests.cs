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
    public class DrawingModelTests
    {
        Model _model;
        PrivateObject _target;

        bool _isNotify;

        // 初始化 _model
        [TestInitialize()]
        public void Initialize()
        {
            _model = new Model();
            _target = new PrivateObject(_model);

            _model._modelChanged += Notify;
            _isNotify = false;
        }

        // 測試按下指標
        [TestMethod()]
        public void PressPointerTest()
        {
            _model.PressPointer(ShapeType.Rectangle, 3.3, -1);
            ShapeType type = (ShapeType)_target.GetField("_type");
            ShapeType testType = ShapeType.Line;
            Assert.AreEqual(type, testType);

            _model.PressPointer(ShapeType.Rectangle, -1, 3.3);
            Assert.AreEqual(type, testType);

            _model.PressPointer(ShapeType.Line, 3.3, 1.2);
            type = (ShapeType)_target.GetField("_type");
            testType = ShapeType.Line;
            Assert.AreEqual(type, testType);

            Point startPoint = (Point)_target.GetField("_startPoint");
            Point testPoint = new Point(3.3, 1.2);
            Assert.AreEqual(startPoint.IsEqual(testPoint), true);
            
            Shape hint = (Shape)_target.GetField("_hint");
            Point hintStartPoint = GetHintField("_startPoint");
            Assert.AreEqual(hint.GetType().Name, "Line");
            Assert.AreEqual(hintStartPoint.IsEqual(testPoint), true);

            bool isPressed = (bool)_target.GetField("_isPressed");
            Assert.AreEqual(isPressed, true);
        }

        // 測試在 !_isPressed 移動指標
        [TestMethod()]
        public void MovePointerWhenNotPressedTest()
        {
            _model.MovePointer(5, 4.5);
            Shape hint = (Shape)_target.GetField("_hint");
            Assert.AreEqual(hint, null);
            Assert.AreEqual(_isNotify, false);
        }

        // 測試在 _isPressed 移動指標
        [TestMethod()]
        public void MovePointerWhenPressedTest()
        {
            _model.PressPointer(ShapeType.Line, 3.3, 4.4);
            _model.MovePointer(5.5, 6.6);
            Point hintEndPoint = GetHintField("_endPoint");
            Point testPoint = new Point(5.5, 6.6);
            Assert.AreEqual(hintEndPoint.IsEqual(testPoint), true);
            Assert.AreEqual(_isNotify, true);
        }

        // 測試在 !_isPressed 放開指標
        [TestMethod()]
        public void ReleasePointerWithDrawingStateWhenNotPressedTest()
        {
            _model.ReleasePointerWithDrawingState(5, 4.5);
            bool isPressed = (bool)_target.GetField("_isPressed");
            List<Shape> list = (List<Shape>)_target.GetField("_shapes");
            Assert.AreEqual(isPressed, false);
            Assert.AreEqual(list.Count, 0);
            Assert.AreEqual(_isNotify, false);
        }

        // 測試在 _isPressed 放開指標
        [TestMethod()]
        public void ReleasePointerWithDrawingStateWhenPressedTest()
        {
            _model.PressPointer(ShapeType.Line, 3.3, 4.4);
            _model.ReleasePointerWithDrawingState(5, 4.5);
            bool isPressed = (bool)_target.GetField("_isPressed");
            List<Shape> list = (List<Shape>)_target.GetField("_shapes");
            Assert.AreEqual(isPressed, false);
            Assert.AreEqual(list.Count, 1);
            Assert.AreEqual(_isNotify, true);
        }

        // 測試在 !_isPressed ReleasePointerWithPointerState
        [TestMethod()]
        public void ReleasePointerWithPointerStateWhenNotPressedTest()
        {
            _model.ReleasePointerWithPointerState(5, 4.5);
            bool isPressed = (bool)_target.GetField("_isPressed");
            Assert.AreEqual(false, isPressed);
            Assert.AreEqual(false, _isNotify);
        }

        // 測試在 _isPressed ReleasePointerWithPointerState
        [TestMethod()]
        public void ReleasePointerWithPointerStateWhenPressedTest()
        {
            _model.PressPointer(ShapeType.Line, 3.3, 4.4);
            _model.ReleasePointerWithPointerState(5, 4.5);
            bool isPressed = (bool)_target.GetField("_isPressed");
            Assert.AreEqual(false, isPressed);
            Assert.AreEqual(true, _isNotify);
        }

        // 測試 AddShape
        [TestMethod()]
        public void AddShapeTest()
        {
            Shape shape = new ShapeFactory().CreateShape(ShapeType.Line);
            _model.AddShape(shape);
            List<Shape> list = (List<Shape>)_target.GetField("_shapes");
            Assert.AreEqual(1, list.Count);
        }

        // 測試 DeleteShape
        [TestMethod()]
        public void DeleteShapeTest()
        {
            Shape shape = new ShapeFactory().CreateShape(ShapeType.Line);
            _model.AddShape(shape);
            _model.DeleteShape();
            List<Shape> list = (List<Shape>)_target.GetField("_shapes");
            Assert.AreEqual(0, list.Count);
        }

        // 測試 clear canvas
        [TestMethod()]
        public void ClearTest()
        {
            _model.PressPointer(ShapeType.Line, 1, 1);
            _model.ReleasePointerWithDrawingState(2, 2);
            _model.PressPointer(ShapeType.Rectangle, 3.2, 4.3);
            _model.ReleasePointerWithDrawingState(1, 2.3);
            _model.Clear();
            bool isPressed = (bool)_target.GetField("_isPressed");
            List<Shape> list = (List<Shape>)_target.GetField("_shapes");
            Assert.AreEqual(isPressed, false);
            Assert.AreEqual(list.Count, 0);
            Assert.AreEqual(_isNotify, true);
        }

        // 測試 Undo
        [TestMethod()]
        public void UndoTest()
        {
            _model.PressPointer(ShapeType.Line, 3.3, 4.4);
            _model.ReleasePointerWithDrawingState(5, 4.5);
            _model.Undo();
            List<Shape> list = (List<Shape>)_target.GetField("_shapes");
            Assert.AreEqual(list.Count, 0);
        }

        // 測試 Redo
        [TestMethod()]
        public void RedoTest()
        {
            _model.PressPointer(ShapeType.Line, 3.3, 4.4);
            _model.ReleasePointerWithDrawingState(5, 4.5);
            _model.Undo();
            _model.Redo();
            List<Shape> list = (List<Shape>)_target.GetField("_shapes");
            Assert.AreEqual(list.Count, 1);
        }

        // 測試 IsUndoEnable
        [TestMethod()]
        public void IsUndoEnableTest()
        {
            _model.PressPointer(ShapeType.Line, 3.3, 4.4);
            _model.ReleasePointerWithDrawingState(5, 4.5);
            Assert.AreEqual(true, _model.IsUndoEnable);
            _model.Undo();
            Assert.AreEqual(false, _model.IsUndoEnable);
        }

        // 測試 IsRedoEnable
        [TestMethod()]
        public void IsRedoEnableTest()
        {
            _model.PressPointer(ShapeType.Line, 3.3, 4.4);
            _model.ReleasePointerWithDrawingState(5, 4.5);
            Assert.AreEqual(false, _model.IsRedoEnable);
            _model.Undo();
            Assert.AreEqual(true, _model.IsRedoEnable);
            _model.Redo();
            Assert.AreEqual(false, _model.IsRedoEnable);
        }

        // 測試 draw
        [TestMethod()]
        public void DrawTest()
        {
            _model.PressPointer(ShapeType.Line, 1, 1);
            _model.ReleasePointerWithDrawingState(1, 2);
            _model.PressPointer(ShapeType.SixSide, 1, 1);
            IGraphics mockGraphics = new MockGraphics();
            _model.Draw(mockGraphics);
        }

        // 測試 MarkOutlines
        [TestMethod()]
        public void MarkOutlinesTest()
        {
            _model.PressPointer(ShapeType.Rectangle, 1, 1);
            _model.ReleasePointerWithDrawingState(2, 2);
            _model.PressPointer(ShapeType.Rectangle, 1.5, 1.5);
            IGraphics mockGraphics = new MockGraphics();
            _model.MarkOutlines(mockGraphics);
        }

        // 測試 RefreshShapes
        [TestMethod()]
        public void RefreshShapesTest()
        {
            IGraphics mockGraphics = new MockGraphics();
            _target.Invoke("RefreshShapes", mockGraphics);
        }

        // 測試 GetSelectShape
        [TestMethod()]
        public void GetSelectShapeWhenHasShapeTest()
        {
            _model.PressPointer(ShapeType.Rectangle, 1, 1);
            _model.ReleasePointerWithDrawingState(2, 2);
            _model.PressPointer(ShapeType.Rectangle, 3, 3);
            _model.ReleasePointerWithDrawingState(4, 4);
            _model.PressPointer(ShapeType.Rectangle, 1.5, 1.5);
            Assert.AreEqual(ShapeType.Rectangle, _model.GetSelectShape().ShapeType);
        }

        // 測試 GetSelectShape
        [TestMethod()]
        public void GetSelectShapeWhenNotHasShapeTest()
        {
            Assert.AreEqual(null, _model.GetSelectShape());
        }

        // 測試 ClearStartPoint
        [TestMethod()]
        public void ClearStartPointTest()
        {
            _target.Invoke("ClearStartPoint");
            Point startPoint = (Point)_target.GetField("_startPoint");
            Assert.AreEqual(-1, startPoint.Left);
            Assert.AreEqual(-1, startPoint.Top);
        }

        // 測試 observer
        [TestMethod()]
        public void NotifyTest()
        {
            _target.Invoke("NotifyModelChanged");
            Assert.AreEqual(_isNotify, true);
        }

        // 取得 hint 的 field
        private Point GetHintField(string fieldName)
        {
            Shape hint = (Shape)_target.GetField("_hint");
            PrivateObject hintTarget = new PrivateObject(hint);
            return (Point)hintTarget.GetField(fieldName);
        }

        // mock observer
        private void Notify()
        {
            _isNotify = true;
        }
    }
}