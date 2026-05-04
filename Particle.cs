using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace particle_system
{
    public class Particle
    {
        public int Radius;
        public float x;
        public float y;

        public float SpeedX;
        public float SpeedY;

        public static Random random = new Random();
        public bool isBadParticle;
      
        public Particle()
        {
            var direction = (double)random.Next(360);
            var speed = 1 + random.Next(10);

            SpeedX = (float)(Math.Cos(direction / 180 * Math.PI) * speed);
            SpeedY = -(float)(Math.Sin(direction / 180 * Math.PI) * speed);

            Radius = 5 + random.Next(10);
        }

        public virtual void Draw(Graphics g)
        {
            var color = Color.Gold;
            var b = new SolidBrush(color);
            g.FillEllipse(b, x - Radius, y - Radius, Radius * 2, Radius * 2);
            b.Dispose();
            }
        }

        public class ParticleColorful : Particle
        {
            public Color FromColor;
            public Color ToColor;

            public static Color MixColor(Color color1, Color color2, float k) {
                return Color.FromArgb(
                    (int)(color2.A * k + color1.A * (1 - k)),
                    (int)(color2.R * k + color1.R * (1 - k)),
                    (int)(color2.G * k + color1.G * (1 - k)),
                    (int)(color2.B * k + color1.B * (1 - k))
                );
        }

        public override void Draw(Graphics g)
        {
            var color = FromColor;
            var b = new SolidBrush(color);
            g.FillEllipse(b, x - Radius, y - Radius, Radius, Radius * 2);
            b.Dispose();
        }
    }
}
