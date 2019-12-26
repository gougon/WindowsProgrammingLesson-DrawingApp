using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using DrawingModel;
using System.Diagnostics;

namespace DrawingForm
{
    public class FormGraphicsAdapter : IGraphics
    {
        Graphics _graphics;

        public FormGraphicsAdapter(Graphics graphics)
        {
            _graphics = graphics;
        }

        // Implement ClearAll
        public void ClearAll()
        {
            // not neccessary to implementation
        }

        // Implement DrawLine
        public void DrawLine(DrawingModel.Point startPoint, DrawingModel.Point endPoint)
        {
            _graphics.DrawLine(Pens.Black, (float)startPoint.Left, (float)startPoint.Top, (float)endPoint.Left, (float)endPoint.Top);
        }

        // Implement DrawRectangle
        public void DrawRectangle(DrawingModel.Point startPoint, DrawingModel.Point endPoint)
        {
            RectangleF rectangle = GetRectangle(startPoint, endPoint);
            _graphics.FillRectangle(new SolidBrush(Color.Yellow), rectangle);
            _graphics.DrawRectangle(Pens.Black, System.Drawing.Rectangle.Round(rectangle));
        }

        // 創建 rectangle
        private RectangleF GetRectangle(DrawingModel.Point startPoint, DrawingModel.Point endPoint)
        {
            float left = (float)startPoint.GetSmallLeft(endPoint);
            float top = (float)startPoint.GetSmallTop(endPoint);
            float width = (float)startPoint.GetLeftDifference(endPoint);
            float height = (float)startPoint.GetTopDifference(endPoint);
            return new RectangleF(new PointF(left, top), new SizeF(new PointF(width, height)));
        }

        // Implement DrawSixSide
        public void DrawSixSide(DrawingModel.Point startPoint, DrawingModel.Point endPoint)
        {
            PointF[] points = GetSixSidePoints(startPoint, endPoint);
            _graphics.FillPolygon(new SolidBrush(Color.Orange), points);
            _graphics.DrawPolygon(Pens.Black, points);
        }

        // use start, end points to calculate six side points
        private PointF[] GetSixSidePoints(DrawingModel.Point startPoint, DrawingModel.Point endPoint)
        {
            double horizontalLine = CalculateHorizontalLine(startPoint, endPoint);
            PointF[] points = new PointF[Constant.SIX];
            for (int side = 0; side < Constant.SIX; side++)
            {
                points[side] = CalculatePointPosition(side, horizontalLine, startPoint, endPoint);
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
        private PointF CalculatePointPosition(int side, double horizontalLine, DrawingModel.Point startPoint, DrawingModel.Point endPoint)
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
            return new PointF((float)left, (float)top);
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

        // Draw four corner
        private void DrawCorners(DrawingModel.Point startPoint, DrawingModel.Point endPoint)
        {
            List<DrawingModel.Point> points = GetCornerPointsPosition(startPoint, endPoint);
            foreach (DrawingModel.Point point in points)
            {
                float left = (float)point.Left;
                float top = (float)point.Top;
                float diameter = Constant.TWO * Constant.MARK_CIRCLE_RADIUS;
                RectangleF rectangle = new RectangleF(new PointF(left, top), new SizeF(new PointF(diameter, diameter)));
                _graphics.FillEllipse(new SolidBrush(Color.White), rectangle);
                _graphics.DrawEllipse(Pens.Black, rectangle);
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

        // 劃出標示線
        private void DrawMarkLines(DrawingModel.Point startPoint, DrawingModel.Point endPoint)
        {
            float[] dashValues = { Constant.TWO, Constant.TWO };
            Pen redPen = new Pen(Color.Red);
            redPen.DashPattern = dashValues;
            RectangleF rectangle = GetRectangle(startPoint, endPoint);
            _graphics.DrawRectangle(redPen, System.Drawing.Rectangle.Round(rectangle));
        }
    }
}
