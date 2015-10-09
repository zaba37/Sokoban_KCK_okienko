using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sokoban
{
    public class Box : MapObject
    {
        public Image graphics;
        public int posX;
        public int posY;

        public Box(int height, int width, int startPosX, int startPosY)
        {
            this.posX = startPosX;
            this.posY = startPosY;
            this.graphics = Image.FromFile(@"Map\Box.png");
            picturebox = new PictureBox();
            picturebox.Height = height;
            picturebox.Width = width;
            point = new Point(this.posX, this.posY);
            picturebox.Image = this.graphics;
            picturebox.Location = point;
            picturebox.BackColor = Color.Transparent;

        }

        override public void setPosition(int x, int y)
        {
            this.posX = x;
            this.posY = y;
            point.X = posX;
            point.Y = posY;
            picturebox.Location = point;

        }
    }
}
