using System;
using System.Collections.Generic;
using System.Text;

namespace VectorLib
{
    public class Vector
    {
        public double X { get; set; }
        public double Y { get; set; }

        
        public Vector(double x = 0, double y = 0)
        {
            X = x;
            Y = y;
        }
        public Vector(PointD p) : this(p.X, p.Y) { }

        public Vector(PointD p1, PointD p2) : this(p2.X - p1.X, p2.Y - p1.Y) { }

        public double Length
        {
            get => Math.Sqrt(SqLength);
        }

        public double SqLength
        {
            get => this * this;
        }
        public double VectorTiltAngle
        {
            get => Math.Asin(Y / Length);
        }

        public Vector Sum(Vector other)
        {
            return new Vector(X + other.X, Y + other.Y);
        }
        public static Vector operator +(Vector c, Vector v)
        {
            return new Vector(v.X + c.X, v.Y + c.Y);
        }

        public static Vector operator -(Vector c)
        {
            return new Vector(-c.X, -c.Y);
        }
        public static Vector operator -(Vector c, Vector v)
        {
            return c + (-v);
        }
        public static Vector operator*(double c, Vector v)
        {
            return new Vector(c * v.X, c * v.Y);     
        }

        public static Vector operator*(Vector v, double c)
        {
            return c * v;
        }

        public static Vector operator /(Vector v, double c)
        {
            return v * (1 / c);
        }

        public static double operator*(Vector v1, Vector v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y;
        }

        /*
        public Vector MultVectorByNum(double number)
        {
            return new Vector(number * X, number * Y);
        }

        public double ScalarOfVectors(Vector other)
        {
            return X * other.X + Y * other.Y; 
        }
        */

        public Vector Normal
        {
            get => new Vector(-Y, X);
        }

        public Vector GetE() => this / Length;

        public Vector Projection(Vector OnVector) => (this * OnVector) / OnVector.SqLength * OnVector;

        public Vector Mirror(Vector v) => this - 2 * this.Projection(v.Normal);

        public void HorizontalBounce()
        {
            Y = -Y;
        }

        public void VerticalBounce()
        {
            X = -X;
        }

        public override bool Equals(object? obj)
        {
            return obj is Vector && this.Equals((Vector)obj);
        }
        public bool Equals(Vector v)
        {
            return v.X == X && v.Y == Y;
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

    }
}
