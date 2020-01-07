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
    public class StateFactoryTests
    {
        StateFactory _stateFactory = new StateFactory();
        Model _model = new Model();
        List<Shape> _shapes = new List<Shape>();

        // 測試 createState
        [TestMethod()]
        public void CreateStateTest()
        {
            State state = _stateFactory.CreateState(StateType.Pointer, _model, _shapes);
            Assert.AreEqual(StateType.Pointer, state.StateType);

            state = _stateFactory.CreateState(StateType.Drawing, _model, _shapes);
            Assert.AreEqual(StateType.Drawing, state.StateType);
        }

        // 測試 shapetype 錯誤的 createState
        [ExpectedException(typeof(Exception))]
        [TestMethod()]
        public void CreateStateWithWrongShapeType()
        {
            State state = _stateFactory.CreateState((StateType)5, _model, _shapes);
            Assert.AreEqual(StateType.Drawing, state.StateType);
        }
    }
}