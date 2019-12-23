using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using DrawingModel;

namespace DrawingApp
{
    public sealed partial class MainPage : Page
    {
        Model _model;
        DrawingAppPresentationModel _presentationModel;

        public MainPage()
        {
            this.InitializeComponent();

            // 設定 _canvas
            _canvas.PointerPressed += HandleCanvasPressed;
            _canvas.PointerMoved += HandleCanvasMoved;
            _canvas.PointerReleased += HandleCanvasReleased;

            // 設定 buttons
            _rectangleButton.Click += HandleRectangleButtonClick;
            _lineButton.Click += HandleLineButtonClick;
            _sixSideButton.Click += HandleSixSideButtonClick;
            _clearButton.Click += HandleClearButtonClick;
            _redoButton.Click += HandleRedoButtonClick;
            _undoButton.Click += HandleUndoButtonClick;

            // 設定 model
            _model = new Model();
            _presentationModel = new DrawingAppPresentationModel(_model, new AppGraphicsAdapter(_canvas));
            _model._modelChanged += HandleModelChanged;
            _presentationModel._presentationModelChanged += HandlePresentationModelChanged;
        }

        // 處理指標的 press 事件
        private void HandleCanvasPressed(object sender, PointerRoutedEventArgs e)
        {
            _presentationModel.PressPointer(e.GetCurrentPoint(_canvas).Position.X, e.GetCurrentPoint(_canvas).Position.Y);
        }

        // 處理指標的 move 事件
        private void HandleCanvasMoved(object sender, PointerRoutedEventArgs e)
        {
            _presentationModel.MovePointer(e.GetCurrentPoint(_canvas).Position.X, e.GetCurrentPoint(_canvas).Position.Y);
        }

        // 處理指標的 release 事件
        private void HandleCanvasReleased(object sender, PointerRoutedEventArgs e)
        {
            _presentationModel.ReleasePointer(e.GetCurrentPoint(_canvas).Position.X, e.GetCurrentPoint(_canvas).Position.Y);
        }

        // 處理 _rectangleButton 的 click 事件
        private void HandleRectangleButtonClick(object sender, RoutedEventArgs e)
        {
            _presentationModel.ClickShapeButton(ShapeType.Rectangle);
        }

        // 處理 _lineButton 的 click 事件
        private void HandleLineButtonClick(object sender, RoutedEventArgs e)
        {
            _presentationModel.ClickShapeButton(ShapeType.Line);
        }

        // 處理 _sixSideButton 的 click 事件
        private void HandleSixSideButtonClick(object sender, RoutedEventArgs e)
        {
            _presentationModel.ClickShapeButton(ShapeType.SixSide);
        }

        // 處理 _undoButton 的 click 事件
        private void HandleUndoButtonClick(object sender, RoutedEventArgs e)
        {
            _presentationModel.ClickUndo();
        }

        // 處理 _redoButton 的 click 事件
        private void HandleRedoButtonClick(object sender, RoutedEventArgs e)
        {
            _presentationModel.ClickRedo();
        }

        // 處理 _clearButton 的 click 事件
        private void HandleClearButtonClick(object sender, RoutedEventArgs e)
        {
            _model.Clear();
        }

        // model observer
        private void HandleModelChanged()
        {
            _presentationModel.Draw();
            _selectLabel.Text = Constant.SELECT_LABEL_TEXT + _presentationModel.GetSelectShapeInformation();
            _redoButton.IsEnabled = _presentationModel.IsRedoEnable;
            _undoButton.IsEnabled = _presentationModel.IsUndoEnable;
        }

        // presentation observer
        private void HandlePresentationModelChanged()
        {
            _rectangleButton.IsEnabled = _presentationModel.IsRectangleEnable;
            _lineButton.IsEnabled = _presentationModel.IsLineEnable;
            _sixSideButton.IsEnabled = _presentationModel.IsSixSideEnable;
        }
    }
}
