using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    public class CommandManager
    {
        private Stack<ICommand> _undo = new Stack<ICommand>();
        private Stack<ICommand> _redo = new Stack<ICommand>();

        // 執行指令，清空 redo，加入 undo
        public void Execute(ICommand command)
        {
            command.Execute();
            _undo.Push(command);
            _redo.Clear();
        }

        // 後一步
        public void Redo()
        {
            if (_redo.Count <= 0)
                throw new Exception(Constant.REDO_ERROR_MESSAGE);
            ICommand command = _redo.Pop();
            _undo.Push(command);
            command.Execute();
        }

        // 前一步
        public void Undo()
        {
            if (_undo.Count <= 0)
                throw new Exception(Constant.UNDO_ERROR_MESSAGE);
            ICommand command = _undo.Pop();
            _redo.Push(command);
            command.BackExecute();
        }

        // redo enable getter
        public bool IsRedoEnable
        {
            get
            {
                return _redo.Count != 0;
            }
        }

        // undo enable getter
        public bool IsUndoEnable
        {
            get
            {
                return _undo.Count != 0;
            }
        }
    }
}
