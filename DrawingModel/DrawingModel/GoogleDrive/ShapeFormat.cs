using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    public class ShapeFormat
    {
        double _startPointLeft;
        double _startPointTop;
        double _endPointLeft;
        double _endPointTop;
        int _shapeType;

        public ShapeFormat(Point startPoint, Point endPoint, ShapeType shapeType)
        {
            _startPointLeft = startPoint.Left;
            _startPointTop = startPoint.Top;
            _endPointLeft = endPoint.Left;
            _endPointTop = endPoint.Top;
            _shapeType = (int)shapeType;
        }

        public double StartPointLeft
        {
            get
            {
                return _startPointLeft;
            }
        }

        public double StartPointTop
        {
            get
            {
                return _startPointTop;
            }
        }

        public double EndPointLeft
        {
            get
            {
                return _endPointLeft;
            }
        }

        public double EndPointTop
        {
            get
            {
                return _endPointTop;
            }
        }

        public int ShapeType
        {
            get
            {
                return _shapeType;
            }
        }
    }
}
