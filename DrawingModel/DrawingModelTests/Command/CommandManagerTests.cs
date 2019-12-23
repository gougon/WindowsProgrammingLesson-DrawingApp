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
    public class CommandManagerTests
    {
        CommandManager _manager;
        Model _model;
        PrivateObject _target;
        Stack<ICommand> _undo;
        Stack<ICommand> _redo;

        // 初始化
        [TestInitialize()]
        public void Initialize()
        {
            _manager = new CommandManager();
            _model = new Model();
            _target = new PrivateObject(_manager);
            _undo = (Stack<ICommand>)_target.GetField("_undo");
            _redo = (Stack<ICommand>)_target.GetField("_redo");
        }

        // 測試 Execute
        [TestMethod()]
        public void ExecuteTest()
        {
            ICommand command = new DrawCommand(_model, new ShapeFactory().CreateShape(ShapeType.Line));
            _manager.Execute(command);
            Assert.AreEqual(1, _undo.Count);
            Assert.AreEqual(0, _redo.Count);
        }

        // 測試 Undo exception
        [ExpectedException(typeof(Exception))]
        [TestMethod()]
        public void UndoWithExceptionTest()
        {
            _manager.Undo();
        }

        // 測試 Undo
        [TestMethod()]
        public void UndoTest()
        {
            ICommand command = new DrawCommand(_model, new ShapeFactory().CreateShape(ShapeType.Line));
            _manager.Execute(command);
            _manager.Undo();
            Assert.AreEqual(0, _undo.Count);
            Assert.AreEqual(1, _redo.Count);
        }

        // 測試 Redo excetion
        [ExpectedException(typeof(Exception))]
        [TestMethod()]
        public void RedoWithExceptionTest()
        {
            _manager.Redo();
        }

        // 測試 Redo
        [TestMethod()]
        public void RedoTest()
        {
            ICommand command = new DrawCommand(_model, new ShapeFactory().CreateShape(ShapeType.Line));
            _manager.Execute(command);
            _manager.Undo();
            _manager.Redo();
            Assert.AreEqual(1, _undo.Count);
            Assert.AreEqual(0, _redo.Count);
        }

        // 測試 IsUndoEnable
        [TestMethod()]
        public void IsUndoEnableTest()
        {
            Assert.AreEqual(false, _manager.IsUndoEnable);
            ICommand command = new DrawCommand(_model, new ShapeFactory().CreateShape(ShapeType.Line));
            _manager.Execute(command);
            Assert.AreEqual(true, _manager.IsUndoEnable);
        }

        // 測試 IsRedoEnable
        [TestMethod()]
        public void IsRedoEnableTest()
        {
            Assert.AreEqual(false, _manager.IsUndoEnable);
            ICommand command = new DrawCommand(_model, new ShapeFactory().CreateShape(ShapeType.Line));
            _manager.Execute(command);
            _manager.Undo();
            Assert.AreEqual(true, _manager.IsRedoEnable);
        }
    }
}