using System;
using System.Collections.Generic;
using System.Text;

namespace VectorLib
{
    public class MP
    {
        public Vector R { get; set; } = new Vector();
        public double M { get; set; } = 0;
        public Vector A { get; set; } = new Vector();
        public Vector V { get; set; } = new Vector();

        public Vector NextR { get; private set; } = new Vector();

        public void Move(double dt)
        {
            V += A * dt;
            R += V * dt;
            NextR = R + dt * (V + A * dt);
        }

        public void Accelerate(Vector F)
        {
            A = F / M;
        }
    }
}
