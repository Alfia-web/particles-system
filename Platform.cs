using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace particle_system
{
    class Platform
    {
        public float X;
        public float Y;
        public float Width = 100;
        public float Height = 15;

        public void Render(Graphics g)
        {
            g.FillRectangle(Brushes.White, X, Y, Width, Height);
        }

        public bool IsCollide(Particle p)
        {
            return p.x > X && p.x < X + Width &&
                p.y > Y && p.y < Y + Height;
        }
    }
}
