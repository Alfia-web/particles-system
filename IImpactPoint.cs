using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace particle_system
{


    public abstract class IImpactPoint
    {
        public float x;
        public float y;

        //абстрактный метод изменения частиц
        public abstract void ImpactParticle(Particle particle);

        public virtual void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.Red), 
                x-5, y-5, 10,10);
        }
    }

    public class GravityPoint : IImpactPoint
    {
        public int Power = 100; // сила притяжения

        // а сюда по сути скопировали с минимальными правками то что было в UpdateState
        public override void ImpactParticle(Particle particle)
        {
            float gX = x - particle.x;
            float gY = y - particle.y;
            float r2 = (float)Math.Max(100, gX * gX + gY * gY);

            particle.SpeedX += gX * Power / r2;
            particle.SpeedY += gY * Power / r2;
        }

        public override void Render(Graphics g)
        {
            g.DrawEllipse(
              new Pen(Color.Red),
              x - Power / 2,
              y - Power / 2,
              Power,
              Power
          );
        }

    }

    public class AntiGravityPoint : IImpactPoint
    {
        public int Power = 100; // сила притяжения

        // а сюда по сути скопировали с минимальными правками то что было в UpdateState
        public override void ImpactParticle(Particle particle)
        {
            float gX = x - particle.x;
            float gY = y - particle.y;
            float r2 = (float)Math.Max(100, gX * gX + gY * gY);

            particle.SpeedX -= gX * Power / r2;
            particle.SpeedY -= gY * Power / r2;
        }
    }
}
