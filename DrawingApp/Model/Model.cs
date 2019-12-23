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

        private Point _startPoint = new Point();
        private bool _isPressed = false;
        private List<Shape> _shapes = new List<Shape>();
        private Shape _hint;

        private CommandManager _commandManager = new CommandManager();
        private ShapeFactory _factory = new ShapeFactory();
        private ShapeType _type;

        // 按下指標，會設定起始點位置以及初始化 _hint
        public void PressPointer(ShapeType type, double left, double top)
        {
            if (left > 0 && top > 0)
            {
                _type = type;
                _startPoint.Left = left;
                _startPoint.Top = top;
                _hint = _factory.CreateShape(_type);
                SetShapePoints(_hint, _startPoint, _startPoint);
                _isPressed = true;
                NotifyModelChanged();
            }
        }

        // 指標移動，若是有被按下，就改變 _hint 位置
        public void MovePointer(double left, double top)
        {
            if (_isPressed)
            {
                SetShapePoints(_hint, _startPoint, new Point(left, top));
                NotifyModelChanged();
            }
        }

        // 放開指標，若是有被按下，加入 shape
        public void ReleasePointerWithDrawingState(double left, double top)
        {
            if (_isPressed)
            {
                _isPressed = false;
                Shape shape = _factory.CreateShape(_type);
                SetShapePoints(shape, _startPoint, new Point(left, top));
                ClearStartPoint();
                _commandManager.Execute(new DrawCommand(this, shape));
                NotifyModelChanged();
            }
        }

        // 放開指標
        public void ReleasePointerWithPointerState(double left, double top)
        {
            if (_isPressed)
            {
                _isPressed = false;
                NotifyModelChanged();
            }
        }

        // 設定 point 的 startPoint 和 endPoint
        private void SetShapePoints(Shape shape, Point startPoint, Point endPoint)
        {
            shape.SetStartPoint(startPoint.Left, startPoint.Top);
            shape.SetEndPoint(endPoint.Left, endPoint.Top);
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
            _isPressed = false;
            _commandManager.Execute(new ClearCommand(this, _shapes));
            NotifyModelChanged();
        }

        // redo
        public void Redo()
        {
            _commandManager.Redo();
            ClearStartPoint();
            NotifyModelChanged();
        }

        // undo 
        public void Undo()
        {
            _commandManager.Undo();
            ClearStartPoint();
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
            RefreshShapes(graphics);
            if (_isPressed)
            {
                _hint.Draw(graphics);
            }
        }

        // 標示外框
        public void MarkOutlines(IGraphics graphics)
        {
            RefreshShapes(graphics);
            Shape selectShape = GetSelectShape();
            if (selectShape != null)
            {
                selectShape.MarkOutlines(graphics);
            }
        }

        // 將所有 shape 重繪
        private void RefreshShapes(IGraphics graphics)
        {
            graphics.ClearAll();
            foreach (Shape shape in _shapes)
            {
                shape.Draw(graphics);
            }
        }

        // 尋找點擊到的 shape
        public Shape GetSelectShape()
        {
            double left = _startPoint.Left;
            double top = _startPoint.Top;
            int shapeQuantity = _shapes.Count;
            for (int order = shapeQuantity - 1; order >= 0; order--)
            {
                Shape shape = _shapes[order];
                if (shape.IsPointInShape(new Point(left, top)))
                {
                    return shape;
                }
            }
            return null;
        }

        // 重設 startPoint
        private void ClearStartPoint()
        {
            _startPoint.Top = -1;
            _startPoint.Left = -1;
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
