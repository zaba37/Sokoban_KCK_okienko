using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sokoban
{
    public partial class Ranking : Form
    {
        private CustomButton cbBack;
        private CustomButton cbArrowUp;
        private CustomButton cbArrowDown;
        private Bitmap pngLogo;
        private PictureBox logo;

        public Ranking()
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            pngLogo = new Bitmap(@"Drawable\logoRanking.png");
            logo = new PictureBox();
            logo.BackColor = Color.Transparent;
            logo.Image = pngLogo;
            logo.Width = pngLogo.Width;
            logo.Height = pngLogo.Height;
            logo.Location = new Point(250,20);

            cbBack = new CustomButton(@"Buttons\RankingButtons\BackNormal.png", @"Buttons\RankingButtons\BackPress.png", @"Buttons\RankingButtons\BackFocus.png", 20, 350, "BackTag");

            this.BackgroundImage = new Bitmap(@"Drawable\Wall_Beige.png");

            this.Controls.Add(logo);
            this.Controls.Add(cbBack);

            cbBack.MouseClick += new MouseEventHandler(mouseClick);
        }

        private void mouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                switch (((CustomButton)sender).Tag.ToString())
                {
                    case "ArrowUpTag":
                        break;
                    case "ArrowDownTag":
                        break;
                    case "BackTag":
                        var menu = (Menu)Tag;
                        menu.Show();
                        this.Hide();
                        break;
                }
            }
        }
    }
}
