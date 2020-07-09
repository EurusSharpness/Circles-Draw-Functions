using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Cirlces_With_Stuff
{
    public partial class Form1 : Form
    {
        MainClass main;
        public Form1()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = (System.Drawing.Drawing2D.SmoothingMode.HighQuality);
            if(main != null)
                main.Draw(g);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ClientSize = new Size(765, 765);
            Text = "Circle Functions";
            BackColor = Color.Black;
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Location = new Point(0, 0);
            ActiveControl = null;
            KeyPreview = true;
            Config.Controls = Controls;
            Config.Screen = Screen.PrimaryScreen.WorkingArea;
            MainClass.ClientSize = ClientSize;
            main = new MainClass();
        }

        private void Intervaler_Tick(object sender, EventArgs e)
        {
            if(Config.Refresh)
            {
                Config.Refresh = false;
                ClientSize = new Size((Config.NumberOfCircles + 1) * 85, (Config.NumberOfCircles + 1) * 85);
                MainClass.ClientSize = ClientSize;
                main.Refreash();
            }
            Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
            if (e.KeyCode == Keys.Space)
                Config.Refresh = true;
        }
    }
}
