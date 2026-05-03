using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace Event_handling.Objects
{
    class Rectangle : BaseObject
    {
        private static Random rnd = new Random();
        private double timeEnd = 10;

        public Action<Rectangle> OnTimeOut;

        public Rectangle(float x, float y, float angle) : base(x, y, angle)
        {
        }
        public override void Render(Graphics g)
        {
            timeEnd -= 0.05;
            if (timeEnd < 0)
            {
                timeEnd = 10;
                OnTimeOut(this);
            }

            g.FillRectangle(new SolidBrush(Color.FloralWhite), 0, 0, 50, 30);
            g.DrawRectangle(new Pen(Color.Wheat, 2), 0, 0, 50, 30);

            g.DrawString(
                ((int)timeEnd).ToString(),
                new Font("Verdana", 8),
                new SolidBrush(Color.Black),
                10, 5
            );
        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddRectangle(new RectangleF(0, 0, 50, 30));
            return path;
        }
    }
}