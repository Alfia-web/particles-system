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

        Emitter.TopEmitter emitter;

        GravityPoint point1;
        GravityPoint point2;

        Platform platform = new Platform();

        Image background;
        public Form1()
        {
            InitializeComponent();

            //привязка изображения
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);
            background = Properties.Resources.b137314d29b249a7d78a2c1243c4063c;


            this.emitter = new TopEmitter // создаю эмиттер и привязываю его к полю emitter
            {
                //Direction = 0,
                Width = picDisplay.Width,
                //Spreading = 10,
                SpeedMin = 2,
                SpeedMax = 6,
                ColorFrom = Color.Gold,
                ColorTo = Color.FromArgb(0, Color.Black),
                //ParticlePerTick = 10,
                //X = picDisplay.Width / 2,
                //Y = picDisplay.Height / 2,
            };

            platform.Y = picDisplay.Height - 40;
            platform.X = picDisplay.Width / 2;
           

            emitters.Add(this.emitter);

            point1 = new GravityPoint
            {
                x = picDisplay.Width / 2 + 100,
                y = picDisplay.Height / 2,
            };
            point2 = new GravityPoint
            {
                x = picDisplay.Width / 2 - 100,
                y = picDisplay.Height / 2,
            };

            // привязываем поля к эмиттеру
            emitter.impactPoints.Add(point1);
            emitter.impactPoints.Add(point2);
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        int counter = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            //emitter.MousePositionX = MousePositionX;
            //emitter.MousePositionY = MousePositionY;
         
            emitter.UpdateState();
            try
            {
                using (var g = Graphics.FromImage(picDisplay.Image))
                {
                    if (background != null)
                        g.DrawImage(background, 0, 0, picDisplay.Width, picDisplay.Height);

                    emitter.Render(g);
                }

                picDisplay.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            foreach (var particle in emitter.particles.ToList())
            {
                if (platform.IsCollide(particle)){
                    
                }
            }

                picDisplay.Invalidate();
        }

        private int MousePositionX = 0;
        private int MousePositionY = 0;
        private void picDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            foreach (var emitter in emitters)
            {
                emitter.MousePositionX = e.X;
                emitter.MousePositionY = e.Y;
            }

            //point2.x = e.X;
            //point2.y = e.Y;
            platform.X = e.X;
        }

        private void tbDirection_Scroll(object sender, EventArgs e)
        {
            emitter.Direction = tbDirection.Value;
            lbDirection.Text = $"{tbDirection.Value}°";
        }

        private void tbGravition_Scroll(object sender, EventArgs e)
        {
            point1.Power = tbGravition.Value;
        }

        private void tbGravition2_Scroll(object sender, EventArgs e)
        {
            point2.Power = tbGravition2.Value;
        }
    }
}
