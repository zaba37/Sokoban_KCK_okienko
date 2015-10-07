using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sokoban
{
    public partial class Menu : Form
    {
        CustomButton cbNewGame;
        CustomButton cbRanking;
        CustomButton cbExit;

        public Menu()
        {
            //Enable full screen
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;


            InitializeComponent();

            cbNewGame = new CustomButton(@"Buttons\MenuButtons\NewGameNormal.png", @"Buttons\MenuButtons\NewGamePress.png", @"Buttons\MenuButtons\NewGameFocus.png", 20, 20, "NewGameTag");
            cbRanking = new CustomButton(@"Buttons\MenuButtons\RankingNormal.png", @"Buttons\MenuButtons\RankingPress.png", @"Buttons\MenuButtons\RankingFocus.png", 20, 150, "ResumeTag");
            cbExit = new CustomButton(@"Buttons\MenuButtons\ExitNormal.png", @"Buttons\MenuButtons\ExitPress.png", @"Buttons\MenuButtons\ExitFocus.png", 20, 300, "ExitTag");
            
            this.Controls.Add(cbNewGame);
            this.Controls.Add(cbRanking);
            this.Controls.Add(cbExit);

            cbNewGame.MouseClick += new MouseEventHandler(mouseClick);
            cbRanking.MouseClick += new MouseEventHandler(mouseClick);
            cbExit.MouseClick += new MouseEventHandler(mouseClick);
        }

        private void mouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                switch(((CustomButton)sender).Tag.ToString()){
                    case "ExitTag":
                        Application.Exit();
                        break;

                    case "NewGameTag":
                        this.Hide();
                        Game newGame = new Game();                       
                        newGame.Show();
                        
                        break;
                }

            }
        }
    }
}
