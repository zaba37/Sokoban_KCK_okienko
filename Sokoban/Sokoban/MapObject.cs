using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sokoban
{
    public class MapObject
    {
        public PictureBox picturebox;
        protected Point point;
        public virtual void setPosition(int x, int y)
        {
            

        }

        public int getX()
        {
            return point.X;
        }

        public int getY()
        {
            return point.Y;
        }
    }
}
