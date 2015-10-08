using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Sokoban
{
   public class Hero:MapObject
    {
        public Image graphics;
        
        public int posX;
        public int posY;

       public Hero(int height,int width,int startPosX,int startPosY)
        {
            this.posX = startPosX;
            this.posY = startPosY;
            this.graphics = Image.FromFile(@"Map\heroDown.png");
            picturebox = new PictureBox();
            picturebox.Height = height;
            picturebox.Width = width;
            point = new Point(this.posX, this.posY);           
            picturebox.Image = this.graphics;
            picturebox.Location = point;
        }

       override public void setPosition(int x, int y)
        {
            this.posX = x;
            this.posY = y;
            point.X = posX;
            point.Y = posY;
            picturebox.Location = point;            
            
        }

        public void setGraphics(string whereLooks)
        {
            if (whereLooks == "up")
            {

                graphics = Image.FromFile(@"Map\heroUp.png");
                picturebox.Image = graphics;
            }
            if (whereLooks == "down")
            {

                graphics = Image.FromFile(@"Map\heroDown.png");
                picturebox.Image = graphics;
            }

            if (whereLooks == "right")
            {
                graphics = Image.FromFile(@"Map\heroRight.png");
            }
            if (whereLooks == "left")
            {
                graphics = Image.FromFile(@"Map\heroLeft.png");
            }
        }

    }
}
