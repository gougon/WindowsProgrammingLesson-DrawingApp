using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrawingModel;

namespace DrawingForm.Tests
{
    public class MockGraphics : IGraphics
    {
        // mock clearAll
        public void ClearAll()
        {
        }

        // mock drawLine
        public void DrawLine(Point startPoint, Point endPoint)
        {
        }

        // mock drawRectangle
        public void DrawRectangle(Point startPoint, Point endPoint)
        {
        }

        // mock drawSixSide
        public void DrawSixSide(Point startPoint, Point endPoint)
        {
        }

        // mock mock outlines
        public void MarkOutlines(Point startPoint, Point endPoint)
        {
        }
    }
}
