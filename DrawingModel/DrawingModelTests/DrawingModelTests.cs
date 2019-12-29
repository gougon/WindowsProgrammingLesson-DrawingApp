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
        }

        // 測試移動指標
        [TestMethod()]
        public void MovePointerTest()
        {
            _model.MovePointer(5, 4.5);
        }

        // 測試在 drawing state 放開指標
        [TestMethod()]
        public void ReleasePointerWhenDrawingStateTest()
        {
            _model.SetModelState(StateType.Drawing);
            _model.PressPointer(ShapeType.Line, 7, 8);
            _model.ReleasePointer(5, 4.5);
            bool isDrawingStateOver = (bool)_target.GetField("_isDrawingStateOver");
            List<Shape> list = (List<Shape>)_target.GetField("_shapes");
            Assert.AreEqual(true, isDrawingStateOver);
            Assert.AreEqual(1, list.Count);
        }

        // 測試在 drawing state 放開指標
        [TestMethod()]
        public void ReleasePointerWhenPointerStateTest()
        {
            _model.SetModelState(StateType.Pointer);
            _model.PressPointer(ShapeType.Line, 7, 8);
            _model.ReleasePointer(5, 4.5);
            bool isDrawingStateOver = (bool)_target.GetField("_isDrawingStateOver");
            List<Shape> list = (List<Shape>)_target.GetField("_shapes");
            Assert.AreEqual(false, isDrawingStateOver);
            Assert.AreEqual(list.Count, 0);
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
            _model.ReleasePointer(2, 2);
            _model.PressPointer(ShapeType.Rectangle, 3.2, 4.3);
            _model.ReleasePointer(1, 2.3);
            _model.Clear();
            List<Shape> list = (List<Shape>)_target.GetField("_shapes");
            Assert.AreEqual(list.Count, 0);
            Assert.AreEqual(_isNotify, true);
        }

        // 測試 Undo
        [TestMethod()]
        public void UndoTest()
        {
            _model.SetModelState(StateType.Drawing);
            _model.PressPointer(ShapeType.Line, 3.3, 4.4);
            _model.ReleasePointer(5, 4.5);
            _model.Undo();
            List<Shape> list = (List<Shape>)_target.GetField("_shapes");
            Assert.AreEqual(list.Count, 0);
        }

        // 測試 Redo
        [TestMethod()]
        public void RedoTest()
        {
            _model.SetModelState(StateType.Drawing);
            _model.PressPointer(ShapeType.Line, 3.3, 4.4);
            _model.ReleasePointer(5, 4.5);
            _model.Undo();
            _model.Redo();
            List<Shape> list = (List<Shape>)_target.GetField("_shapes");
            Assert.AreEqual(list.Count, 1);
        }

        // 測試 IsUndoEnable
        [TestMethod()]
        public void IsUndoEnableTest()
        {
            _model.SetModelState(StateType.Drawing);
            _model.PressPointer(ShapeType.Line, 3.3, 4.4);
            _model.ReleasePointer(5, 4.5);
            Assert.AreEqual(true, _model.IsUndoEnable);
            _model.Undo();
            Assert.AreEqual(false, _model.IsUndoEnable);
        }

        // 測試 IsRedoEnable
        [TestMethod()]
        public void IsRedoEnableTest()
        {
            _model.SetModelState(StateType.Drawing);
            _model.PressPointer(ShapeType.Line, 3.3, 4.4);
            _model.ReleasePointer(5, 4.5);
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
            _model.SetModelState(StateType.Drawing);
            _model.PressPointer(ShapeType.Line, 1, 1);
            _model.ReleasePointer(1, 2);
            _model.PressPointer(ShapeType.SixSide, 1, 1);
            IGraphics mockGraphics = new MockGraphics();
            _model.Draw(mockGraphics);
        }

        // 測試 MarkOutlines
        [TestMethod()]
        public void MarkOutlinesTest()
        {
            _model.PressPointer(ShapeType.Rectangle, 1, 1);
            _model.ReleasePointer(2, 2);
            _model.PressPointer(ShapeType.Rectangle, 1.5, 1.5);
            IGraphics mockGraphics = new MockGraphics();
            _model.Draw(mockGraphics);
        }

        // 測試 GetSelectShape
        [TestMethod()]
        public void GetSelectShapeWhenNotHasShapeTest()
        {
            Assert.AreEqual(null, _model.GetSelectShape());
        }

        // 測試 Information
        [TestMethod()]
        public void InformationTest()
        {
            Assert.AreEqual("", _model.Information);
            _model.SetModelState(StateType.Drawing);
            _model.PressPointer(ShapeType.Rectangle, 1, 1);
            _model.ReleasePointer(10, 10);
            Assert.AreEqual("", _model.Information);
            _model.SetModelState(StateType.Pointer);
            _model.PressPointer(ShapeType.Line, 2, 2);
            Assert.AreEqual("Rectangle (1, 1, 9, 9)", _model.Information);
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