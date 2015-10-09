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
    public partial class Pause : Form
    {
        private CustomButton cbResume;
        private CustomButton cbRestart;
        private CustomButton cbExit;
        private Bitmap pngLogo;
        private PictureBox logo;
        public int flag;
        public Pause()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            this.BackgroundImage = new Bitmap(@"Drawable\Wall_Beige.png");

            logo = new PictureBox();
            pngLogo = new Bitmap(@"Drawable\logoMenu.png");
            logo.BackColor = Color.Transparent;
            logo.Image = pngLogo;
            logo.Width = pngLogo.Width;
            logo.Height = pngLogo.Height;
            logo.Location = new Point(250, 20);


            
            cbResume = new CustomButton(@"Buttons\MenuButtons\NewGameNormal.png", @"Buttons\MenuButtons\NewGamePress.png", @"Buttons\MenuButtons\NewGameFocus.png", 450, 350, "ResumeTag");
            cbRestart= new CustomButton(@"Buttons\MenuButtons\RankingNormal.png", @"Buttons\MenuButtons\RankingPress.png", @"Buttons\MenuButtons\RankingFocus.png", 480, 450, "RestartTag");
            cbExit = new CustomButton(@"Buttons\MenuButtons\ExitNormal.png", @"Buttons\MenuButtons\ExitPress.png", @"Buttons\MenuButtons\ExitFocus.png", 540, 550, "ExitTag");


           // this.Controls.Add(logo);
            this.Controls.Add(cbResume);
            this.Controls.Add(cbRestart);
            this.Controls.Add(cbExit);
            this.Controls.Add(logo);

            cbResume.MouseClick += new MouseEventHandler(mouseClick);
            cbRestart.MouseClick += new MouseEventHandler(mouseClick);
            cbExit.MouseClick += new MouseEventHandler(mouseClick);
        }


        private void mouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                switch (((CustomButton)sender).Tag.ToString())
                {
                    case "ResumeTag":
                        flag = 1;
                      this.Close();
                        break;
                    case "ExitTag":
                        Environment.Exit(0);
                        break;

                    case "RestartTag":
                        flag = 2;
                        this.Close();

                        break;
                }
            }
        }
    
    }
}
