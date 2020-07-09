using System.Drawing;
using System;
using System.Windows.Forms;

namespace Cirlces_With_Stuff
{
    class Circles
    {
        /// <summary>
        /// Location of the circle.
        /// </summary>
        private readonly float x, y;
        /// <summary>
        /// The angel.
        /// </summary>
        private double alpha = 0;
        /// <summary>
        /// Alpha increase rate.
        /// </summary>
        private readonly float speed;
        /// <summary>
        /// The Center on the circle.
        /// </summary>
        private readonly float mX, mY;
        /// <summary>
        /// The point on the Circle's perimeter.
        /// </summary>
        public PointF point;
        /// <summary>
        /// Increase the Alpha and move the point on the perimeter every 10ms.
        /// </summary>
        readonly Timer t;
        public Circles(float X, float Y, float speed)
        {
            this.x = X;
            this.y = Y;
            mX = x + 75 / 2;
            mY = y + 75 / 2;
            this.speed = speed;
            point = new PointF(mX + (75 / 2) * (float)Math.Cos(alpha), mY + (75 / 2) * (float)Math.Sin(alpha));
            t = new Timer();
            t.Tick += Tick;
            t.Interval = 10;
            t.Start();
        }

        private void Tick(object sender, EventArgs e)
        {
            alpha += speed;
            point = new PointF(mX + (75 / 2) * (float)Math.Cos(alpha), mY + (75 / 2) * (float)Math.Sin(alpha));
        }

        public void Draw(Graphics g)
        {
            g.DrawEllipse(new Pen(Brushes.White, 2), new RectangleF(new PointF(x, y), new Size(75,75)));
            g.FillEllipse(Brushes.Red, new RectangleF(new PointF(point.X - 2.5F, point.Y - 2.5F ), new SizeF(5, 5)));
        }
    }
}
