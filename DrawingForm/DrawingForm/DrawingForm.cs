using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DrawingModel;

namespace DrawingForm
{
    public partial class DrawingForm : Form
    {
        private const string CANVAS_NAME = "canvas";
        private Model _model;
        private DrawingFormPresentationModel _presentationModel;
        private Panel _canvas = new DoubleBufferedPanel();

        public DrawingForm()
        {
            InitializeComponent();

            // 設定 canvas
            _canvas.AccessibleName = CANVAS_NAME;
            _canvas.Dock = DockStyle.Fill;
            _canvas.BackColor = Color.LightYellow;
            _canvas.MouseDown += HandleCanvasPressed;
            _canvas.MouseMove += HandleCanvasMoved;
            _canvas.MouseUp += HandleCanvasReleased;
            _canvas.Paint += HandleCanvasPainted;
            this.Controls.Add(_canvas);

            // 設定 buttons
            _clearButton.Click += HandleClearButtonClick;
            _lineButton.Click += HandleLineButtonClick;
            _rectangleButton.Click += HandleRectangleButtonClick;
            _sixSideButton.Click += HandleSixSideButtonClick;
            _redoMenuItem.Click += HandleRedoButtonClick;
            _undoMenuItem.Click += HandleUndoButtonClick;
            _saveMenuItem.Click += HandleSaveButtonClick;
            _loadMenuItem.Click += HandleLoadButtonClick;

            // 設定 model
            _model = new Model();
            _presentationModel = new DrawingFormPresentationModel(_model);
            _model._modelChanged += HandleModelChanged;
            _presentationModel._presentationModelChanged += HandlePresentationModelChanged;
        }

        // 處理按下指標的 event
        private void HandleCanvasPressed(object sender, MouseEventArgs e)
        {
            _presentationModel.PressPointer(e.X, e.Y);
        }

        // 處理移動指標的 event
        private void HandleCanvasMoved(object sender, MouseEventArgs e)
        {
            _presentationModel.MovePointer(e.X, e.Y);
        }

        // 處理放開指標的 event
        private void HandleCanvasReleased(object sender, MouseEventArgs e)
        {
            _presentationModel.ReleasePointer(e.X, e.Y);
        }

        // 處理 canvas 的 paint event
        private void HandleCanvasPainted(object sender, PaintEventArgs e)
        {
            _presentationModel.Draw(new FormGraphicsAdapter(e.Graphics));
        }

        // 按下 clear button 的 click event，清空畫面
        private void HandleClearButtonClick(object sender, EventArgs e)
        {
            _model.Clear();
        }

        // 按下 line button 的 click event，畫 line 模式
        private void HandleLineButtonClick(object sender, EventArgs e)
        {
            _presentationModel.ClickShapeButton(ShapeType.Line);
        }

        // 按下 rectangle button 的 click event，畫 rectangle 模式
        private void HandleRectangleButtonClick(object sender, EventArgs e)
        {
            _presentationModel.ClickShapeButton(ShapeType.Rectangle);
        }

        // 按下 six side button 的 click event，畫 six side 模式
        private void HandleSixSideButtonClick(object sender, EventArgs e)
        {
            _presentationModel.ClickShapeButton(ShapeType.SixSide);
        }

        // 按下 redo button 的 click event
        private void HandleRedoButtonClick(object sender, EventArgs e)
        {
            _model.Redo();
        }

        // 按下 undo button 的 click event
        private void HandleUndoButtonClick(object sender, EventArgs e)
        {
            _model.Undo();
        }

        // 按下 save button 的 click event
        private void HandleSaveButtonClick(object sender, EventArgs e)
        {
            _model.Save();
        }

        // 按下 load button 的 click event
        private void HandleLoadButtonClick(object sender, EventArgs e)
        {
            _model.Load();
        }

        // model observer
        private void HandleModelChanged()
        {
            _selectLabel.Text = Constant.SELECT_LABEL_TEXT;
            _redoMenuItem.Enabled = _presentationModel.IsRedoEnable;
            _undoMenuItem.Enabled = _presentationModel.IsUndoEnable;
            _selectLabel.Text = Constant.SELECT_LABEL_TEXT + _presentationModel.GetSelectShapeInformation();
            Invalidate(true);
        }

        // presentationModel observer
        private void HandlePresentationModelChanged()
        {
            _rectangleButton.Enabled = _presentationModel.IsRectangleEnable;
            _lineButton.Enabled = _presentationModel.IsLineEnable;
            _sixSideButton.Enabled = _presentationModel.IsSixSideEnable;
        }
    }
}
