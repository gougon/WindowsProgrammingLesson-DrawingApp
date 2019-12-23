using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Media;
using DrawingModel;

namespace DrawingApp
{
    public class AppGraphicsAdapter : IGraphics
    {
        Canvas _canvas;

        public AppGraphicsAdapter(Canvas canvas)
        {
            _canvas = canvas;
        }

        // override IGraphics 的 ClearAll，清除畫面上的圖案
        public void ClearAll()
        {
            _canvas.Children.Clear();
        }

        // override IGraphics 的 DrawLine，畫 line
        public void DrawLine(Point startPoint, Point endPoint)
        {
            Windows.UI.Xaml.Shapes.Line line = new Windows.UI.Xaml.Shapes.Line();
            line.X1 = startPoint.Left;
            line.Y1 = startPoint.Top;
            line.X2 = endPoint.Left;
            line.Y2 = endPoint.Top;
            line.Stroke = new SolidColorBrush(Colors.Black);
            _canvas.Children.Add(line);
        }

        /// override IGraphics 的 DrawRectangle，畫 rectangle
        public void DrawRectangle(Point startPoint, Point endPoint)
        {
            Windows.UI.Xaml.Shapes.Rectangle rectangle = GetRectangle(startPoint, endPoint);
            rectangle.Fill = new SolidColorBrush(Colors.Yellow);
            rectangle.Stroke = new SolidColorBrush(Colors.Black);
            _canvas.Children.Add(rectangle);
        }

        // 取得 rectangle
        private Windows.UI.Xaml.Shapes.Rectangle GetRectangle(Point startPoint, Point endPoint)
        {
            Windows.UI.Xaml.Shapes.Rectangle rectangle = new Windows.UI.Xaml.Shapes.Rectangle();
            float left = (float)startPoint.GetSmallLeft(endPoint);
            float top = (float)startPoint.GetSmallTop(endPoint);
            double width = startPoint.GetLeftDifference(endPoint);
            double height = startPoint.GetTopDifference(endPoint);
            rectangle.Margin = new Windows.UI.Xaml.Thickness(left, top, 0, 0);
            rectangle.Width = width;
            rectangle.Height = height;
            return rectangle;
        }

        // Implement DrawSixSide
        public void DrawSixSide(DrawingModel.Point startPoint, DrawingModel.Point endPoint)
        {
            Windows.UI.Xaml.Shapes.Polygon sixSide = new Windows.UI.Xaml.Shapes.Polygon();
            PointCollection collection = GetSixSidePoints(startPoint, endPoint);
            sixSide.Points = collection;
            sixSide.Fill = new SolidColorBrush(Colors.Orange);
            sixSide.Stroke = new SolidColorBrush(Colors.Black);
            _canvas.Children.Add(sixSide);
        }

        // use start, end points to calculate six side points
        private PointCollection GetSixSidePoints(DrawingModel.Point startPoint, DrawingModel.Point endPoint)
        {
            double horizontalLine = CalculateHorizontalLine(startPoint, endPoint);
            PointCollection points = new PointCollection();
            for (int side = 0; side < Constant.SIX; side++)
            {
                points.Add(CalculatePointPosition(side, horizontalLine, startPoint, endPoint));
            }
            return points;
        }

        // caculate horizontal line length
        private double CalculateHorizontalLine(DrawingModel.Point startPoint, DrawingModel.Point endPoint)
        {
            double length = startPoint.GetLeftDifference(endPoint);
            double width = startPoint.GetTopDifference(endPoint);
            double horizontalLine = 0;
            if (IsLengthBiggerThanWidth(length, width))
                horizontalLine = GetHorizontalLineWithBiggerLength(length, width);
            else
                horizontalLine = GetHorizontalLineWithBiggerWidth(length, width);
            return horizontalLine;
        }

        // whether length > width
        private bool IsLengthBiggerThanWidth(double length, double width)
        {
            return length > width * Constant.TWO / Constant.ROOT_THREE;
        }

        // calculate horizontalline in length > width
        private double GetHorizontalLineWithBiggerLength(double length, double width)
        {
            return length - (width / (Constant.TWO * Constant.ROOT_THREE));
        }

        // calculate horizontalline in length <= width
        private double GetHorizontalLineWithBiggerWidth(double length, double width)
        {
            return length / Constant.FOUR * Constant.THREE;
        }

        // use side to calculate point's position
        private Windows.Foundation.Point CalculatePointPosition(int side, double horizontalLine, DrawingModel.Point startPoint, DrawingModel.Point endPoint)
        {
            double left = 0;
            double top = 0;
            double length = startPoint.GetLeftDifference(endPoint);
            double width = startPoint.GetTopDifference(endPoint);
            if (side % Constant.THREE == 0)
            {
                left = (side == 0) ? length : 0;
                top = width / Constant.TWO;
            }
            else
            {
                left = (side % Constant.TWO == 0) ? length - horizontalLine : horizontalLine;
                top = (side < Constant.THREE) ? 0 : width;
            }
            DetermineAbsolutePosition(ref left, ref top, startPoint, endPoint);
            return new Windows.Foundation.Point((float)left, (float)top);
        }

        // Determine the position on canvas
        private void DetermineAbsolutePosition(ref double left, ref double top, DrawingModel.Point startPoint, DrawingModel.Point endPoint)
        {
            left += startPoint.GetSmallLeft(endPoint);
            top += startPoint.GetSmallTop(endPoint);
        }

        // Implement mark outlines
        public void MarkOutlines(DrawingModel.Point startPoint, DrawingModel.Point endPoint)
        {
            DrawMarkLines(startPoint, endPoint);
            DrawCorners(startPoint, endPoint);
        }

        // 劃出標示線
        private void DrawMarkLines(DrawingModel.Point startPoint, DrawingModel.Point endPoint)
        {
            DoubleCollection dashCollection = new DoubleCollection();
            dashCollection.Add(Constant.TWO);
            dashCollection.Add(Constant.TWO);
            Windows.UI.Xaml.Shapes.Rectangle rectangle = GetRectangle(startPoint, endPoint);
            rectangle.StrokeDashArray = dashCollection;
            rectangle.Stroke = new SolidColorBrush(Colors.Red);
            _canvas.Children.Add(rectangle);
        }

        // Draw four corner
        private void DrawCorners(DrawingModel.Point startPoint, DrawingModel.Point endPoint)
        {
            List<DrawingModel.Point> points = GetCornerPointsPosition(startPoint, endPoint);
            foreach (DrawingModel.Point point in points)
            {
                float left = (float)point.Left;
                float top = (float)point.Top;
                float diameter = Constant.TWO * Constant.MARK_CIRCLE_RADIUS;
                Ellipse circle = new Ellipse();
                circle.Width = diameter;
                circle.Height = diameter;
                circle.Margin = new Windows.UI.Xaml.Thickness(left, top, 0, 0);
                circle.Fill = new SolidColorBrush(Colors.White);
                circle.Stroke = new SolidColorBrush(Colors.Black);
                _canvas.Children.Add(circle);
            }
        }

        // 取得 4 個corner 的圓座標
        private List<DrawingModel.Point> GetCornerPointsPosition(DrawingModel.Point startPoint, DrawingModel.Point endPoint)
        {
            List<DrawingModel.Point> points = new List<DrawingModel.Point>();
            for (int count = 0; count < Constant.FOUR; count++)
            {
                double left = (count % Constant.TWO == 0) ? startPoint.GetSmallLeft(endPoint) : startPoint.GetBigLeft(endPoint);
                double top = (count / Constant.TWO == 0) ? startPoint.GetSmallTop(endPoint) : startPoint.GetBigTop(endPoint);
                left -= Constant.MARK_CIRCLE_RADIUS;
                top -= Constant.MARK_CIRCLE_RADIUS;
                points.Add(new DrawingModel.Point(left, top));
            }
            return points;
        }
    }
}
