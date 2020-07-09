using System.Drawing;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing.Imaging;

namespace Cirlces_With_Stuff
{
    class MainClass
    {
        Config Config;
        Circles[] circle;
        Brush[] brushes; // Color of each function.
        public Bitmap Canvas { get; set; } // Store the old drawing.
        public static Size ClientSize;
        public MainClass()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            circle = new Circles[Config.NumberOfCircles * 2 + 1];
            brushes = new Brush[Config.NumberOfCircles * Config.NumberOfCircles + 1];
            Config = new Config();
            Set_Postions();
            Stuff();
            ResizeCanvas();
        }

        /// <summary>
        /// Create every Circle object.
        /// </summary>
        private void Set_Postions()
        {
            int j = 1;
            for (int i = 1; i < circle.Length; i++)
                circle[i] = (i <= Config.NumberOfCircles) ? new Circles(85 * i, 0, (float)i / (16 * 10)) : new Circles(0, 85 * j, (float)j++ / (16 * 10));
        }

        /// <summary>
        /// Set a random color for each function.
        /// </summary>
        private void Stuff()
        {
            Random random = new Random();
            for (int i = 0; i < brushes.Length; i++)
            {
                var R = random.Next(100, 256);
                var G = random.Next(100, 256);
                var B = random.Next(100, 256);
                var brush = new SolidBrush(Color.FromArgb(255, R,G,B));
                brushes[i] = brush;
            }
        }
        public void Draw(Graphics g)
        {
            /**
             * Draw the previous picture.
             */

            g.DrawImage(Canvas, 0, 0);

            Config.Draw(g);
            /* ------------------------------------------------------- */

            /**
             * Draw the background lines.
             */
            for (int i = 1; i <= Config.NumberOfCircles; i++)
                g.DrawLine(new Pen(Brushes.Gray, 2), 0, -5 + 85 * i, ClientSize.Width, -5 + 85 * i);
            for (int i = 1; i <= Config.NumberOfCircles; i++)
                g.DrawLine(new Pen(Brushes.Gray, 2), -5 + 85 * i, 0, -5 + 85 * i, ClientSize.Height);

            /* ------------------------------------------------------- */

            /**
             * Draw the circles
             */

            for (int i = 1; i < circle.Length; i++)
                circle[i].Draw(g);

            /* ------------------------------------------------------- */

            /**
             * Draw the golden dot of each function.
             */

            for (int i = 1; i <= Config.NumberOfCircles; i++)
                for (int j = Config.NumberOfCircles + 1; j <= 2 * Config.NumberOfCircles; j++)
                    g.FillEllipse(Brushes.Gold, new RectangleF(new PointF(circle[i].point.X, circle[j].point.Y), new SizeF(3.5F, 3.5F)));

            /* ------------------------------------------------------- */

            /**
             * Draw the functions.
             */

            for (int i = 1; i <= Config.NumberOfCircles; i++)
                for (int j = Config.NumberOfCircles + 1; j <= 2 * Config.NumberOfCircles; j++)
                {
                    PointF pointF = new PointF(circle[i].point.X, circle[j].point.Y); 
                    var brushIndex = Get_Pos(pointF); // No need to check because the intersection point is 100% inside the grid.
                    using (Graphics gra = Graphics.FromImage(Canvas))
                    {
                        gra.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        gra.FillEllipse(brushes[brushIndex], new RectangleF(pointF, new SizeF(1.5F, 1.5F)));
                    }
                }
            /* ------------------------------------------------------- */
        }

        /// <summary>
        ///  Get the point location on the Grid.
        /// </summary>
        /// <param name="p"> The point to check its place on the Grid. </param> 
        /// <returns> -1 If the point is not on the Grid </returns> 
        private int Get_Pos(PointF p)
        {
            for (int i = 1; i <= Config.NumberOfCircles + 1; i++)
            {
                for (int j = 1; j <= Config.NumberOfCircles + 1; j++)
                {
                    if (p.X <= 85 * i && p.X >= 85 * (i - 1))
                        if (p.Y <= 85 * j && p.Y >= 85 * (j - 1))
                            return (i - 1) * (j - 1);
                }
            }
            return -1; // Points not found
        }

        /// <summary>
        /// Create the canves and set it to Form's size.
        /// </summary>
        private void ResizeCanvas()
        {
            Bitmap tmp = new Bitmap(ClientSize.Width, ClientSize.Height, PixelFormat.Format32bppRgb);
            using (Graphics g = Graphics.FromImage(tmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.Clear(Color.Transparent);
                if (Canvas != null)
                {
                    //g.DrawImage(Canvas, 0, 0);
                    Canvas.Dispose();
                }
            }
            Canvas = tmp;
        }

        public void Refreash()
        {
            InitializeComponent();
            ResizeCanvas();
        }
    }
}
