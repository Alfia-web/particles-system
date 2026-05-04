using Event_handling.Objects;
using particle_system.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static particle_system.Emitter;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace particle_system
{
    public partial class Form1 : Form
    {

        List<Emitter> emitters = new List<Emitter>();
        List<FallingRectangle> rects = new List<FallingRectangle>();


        Emitter.TopEmitter emitter;
        Emitter.ClaudeEmitter cloudeEmitter; 
        GravityPoint point1;
        GravityPoint point2;

        Platform platform = new Platform();

        Image background;
        int points;
        int spawnCounter = 0;
        int spawnInterval = 20;
        int fallingparticles = 0;

        public Form1()
        {
            InitializeComponent();

            //привязка изображения
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);
            background = Properties.Resources.b137314d29b249a7d78a2c1243c4063c;

            timer1.Interval = 19;
            emitter = new TopEmitter // создаю эмиттер и привязываю его к полю emitter
            {
                //Direction = 0,
                Width = picDisplay.Width,
                //Spreading = 10,
                SpeedMin = 2,
                SpeedMax = 6,
                ParticlePerTick = 1,
                RadiusMin = 5,
                RadiusMax = 10,
            };

            cloudeEmitter = new ClaudeEmitter
            {
                Width = picDisplay.Width,
                ParticlesCount = 1000,
              
                SpeedMin = 1,
                SpeedMax = 3,
            };

            platform.Y = picDisplay.Height - 40;
            platform.X = picDisplay.Width / 2;


            emitters.Add(this.emitter);

            point1 = new GravityPoint
            {
                x = picDisplay.Width / 2 + 100,
                y = 80,
                Power = 10,
            };
            point2 = new GravityPoint
            {
                x = picDisplay.Width / 2 - 100,
                y = 80,
                Power = 10,
            };

            // привязываем поля к эмиттеру
            cloudeEmitter.impactPoints.Add(point1);
            cloudeEmitter.impactPoints.Add(point2);

            emitter.X = picDisplay.Width / 2;
            emitter.Y = 0;
            emitter.ParticlePerTick = 2;

            while (cloudeEmitter.particles.Count < cloudeEmitter.ParticlesCount)
            {
                var p = cloudeEmitter.CreatParticle();
                cloudeEmitter.ResetParticle(p);
                cloudeEmitter.particles.Add(p);
            }
        }

        int counter = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            //emitter.MousePositionX = MousePositionX;
            //emitter.MousePositionY = MousePositionY;

            spawnCounter++;
            if (spawnCounter >= spawnInterval)
            {
                spawnCounter = 0;
                emitter.UpdateState();
            }
            else
            {
                emitter.ParticlePerTick = 0;
                emitter.UpdateState();
                emitter.ParticlePerTick = 1;
            }

            cloudeEmitter.UpdateState();
            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.DrawImage(background, 0, 0, picDisplay.Width, picDisplay.Height);
                
                cloudeEmitter.Render(g);
                emitter.Render(g);
                platform.Render(g);

                foreach (var r in rects)
                {
                    r.Update();
                    r.Render(g);
                }
            }

            if (fallingparticles >= Particle.random.Next(1,3))
            {
                fallingparticles = 0;
                var rect = new FallingRectangle(Particle.random.Next(picDisplay.Width),
                    0, 0);

                int type = Particle.random.Next(1);

                switch (type)
                {
                    //case 0:
                    //    rect.bonus = BonusType.Life;
                    //    rect.RectColor = Color.Green;
                    //    break;
                    //case 1:
                    //    rect.bonus = BonusType.Size;
                    //    rect.RectColor = Color.Red;
                    //    break;
                    case 0:
                        rect.bonus = BonusType.Magnet;
                        rect.RectColor = Color.Pink;
                        break;
                }

                rects.Add(rect);
            }

            picDisplay.Invalidate();

            foreach (var particle in emitter.particles.ToList())
            {
                bool outOfSpace = particle.y > picDisplay.Height;

                if (platform.IsCollide(particle))
                {
                    fallingparticles++;
                    if (particle.isBadParticle)
                    {
                        platform.Life -= 2;
                    }
                    else
                    {
                        points++;
                        if (points % 5 ==0 && spawnCounter > 10)
                        {
                            spawnInterval -= 5;
                        }
                    }
                    txbScore.Text = points.ToString();
                    emitter.particles.Remove(particle);
                }
                if (outOfSpace && !particle.isBadParticle)
                {
                    fallingparticles++;
                    if (!platform.isMagnet && !particle.isBadParticle)
                    {
                        platform.Life -= 10;
                    }
                    emitter.particles.Remove(particle);
                }
                if (platform.isMagnet && !particle.isBadParticle)
                {
                    points++;
                }
            }

            foreach(var rect in rects.ToList())
            {
                if(rect.Y > picDisplay.Height)
                {
                    rects.Remove(rect);
                }

                if (platform.IsCollide(rect))
                {
                    switch (rect.bonus)
                    {
                        case BonusType.Life:
                            platform.Life += 1;
                            break;
                        case BonusType.Size:
                            if (!platform.isBig)
                            {
                                platform.isBig = true;
                                platform.Width += 30;
                                platform.bigTime = 250;
                            }
                            break;
                        case BonusType.Magnet:
                            if (!platform.isMagnet)
                            {
                                platform.isMagnet = true;
                                platform.magnetTime = 100;

                                platform.magnetPoint = new GravityPoint
                                {
                                    x = platform.X + platform.Width / 2,
                                    y = platform.Y,
                                    Power = 8000
                                };

                                emitter.impactPoints.Add(platform.magnetPoint);
                            }
                            break;
                    }
                    rects.Remove(rect);
                }
            }

            if (platform.isBig)
            {
                platform.bigTime--;
                if(platform.bigTime <= 0)
                {
                    platform.isBig = false;
                    platform.Width -= 30;
                }
            }

            if (platform.isMagnet)
            {
                platform.magnetTime--;
                platform.magnetPoint.x = platform.X + platform.Width / 2;
                platform.magnetPoint.y = platform.Y + platform.Height / 2;
                if(platform.magnetTime <= 0)
                {
                    platform.isMagnet = false;
                    emitter.impactPoints.Remove(platform.magnetPoint);
                    platform.magnetPoint = null;
                }
            }

            if (platform.Life <= 0)
            {
                timer1.Stop();
                MessageBox.Show("Game Over");
            }
        }

        private int MousePositionX = 0;
        private int MousePositionY = 0;
        private void picDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            //foreach (var emitter in emitters)
            //{
            //    emitter.MousePositionX = e.X;
            //    emitter.MousePositionY = e.Y;
            //}

            //point2.x = e.X;
            //point2.y = e.Y;
            platform.X = e.X;
        }

        private void tbDirection_Scroll(object sender, EventArgs e)
        {
            emitter.Direction = tbDirection.Value;
            lbDirection.Text = $"{tbDirection.Value}°";
        }

       

    //private void tbGravition_Scroll(object sender, EventArgs e)
    //{
    //    point1.Power = tbGravition.Value;
    //}

    //private void tbGravition2_Scroll(object sender, EventArgs e)
    //{
    //    point2.Power = tbGravition2.Value;
    //}
}
}
