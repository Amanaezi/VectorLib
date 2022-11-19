using System;
using System.Collections.Generic;
using System.Text;

namespace VectorLib
{
    public struct PointD
    {
        public double X { get; set; }
        public double Y { get; set; }
        public PointD(double x = 0, double y = 0)
        {
            X = x;
            Y = y;
        }

    }
}
