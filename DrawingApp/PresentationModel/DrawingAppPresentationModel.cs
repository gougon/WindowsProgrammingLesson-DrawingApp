using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using DrawingModel;

namespace DrawingApp
{
    public class DrawingAppPresentationModel
    {
        public delegate void PresentationModelChangedEventHandler();
        public event PresentationModelChangedEventHandler _presentationModelChanged;

        private Model _model;
        private State _state;
        private ShapeType _shapeType = ShapeType.Line; // 預設是 line
        private bool _isRectangleEnable = true;
        private bool _isLineEnable = true;
        private bool _isSixSideEnable = true;
        private bool _isStateChange = false;
        private IGraphics _graphics;

        // 初始化 model
        public DrawingAppPresentationModel(Model model, IGraphics adapter)
        {
            _model = model;
            _graphics = adapter;
            _state = new PointerState(_model);
        }

        // 按下 shape button，notify observer
        public void ClickShapeButton(ShapeType shapeType)
        {
            _state = new DrawingState(_model);
            List<bool> buttonEnableStatus = new List<bool>()
            { 
                true, true, true };
            buttonEnableStatus[(int)shapeType] = false;
            SetButtonEnable(buttonEnableStatus);
            _shapeType = shapeType;
            _model.NotifyModelChanged();
            NotifyPresentationModelChanged();
        }

        // 按照 enable list 設定 button enable
        private void SetButtonEnable(List<bool> buttonEnableStatus)
        {
            _isLineEnable = buttonEnableStatus[(int)ShapeType.Line];
            _isRectangleEnable = buttonEnableStatus[(int)ShapeType.Rectangle];
            _isSixSideEnable = buttonEnableStatus[(int)ShapeType.SixSide];
        }

        // 處理按下 redo 的事件
        public void ClickRedo()
        {
            _model.Redo();
            NotifyPresentationModelChanged();
        }

        // 處理按下 undo 的事件
        public void ClickUndo()
        {
            _model.Undo();
            NotifyPresentationModelChanged();
        }

        // 處理按下指標的事件
        public void PressPointer(double left, double top)
        {
            _state.PressPointer(_shapeType, left, top);
        }

        // 處理移動指標的事件
        public void MovePointer(double left, double top)
        {
            _state.MovePointer(left, top);
        }

        // 處理放開指標的事件
        public void ReleasePointer(double left, double top)
        {
            _isStateChange = IsStateChange();
            _state.ReleasePointer(left, top);
            RefreshEnableStatus();
        }

        // 決定 state 狀態
        private bool IsStateChange()
        {
            return _state.StateType == StateType.Drawing;
        }

        // 若是 DrawingState，改變 enable 狀態
        private void RefreshEnableStatus()
        {
            List<bool> buttonEnableStatus = new List<bool>()
            { 
                true, true, true };
            SetButtonEnable(buttonEnableStatus);
            NotifyPresentationModelChanged();
        }

        // 處理 paint 事件
        public void Draw()
        {
            _state.Draw(_graphics);
            if (_isStateChange)
            {
                _state = new PointerState(_model);
                _isStateChange = false;
            }
        }

        // 取得 select label 訊息
        public string GetSelectShapeInformation()
        {
            string information = "";
            Shape selectShape = _model.GetSelectShape();
            if (_state.StateType == StateType.Pointer && selectShape != null)
            {
                information = selectShape.Information;
            }
            return information;
        }

        // _shapeType 的 getter & setter
        public ShapeType ShapeType
        {
            get
            {
                return _shapeType;
            }
            set
            {
                _shapeType = value;
            }
        }

        // _isRectangle 的 getter
        public bool IsRectangleEnable
        {
            get
            {
                return _isRectangleEnable;
            }
        }

        // _isLine 的 getter
        public bool IsLineEnable
        {
            get
            {
                return _isLineEnable;
            }
        }

        // _isHexagon 的 getter
        public bool IsSixSideEnable
        {
            get
            {
                return _isSixSideEnable;
            }
        }

        // _isRedoEnable 的 getter
        public bool IsRedoEnable
        {
            get
            {
                return _model.IsRedoEnable;
            }
        }

        // _isUndoEnable 的 getter
        public bool IsUndoEnable
        {
            get
            {
                return _model.IsUndoEnable;
            }
        }

        // observer
        public void NotifyPresentationModelChanged()
        {
            if (_presentationModelChanged != null)
            {
                _presentationModelChanged();
            }
        }
    }
}
