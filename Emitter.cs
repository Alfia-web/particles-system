using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static particle_system.Emitter;

namespace particle_system
{
    internal class Emitter
    {
        public int X;
        public int Y;
        public int Direction = 0;
        public int Spreading = 360;
        public int SpeedMin = 1;
        public int SpeedMax = 10;
        public int RadiusMin = 2;
        public int RadiusMax = 10;
        public int Width = 0;

        public int ParticlePerTick = 5;

        public Color ColorFrom = Color.White;
        public Color ColorTo = Color.FromArgb(0, Color.Black);

        public int ParticlesCount = 100;

        public List<Particle> particles = new List<Particle>();
        public List<IImpactPoint> impactPoints = new List<IImpactPoint>();

        public int MousePositionX;
        public int MousePositionY;

        public virtual void UpdateState()
        {
            int particlesToCreate = ParticlePerTick;

            foreach (var particle in particles)
            {
                foreach (var point in impactPoints)
                {
                    point.ImpactParticle(particle);
                }

                particle.x += particle.SpeedX;
                particle.y += particle.SpeedY;

                if (particle.x < 0 || particle.x > Width)
                {
                    particle.SpeedX *= -1;
                }

            }

            while (particlesToCreate >= 1)
            {
                particlesToCreate -= 1;
                if (particles.Count >= ParticlesCount)
                {
                    break;
                }
                var particle = CreatParticle();
                ResetParticle(particle);
                particles.Add(particle);
            }
        }

        public virtual void ResetParticle(Particle particle)
        {
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
            foreach (var particle in particles)
            {
                particle.Draw(g);
            }

            foreach (var point in impactPoints)
            {
                point.Render(g);
            }
        }
        public virtual Particle CreatParticle()
        {
            var particle = new ParticleColorful();
            particle.Life = 20;
            if (Particle.random.Next(10) < 4)
            {
                particle.FromColor = Color.Red;
                particle.ToColor = Color.Red;
                particle.isBadParticle = true;
            }
            else
            {
                particle.FromColor = Color.Gold;
                particle.ToColor = Color.Gold;
                particle.isBadParticle = false;
            }
            return particle;
        }

        public class TopEmitter : Emitter
        {
            public int Width;

            public override void ResetParticle(Particle particle)
            {
                particle.x = Particle.random.Next(Width);
                particle.y = 0;

                particle.SpeedY = SpeedMin + Particle.random.Next(SpeedMax - SpeedMin);
                particle.SpeedX = Particle.random.Next(-2, 2);
            }
        }

        public class ClaudeEmitter : Emitter
        {
            public int Width;

            public override void ResetParticle(Particle particle)
            {
                particle.Life = 20;
                particle.x = Particle.random.Next(Width);
                particle.y = Particle.random.Next(0, 150);

                particle.SpeedX = (float)(Particle.random.NextDouble() - 0.5);
                particle.SpeedY = (float)(Particle.random.NextDouble() - 0.5);
            }

            public override Particle CreatParticle()
            {
                return new ParticleColorful()
                {
                    FromColor = Color.White,
                    ToColor = Color.LightBlue,
                    isBadParticle = false,
                    Radius = 3 + Particle.random.Next(5)
                };
            }

            public override void UpdateState()
            {
                int particlesToCreate = ParticlePerTick;

                foreach (var particle in particles)
                {
                    particle.x += particle.SpeedX;
                    particle.y += particle.SpeedY;

                    particle.Life -= 0.1f;

                    if (particle.y > 200)
                    {
                        particle.y = 200;
                        particle.SpeedY *= -1;
                    }
                    if (particle.y < 0)
                    {
                        particle.y = 0;
                        particle.SpeedY *= -1;
                    }
                    if (particle.x < 0)
                    {
                        particle.x = 0;
                        particle.SpeedX *= -1;
                    }
                    if (particle.x > Width)
                    {
                        particle.x = Width;
                        particle.SpeedX *= -1;
                    }
                }

                particles.RemoveAll( p => p.Life <= 0);

                while (particlesToCreate-- > 0)
                {
                    if (particles.Count >= ParticlesCount)
                        break;

                    var p = CreatParticle();
                    ResetParticle(p);
                    particles.Add(p);
                }
            }


            //public class TrainEmitter : Emitter
            //{
            //    public override void ResetParticle(Particle particle)
            //    {
            //        base.ResetParticle(particle);

            //        particle.x = X;
            //        particle.y = Y;

            //        particle.SpeedX = (float)(Particle.random.NextDouble() - 0.5);
            //        particle.SpeedY = (float)(Particle.random.NextDouble() - 0.5);

            //        particle.Life = 20 + Particle.random.Next(20);
            //    }

            //    public override Particle CreatParticle()
            //    {
            //        return new ParticleColorful()
            //        {
            //            FromColor = Color.Coral,
            //            ToColor = Color.FromArgb(0, Color.Transparent),
            //            isBadParticle = false,
            //            Radius = 3
            //        };
            //    }
            //}
        }
    }
}
