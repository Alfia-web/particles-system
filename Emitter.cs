using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace particle_system
{
    internal class Emitter
    {
        public int X; // координата X центра эмиттера, будем ее использовать вместо MousePositionX
        public int Y; // соответствующая координата Y 
        public int Direction = 0; // вектор направления в градусах куда сыпет эмиттер
        public int Spreading = 360; // разброс частиц относительно Direction
        public int SpeedMin = 1; // начальная минимальная скорость движения частицы
        public int SpeedMax = 10; // начальная максимальная скорость движения частицы
        public int RadiusMin = 2; // минимальный радиус частицы
        public int RadiusMax = 10; // максимальный радиус частицы
        public int LifeMin = 20; // минимальное время жизни частицы
        public int LifeMax = 100; // максимальное время жизни частицы

        public int ParticlePerTick = 5; 

        public Color ColorFrom = Color.White; // начальный цвет частицы
        public Color ColorTo = Color.FromArgb(0, Color.Black); // конечный цвет частиц

        public int ParticlesCount = 500;

        public List<Particle> particles = new List<Particle>();
        public List<IImpactPoint> impactPoints = new List<IImpactPoint>(); //точки притяженния

        public int MousePositionX;
        public int MousePositionY;

        public float GravitationX = 0;
        public float GravitationY = 0; //гравитация силой 1 пиксель

       
        public void UpdateState()
        {
            int particlesToCreate = ParticlePerTick; //сколько частиц за раз

            foreach (var particle in particles)
            { 
                //если частица умерла
                //проверяем нужна ли новая
                if (particle.Life <= 0)
                {
                    if(particlesToCreate > 0)
                    {
                        particlesToCreate -= 1;
                        ResetParticle(particle);
                    }
                }
                else
                {
                    //считаем вектор притяжения к точкне
                    particle.Life -= 1;
                    foreach (var point in impactPoints)
                    {
                        point.ImpactParticle(particle);
                    }

                    particle.SpeedX += GravitationX;
                    particle.SpeedY += GravitationY;

                    particle.x += particle.SpeedX;
                    particle.y += particle.SpeedY;
                }

                //particle.x += (float)(Math.Cos(particle.Direction * Math.PI /180) * particle.Speed);
                //particle.y += (float)(Math.Sin(particle.Direction * Math.PI / 180) * particle.Speed);

                //particle.x += (float)(Math.Cos(particle.Direction * Math.PI /180) * particle.Speed);
                //particle.y += (float)(Math.Sin(particle.Direction * Math.PI / 180) * particle.Speed);
                
            }

            while (particlesToCreate >= 1)
            {
                particlesToCreate -= 1;
                var particle = CreatParticle();
                ResetParticle(particle);
                particles.Add(particle);
            }
        }

        public virtual void ResetParticle(Particle particle)
        {
            particle.Life = Particle.random.Next(LifeMin, LifeMax);
            particle.x = X;
            particle.y = Y;

            var direction = Direction
                + (double)Particle.random.Next(Spreading)
                - Spreading / 2;

            var speed = Particle.random.Next(SpeedMin, SpeedMax);

            particle.SpeedX = (float)(Math.Cos(direction / 180 * Math.PI) * speed);
            particle.SpeedY = -(float)(Math.Sin(direction / 180 * Math.PI) * speed);

            particle.Radius = Particle.random.Next(RadiusMin, RadiusMax);
        }

        public void Render(Graphics g)
        {
            foreach(var particle in particles)
            {
                particle.Draw(g);
            }

            foreach(var point in impactPoints)
            {
               point.Render(g); 
            }
        }

        //падающие сверху частицы
        public class TopEmitter : Emitter
        {
            public int Width; //длина экрана

            public override void ResetParticle(Particle particle)
            {
                base.ResetParticle(particle);

                particle.x = Particle.random.Next(Width);
                particle.y = 0;

                particle.SpeedY = 1;
                particle.SpeedX = Particle.random.Next(-2, 2);
            }
        }

        //метод генерации частиц (если надо переопределить)
        public virtual Particle CreatParticle()
        {
            var particle = new ParticleColorful();

            if (Particle.random.Next(10) == 0)
            {
                particle.FromColor = Color.Black;
                particle.ToColor = Color.Gray;
                particle.isBadParticle = true;
            }
            else
            {
                particle.FromColor = Color.Gold;
                particle.ToColor = Color.Transparent;
            }
            return particle; 
        }
    }
}
