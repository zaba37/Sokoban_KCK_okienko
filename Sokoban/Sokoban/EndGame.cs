using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sokoban
{
    public partial class EndGame : Form
    {
        private CustomButton cbExit;
        private CustomButton cbSave;
        private Bitmap pngLogo;
        private PictureBox logo;
        private Bitmap pngMSG;
        private PictureBox logoMSG;
        private PictureBox logoEnterName;
      

        public EndGame()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            pngLogo = new Bitmap(@"Drawable\logoEndGame.png");
            logo = new PictureBox();
            logo.BackColor = Color.Transparent;
            logo.Image = pngLogo;
            logo.Width = pngLogo.Width;
            logo.Height = pngLogo.Height;
            logo.Location = new Point(170, 20);

            pngMSG = new Bitmap(@"Drawable\logoMSG.png");
            logoMSG = new PictureBox();
            logoMSG.BackColor = Color.Transparent;
            logoMSG.Image = pngMSG;
            logoMSG.Width = pngMSG.Width;
            logoMSG.Height = pngMSG.Height;
            logoMSG.Location = new Point(160, 300);

            logoEnterName = new PictureBox();
            logoEnterName.Image = new Bitmap(@"Drawable\logoEnterName.png");
            logoEnterName.Width = logoEnterName.Image.Width;
            logoEnterName.Height = logoEnterName.Image.Height;
            logoEnterName.Location = new Point(350, 450);
            logoEnterName.BackColor = Color.Transparent;

            this.BackgroundImage = new Bitmap(@"Drawable\Wall_Beige.png");

            cbExit = new CustomButton(@"Buttons\EndGameButtons\ExitNormal.png", @"Buttons\EndGameButtons\ExitPress.png", @"Buttons\EndGameButtons\ExitFocus.png", 1050, 650, "ExitTag");
            cbSave = new CustomButton(@"Buttons\EndGameButtons\SaveNormal.png", @"Buttons\EndGameButtons\SavePress.png", @"Buttons\EndGameButtons\SaveFocus.png", 20, 650, "SaveTag");
           
            this.Controls.Add(logoMSG);
            this.Controls.Add(logo);
            this.Controls.Add(cbExit);
            this.Controls.Add(cbSave);
            this.Controls.Add(logoEnterName);
        }
    }
}
