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
            pngLogo = new Bitmap(@"Drawable\logoPause.png");
            logo.BackColor = Color.Transparent;
            logo.Image = pngLogo;
            logo.Width = pngLogo.Width;
            logo.Height = pngLogo.Height;
            logo.Location = new Point(380, 20);

            cbResume = new CustomButton(@"Buttons\PauseButtons\ResumeNormal.png", @"Buttons\PauseButtons\ResumePress.png", @"Buttons\PauseButtons\ResumeFocus.png", 540, 350, "ResumeTag");
            cbRestart = new CustomButton(@"Buttons\PauseButtons\RestartNormal.png", @"Buttons\PauseButtons\RestartPress.png", @"Buttons\PauseButtons\RestartFocus.png", 540, 450, "RestartTag");
            cbExit = new CustomButton(@"Buttons\PauseButtons\ExitNormal.png", @"Buttons\PauseButtons\ExitPress.png", @"Buttons\PauseButtons\ExitFocus.png", 540, 550, "ExitTag");

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
                        flag = 3;
                        this.Hide();
                        var menu = (Menu)Tag;
                        menu.Show();
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
