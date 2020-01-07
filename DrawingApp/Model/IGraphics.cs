using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    public interface IGraphics
    {
        // 清除 canvas 上所有圖形
        void ClearAll();

        // 畫 line
        void DrawLine(Point startPoint, Point endPoint);

        // 畫 rectangle
        void DrawRectangle(Point startPoint, Point endPoint);

        // 畫 six side
        void DrawSixSide(Point startPoint, Point endPoint);

        // 標示外框
        void MarkOutlines(Point startPoint, Point endPoint);
    }
}
