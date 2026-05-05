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
        //Emitter.TrainEmitter trainEmitter;

        Platform platform = new Platform();

        Image background;
        int points;
        int spawnCounter = 0;
        int spawnInterval = 20;
        int fallingparticles = 0;

        public Form1()
        {
            InitializeComponent();

            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);
            background = Properties.Resources.c83f9f4196cdd42665099216ff85408e;

            emitter = new TopEmitter
            {
                Width = picDisplay.Width,
                SpeedMax = 3,
                RadiusMin = 5,
                RadiusMax = 10,
            };

            cloudeEmitter = new ClaudeEmitter
            {
                Width = picDisplay.Width,
                ParticlesCount = 3000,
                SpeedMin = 1,
                SpeedMax = 3,
            };

            //trainEmitter = new TrainEmitter
            //{
            //    Width = picDisplay.Width,
            //    ParticlePerTick = 2,
            //    SpeedMax = 2,
            //    SpeedMin=1
            //};

            platform.X = picDisplay.Width / 2;
            platform.Y = picDisplay.Height - 40;

            emitters.Add(this.emitter);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //trainEmitter.X = (int)(platform.X + platform.Width / 2);
            //trainEmitter.Y = (int)(platform.Y);

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
            //trainEmitter.UpdateState();

            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.Clear(Color.White);
                g.DrawImage(background, 0, 0, picDisplay.Width, picDisplay.Height);
                
                cloudeEmitter.Render(g);
                emitter.Render(g);
                //trainEmitter.Render(g);
                platform.Render(g);

                foreach (var r in rects)
                {
                    r.Update();
                    r.Render(g);
                }

                if (platform.isMagnet)
                {
                    platform.magnetPoint.Render(g);
                }
            }

            if (fallingparticles >= Particle.random.Next(3, 7))
            {
                fallingparticles = 0;
                var rect = new FallingRectangle(Particle.random.Next(picDisplay.Width),
                    0, 0);

                int type = Particle.random.Next(3);

                switch (type)
                {
                    case 0:
                        rect.bonus = BonusType.Life;
                        rect.RectColor = Color.Green;
                        break;
                    case 1:
                        rect.bonus = BonusType.Size;
                        rect.RectColor = Color.Red;
                        break;
                    case 2:
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
                        if (points % 5 == 0)
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
                    if (platform.isMagnet)
                    {
                        points++;
                        txbScore.Text = points.ToString();
                    }
                    if (!platform.isMagnet && !particle.isBadParticle)
                    {
                        platform.Life -= 10;
                    }
                    emitter.particles.Remove(particle);
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
                                platform.magnetTime = 80;

                                platform.magnetPoint = new GravityPoint
                                {
                                    x = platform.X + platform.Width / 2,
                                    y = platform.Y,
                                    Power = 1000
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
        private void picDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            float moveTo = e.X;

            if (moveTo > picDisplay.Width - platform.Width)
                moveTo = picDisplay.Width - platform.Width;

            platform.X = moveTo;

            if (platform.isMagnet)
            {
                platform.magnetPoint.x = platform.X + platform.Width / 2;
                platform.magnetPoint.y = platform.Y;
            }
        }

        private void tbSpeed_Scroll(object sender, EventArgs e)
        {
            emitter.SpeedMin = tbSpeed.Value;
            emitter.SpeedMax = tbSpeed.Value + 4;
            lbDirection.Text = $"Скорость: {tbSpeed.Value}";
        }

        private void picDisplay_Click(object sender, EventArgs e)
        {

        }
    }
}
