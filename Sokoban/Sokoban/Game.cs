using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Windows.Input;

namespace Sokoban
{
    public partial class Game : Form
    {
        SoundPlayer typewriter = SoundSingleton.getSoundPlayerInstance();
        Pause pauseWindow;

        private List<List<int>> readNumbers;
        List<List<MapObject>> Map;


        private int mapNumber;
        private int numberOfMap;


        private int totalPoints;

        private int posX;
        private int posY;
        List<PointPosition> PointsList;

        private int SetBoxes;
        private int numberSteps;
        private int numberShiftsBoxes;

        private int previousNumberSteps;
        private int previousnumberShiftsBoxes;

        private int widthElement;
        private int heightElement;
        private CustomButton cbArrowUp;
        private CustomButton cbArrowDown;
        private CustomButton cbArrowRight;
        private CustomButton cbArrowLeft;
        private System.Timers.Timer timer;
        private DateTime startTime;
        private String elapsedTime;
        private TimeSpan elapsedTimeDateTime;

        private PictureBox framePb;


        private DateTime pauseTime;
        private Label infoTimeLabel;
        private Point infoTimeLabelLocation;

        private Label infoStepsLabel;
        private Point infoStepsLabelLocation;

        private Label infoBoxesLabel;
        private Point infoBoxesLabelLocation;


        private Label TimeLabel;
        private Point TimeLabelLocation;

        private Label StepsLabel;
        private Point StepsLabelLocation;

        private Label BoxesLabel;
        private Point BoxesLabelLocation;


        private PictureBox[] startScreen;
        private CustomButton cbStart;


        public Game()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            Control.CheckForIllegalCrossThreadCalls = false;
            this.BackgroundImage = Image.FromFile(@"Map\Floor.png");
            this.DoubleBuffered = true;

            mapNumber = 1;
            numberOfMap = 4;  //ILOSC MAP

            PointsList = null;
            SetBoxes = 0;
            posX = 0;
            posY = 0;
            widthElement = 64;
            heightElement = 64;
            totalPoints = 0;

            typewriter.Stop();
            typewriter.SoundLocation = @"Music\step.wav";

            startScreen = new PictureBox[9];
            startScreen[0] = new PictureBox();
            startScreen[0].Image = new Bitmap(@"Drawable\L1.png");
            startScreen[1] = new PictureBox();
            startScreen[1].Image = new Bitmap(@"Drawable\L2.png");
            startScreen[2] = new PictureBox();
            startScreen[2].Image = new Bitmap(@"Drawable\L3.png");
            startScreen[3] = new PictureBox();
            startScreen[3].Image = new Bitmap(@"Drawable\L4.png");
            startScreen[4] = new PictureBox();
            startScreen[4].Image = new Bitmap(@"Drawable\L5.png");
            startScreen[5] = new PictureBox();
            startScreen[5].Image = new Bitmap(@"Drawable\L6.png");
            startScreen[6] = new PictureBox();
            startScreen[6].Image = new Bitmap(@"Drawable\L7.png");
            startScreen[7] = new PictureBox();
            startScreen[7].Image = new Bitmap(@"Drawable\L8.png");
            startScreen[8] = new PictureBox();
            startScreen[8].Image = new Bitmap(@"Drawable\L9.png");

            foreach (PictureBox start in startScreen)
            {
                start.Location = new Point(250, 150);
                start.Height = start.Image.Height;
                start.Width = start.Image.Width;
                start.BackColor = Color.Transparent;
            }

            cbStart = new CustomButton(@"Buttons\GameButtons\StartNormal.png", @"Buttons\GameButtons\StartPress.png", @"Buttons\GameButtons\StartFocus.png", 550, 380, "StartTag");
            cbStart.MouseClick += new MouseEventHandler(mouseClick);
            initMap("sokoban_" + mapNumber + ".txt");
        }



        private List<List<int>> readFile(string path)
        {
            List<List<int>> intMap = null;

            try
            {
                var lines = File.ReadAllLines(path);
                var map = lines.Select(l => l.Split(' ')).ToList();
                intMap = map.Select(l => l.Select(i => int.Parse(i)).ToList()).ToList();
                return intMap;
            }
            catch
            {
                Environment.Exit(0);
            }

            return intMap;
        }

        private void initLabels()
        {

            framePb = new PictureBox();
            framePb.Location = new Point(960, 0);
            framePb.Image = new Bitmap(@"Map\frame.png");
            framePb.Height = framePb.Image.Height;
            framePb.Width = framePb.Image.Width;
            framePb.BackColor = Color.Transparent;






            infoTimeLabelLocation = new Point(1026, 70);
            infoTimeLabel = new Label();
            // infoTimeLabel.Width = 80;
            //infoTimeLabel.Height = 30;
            infoTimeLabel.AutoSize = true;
            infoTimeLabel.BackColor = Color.Red;
            infoTimeLabel.Font = new Font(Font.Name, 13);
            infoTimeLabel.Location = infoTimeLabelLocation;
            infoTimeLabel.BackColor = System.Drawing.Color.Transparent;
            // infoTimeLabel.Image = new Bitmap(@"Drawable\Wall_Gray.png");
            infoTimeLabel.Text = "Time: ";
            this.Controls.Add(infoTimeLabel);

            TimeLabelLocation = new Point(1086, 70);
            TimeLabel = new Label();
            TimeLabel.Width = 100;
            TimeLabel.Height = 30;
            TimeLabel.Font = new Font(Font.Name, 13);
            TimeLabel.Location = TimeLabelLocation;
            TimeLabel.BackColor = System.Drawing.Color.Transparent;
            TimeLabel.Text = "00:00";
            this.Controls.Add(TimeLabel);

            infoStepsLabelLocation = new Point(1026, 100);
            infoStepsLabel = new Label();
            // infoStepsLabel.Width = 175;
            // infoStepsLabel.Height = 30;
            infoStepsLabel.Font = new Font(Font.Name, 13);
            infoStepsLabel.Location = infoStepsLabelLocation;
            infoStepsLabel.BackColor = System.Drawing.Color.Transparent;
            infoStepsLabel.Text = "Number of steps: ";
            infoStepsLabel.AutoSize = true;
            this.Controls.Add(infoStepsLabel);

            StepsLabelLocation = new Point(1181, 100);
            StepsLabel = new Label();
            StepsLabel.Width = 60;
            StepsLabel.Height = 30;
            StepsLabel.Font = new Font(Font.Name, 13);
            StepsLabel.Location = StepsLabelLocation;
            StepsLabel.BackColor = System.Drawing.Color.Transparent;
            StepsLabel.Text = "0";
            this.Controls.Add(StepsLabel);


            infoBoxesLabelLocation = new Point(1026, 130);
            infoBoxesLabel = new Label();
            //infoBoxesLabel.Width = 175;
            //   infoBoxesLabel.Height = 30;
            infoBoxesLabel.Font = new Font(Font.Name, 13);
            infoBoxesLabel.Location = infoBoxesLabelLocation;
            infoBoxesLabel.BackColor = System.Drawing.Color.Transparent;
            infoBoxesLabel.Text = "Number of shifts boxes: ";
            infoBoxesLabel.AutoSize = true;
            this.Controls.Add(infoBoxesLabel);

            BoxesLabelLocation = new Point(1225, 130); //1136
            BoxesLabel = new Label();
            BoxesLabel.Width = 40;
            BoxesLabel.Height = 30;
            BoxesLabel.Font = new Font(Font.Name, 13);
            BoxesLabel.Location = BoxesLabelLocation;
            BoxesLabel.BackColor = System.Drawing.Color.Transparent;
            BoxesLabel.Text = "0";
            this.Controls.Add(BoxesLabel);
            this.Controls.Add(framePb);
        }


        private void initButtons()
        {
            cbArrowUp = new CustomButton(@"Buttons\GameButtons\UpNormal.png", @"Buttons\GameButtons\UpPress.png", @"Buttons\GameButtons\UpFocus.png", 1200, 550, "UpTag");
            cbArrowDown = new CustomButton(@"Buttons\GameButtons\DownNormal.png", @"Buttons\GameButtons\DownPress.png", @"Buttons\GameButtons\DownFocus.png", 1200, 620, "DownTag");
            cbArrowRight = new CustomButton(@"Buttons\GameButtons\RightNormal.png", @"Buttons\GameButtons\RightPress.png", @"Buttons\GameButtons\RightFocus.png", 1270, 620, "RightTag");
            cbArrowLeft = new CustomButton(@"Buttons\GameButtons\LeftNormal.png", @"Buttons\GameButtons\LeftPress.png", @"Buttons\GameButtons\LeftFocus.png", 1130, 620, "LeftTag");

            this.Controls.Add(cbArrowUp);
            this.Controls.Add(cbArrowDown);
            this.Controls.Add(cbArrowRight);
            this.Controls.Add(cbArrowLeft);
        }

        private void mouseClick(object sender, MouseEventArgs e)
        {
            // int[] heroPosition;
            if (e.Button == MouseButtons.Left)
            {
                switch (((CustomButton)sender).Tag.ToString())
                {
                    case "UpTag":
                        Map = refreshMap(Map, 1, 0, 0, 0);
                        SetBoxes = numberSetBoxes(Map, PointsList);
                        updateInfo();
                        typewriter.Play();
                        if (CheckEndRound(SetBoxes, PointsList))
                        {
                            if (mapNumber == numberOfMap)
                                endgame();
                            endRound();
                        }
                        break;
                    case "DownTag":
                        Map = refreshMap(Map, 0, 1, 0, 0);
                        SetBoxes = numberSetBoxes(Map, PointsList);
                        updateInfo();
                        typewriter.Play();
                        if (CheckEndRound(SetBoxes, PointsList))
                        {
                            if (mapNumber == numberOfMap)
                                endgame();
                            endRound();
                        }
                        break;

                    case "LeftTag":
                        Map = refreshMap(Map, 0, 0, 1, 0);
                        SetBoxes = numberSetBoxes(Map, PointsList);
                        updateInfo();
                        typewriter.Play();
                        if (CheckEndRound(SetBoxes, PointsList))
                        {
                            if (mapNumber == numberOfMap)
                                endgame();
                            endRound();
                        }
                        break;

                    case "RightTag":
                        Map = refreshMap(Map, 0, 0, 0, 1);
                        SetBoxes = numberSetBoxes(Map, PointsList);
                        updateInfo();
                        typewriter.Play();
                        if (CheckEndRound(SetBoxes, PointsList))
                        {
                            if (mapNumber == numberOfMap)
                                endgame();
                            endRound();
                        }
                        break;

                    case "StartTag":
                        timer.AutoReset = true;
                        startTime = DateTime.Now;
                        timer.Start();
                        startScreen[mapNumber - 1].Hide();
                        cbStart.Hide();
                        cbArrowUp.MouseClick += new MouseEventHandler(mouseClick);
                        cbArrowDown.MouseClick += new MouseEventHandler(mouseClick);
                        cbArrowRight.MouseClick += new MouseEventHandler(mouseClick);
                        cbArrowLeft.MouseClick += new MouseEventHandler(mouseClick);
                        break;
                }
            }
        }


        private void updateInfo()
        {
            if (numberSteps != previousNumberSteps)
            {
                StepsLabel.Text = numberSteps.ToString();
                previousNumberSteps = numberSteps;
            }

            if (numberShiftsBoxes != previousnumberShiftsBoxes)
            {
                BoxesLabel.Text = numberShiftsBoxes.ToString();
                previousnumberShiftsBoxes = numberShiftsBoxes; ;
            }
        }

        //POSTAC: 5
        //PUDELKO:6
        //PODLOGA:3
        //SCIANA:2
        //PUNKT:4
        void initMap(string pathFileMap)
        {
            this.Controls.Add(cbStart);
            cbStart.Show();
            this.Controls.Add(startScreen[mapNumber - 1]);
            startScreen[mapNumber - 1].Show();
            initButtons();
            PointsList = null;

            numberSteps = 0;
            numberShiftsBoxes = 0;

            previousnumberShiftsBoxes = 0;
            previousNumberSteps = 0;

            SetBoxes = 0;
            posX = 0;
            posY = 0;
            initLabels();

            readNumbers = readFile(pathFileMap);
            Map = new List<List<MapObject>>();
            PointsList = findPositionPoints(readNumbers);

            for (int i = 0; i < readNumbers.Count(); i++)
            {

                List<MapObject> initList = new List<MapObject>();

                for (int j = 0; j < readNumbers[i].Count(); j++)
                {

                    if (readNumbers[i][j] == 5)
                    {
                        Hero newHero = new Hero(heightElement, widthElement, posX, posY);
                        initList.Add(newHero);
                        this.Controls.Add(newHero.picturebox);
                    }


                    if (readNumbers[i][j] == 6)
                    {
                        Box newBox = new Box(heightElement, widthElement, posX, posY);
                        initList.Add(newBox);
                        this.Controls.Add(newBox.picturebox);
                    }


                    if (readNumbers[i][j] == 1)
                    {
                        NullElement newNullElement = new NullElement();
                        initList.Add(newNullElement);

                    }


                    if (readNumbers[i][j] == 2)
                    {
                        Wall newWall = new Wall(heightElement, widthElement, posX, posY);
                        initList.Add(newWall);
                        this.Controls.Add(newWall.picturebox);
                    }


                    if (readNumbers[i][j] == 4)
                    {
                        EndPoint newEndPoint = new EndPoint(heightElement, widthElement, posX, posY);
                        initList.Add(newEndPoint);
                        this.Controls.Add(newEndPoint.picturebox);
                    }


                    if (readNumbers[i][j] == 3)
                    {

                        Floor newFloor = new Floor(heightElement, widthElement, posX, posY);
                        initList.Add(newFloor);
                        this.Controls.Add(newFloor.picturebox);
                    }

                    posX = posX + 64;

                }
                posY = posY + 64;
                posX = posX - (64 * initList.Count());

                Map.Add(initList);
            }

            timer = new System.Timers.Timer(100);
            timer.Elapsed += (s, e) => UpdateTime(e);
        }

        private void UpdateTime(ElapsedEventArgs e)
        {
            try
            {
                elapsedTime = (DateTime.Now - startTime).ToString(@"mm\:ss");
                elapsedTimeDateTime = (DateTime.Now - startTime);
                // infoLabel.Text = elapsedTime;
                if (TimeLabel != null && elapsedTime != null)
                    TimeLabel.Text = elapsedTime;
            }
            catch
            {

            }
        }


        private List<List<MapObject>> refreshMap(List<List<MapObject>> map, int up, int down, int left, int right)
        {
            MapObject help;
            MapObject help2;
            MapObject toRemove;
            List<List<MapObject>> toReturn = new List<List<MapObject>>();
            int[] heroPosition = findHeroPosition(map);

            if (up != 0)
            {

                if (map[heroPosition[0] - 1][heroPosition[1]].GetType() == typeof(Wall)) //gdy na gorze bedzie sciana
                {
                    toReturn = map;

                }
                if (map[heroPosition[0] - 1][heroPosition[1]].GetType() == typeof(Floor)) //gdy na gorze bedzie podloga
                {
                    help = map[heroPosition[0] - 1][heroPosition[1]];
                    map[heroPosition[0] - 1][heroPosition[1]] = map[heroPosition[0]][heroPosition[1]];
                    map[heroPosition[0]][heroPosition[1]] = help;


                    map[heroPosition[0] - 1][heroPosition[1]].setPosition(map[heroPosition[0] - 1][heroPosition[1]].getX(), (map[heroPosition[0] - 1][heroPosition[1]].getY() - heightElement));
                    map[heroPosition[0]][heroPosition[1]].setPosition(map[heroPosition[0]][heroPosition[1]].getX(), (map[heroPosition[0]][heroPosition[1]].getY() + heightElement));

                    numberSteps++;
                    toReturn = map;

                }
                if (map[heroPosition[0] - 1][heroPosition[1]].GetType() == typeof(Box)) //gdy na gorze bedzie skrzynka
                {
                    if (map[heroPosition[0] - 2][heroPosition[1]].GetType() == typeof(Floor) || map[heroPosition[0] - 2][heroPosition[1]].GetType() == typeof(EndPoint)) //sprawdz czy mozna przesunac skrzynke(podloga lub punkt)
                    {


                        help = map[heroPosition[0]][heroPosition[1]];
                        map[heroPosition[0]][heroPosition[1]] = new Floor(heightElement, widthElement, heroPosition[1] * widthElement, heroPosition[0] * heightElement);
                        this.Controls.Add(map[heroPosition[0]][heroPosition[1]].picturebox);
                        help2 = map[heroPosition[0] - 1][heroPosition[1]];// = 5;
                        map[heroPosition[0] - 1][heroPosition[1]] = help;
                        toRemove = map[heroPosition[0] - 2][heroPosition[1]];

                        map[heroPosition[0] - 2][heroPosition[1]] = help2;


                        map[heroPosition[0] - 1][heroPosition[1]].setPosition(map[heroPosition[0] - 1][heroPosition[1]].getX(), (map[heroPosition[0] - 1][heroPosition[1]].getY() - heightElement));
                        map[heroPosition[0] - 2][heroPosition[1]].setPosition(map[heroPosition[0] - 2][heroPosition[1]].getX(), (map[heroPosition[0] - 2][heroPosition[1]].getY() - heightElement));

                        this.Controls.Remove(toRemove.picturebox);

                        numberSteps++;
                        numberShiftsBoxes++;
                        toReturn = map;
                    }
                    else
                    {
                        toReturn = map;
                    }
                }

                if (map[heroPosition[0] - 1][heroPosition[1]].GetType() == typeof(EndPoint)) //gdy na gorze bedzie punkt
                {

                    help = map[heroPosition[0]][heroPosition[1]];
                    map[heroPosition[0]][heroPosition[1]] = new Floor(heightElement, widthElement, heroPosition[1] * widthElement, heroPosition[0] * heightElement);
                    this.Controls.Add(map[heroPosition[0]][heroPosition[1]].picturebox);

                    toRemove = (map[heroPosition[0] - 1][heroPosition[1]]);
                    map[heroPosition[0] - 1][heroPosition[1]] = help;
                    map[heroPosition[0] - 1][heroPosition[1]].setPosition(map[heroPosition[0] - 1][heroPosition[1]].getX(), (map[heroPosition[0] - 1][heroPosition[1]].getY() - heightElement));
                    this.Controls.Remove(toRemove.picturebox);

                    numberSteps++;
                    toReturn = map;
                }
            }

            if (down != 0)
            {

                if (map[heroPosition[0] + 1][heroPosition[1]].GetType() == typeof(Wall)) //gdy na dole bedzie sciana
                {
                    toReturn = map;

                }
                if (map[heroPosition[0] + 1][heroPosition[1]].GetType() == typeof(Floor)) //gdy na dole bedzie podloga
                {
                    help = map[heroPosition[0] + 1][heroPosition[1]];
                    map[heroPosition[0] + 1][heroPosition[1]] = map[heroPosition[0]][heroPosition[1]];
                    map[heroPosition[0]][heroPosition[1]] = help;

                    map[heroPosition[0] + 1][heroPosition[1]].setPosition(map[heroPosition[0] + 1][heroPosition[1]].getX(), (map[heroPosition[0] + 1][heroPosition[1]].getY() + heightElement));
                    map[heroPosition[0]][heroPosition[1]].setPosition(map[heroPosition[0]][heroPosition[1]].getX(), (map[heroPosition[0]][heroPosition[1]].getY() - heightElement));

                    numberSteps++;

                    toReturn = map;
                }
                if (map[heroPosition[0] + 1][heroPosition[1]].GetType() == typeof(Box)) //gdy na dole bedzie skrzynka
                {
                    if (map[heroPosition[0] + 2][heroPosition[1]].GetType() == typeof(Floor) || map[heroPosition[0] + 2][heroPosition[1]].GetType() == typeof(EndPoint)) //sprawdz czy mozna przesunac skrzynke(podloga lub punkt)
                    {


                        help = map[heroPosition[0]][heroPosition[1]];
                        map[heroPosition[0]][heroPosition[1]] = new Floor(heightElement, widthElement, heroPosition[1] * widthElement, heroPosition[0] * heightElement);
                        this.Controls.Add(map[heroPosition[0]][heroPosition[1]].picturebox);
                        help2 = map[heroPosition[0] + 1][heroPosition[1]];// = 5;
                        map[heroPosition[0] + 1][heroPosition[1]] = help;
                        toRemove = (map[heroPosition[0] + 2][heroPosition[1]]);
                        map[heroPosition[0] + 2][heroPosition[1]] = help2;


                        map[heroPosition[0] + 1][heroPosition[1]].setPosition(map[heroPosition[0] + 1][heroPosition[1]].getX(), (map[heroPosition[0] + 1][heroPosition[1]].getY() + heightElement));
                        map[heroPosition[0] + 2][heroPosition[1]].setPosition(map[heroPosition[0] + 2][heroPosition[1]].getX(), (map[heroPosition[0] + 2][heroPosition[1]].getY() + heightElement));

                        this.Controls.Remove(toRemove.picturebox);

                        numberSteps++;
                        numberShiftsBoxes++;

                        toReturn = map;
                    }
                    else
                    {
                        toReturn = map;
                    }
                }

                if (map[heroPosition[0] + 1][heroPosition[1]].GetType() == typeof(EndPoint)) //gdy na dole bedzie punkt
                {

                    help = map[heroPosition[0]][heroPosition[1]];
                    map[heroPosition[0]][heroPosition[1]] = new Floor(heightElement, widthElement, heroPosition[1] * widthElement, heroPosition[0] * heightElement);
                    this.Controls.Add(map[heroPosition[0]][heroPosition[1]].picturebox);
                    this.Controls.Remove(map[heroPosition[0] + 1][heroPosition[1]].picturebox);
                    toRemove = (map[heroPosition[0] + 1][heroPosition[1]]);
                    map[heroPosition[0] + 1][heroPosition[1]] = help;
                    map[heroPosition[0] + 1][heroPosition[1]].setPosition(map[heroPosition[0] + 1][heroPosition[1]].getX(), (map[heroPosition[0] + 1][heroPosition[1]].getY() + heightElement));
                    this.Controls.Remove(toRemove.picturebox);

                    numberSteps++;


                    toReturn = map;
                }
            }

            if (left != 0)
            {

                if (map[heroPosition[0]][heroPosition[1] - 1].GetType() == typeof(Wall)) //gdy na lewo bedzie sciana
                {
                    toReturn = map;

                }
                if (map[heroPosition[0]][heroPosition[1] - 1].GetType() == typeof(Floor)) //gdy na lewo bedzie podloga
                {
                    help = map[heroPosition[0]][heroPosition[1] - 1];
                    map[heroPosition[0]][heroPosition[1] - 1] = map[heroPosition[0]][heroPosition[1]];
                    map[heroPosition[0]][heroPosition[1]] = help;

                    map[heroPosition[0]][heroPosition[1] - 1].setPosition(map[heroPosition[0]][heroPosition[1] - 1].getX() - widthElement, (map[heroPosition[0]][heroPosition[1] - 1].getY()));
                    map[heroPosition[0]][heroPosition[1]].setPosition(map[heroPosition[0]][heroPosition[1]].getX() + widthElement, (map[heroPosition[0]][heroPosition[1]].getY()));

                    numberSteps++;

                    toReturn = map;
                }
                if (map[heroPosition[0]][heroPosition[1] - 1].GetType() == typeof(Box)) //gdy na lewo bedzie skrzynka
                {
                    if (map[heroPosition[0]][heroPosition[1] - 2].GetType() == typeof(Floor) || map[heroPosition[0]][heroPosition[1] - 2].GetType() == typeof(EndPoint)) //sprawdz czy mozna przesunac skrzynke(podloga lub punkt)
                    {


                        help = map[heroPosition[0]][heroPosition[1]];
                        map[heroPosition[0]][heroPosition[1]] = new Floor(heightElement, widthElement, heroPosition[1] * widthElement, heroPosition[0] * heightElement);
                        this.Controls.Add(map[heroPosition[0]][heroPosition[1]].picturebox);
                        help2 = map[heroPosition[0]][heroPosition[1] - 1];// = 5;
                        map[heroPosition[0]][heroPosition[1] - 1] = help;

                        toRemove = map[heroPosition[0]][heroPosition[1] - 2];
                        map[heroPosition[0]][heroPosition[1] - 2] = help2;


                        map[heroPosition[0]][heroPosition[1] - 1].setPosition(map[heroPosition[0]][heroPosition[1] - 1].getX() - widthElement, (map[heroPosition[0]][heroPosition[1] - 1].getY()));
                        map[heroPosition[0]][heroPosition[1] - 2].setPosition(map[heroPosition[0]][heroPosition[1] - 2].getX() - widthElement, (map[heroPosition[0]][heroPosition[1] - 2].getY()));

                        this.Controls.Remove(toRemove.picturebox);

                        numberSteps++;
                        numberShiftsBoxes++;

                        toReturn = map;
                    }
                    else
                    {
                        toReturn = map;
                    }
                }

                if (map[heroPosition[0]][heroPosition[1] - 1].GetType() == typeof(EndPoint)) //gdy na lewo bedzie punkt
                {

                    help = map[heroPosition[0]][heroPosition[1]];
                    map[heroPosition[0]][heroPosition[1]] = new Floor(heightElement, widthElement, heroPosition[1] * widthElement, heroPosition[0] * heightElement);
                    this.Controls.Add(map[heroPosition[0]][heroPosition[1]].picturebox);
                    toRemove = map[heroPosition[0]][heroPosition[1] - 1];
                    map[heroPosition[0]][heroPosition[1] - 1] = help;
                    map[heroPosition[0]][heroPosition[1] - 1].setPosition(map[heroPosition[0]][heroPosition[1] - 1].getX() - widthElement, (map[heroPosition[0]][heroPosition[1] - 1].getY()));
                    this.Controls.Remove(toRemove.picturebox);

                    numberSteps++;

                    toReturn = map;
                }
            }

            if (right != 0)
            {

                if (map[heroPosition[0]][heroPosition[1] + 1].GetType() == typeof(Wall)) //gdy na prawo bedzie sciana
                {
                    toReturn = map;

                }
                if (map[heroPosition[0]][heroPosition[1] + 1].GetType() == typeof(Floor)) //gdy na prawo bedzie podloga
                {
                    help = map[heroPosition[0]][heroPosition[1] + 1];
                    map[heroPosition[0]][heroPosition[1] + 1] = map[heroPosition[0]][heroPosition[1]];
                    map[heroPosition[0]][heroPosition[1]] = help;

                    map[heroPosition[0]][heroPosition[1] + 1].setPosition(map[heroPosition[0]][heroPosition[1] + 1].getX() + widthElement, (map[heroPosition[0]][heroPosition[1] + 1].getY()));
                    map[heroPosition[0]][heroPosition[1]].setPosition(map[heroPosition[0]][heroPosition[1]].getX() - widthElement, (map[heroPosition[0]][heroPosition[1]].getY()));

                    numberSteps++;

                    toReturn = map;
                }
                if (map[heroPosition[0]][heroPosition[1] + 1].GetType() == typeof(Box)) //gdy na prawo bedzie skrzynka
                {
                    if (map[heroPosition[0]][heroPosition[1] + 2].GetType() == typeof(Floor) || map[heroPosition[0]][heroPosition[1] + 2].GetType() == typeof(EndPoint)) //sprawdz czy mozna przesunac skrzynke(podloga lub punkt)
                    {


                        help = map[heroPosition[0]][heroPosition[1]];
                        map[heroPosition[0]][heroPosition[1]] = new Floor(heightElement, widthElement, heroPosition[1] * widthElement, heroPosition[0] * heightElement);
                        this.Controls.Add(map[heroPosition[0]][heroPosition[1]].picturebox);

                        help2 = map[heroPosition[0]][heroPosition[1] + 1];// = 5;
                        map[heroPosition[0]][heroPosition[1] + 1] = help;
                        toRemove = map[heroPosition[0]][heroPosition[1] + 2];
                        map[heroPosition[0]][heroPosition[1] + 2] = help2;


                        map[heroPosition[0]][heroPosition[1] + 1].setPosition(map[heroPosition[0]][heroPosition[1] + 1].getX() + widthElement, (map[heroPosition[0]][heroPosition[1] + 1].getY()));
                        map[heroPosition[0]][heroPosition[1] + 2].setPosition(map[heroPosition[0]][heroPosition[1] + 2].getX() + widthElement, (map[heroPosition[0]][heroPosition[1] + 2].getY()));

                        this.Controls.Remove(toRemove.picturebox);

                        numberSteps++;
                        numberShiftsBoxes++;


                        toReturn = map;
                    }
                    else
                    {
                        toReturn = map;
                    }
                }

                if (map[heroPosition[0]][heroPosition[1] + 1].GetType() == typeof(EndPoint)) //gdy na prawo bedzie punkt
                {

                    help = map[heroPosition[0]][heroPosition[1]];
                    map[heroPosition[0]][heroPosition[1]] = new Floor(heightElement, widthElement, heroPosition[1] * widthElement, heroPosition[0] * heightElement);
                    this.Controls.Add(map[heroPosition[0]][heroPosition[1]].picturebox);
                    toRemove = map[heroPosition[0]][heroPosition[1] + 1];
                    map[heroPosition[0]][heroPosition[1] + 1] = help;
                    map[heroPosition[0]][heroPosition[1] + 1].setPosition(map[heroPosition[0]][heroPosition[1] + 1].getX() + widthElement, (map[heroPosition[0]][heroPosition[1] + 1].getY()));
                    this.Controls.Remove(toRemove.picturebox);

                    numberSteps++;

                    toReturn = map;
                }

            }

            foreach (PointPosition p in PointsList)
            {
                if (Map[p.X][p.Y].GetType() == typeof(Floor))
                {
                    int x = Map[p.X][p.Y].getX();
                    int y = Map[p.X][p.Y].getY();
                    toRemove = Map[p.X][p.Y];

                    Map[p.X][p.Y] = new EndPoint(heightElement, widthElement, x, y);
                    this.Controls.Add(Map[p.X][p.Y].picturebox);
                    this.Controls.Remove(toRemove.picturebox);
                }
            }

            return toReturn;
        }

        private int[] findHeroPosition(List<List<MapObject>> map)
        {
            Type t = typeof(Hero);
            int[] position = new int[2];
            for (int i = 0; i < map.Count(); i++)
            {
                for (int j = 0; j < map[i].Count(); j++)
                {
                    if (map[i][j].GetType() == t)
                    {
                        position[0] = i;
                        position[1] = j;
                        return position;
                    }
                }
            }
            return position;
        }

        private int numberSetBoxes(List<List<MapObject>> map, List<PointPosition> listPoints)
        {
            int number = 0;
            foreach (PointPosition p in listPoints)
            {
                if (map[p.X][p.Y].GetType() == typeof(Box))
                    number++;
            }
            return number;
        }

        private bool CheckEndRound(int numberSetBox, List<PointPosition> PointsPositionList)
        {
            bool endRound = false;
            if (numberSetBox == PointsPositionList.Count())
                endRound = true;

            return endRound;
        }

        private List<PointPosition> findPositionPoints(List<List<int>> map)
        {
            List<PointPosition> positionsList = new List<PointPosition>();
            for (int i = 0; i < map.Count(); i++)
            {

                for (int j = 0; j < map[i].Count(); j++)
                {
                    if (map[i][j] == 4)
                    {
                        PointPosition newPosition = new PointPosition(i, j);
                        positionsList.Add(newPosition);
                    }
                }
            }
            return positionsList;
        }

        private void endRound()
        {
            timer.Stop();
            mapNumber++;


            //  DateTime ElapsedTime = DateTime.Parse(elapsedTime);
            int totalSeconds = (elapsedTimeDateTime.Hours * 360) + (elapsedTimeDateTime.Minutes * 60) + elapsedTimeDateTime.Seconds;
            if (totalSeconds < 20)
                totalPoints = totalPoints + 100;
            if (totalSeconds >= 20 && totalSeconds <= 40)
                totalPoints = totalPoints + 50;
            if (totalSeconds > 40)
                totalPoints = totalPoints + 20;
            double pointsForSteps = ((double)numberSteps) * 0.1;

            totalPoints = totalPoints - (int)pointsForSteps;
            if (totalPoints < 0)
                totalPoints = 0;


            this.Controls.Clear();
            initMap("sokoban_" + mapNumber + ".txt");
        }

        private void pressEsc()
        {

            timer.Stop();
            pauseTime = DateTime.Now;

            typewriter.Stop();
            typewriter.SoundLocation = @"Music\pauseMusic.wav";
            typewriter.PlayLooping();

            if (pauseWindow == null)
            {
                pauseWindow = new Pause();
                pauseWindow.Tag = Tag;
            }

            this.Hide();

            pauseWindow.ShowDialog();

            if (pauseWindow.flag == 1)
            {
                var difference = DateTime.Now - pauseTime;
                startTime = startTime.Add(difference);

                typewriter.Stop();
                typewriter.SoundLocation = @"Music\step.wav";

                timer.Start();
                this.Show();
            }

            if (pauseWindow.flag == 2)
            {
                this.Controls.Clear();

                typewriter.Stop();
                typewriter.SoundLocation = @"Music\step.wav";


                double pointsForSteps = ((double)numberSteps) * 0.1;
                totalPoints = totalPoints - (int)pointsForSteps;

                initMap("sokoban_" + mapNumber + ".txt");
                this.Show();
            }

            if (pauseWindow.flag == 3)
            {
                typewriter.Stop();
                typewriter.SoundLocation = @"Music\mainMusic.wav";
                typewriter.PlayLooping();
                this.Close();
            }
        }


        private void endgame()
        {
            timer.Stop();
            TimeSpan test = elapsedTimeDateTime;
            // DateTime ElapsedTime = DateTime.Parse(elapsedTime);
            int totalSeconds = (elapsedTimeDateTime.Hours * 360) + (elapsedTimeDateTime.Minutes * 60) + elapsedTimeDateTime.Seconds;
            if (totalSeconds < 20)
                totalPoints = totalPoints + 100;
            if (totalSeconds >= 20 && totalSeconds <= 40)
                totalPoints = totalPoints + 50;
            if (totalSeconds > 40)
                totalPoints = totalPoints + 20;
            double pointsForSteps = ((double)numberSteps) * 0.1;

            totalPoints = totalPoints - (int)pointsForSteps;
            if (totalPoints < 0)
                totalPoints = 0;

            typewriter.Stop();
            typewriter.SoundLocation = @"Music\mainMusic.wav";
            typewriter.PlayLooping();

            EndGame endGameWindow = new EndGame(totalPoints);
            endGameWindow.Tag = this.Tag;
            endGameWindow.Show();
            this.Close();
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 38) //gora
            {
                int[] heroPosition = findHeroPosition(Map);
                Map = refreshMap(Map, 1, 0, 0, 0);
                SetBoxes = numberSetBoxes(Map, PointsList);
                updateInfo();
                if (CheckEndRound(SetBoxes, PointsList))
                {
                    if (mapNumber == numberOfMap)
                        endgame();
                    endRound();
                }
            }
            if (e.KeyValue == 40) //dol
            {
                int[] heroPosition = findHeroPosition(Map);
                Map = refreshMap(Map, 0, 1, 0, 0);
                SetBoxes = numberSetBoxes(Map, PointsList);
                updateInfo();
                if (CheckEndRound(SetBoxes, PointsList))
                {
                    if (mapNumber == numberOfMap)
                        endgame();
                    endRound();
                }

            }
            if (e.KeyValue == 39) //prawo
            {
                int[] heroPosition = findHeroPosition(Map);
                Map = refreshMap(Map, 0, 0, 0, 1);
                SetBoxes = numberSetBoxes(Map, PointsList);
                updateInfo();
                if (CheckEndRound(SetBoxes, PointsList))
                {
                    if (mapNumber == numberOfMap)
                        endgame();
                    endRound();
                }


            }

            if (e.KeyValue == 37) //lewo
            {
                int[] heroPosition = findHeroPosition(Map);
                Map = refreshMap(Map, 0, 0, 1, 0);
                SetBoxes = numberSetBoxes(Map, PointsList);
                updateInfo();
                if (CheckEndRound(SetBoxes, PointsList))
                {
                    if (mapNumber == numberOfMap)
                        endgame();
                    endRound();
                }
            }

            if (e.KeyValue == 27) //escape
            {
                pressEsc();
                //  Environment.Exit(0);
            }

        }
    }
}
