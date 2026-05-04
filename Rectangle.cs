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
    public enum BonusType
    {
        Life, Size, Magnet
    }

    class FallingRectangle : BaseObject
    {
        private static Random rnd = new Random();

        private double timeEnd = 10;
        public int speedRectangle = 6;
        public BonusType bonus;
        public Color RectColor;
        public BonusType BonusType;

        public Action<Rectangle> OnTimeOut;

        public FallingRectangle(float x, float y, float angle) : base(x, y, angle)
        {
        }
        public override void Render(Graphics g)
        {
            var state = g.Transform;
            var matrix = new Matrix();
            matrix.Translate(X, Y);
            matrix.Rotate(Angle);
            g.Transform = matrix;

            g.FillRectangle(new SolidBrush(RectColor), -25, -15, 25, 15);
            g.DrawRectangle(new Pen(RectColor, 2), -25, -15, 25, 15);

            g.Transform = state;
        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddRectangle(new RectangleF(0, 0, 50, 30));
            return path;
        }

        public void Update()
        {
            Y += speedRectangle;
        }
    }
}