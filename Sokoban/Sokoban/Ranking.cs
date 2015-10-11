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
            RankingHeadLabelLocation = new Point(512, 320);
            RankingHeadLabel = new Label();
            RankingHeadLabel.Width = 320;
            RankingHeadLabel.Height = 30;
            RankingHeadLabel.Font = new Font("Arial", 24, FontStyle.Bold);
            RankingHeadLabel.Location = RankingHeadLabelLocation;
            RankingHeadLabel.BackColor = System.Drawing.Color.Transparent;
            RankingHeadLabel.Text = "   Name         Score";
            
            RankingHeadLabel.ForeColor = System.Drawing.Color.Brown;
            this.Controls.Add(RankingHeadLabel);

            RankingNameLabelLocation = new Point(512, 350);
            RankingNameLabel = new Label();
            RankingNameLabel.Width = 160;
            RankingNameLabel.Height = 290;
            RankingNameLabel.TextAlign = ContentAlignment.TopCenter;
            RankingNameLabel.Font = new Font("Arial", 16, FontStyle.Bold);
            RankingNameLabel.ForeColor = System.Drawing.Color.Black;
            RankingNameLabel.Location = RankingNameLabelLocation;
            RankingNameLabel.BackColor = System.Drawing.Color.Transparent;
            RankingNameLabel.Text = "";
            
            for (int i = from; i < to; i++)
            {
                RankingNameLabel.Text += RankingItemList[i].name + "\n";

            }

            this.Controls.Add(RankingNameLabel);

            RankingScoreLabelLocation = new Point(671, 350);

            RankingScoreLabel = new Label();
            RankingScoreLabel.Width = 160;
            RankingScoreLabel.Height = 290;
            RankingScoreLabel.TextAlign = ContentAlignment.TopCenter;
            RankingScoreLabel.Font = new Font("Arial", 16, FontStyle.Bold);
            RankingScoreLabel.Location = RankingScoreLabelLocation;
            RankingScoreLabel.BackColor = System.Drawing.Color.Transparent;
            RankingScoreLabel.Text = "";

            for (int i = from; i < to; i++)
            {
                RankingScoreLabel.Text += RankingItemList[i].score + "\n";

            }

            this.Controls.Add(RankingScoreLabel);
        }

        public void scrollRanking(int from, int to)
        {
            RankingScoreLabel.Text = null;
            RankingNameLabel.Text = null;

            for (int i = from; i < to; i++)
            {
                RankingScoreLabel.Text += RankingItemList[i].score + "\n";

            }

            for (int i = from; i < to; i++)
            {
                RankingNameLabel.Text += RankingItemList[i].name + "\n";

            }
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
            

            this.DoubleBuffered = true;

            cbBack = new CustomButton(@"Buttons\RankingButtons\BackNormal.png", @"Buttons\RankingButtons\BackPress.png", @"Buttons\RankingButtons\BackFocus.png", 20, 660, "BackTag");
            cbArrowUp = new CustomButton(@"Buttons\RankingButtons\UpNormal.png", @"Buttons\RankingButtons\UpPress.png", @"Buttons\RankingButtons\UpFocus.png", 920, 580, "ArrowUpTag");
            cbArrowDown = new CustomButton(@"Buttons\RankingButtons\DownNormal.png", @"Buttons\RankingButtons\DownPress.png", @"Buttons\RankingButtons\DownFocus.png", 920, 680, "ArrowDownTag");
            this.BackgroundImage = new Bitmap(@"Drawable\Wall_Beige.png");

            this.Controls.Add(cbBack);
            this.Controls.Add(cbArrowUp);
            this.Controls.Add(cbArrowDown);

            cbBack.MouseClick += new MouseEventHandler(mouseClick);

            cbArrowUp.MouseClick += new MouseEventHandler(mouseClick);
            cbArrowDown.MouseClick += new MouseEventHandler(mouseClick);

            if (RankingItemList.Count() > 11)
            {
                printRanking(0, 11);
            }
            else
            {
                printRanking(0, RankingItemList.Count());
            }

            this.Controls.Add(RankingBackground);
            this.Controls.Add(logo);

        }

        private void mouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                switch (((CustomButton)sender).Tag.ToString())
                {
                    case "ArrowUpTag":
                        if (from > 0)
                        {
                            from--;
                            to--;
                            scrollRanking(from, to);
                        }
                        break;
                    case "ArrowDownTag":
                        RankingScoreLabel.Text = " ";
                        if (to < RankingItemList.Count())
                        {
                            from++;
                            to++;
                            scrollRanking(from, to);
                        }
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
