using System;
using System.Collections.Generic;
using System.Text;

namespace VectorLib
{
    public class Ball
    {
        public MP Mp { get; set; } = new MP();

        public double Radius { get; set; }

        public Vector V { get => Mp.V; set => Mp.V = value; }
        public Vector A { get => Mp.A; set => Mp.A = value; }
        public double M { get => Mp.M; set => Mp.M = value; }
        public Vector R { get => Mp.R; set => Mp.R = value; }

        public Vector NextR { get => Mp.NextR; }

        private void Bounce(PointD p1, PointD p2)
        {
            V = V.Mirror(new Vector(p1, p2));
        }

        public void Bounce(PointD p)
        {
            V = V.Mirror((R - new Vector(p)).Normal);
        }

        private void Bounce(Ball other)
        {
            Vector OO = R - other.R;

            Vector v0y = V.Projection(OO);
            Vector u0y = other.V.Projection(OO);

            Vector v0x = V - v0y;
            Vector u0x = other.V - u0y;

            Vector vy = (2 * other.M * u0y + v0y * (M - other.M)) / (M + other.M);
            Vector uy = v0y + vy - u0y;

            V = vy + v0x;
            other.V = uy + u0x;
        }

        public void CheckBounce(Wall wall)
        {
            foreach(var point in wall.Points)
            {
                if(IsCollide(point))
                {
                    Bounce(point);
                    return;
                }
            }

            for(int i = 0; i < wall.Points.Count - 1; i++)
            {
                if (IsCollide(wall.Points[i], wall.Points[i + 1]))
                {
                    Bounce(wall.Points[i], wall.Points[i + 1]);
                    return;
                }
            }
        }

        public void CheckBounce(params Ball[] balls)
        {
            foreach(var ball in balls)
            {
                if (IsCollide(ball)) Bounce(ball);
            }
        }

        private bool IsCollide(Ball other)
        {
            return this != other && (NextR - other.NextR).SqLength <= (Radius + other.Radius) * (Radius + other.Radius);
        }

        public bool IsCollide(PointD p1, PointD p2)
        {
            Vector vP1 = new Vector(p1);
            Vector vP2 = new Vector(p2);
            Vector rp1 = NextR - vP1;
            Vector rp2 = NextR - vP2;
            Vector p1p2 = new Vector(p1, p2);
            Vector rp1p2 = rp1.Projection(p1p2.Normal);

            return rp1p2.SqLength <= Radius * Radius && rp1.Projection(p1p2) * rp2.Projection(p1p2) < 0;
        }

        public bool IsCollide(PointD p)
        {
            Vector vp = new Vector(p);
            return (NextR - vp).SqLength <= Radius * Radius;
        }
    }
}
