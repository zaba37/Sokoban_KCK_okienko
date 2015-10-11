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
        private TextBox nameTb;
        private int Points;
        Menu menuWindow;
        public EndGame(int points)
        {
            InitializeComponent();
            Points = 0;
            Points = points;

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

            this.BackgroundImage = new Bitmap(@"Drawable\Wall_Beige.png");

            cbExit = new CustomButton(@"Buttons\EndGameButtons\ExitNormal.png", @"Buttons\EndGameButtons\ExitPress.png", @"Buttons\EndGameButtons\ExitFocus.png", 1050, 650, "ExitTag");
            cbSave = new CustomButton(@"Buttons\EndGameButtons\SaveNormal.png", @"Buttons\EndGameButtons\SavePress.png", @"Buttons\EndGameButtons\SaveFocus.png", 20, 650, "SaveTag");

            nameTb = new TextBox();
            nameTb.Location = new Point(150, 300);
            nameTb.Width = 400;
            nameTb.Height = 220;
            nameTb.BackColor = Color.Red;

            this.Controls.Add(nameTb);
            this.Controls.Add(logoMSG);
            this.Controls.Add(logo);
            this.Controls.Add(cbExit);
            this.Controls.Add(cbSave);
            
            cbSave.MouseClick += new MouseEventHandler(mouseClick);
            cbExit.MouseClick += new MouseEventHandler(mouseClick);
        }


        
          

        private void mouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                switch (((CustomButton)sender).Tag.ToString())
                {
                    case "SaveTag":
                        saveRanking(nameTb.Text, Points);                       
                        var menu2 = (Menu)Tag;
                        menu2.Show();
                        this.Close();
                        break;
                    case "ExitTag":
                        this.Close();
                        var menu = (Menu)Tag;
                        menu.Show();
                        break;                  
                }
            }
        }



        private void saveRanking(string name, int points)
        {
            Sokoban.Ranking.RankingItem newItem = new Sokoban.Ranking.RankingItem(name, points);
            String fileName = @"ranking.txt";
            System.IO.StreamWriter file;
            if (System.IO.File.Exists(fileName))
            {

                file = new System.IO.StreamWriter(fileName, true);
                file.WriteLine("{0} {1}", newItem.name, newItem.score);
            }
            else
            {
                System.IO.File.Create(fileName).Close();
                file = new System.IO.StreamWriter(fileName, true);
                file.WriteLine("{0} {1}", newItem.name, newItem.score);
            }

            file.Close();


        }
    
    }
}
