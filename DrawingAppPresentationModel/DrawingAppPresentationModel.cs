using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrawingModel;

namespace DrawingApp
{
    public class DrawingAppPresentationModel
    {
        public delegate void PresentationModelChangedEventHandler();
        public event PresentationModelChangedEventHandler _presentationModelChanged;
        ShapeType _shapeType = ShapeType.Line;
        bool _isRectangleEnable = true;
        bool _isLineEnable = false;

        // 按下 rectangle button，notify observer
        public void ClickRectangleButton()
        {
            _isRectangleEnable = false;
            _isLineEnable = true;
            _shapeType = ShapeType.Rectangle;
            NotifyPresentationModelChanged();
        }

        // 按下 line button，notify observer
        public void ClickLineButton()
        {
            _isRectangleEnable = true;
            _isLineEnable = false;
            _shapeType = ShapeType.Line;
            NotifyPresentationModelChanged();
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
