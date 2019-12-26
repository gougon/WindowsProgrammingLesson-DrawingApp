using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    public class Model
    {
        // Observer
        public delegate void ModelChangedEventHandler();
        public event ModelChangedEventHandler _modelChanged;

        private State _state;
        private CommandManager _commandManager = new CommandManager();

        private List<Shape> _shapes = new List<Shape>();

        private bool _isDrawingStateOver = false;

        // Constructor
        public Model()
        {
            _state = new StateFactory().CreateState(StateType.Pointer, this, _shapes);
        }

        // 設定 state
        public void SetModelState(StateType stateType)
        {
            _state = new StateFactory().CreateState(stateType, this, _shapes);
        }

        // 按下指標，會設定起始點位置以及初始化 _hint
        public void PressPointer(ShapeType shapeType, double left, double top)
        {
            _state.PressPointer(shapeType, left, top);
        }

        // 指標移動，若是有被按下，就改變 _hint 位置
        public void MovePointer(double left, double top)
        {
            _state.MovePointer(left, top);
        }

        // 放開指標，若是有被按下，加入 shape
        public void ReleasePointer(double left, double top)
        {
            _state.ReleasePointer(_commandManager, left, top);
            if (_state.StateType == StateType.Drawing)
            {
                _isDrawingStateOver = true;
            }
        }

        // 加入 shape
        public void AddShape(Shape shape)
        {
            _shapes.Add(shape);
        }

        // 刪除 shape
        public void DeleteShape()
        {
            int length = _shapes.Count;
            _shapes.RemoveAt(length - 1);
        }

        // 清除 canvas
        public void Clear()
        {
            _commandManager.Execute(new ClearCommand(this, _shapes));
            _state.Clear();
            NotifyModelChanged();
        }

        // redo
        public void Redo()
        {
            _commandManager.Redo();
            _state.Clear();
            NotifyModelChanged();
        }

        // undo 
        public void Undo()
        {
            _commandManager.Undo();
            _state.Clear();
            NotifyModelChanged();
        }

        // Is Redo enable
        public bool IsRedoEnable
        {
            get
            {
                return _commandManager.IsRedoEnable;
            }
        }

        // Is Undo enable
        public bool IsUndoEnable
        {
            get
            {
                return _commandManager.IsUndoEnable;
            }
        }

        // 在 canvas 上繪圖
        public void Draw(IGraphics graphics)
        {
            _state.Draw(graphics);
            if (_isDrawingStateOver)
            {
                _state = new StateFactory().CreateState(StateType.Pointer, this, _shapes);
                _isDrawingStateOver = false;
            }
        }

        // 尋找點擊到的 shape
        public Shape GetSelectShape()
        {
            return _state.GetSelectShape();
        }

        // 返回 select shape 的 information
        public string Information
        {
            get
            {
                if (_state.StateType == StateType.Pointer && GetSelectShape() != null)
                {
                    return GetSelectShape().Information;
                }
                return "";
            }
        }

        // observer
        public void NotifyModelChanged()
        {
            if (_modelChanged != null)
            {
                _modelChanged();
            }
        }
    }
}
