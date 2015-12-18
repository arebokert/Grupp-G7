using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Othello.Model
{
    public class GameRules
    {
        private Boolean allowed;
        private int letterValue;
        private int tileMultiple;
        private int tileValue;
        private int nextTileValue;
        private List<PictureBox> temp = new List<PictureBox>();
        private List<int> storedTiles = new List<int>();
        private string tempColor = "";
        private string playerTurn;
        private int counter = 1;
        private Image green;
        private Image white;
        private Image black;
        private Boolean setPressed = false;


        public GameRules()
        {
            Allowed = false;
        }

        private string changeTurn(int counter)
        {
            if (counter % 2 == 1)
            {
                playerTurn = "white";

            }
            else
            {
                playerTurn = "black";

            }
            return playerTurn;
        }


        public void checkIfAllowed(List<PictureBox> boxes, PictureBox p, Image g, Image w, Image b)
        {

           
            green = g;
            green.Tag = "green";
            black = b;
            black.Tag = "black";
            white = w;
            white.Tag = "white";

            checkLeft(boxes, p);
            checkRight(boxes, p);
            checkUp(boxes, p);
            checkUpRight(boxes, p);
            checkUpLeft(boxes, p);
            checkDown(boxes, p);
            checkDownRight(boxes, p);
            checkDownLeft(boxes, p);
            if (setPressed)
            {
                setPressedTileColor(p);
                counter++;
            }
             
          
            //temp.Clear();
        }
        private Boolean paint(List<PictureBox> temp, List<PictureBox> boxes, PictureBox p)
        {
            Console.WriteLine(temp.Any() + " LIST NOT EMPTY");
            // Console.WriteLine(!temp.First().Tag.Equals(boxes.ElementAt(tileValue).Tag));

            Console.WriteLine("1");
            switch (changeTurn(counter))
            {

                case "white":
                    Console.WriteLine("2");
                    foreach (PictureBox pb in temp)
                    {
                        pb.Tag = "white";
                        pb.Image = white;


                    }
                    break;
                case "black":
                    Console.WriteLine("3");
                    foreach (PictureBox pb in temp)
                    {
                        pb.Tag = "black";
                        pb.Image = black;

                    }
                    break;
            }
            // setPressedTileColor(p);
            //   this.temp.Clear();
            //tempList.Insert(0, p);

            return true;
        }




        private void setPressedTileColor(PictureBox pressed)
        {
            if (changeTurn(counter).Equals("black"))
            {
                pressed.Image = black;
                pressed.Tag = "black";
            }
            else if (changeTurn(counter).Equals("white"))
            {

                pressed.Image = white;
                pressed.Tag = "white";
            }
            setPressed = false;
            temp.Clear();
        }
        private void changeTag()
        {
            for (int i = 0; i < temp.Count(); i++)
            {
                temp.ElementAt(i).Tag = tempColor;
            }
        }

        private void extractValues(char f, char l)
        {
            tileMultiple = (int)Char.GetNumericValue(l);
            switch (f)
            {
                case 'a':
                    letterValue = 1;
                    break;
                case 'b':
                    letterValue = 2;
                    break;
                case 'c':
                    letterValue = 3;
                    break;
                case 'd':
                    letterValue = 4;
                    break;
                case 'e':
                    letterValue = 5;
                    break;
                case 'f':
                    letterValue = 6;
                    break;
                case 'g':
                    letterValue = 7;
                    break;
                case 'h':
                    letterValue = 8;
                    break;
            }

            tileValue = (8 * tileMultiple) - (8 - letterValue) - 1;
            nextTileValue = tileValue;
        }

        private void checkLeft(List<PictureBox> boxes, PictureBox p)
        {
            tempColor = changeTurn(counter);
            List<PictureBox> temp = new List<PictureBox>();
            extractValues(p.Name.First(), p.Name.Last());
            while (p.Tag.Equals("green") && !boxes.ElementAt(tileValue).Tag.Equals(changeTurn(counter)) && p.Name.Last().Equals(boxes.ElementAt(tileValue).Name.Last()))
            {
                tileValue--;
                if (tileValue < 0 || boxes.ElementAt(tileValue).Tag.Equals("green") || boxes.ElementAt(tileValue).Tag.Equals(changeTurn(counter)))
                {
                    break;
                }
                temp.Add(boxes.ElementAt(tileValue));
                Console.WriteLine(boxes.ElementAt(tileValue).Name);
                Console.WriteLine(boxes.ElementAt(tileValue).Tag);

            }

            if (temp.Any() && boxes.ElementAt(tileValue).Tag.Equals(changeTurn(counter)))
            {
                setPressed = true;
                paint(temp, boxes, p);
            }
        }

        private void checkRight(List<PictureBox> boxes, PictureBox p)
        {
            tempColor = changeTurn(counter);
            List<PictureBox> temp = new List<PictureBox>();
            extractValues(p.Name.First(), p.Name.Last());
            while (tileValue < 63 &&p.Tag.Equals("green") && !boxes.ElementAt(tileValue + 1).Tag.Equals(changeTurn(counter)))
            {

                tileValue++;
                if (tileValue >= 64 || boxes.ElementAt(tileValue).Tag.Equals("green") || boxes.ElementAt(tileValue).Tag.Equals(changeTurn(counter)))
                {
                    break;
                }
                temp.Add(boxes.ElementAt(tileValue));
            }
          
            if (tileValue + 1 <= 64 && temp.Any() && boxes.ElementAt(tileValue + 1).Tag.Equals(changeTurn(counter)))
            {
                setPressed = true;
                paint(temp, boxes, p);
            }

        }

        private void checkUp(List<PictureBox> boxes, PictureBox p)
        {
            tempColor = changeTurn(counter);
            List<PictureBox> temp = new List<PictureBox>();
            extractValues(p.Name.First(), p.Name.Last());
            while (tileValue >= 8 && p.Tag.Equals("green") && !boxes.ElementAt(tileValue - 8).Tag.Equals(changeTurn(counter)) && (p.Name.First().Equals(boxes.ElementAt(tileValue).Name.First())))
            {
                tileValue = tileValue - 8;
                if (tileValue <= 8 || boxes.ElementAt(tileValue).Tag.Equals("green") || boxes.ElementAt(tileValue).Tag.Equals(changeTurn(counter)))
                {
                    break;
                }
                temp.Add(boxes.ElementAt(tileValue));
            }
          
            if (tileValue - 8 >= 0 && (temp.Any() && boxes.ElementAt(tileValue - 8).Tag.Equals(changeTurn(counter))))
            {
                setPressed = true;
                paint(temp, boxes, p);
            }
        }

        private void checkUpRight(List<PictureBox> boxes, PictureBox p)
        {
            tempColor = changeTurn(counter);
            List<PictureBox> temp = new List<PictureBox>();
            extractValues(p.Name.First(), p.Name.Last());
            while (tileValue >= 7 && p.Tag.Equals("green") && !boxes.ElementAt(tileValue - 7).Tag.Equals(changeTurn(counter)))
            {
                tileValue = tileValue - 7;
                if (tileValue < 8 || boxes.ElementAt(tileValue).Tag.Equals("green") || boxes.ElementAt(tileValue).Tag.Equals(changeTurn(counter)))
                {
                    break;
                }
                temp.Add(boxes.ElementAt(tileValue));
            }


            if (tileValue - 7 >= 0 && temp.Any() && boxes.ElementAt(tileValue - 7).Tag.Equals(changeTurn(counter)))
            {
                setPressed = true;
                paint(temp, boxes, p);
            }
        }

        private void checkUpLeft(List<PictureBox> boxes, PictureBox p)
        {
            tempColor = changeTurn(counter);
            //  List<PictureBox> temp = new List<PictureBox>();
            extractValues(p.Name.First(), p.Name.Last());
            while (tileValue >= 9&&p.Tag.Equals("green") && !boxes.ElementAt(tileValue - 9).Tag.Equals(changeTurn(counter)))
            {
                tileValue = tileValue - 9;
                if (tileValue < 8 || boxes.ElementAt(tileValue).Tag.Equals("green") || boxes.ElementAt(tileValue).Tag.Equals(changeTurn(counter)))
                {
                    break;
                }
                temp.Add(boxes.ElementAt(tileValue));
            }
   
            if (tileValue -9  >= 0 && temp.Any() && boxes.ElementAt(tileValue - 9).Tag.Equals(changeTurn(counter)))
            {
                setPressed = true;
                paint(temp, boxes, p);
            }
        }

        private void checkDown(List<PictureBox> boxes, PictureBox p)
        {
            tempColor = changeTurn(counter);
            List<PictureBox> temp = new List<PictureBox>();
            extractValues(p.Name.First(), p.Name.Last());
            while (tileValue < 56&&p.Tag.Equals("green") && !boxes.ElementAt(tileValue + 8).Tag.Equals(changeTurn(counter)) && (p.Name.First().Equals(boxes.ElementAt(tileValue).Name.First())))
            {
                tileValue = tileValue + 8;

                if (tileValue >= 56 || boxes.ElementAt(tileValue).Tag.Equals("green") || boxes.ElementAt(tileValue).Tag.Equals(changeTurn(counter)))
                {
                    break;
                }
                temp.Add(boxes.ElementAt(tileValue));
            }
            if (tileValue + 8 <= 64&&temp.Any() && boxes.ElementAt(tileValue + 8).Tag.Equals(changeTurn(counter)))
            {
                setPressed = true;
                paint(temp, boxes, p);
            }
        }

        private void checkDownRight(List<PictureBox> boxes, PictureBox p)
        {
            tempColor = changeTurn(counter);
             List<PictureBox> temp = new List<PictureBox>();
            extractValues(p.Name.First(), p.Name.Last());
            while (tileValue < 55 && p.Tag.Equals("green") && !boxes.ElementAt(tileValue + 9).Tag.Equals(changeTurn(counter)))
            {
                tileValue = tileValue + 9;

                if (tileValue >= 64 || boxes.ElementAt(tileValue).Tag.Equals("green") || boxes.ElementAt(tileValue).Tag.Equals(changeTurn(counter)))
                {
                    break;
                }
                temp.Add(boxes.ElementAt(tileValue));
            }
            if (tileValue + 9 <= 64 && temp.Any() && boxes.ElementAt(tileValue + 9).Tag.Equals(changeTurn(counter)))
            {
                setPressed = true;
                paint(temp, boxes, p);
            }
           
        }

        private void checkDownLeft(List<PictureBox> boxes, PictureBox p)
        {
            tempColor = changeTurn(counter);
            // List<PictureBox> temp = new List<PictureBox>();
            extractValues(p.Name.First(), p.Name.Last());
            while (tileValue < 57 && p.Tag.Equals("green") && !boxes.ElementAt(tileValue + 7).Tag.Equals(changeTurn(counter)))
            {
                tileValue = tileValue + 7;

                if (tileValue >= 57 || boxes.ElementAt(tileValue).Tag.Equals("green") || boxes.ElementAt(tileValue).Tag.Equals(changeTurn(counter)))
                {
                    break;
                }
                temp.Add(boxes.ElementAt(tileValue));
            }
            if (tileValue+7 <= 64 &&temp.Any() && boxes.ElementAt(tileValue + 7).Tag.Equals(changeTurn(counter)))
            {
                setPressed = true;
                paint(temp, boxes, p);
            }
        }

        public bool Allowed
        {
            get
            {
                return allowed;
            }

            set
            {
                allowed = value;
            }
        }

    }
}

