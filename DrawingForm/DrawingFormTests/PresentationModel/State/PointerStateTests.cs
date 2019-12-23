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
    public class PointerStateTests
    {
        Model _model;
        State _state;

        // 初始化
        [TestInitialize()]
        public void PointerStateTest()
        {
            _model = new Model();
            _state = new PointerState(_model);
            Assert.AreEqual(StateType.Pointer, _state.StateType);
        }

        // 測試 PressPointer
        [TestMethod()]
        public void PressPointerTest()
        {
            _state.PressPointer(ShapeType.Line, 10, 10);
        }

        // 測試 MovePointer
        [TestMethod()]
        public void MovePointerTest()
        {
            _state.MovePointer(10, 10);
        }

        // 測試 ReleasePointer
        [TestMethod()]
        public void ReleasePointerTest()
        {
            _state.ReleasePointer(10, 10);
        }

        // 測試 Draw
        [TestMethod()]
        public void DrawTest()
        {
            _state.Draw(new MockGraphics());
        }
    }
}