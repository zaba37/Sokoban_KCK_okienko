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
using System.Windows.Input;
namespace Sokoban
{
    public partial class Game : Form
    {
        private List<List<int>> readNumbers;
        List<List<MapObject>> Map;
        private int posX;
        private int posY;
        List<PointPosition> PointsList;
        private int SetBoxes;
        private int widthElement;
        private int heightElement;
  
        public Game()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            this.BackgroundImage = Image.FromFile(@"Map\Floor.png");
            
            PointsList = null;
            SetBoxes = 0;
            posX = 0;
            posY = 0;
            widthElement = 64;
            heightElement = 64;
            
            
    
            initMap("sokoban_1.txt");
        }
        

        
        private List<List<int>> readFile(string path)
        {
            List<List<int>> intMap = null; ;
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
        //POSTAC: 5
        //PUDELKO:6
        //PODLOGA:3
        //SCIANA:2
        //PUNKT:4
        void initMap(string pathFileMap)
        {
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
                    help = map[heroPosition[0]-1][heroPosition[1]];
                    map[heroPosition[0] - 1][heroPosition[1]] = map[heroPosition[0]][heroPosition[1]];
                    map[heroPosition[0]][heroPosition[1]] = help;

                    map[heroPosition[0] - 1][heroPosition[1]].setPosition(map[heroPosition[0] - 1][heroPosition[1]].getX(), (map[heroPosition[0] - 1][heroPosition[1]].getY() - heightElement));
                    map[heroPosition[0]][heroPosition[1]].setPosition(map[heroPosition[0]][heroPosition[1]].getX(), (map[heroPosition[0]][heroPosition[1]].getY() + heightElement));
                    toReturn = map;
                }
                if (map[heroPosition[0] - 1][heroPosition[1]].GetType() == typeof(Box)) //gdy na gorze bedzie skrzynka
                {
                    if (map[heroPosition[0] - 2][heroPosition[1]].GetType() == typeof(Floor) || map[heroPosition[0] - 2][heroPosition[1]].GetType()==typeof(EndPoint)) //sprawdz czy mozna przesunac skrzynke(podloga lub punkt)
                    {


                        help = map[heroPosition[0]][heroPosition[1]];
                        map[heroPosition[0]][heroPosition[1]] = new Floor(heightElement, widthElement, heroPosition[1] * widthElement, heroPosition[0] * heightElement);
                        this.Controls.Add(map[heroPosition[0]][heroPosition[1]].picturebox);
                        help2= map[heroPosition[0] - 1][heroPosition[1]];// = 5;
                        map[heroPosition[0] - 1][heroPosition[1]] = help;
                        toRemove =map[heroPosition[0] - 2][heroPosition[1]];
                        
                        map[heroPosition[0] - 2][heroPosition[1]] = help2;
                        

                        map[heroPosition[0] - 1][heroPosition[1]].setPosition(map[heroPosition[0] - 1][heroPosition[1]].getX(), (map[heroPosition[0] - 1][heroPosition[1]].getY() - heightElement));
                        map[heroPosition[0] - 2][heroPosition[1]].setPosition(map[heroPosition[0] - 2][heroPosition[1]].getX(), (map[heroPosition[0] - 2][heroPosition[1]].getY() - heightElement));

                        this.Controls.Remove(toRemove.picturebox);
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

                    toRemove=(map[heroPosition[0] - 1][heroPosition[1]]);
                    map[heroPosition[0] - 1][heroPosition[1]] = help;
                    map[heroPosition[0] - 1][heroPosition[1]].setPosition(map[heroPosition[0] - 1][heroPosition[1]].getX(), (map[heroPosition[0] - 1][heroPosition[1]].getY() - heightElement));
                    this.Controls.Remove(toRemove.picturebox);
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
                        toRemove = (map[heroPosition[0]+2][heroPosition[1]]);
                        map[heroPosition[0] + 2][heroPosition[1]] = help2;


                        map[heroPosition[0] + 1][heroPosition[1]].setPosition(map[heroPosition[0] + 1][heroPosition[1]].getX(), (map[heroPosition[0] + 1][heroPosition[1]].getY() + heightElement));
                        map[heroPosition[0] + 2][heroPosition[1]].setPosition(map[heroPosition[0] + 2][heroPosition[1]].getX(), (map[heroPosition[0] + 2][heroPosition[1]].getY() + heightElement));

                        this.Controls.Remove(toRemove.picturebox);

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
                    
                    toReturn = map;
                }
            }








            if (left != 0)
            {

                if (map[heroPosition[0]][heroPosition[1]-1].GetType() == typeof(Wall)) //gdy na lewo bedzie sciana
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
                    map[heroPosition[0]][heroPosition[1] - 1].setPosition(map[heroPosition[0]][heroPosition[1] - 1].getX()-widthElement, (map[heroPosition[0]][heroPosition[1] - 1].getY()));
                    this.Controls.Remove(toRemove.picturebox);
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
                    toRemove=map[heroPosition[0]][heroPosition[1] + 1];
                    map[heroPosition[0]][heroPosition[1] + 1] = help;
                    map[heroPosition[0]][heroPosition[1] + 1].setPosition(map[heroPosition[0]][heroPosition[1] + 1].getX() + widthElement, (map[heroPosition[0]][heroPosition[1] + 1].getY()));
                    this.Controls.Remove(toRemove.picturebox);
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
            
            this.Close();
            
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
           if(e.KeyValue==38) //gora
           {
               int[] heroPosition = findHeroPosition(Map);
               Map = refreshMap(Map, 1, 0, 0, 0);
               SetBoxes = numberSetBoxes(Map, PointsList);
               if (CheckEndRound(SetBoxes, PointsList))
                   endRound();
           }
           if (e.KeyValue == 40) //dol
           {
               int[] heroPosition = findHeroPosition(Map);
               Map = refreshMap(Map, 0, 1, 0, 0);
               SetBoxes = numberSetBoxes(Map, PointsList);
               if (CheckEndRound(SetBoxes, PointsList))
                   endRound();
           }
           if (e.KeyValue == 39) //prawo
           {
               int[] heroPosition = findHeroPosition(Map);
               Map = refreshMap(Map, 0, 0, 0, 1);
               SetBoxes = numberSetBoxes(Map, PointsList);
               if (CheckEndRound(SetBoxes, PointsList))
                   endRound();
           }

           if (e.KeyValue == 37) //lewo
           {
               int[] heroPosition = findHeroPosition(Map);
               Map = refreshMap(Map, 0, 0, 1, 0);
               SetBoxes = numberSetBoxes(Map, PointsList);
               if (CheckEndRound(SetBoxes, PointsList))
                   endRound();
           }
        }


    }
}
