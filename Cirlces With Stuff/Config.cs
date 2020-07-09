using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Cirlces_With_Stuff
{
    class Config
    {
        TextBox NumberOfCircles_TextBox;
        public static int NumberOfCircles = 8;

        /// <summary>
        /// Form Controls, to ADD controls to the Main Form.
        /// </summary>
        public static Control.ControlCollection Controls;

        /// <summary>
        /// Indecator if the user wants to Refresh the drawing or modify the number of circles.
        /// </summary>
        public static bool Refresh;

        /// <summary>
        /// Screen WorkingArea Size.
        /// </summary>
        public static Rectangle Screen;

        /// <summary>
        /// Refresh Button, Run the program again with the new NumberOfCircles value.
        /// </summary>
        Button Refresh_button;
        public Config()
        {
            CreateTextBox();
            CreateButton();
            Controls.Add(NumberOfCircles_TextBox);
            Controls.Add(Refresh_button);
        }

        /// <summary>
        /// Create the TextBox configurations.
        /// </summary>
        private void CreateTextBox()
        {
            NumberOfCircles_TextBox = new TextBox
            {
                Text = NumberOfCircles.ToString(),
                Name = "Number OF Circles",
                Font = new Font("", 12),
                BorderStyle = BorderStyle.None,
                ForeColor = Color.White,
                BackColor = Color.Black,
                MaxLength = 2, // Accept 2 digit number.
                
            };

            /**
             * Take the Size of the string, and adjust the TextBox location and Size to it.
             */
            using (Graphics graphics = Graphics.FromImage(new Bitmap(1, 1)))
            {
                SizeF size = graphics.MeasureString("Num.O:", new Font("", 10, FontStyle.Regular, GraphicsUnit.Point));
                NumberOfCircles_TextBox.Location = new Point((int)(size.Width), 0);
                NumberOfCircles_TextBox.Size = new Size(70 - (int)size.Width, 10);
            }

            /**
             * Accept the keypress from the keyboard.
             * Check if the key pressed is a digit, if not then dont add it to the text.
             */
            NumberOfCircles_TextBox.KeyPress += new KeyPressEventHandler((s, e) =>
            {
                if (!int.TryParse(e.KeyChar.ToString(), out _))
                    e.Handled = true;
            });
        }

        /// <summary>
        /// Create the RefreshButton configurations.
        /// </summary>
        public void CreateButton()
        {
            // Refreash button configs // 
            Refresh_button = new Button()
            {
                Text = "Refresh",
                Name = "Refresh Button",
                BackColor = Color.Black,
                ForeColor = Color.Cyan,
                Font = new Font("", 12),
                Size = new Size(85, 40),
                FlatStyle = FlatStyle.Flat
            };

            // Set the button to be invisible.
            Refresh_button.FlatAppearance.BorderSize = 0;
            Refresh_button.FlatAppearance.BorderColor= Color.Black;
            Refresh_button.FlatAppearance.MouseOverBackColor = Color.Transparent;
            Refresh_button.FlatAppearance.MouseDownBackColor = Color.Transparent;

            /**
             * Take the Size of the string, and adjust the Button location to it.
             */
            using (Graphics graphics = Graphics.FromImage(new Bitmap(1, 1)))
            {
                SizeF size = graphics.MeasureString("Num.O:", new Font("", 10, FontStyle.Regular, GraphicsUnit.Point));
                Refresh_button.Location = new Point(-5, (int)(size.Height));
            }

            // Change the color of the text if the mouse went in the button area.
            Refresh_button.MouseEnter += (s, e) =>
                Refresh_button.ForeColor = Color.Green;
            Refresh_button.MouseLeave += (s, e) =>
                Refresh_button.ForeColor = Color.Cyan;

            /**
             * When Clicked take the value from the TextBox.
             * Check if the value is legit (the circles dont go outside the user screen).
             * Refresh if everything is fine.
             */
            Refresh_button.MouseClick += (s, e) =>
            {
                if (NumberOfCircles_TextBox.Text.Length == 0)
                    NumberOfCircles_TextBox.Text = "0";
                var temp = int.Parse(NumberOfCircles_TextBox.Text);
                if ((temp + 1) * 85 > Screen.Height || (temp + 1) * 85 > Screen.Width)
                {
                    MessageBox.Show($"Enter a value between 0 and {(int)(((temp + 1) * 85 > Screen.Height) ? Screen.Height / 85 : Screen.Width / 85) - 1}");
                    return;
                }
                
                NumberOfCircles = temp;
                Refresh = true;
            };
        }

        public void Draw(Graphics g)
        {
            g.DrawString($"Num.O: ", new Font("", 10), Brushes.Red, new Point(0, 2));
        }
    }
}
