using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sokoban
{
    class CustomButton : PictureBox
    {
        Bitmap normal;
        Bitmap pressed;
        Bitmap focus;

        public CustomButton(String pathToImageNormal, String pathToImagePressed, String pathToImageFocused, int positionX, int positionY, String tag)
        {
            normal = new Bitmap(pathToImageNormal);
            pressed = new Bitmap(pathToImagePressed);
            focus = new Bitmap(pathToImageFocused);
            Image = normal;
            Tag = tag;
            Location = new Point(positionX, positionY);
            Height = normal.Height;
            Width = normal.Width;

            MouseDown += new MouseEventHandler(mouseClickDown);
            MouseUp += new MouseEventHandler(mouseClickUp);
            MouseMove += new MouseEventHandler(mouseMove);
            MouseLeave += mouseLeave;
        }

        private void mouseClickDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Image = pressed;
                Refresh();
            }
        }

        private void mouseClickUp(object sender, MouseEventArgs e)
        {
            Image = normal;
            Refresh();
        }

        private void mouseMove(object sender, MouseEventArgs e)
        {
            Image = focus;
            Refresh();
        }

        private void mouseLeave(object sender, EventArgs e)
        {
            Image = normal;
            Refresh();
        }

    }
}
