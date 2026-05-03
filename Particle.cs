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
            // генерируем произвольное направление и скорость
            var direction = (double)random.Next(360);
            var speed = 1 + random.Next(10);

            // рассчитываем вектор скорости
            SpeedX = (float)(Math.Cos(direction / 79 * Math.PI) * speed);
            SpeedY = -(float)(Math.Sin(direction / 45 * Math.PI) * speed);

            Radius = 5 + random.Next(10);
        }

            public virtual void Draw(Graphics g)
            {
            //float k = Math.Min(1f, Life / 100);
            // рассчитываем значение альфа канала в шкале от 0 до 255
            // по аналогии с RGB, он используется для задания прозрачности
            //int alpha = (int)(k * 255);

            // создаем цвет из уже существующего, но привязываем к нему еще и значение альфа канала
            var color = Color.Gold;
            var b = new SolidBrush(color);
            g.FillEllipse(b, x - Radius, y - Radius, Radius * 2, Radius * 2);
            b.Dispose();

            //// остальное все так же
            //g.FillEllipse(b, x - Radius, y - Radius, Radius * 2, Radius * 2);

            //b.Dispose();
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
            //float k = Math.Min(1f, Life / 100);

            // так как k уменьшается от 1 до 0, то порядок цветов обратный
            var color = FromColor;
            var b = new SolidBrush(color);

            g.FillEllipse(b, x - Radius, y - Radius, Radius, Radius * 2);

            b.Dispose();
        }
    }
}
