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
    public class StateTests
    {
        // 測試 RefreshShapes
        [TestMethod()]
        public void RefreshShapesTest()
        {
            Model model = new Model();
            List<Shape> shapes = new List<Shape>();
            shapes.Add(new ShapeFactory().CreateShape(ShapeType.Line));
            State state = new DrawingState(model, shapes);
            PrivateObject _target = new PrivateObject(state);
            _target.Invoke("RefreshShapes", new MockGraphics());
        }
    }
}