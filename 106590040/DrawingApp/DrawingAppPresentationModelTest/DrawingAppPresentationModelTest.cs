using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DrawingApp;
using DrawingModel;

namespace DrawingAppPresentationModelTest
{
    [TestClass]
    public class DrawingAppPresentationModelTest
    {
        DrawingAppPresentationModel _presentationModel;
        bool _isNotify;

        // 初始化 test
        [TestInitialize()]
        public void Initialize()
        {
            _presentationModel = new DrawingAppPresentationModel();
            _presentationModel._presentationModelChanged += Notify;
            _isNotify = false;
        }

        // 測試 click rectangle button
        [TestMethod()]
        public void ClickRectangleButtonTest()
        {
            _presentationModel.ClickRectangleButton();
            Assert.AreEqual(_presentationModel.IsRectangleEnable, false);
            Assert.AreEqual(_presentationModel.IsLineEnable, true);
            Assert.AreEqual(_presentationModel.ShapeType, ShapeType.Rectangle);
            Assert.AreEqual(_isNotify, true);
        }

        // 測試 click line button
        [TestMethod()]
        public void ClickLineButtonTest()
        {
            _presentationModel.ClickLineButton();
            Assert.AreEqual(_presentationModel.IsRectangleEnable, true);
            Assert.AreEqual(_presentationModel.IsLineEnable, false);
            Assert.AreEqual(_presentationModel.ShapeType, ShapeType.Line);
            Assert.AreEqual(_isNotify, true);
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
            Assert.AreEqual(_presentationModel.IsLineEnable, false);
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
