using Microsoft.VisualStudio.TestTools.UnitTesting;
using DrawingForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrawingModel;

namespace DrawingForm.Tests
{
    [TestClass()]
    public class DrawingFormPresentationModelTests
    {
        Model _model;
        PrivateObject _modelTarget;
        DrawingFormPresentationModel _presentationModel;
        PrivateObject _target;
        bool _isNotify;

        // 初始化 test
        [TestInitialize()]
        public void Initialize()
        {
            _model = new Model();
            _modelTarget = new PrivateObject(_model);
            _presentationModel = new DrawingFormPresentationModel(_model);
            _target = new PrivateObject(_presentationModel);
            _presentationModel._presentationModelChanged += Notify;
            _isNotify = false;
        }

        // 測試 click shape button
        [TestMethod()]
        public void ClickShapeButtonTest()
        {
            _presentationModel.ClickShapeButton(ShapeType.Rectangle);
            Assert.AreEqual(false, _presentationModel.IsRectangleEnable);
            Assert.AreEqual(true, _presentationModel.IsLineEnable);
            Assert.AreEqual(ShapeType.Rectangle, _presentationModel.ShapeType);
            Assert.AreEqual(true, _isNotify);
        }

        // 測試 SetButtonEnable
        [TestMethod()]
        public void SetButtonEnableTest()
        {
            List<bool> buttonEnableStatus = new List<bool>() { true, false, true };
            _target.Invoke("SetButtonEnable", buttonEnableStatus);
            Assert.AreEqual(true, _presentationModel.IsLineEnable);
            Assert.AreEqual(false, _presentationModel.IsRectangleEnable);
            Assert.AreEqual(true, _presentationModel.IsSixSideEnable);
        }

        // 測試 ClickUndo
        [TestMethod()]
        public void ClickUndoButton()
        {
            AddCommandToShape();
            _presentationModel.ClickUndo();
            List<Shape> shapes = (List<Shape>)_modelTarget.GetField("_shapes");
            Assert.AreEqual(0, shapes.Count);
            Assert.AreEqual(true, _isNotify);
        }

        // 測試 ClickRedo
        [TestMethod()]
        public void ClickRedoButton()
        {
            AddCommandToShape();
            _presentationModel.ClickUndo();
            _presentationModel.ClickRedo();
            List<Shape> shapes = (List<Shape>)_modelTarget.GetField("_shapes");
            Assert.AreEqual(1, shapes.Count);
            Assert.AreEqual(true, _isNotify);
        }

        // 在 model 加入一個 command
        private void AddCommandToShape()
        {
            ICommand command = new DrawCommand(_model, new ShapeFactory().CreateShape(ShapeType.Line));
            CommandManager manager = (CommandManager)_modelTarget.GetField("_commandManager");
            manager.Execute(command);
        }

        // 測試 PressPointer
        [TestMethod()]
        public void PressPointerTest()
        {
            _presentationModel.PressPointer(10, 5);
        }

        // 測試 MovePointer
        [TestMethod()]
        public void MovePointerTest()
        {
            _presentationModel.MovePointer(5, 10);
        }

        // 測試 ReleasePointer
        [TestMethod()]
        public void ReleasePointerTest()
        {
            _presentationModel.ClickShapeButton(ShapeType.Line);
            _presentationModel.PressPointer(10, 5);
            _presentationModel.ReleasePointer(5, 10);
            Assert.AreEqual(true, _presentationModel.IsLineEnable);
            Assert.AreEqual(true, _presentationModel.IsRectangleEnable);
            Assert.AreEqual(true, _presentationModel.IsSixSideEnable);
        }

        // 測試 RefreshEnableStatus
        [TestMethod()]
        public void RefreshEnableStatusTest()
        {
            _target.Invoke("RefreshEnableStatus");
            Assert.AreEqual(true, _presentationModel.IsLineEnable);
            Assert.AreEqual(true, _presentationModel.IsRectangleEnable);
            Assert.AreEqual(true, _presentationModel.IsSixSideEnable);
            Assert.AreEqual(true, _isNotify);
        }

        // 測試 Draw
        [TestMethod()]
        public void DrawTest()
        {
            _presentationModel.Draw(new MockGraphics());
        }

        // 測試 GetSelectShapeInformation
        [TestMethod()]
        public void GetSelectShapeInformationTest()
        {
            Assert.AreEqual("", _presentationModel.GetSelectShapeInformation());
            _presentationModel.ClickShapeButton(ShapeType.Rectangle);
            Assert.AreEqual("", _presentationModel.GetSelectShapeInformation());
            _presentationModel.PressPointer(1, 1);
            _presentationModel.ReleasePointer(10, 10);
            _presentationModel.Draw(new MockGraphics());
            _presentationModel.PressPointer(5, 5);
            Assert.AreEqual("Rectangle (1, 1, 9, 9)", _presentationModel.GetSelectShapeInformation());
        }

        // 測試 ShapeType property
        [TestMethod()]
        public void ShapeTypeTest()
        {
            
            Assert.AreEqual(_presentationModel.ShapeType, ShapeType.Line);
            _presentationModel.ShapeType = ShapeType.Rectangle;
            Assert.AreEqual(_presentationModel.ShapeType, ShapeType.Rectangle);
        }

        // 測試 IsRectangleEnable Property
        [TestMethod()]
        public void IsRectangleEnableTest()
        {
            Assert.AreEqual(_presentationModel.IsRectangleEnable, true);
        }

        // 測試 IsLineEnable Property
        [TestMethod()]
        public void IsLineEnableTest()
        {
            Assert.AreEqual(_presentationModel.IsLineEnable, true);
        }

        // 測試 IsSixSideEnable Property
        [TestMethod()]
        public void IsSixSideEnableTest()
        {
            Assert.AreEqual(_presentationModel.IsSixSideEnable, true);
        }

        // 測試 IsRedoEnable Property
        [TestMethod()]
        public void IsRedoEnableTest()
        {
            Assert.AreEqual(_presentationModel.IsRedoEnable, false);
        }

        // 測試 IsUndoEnable Property
        [TestMethod()]
        public void IsUndoEnableTest()
        {
            Assert.AreEqual(_presentationModel.IsUndoEnable, false);
        }

        // 測試 observer
        [TestMethod()]
        public void NotifyPresentationModelChangedTest()
        {
            _presentationModel.NotifyPresentationModelChanged();
            Assert.AreEqual(_isNotify, true);
        }

        // mock observer
        private void Notify()
        {
            _isNotify = true;
        }
    }
}