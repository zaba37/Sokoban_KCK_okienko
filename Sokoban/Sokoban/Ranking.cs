using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.IO;
using System.Linq;

namespace Sokoban
{
    public partial class Ranking : Form
    {
        private CustomButton cbBack;
        private CustomButton cbArrowUp;
        private CustomButton cbArrowDown;
        private Bitmap pngLogo;
        private PictureBox logo;
        public List<RankingItem> RankingItemList = new List<RankingItem>();
        private int from = 0;
        private int to = 12;
        private Label RankingHeadLabel;
        private Point RankingHeadLabelLocation;
        private Label RankingNameLabel;
        private Point RankingNameLabelLocation;
        private Label RankingScoreLabel;
        private Point RankingScoreLabelLocation;
        private Bitmap pngRankingBackground;
        private PictureBox RankingBackground;


        public class RankingItem
        {
            public int score { get; set; }
            public string name { get; set; }

            public RankingItem(string n, int s)
            {
                name = n;
                score = s;
            }

        }

        private void loadRanking()
        {
            string line;
            String fileName = @"ranking.txt";
            System.IO.StreamReader file;

            if (System.IO.File.Exists(fileName))
            {
                file = new System.IO.StreamReader(fileName);
            }
            else
            {
                System.IO.File.Create(fileName).Close();
                file = new System.IO.StreamReader(fileName);
            }

            while ((line = file.ReadLine()) != null)
            {
                string[] namescore;
                namescore = line.Split(' ');
                RankingItemList.Add(new RankingItem(namescore[0], Int32.Parse(namescore[1])));
            }
            RankingItemList.Sort(delegate(RankingItem x, RankingItem y)
            {
                return y.score.CompareTo(x.score);
            });
            file.Close();
        }

        public void addScore(string name, int score)
        {
            RankingItemList.Add(new RankingItem(name, score));
        }
        
        /*
        private void saveRanking()
        {
            String fileName = @"ranking.txt";
            System.IO.StreamWriter file;
            if (System.IO.File.Exists(fileName))
            {
                file = new System.IO.StreamWriter(fileName);
            }
            else
            {
                System.IO.File.Create(fileName).Close();
                file = new System.IO.StreamWriter(fileName);
            }
            foreach (var RankingItem in RankingItemList)
            {
                file.WriteLine("{0} {1}", RankingItem.name, RankingItem.score);
            }
            file.Close();
            
            
        }
        */

        public void printRanking(int from, int to)
        {
            RankingHeadLabelLocation = new Point(500, 350);
            RankingHeadLabel = new Label();
            RankingHeadLabel.Width = 350;
            RankingHeadLabel.Height = 30;
            RankingHeadLabel.Font = new Font("Serif", 24, FontStyle.Bold);
            RankingHeadLabel.Location = RankingHeadLabelLocation;
            RankingHeadLabel.BackColor = System.Drawing.Color.Transparent;
            RankingHeadLabel.Text = "Name     Score";
            RankingHeadLabel.Parent = RankingBackground;
            this.Controls.Add(RankingHeadLabel);

            RankingNameLabelLocation = new Point(500, 380);
            RankingNameLabel = new Label();
            RankingNameLabel.Width = 175;
            RankingNameLabel.Height = 300;
            RankingNameLabel.Font = new Font("Arial", 16, FontStyle.Bold);
            RankingNameLabel.Location = RankingNameLabelLocation;
            RankingNameLabel.Parent = RankingBackground;
            RankingNameLabel.BackColor = System.Drawing.Color.Transparent;
            
            for (int i = from; i < to; i++)
            {
                RankingNameLabel.Text += RankingItemList[i].name + "\n";

            }

            this.Controls.Add(RankingNameLabel);

            RankingScoreLabelLocation = new Point(675, 380);
            RankingScoreLabel = new Label();
            RankingScoreLabel.Width = 175;
            RankingScoreLabel.Height = 300;
            RankingScoreLabel.Font = new Font("Arial", 16, FontStyle.Bold);
            RankingScoreLabel.Location = RankingScoreLabelLocation;
            RankingScoreLabel.Parent = RankingBackground;
            RankingScoreLabel.BackColor = System.Drawing.Color.Transparent;
            RankingScoreLabel.Text = "";
            for (int i = from; i < to; i++)
            {
                RankingScoreLabel.Text += RankingItemList[i].score + "\n";

            }
            this.Controls.Add(RankingScoreLabel);

        }

        public Ranking()
        {
            loadRanking();
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            pngLogo = new Bitmap(@"Drawable\logoRanking.png");
            logo = new PictureBox();
            logo.BackColor = Color.Transparent;
            logo.Image = pngLogo;
            logo.Width = pngLogo.Width;
            logo.Height = pngLogo.Height;
            logo.Location = new Point(290,20);

            RankingBackground = new PictureBox();
            pngRankingBackground = new Bitmap(@"Drawable\BackRanking.png");
            RankingBackground.Image = pngRankingBackground;
            RankingBackground.Width = pngRankingBackground.Width;
            RankingBackground.Height = pngRankingBackground.Height;
            RankingBackground.Location = new Point(448, 256);
            this.Controls.Add(RankingBackground);

            this.DoubleBuffered = true;

            cbBack = new CustomButton(@"Buttons\RankingButtons\BackNormal.png", @"Buttons\RankingButtons\BackPress.png", @"Buttons\RankingButtons\BackFocus.png", 20, 660, "BackTag");

            this.BackgroundImage = new Bitmap(@"Drawable\Wall_Beige.png");

            this.Controls.Add(logo);
            this.Controls.Add(cbBack);

            cbBack.MouseClick += new MouseEventHandler(mouseClick);

            if (RankingItemList.Count() > 11)
            {
                printRanking(0, 11);
            }
            else
            {
                printRanking(0, RankingItemList.Count());
            }
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
