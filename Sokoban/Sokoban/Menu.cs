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
            

            String pat = new FileInfo("buttons").Directory.FullName;
            string fileName = "ich_will.mp3";
            string path = Path.Combine(Environment.CurrentDirectory, @"buttons\", "normal.png");
            //cbNewGame = new CustomButton();
            InitializeComponent();
        }
    }
}
