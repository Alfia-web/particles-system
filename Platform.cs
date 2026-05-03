using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace particle_system
{
    class Platform
    {
        public float X;
        public float Y;
        public float Width = 100;
        public float Height = 15;
        public int Life = 10;

        public void Render(Graphics g)
        {
            g.FillRectangle(Brushes.White, X, Y, Width, Height);

            g.DrawString(
                ((int)Life).ToString(),
                new Font("Verdana", 10),
                Brushes.Black,
                X + 40,
                Y 
            );
        }

        public bool IsCollide(Particle p)
        {
            return p.x + p.Radius > X && p.x - p.Radius < X + Width &&
                p.y + p.Radius > Y && p.y - p.Radius < Y + Height;
        }


    }
}
