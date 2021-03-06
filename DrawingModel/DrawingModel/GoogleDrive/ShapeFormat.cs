﻿using System;
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
        bool _isReverse;

        public ShapeFormat()
        {
            // do nothing
        }

        public ShapeFormat(Point startPoint, Point endPoint, ShapeType shapeType, bool isReverse)
        {
            _startPointLeft = startPoint.Left;
            _startPointTop = startPoint.Top;
            _endPointLeft = endPoint.Left;
            _endPointTop = endPoint.Top;
            _shapeType = (int)shapeType;
            _isReverse = isReverse;
        }

        public double StartPointLeft
        {
            get
            {
                return _startPointLeft;
            }
            set
            {
                _startPointLeft = value;
            }
        }

        public double StartPointTop
        {
            get
            {
                return _startPointTop;
            }
            set
            {
                _startPointTop = value;
            }
        }

        public double EndPointLeft
        {
            get
            {
                return _endPointLeft;
            }
            set
            {
                _endPointLeft = value;
            }
        }

        public double EndPointTop
        {
            get
            {
                return _endPointTop;
            }
            set
            {
                _endPointTop = value;
            }
        }

        public int ShapeType
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

        public bool IsReverse
        {
            get
            {
                return _isReverse;
            }
            set
            {
                _isReverse = value;
            }
        }
    }
}
