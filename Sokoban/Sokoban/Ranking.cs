using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.IO;

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
        private int to = 20;


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



        private void rankingListView()
        {
            
            ListView listView1 = new ListView();
            listView1.Bounds = new Rectangle(new Point(500, 400), new Size(300, 200));
            listView1.View = View.Details;
            
            foreach(RankingItem k in RankingItemList)
            {

                ListViewItem item1 = new ListViewItem(k.name, 0);
                item1.SubItems.Add(Convert.ToString(k.score));
                listView1.Items.AddRange(new ListViewItem[] { item1 });
            }

            listView1.Columns.Add("name", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("score", -2, HorizontalAlignment.Left);
            
            Controls.Add(listView1);
        }



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
            logo.Location = new Point(290,20);

            this.DoubleBuffered = true;

            cbBack = new CustomButton(@"Buttons\RankingButtons\BackNormal.png", @"Buttons\RankingButtons\BackPress.png", @"Buttons\RankingButtons\BackFocus.png", 20, 660, "BackTag");

            this.BackgroundImage = new Bitmap(@"Drawable\Wall_Beige.png");

            this.Controls.Add(logo);
            this.Controls.Add(cbBack);

            cbBack.MouseClick += new MouseEventHandler(mouseClick);
            loadRanking();
            rankingListView();

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
