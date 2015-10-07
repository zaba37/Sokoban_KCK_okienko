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
        Boolean flagDown = false;

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
            if (e.Button == MouseButtons.Left && flagDown == false)
            {
                flagDown = true;
                Image = pressed;
            }
            else
            {
                flagDown = false;
            }
        }

        private void mouseClickUp(object sender, MouseEventArgs e)
        {
            flagDown = false;
            Image = normal;
        }

        private void mouseMove(object sender, MouseEventArgs e)
        {
            if(flagDown == false){
                Image = focus;
            }     
        }

        private void mouseLeave(object sender, EventArgs e)
        {
            Image = normal;
        }

    }
}
