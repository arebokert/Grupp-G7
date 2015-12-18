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

        public GameRules()
        {
            Allowed = false;
        }

        private string changeTurn(int counter)
        {
            if (counter % 2 == 0)
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

            counter++;
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

        }
        private Boolean paint(string tempColor, List<PictureBox> tempList, List<PictureBox> boxes, PictureBox p)
        {
            Console.WriteLine((boxes.ElementAt(tileValue).Tag));
            if (tempList.Any() && !tempList.First().Tag.Equals(boxes.ElementAt(tileValue).Tag))
            {
                switch (changeTurn(counter))
                {
                    case "white":
                        foreach (PictureBox pb in tempList)
                        {
                            pb.Tag = "white";
                            pb.Image = white;


                        }
                        break;
                    case "black":
                        foreach (PictureBox pb in tempList)
                        {
                            pb.Tag = "black";
                            pb.Image = black;

                        }
                        break;
                }
                setPressedTileColor(p);
                temp.Clear();
                //tempList.Insert(0, p);
                //setPressedTileColor(p);
                return true;
            }
            return false;
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
            List<PictureBox> tempLeft = new List<PictureBox>();
            extractValues(p.Name.First(), p.Name.Last());
            while (p.Tag.Equals("green") && !boxes.ElementAt(tileValue).Tag.Equals(changeTurn(counter)) && p.Name.Last().Equals(boxes.ElementAt(tileValue).Name.Last()))
            {
                tileValue--;
                if (tileValue < 0 || boxes.ElementAt(tileValue).Tag.Equals("green") || boxes.ElementAt(tileValue).Tag.Equals(changeTurn(counter)))
                {
                    break;
                }
                tempLeft.Add(boxes.ElementAt(tileValue));
            }
            paint(tempColor, tempLeft, boxes, p);
        }

        private void checkRight(List<PictureBox> boxes, PictureBox p)
        {
            tempColor = changeTurn(counter);
            List<PictureBox> tempRight = new List<PictureBox>();
            extractValues(p.Name.First(), p.Name.Last());
            while (p.Tag.Equals("green") && !boxes.ElementAt(tileValue).Tag.Equals(changeTurn(counter)) && p.Name.Last().Equals(boxes.ElementAt(tileValue).Name.Last()))
            {

                tileValue++;
                if (tileValue >= 64 || boxes.ElementAt(tileValue).Tag.Equals("green") || boxes.ElementAt(tileValue).Tag.Equals(changeTurn(counter)))
                {
                    break;
                }
                tempRight.Add(boxes.ElementAt(tileValue));
            }
            paint(tempColor, tempRight, boxes, p);
        }

        private void checkUp(List<PictureBox> boxes, PictureBox p)
        {
            tempColor = changeTurn(counter);
            List<PictureBox> tempUp = new List<PictureBox>();
            extractValues(p.Name.First(), p.Name.Last());
            while (p.Tag.Equals("green") && !boxes.ElementAt(tileValue).Tag.Equals(changeTurn(counter)) && (p.Name.First().Equals(boxes.ElementAt(tileValue).Name.First())))
            {
                tileValue = tileValue - 8;
                if (tileValue < 8 || boxes.ElementAt(tileValue).Tag.Equals("green") || boxes.ElementAt(tileValue).Tag.Equals(changeTurn(counter)))
                {
                    break;
                }
                tempUp.Add(boxes.ElementAt(tileValue));
            }
            paint(tempColor, tempUp, boxes, p);
        }

        private void checkUpRight(List<PictureBox> boxes, PictureBox p)
        {
            tempColor = changeTurn(counter);
            List<PictureBox> tempUpRight = new List<PictureBox>();
            extractValues(p.Name.First(), p.Name.Last());
            while (p.Tag.Equals("green") && !boxes.ElementAt(tileValue).Tag.Equals(changeTurn(counter)))
            {
                tileValue = tileValue - 7;
                if (tileValue < 8 || boxes.ElementAt(tileValue).Tag.Equals("green") || boxes.ElementAt(tileValue).Tag.Equals(changeTurn(counter)))
                {
                    break;
                }
                tempUpRight.Add(boxes.ElementAt(tileValue));
            }
            paint(tempColor, tempUpRight, boxes, p);
        }

        private void checkUpLeft(List<PictureBox> boxes, PictureBox p)
        {
            tempColor = changeTurn(counter);
            List<PictureBox> tempUpLeft = new List<PictureBox>();
            extractValues(p.Name.First(), p.Name.Last());
            while (p.Tag.Equals("green") && !boxes.ElementAt(tileValue).Tag.Equals(changeTurn(counter)))
            {
                tileValue = tileValue - 9;
                if (tileValue < 8 || boxes.ElementAt(tileValue).Tag.Equals("green") || boxes.ElementAt(tileValue).Tag.Equals(changeTurn(counter)))
                {
                    break;
                }
                tempUpLeft.Add(boxes.ElementAt(tileValue));
            }
            paint(tempColor, tempUpLeft, boxes, p);
        }

        private void checkDown(List<PictureBox> boxes, PictureBox p)
        {
            tempColor = changeTurn(counter);
            List<PictureBox> tempDown = new List<PictureBox>();
            extractValues(p.Name.First(), p.Name.Last());
            while (p.Tag.Equals("green") && !boxes.ElementAt(tileValue).Tag.Equals(changeTurn(counter)) && (p.Name.First().Equals(boxes.ElementAt(tileValue).Name.First())))
            {
                tileValue = tileValue + 8;

                if (tileValue >= 64 || boxes.ElementAt(tileValue).Tag.Equals("green") || boxes.ElementAt(tileValue).Tag.Equals(changeTurn(counter)))
                {
                    break;
                }
                tempDown.Add(boxes.ElementAt(tileValue));
            }
            paint(tempColor, tempDown, boxes, p);
        }

        private void checkDownRight(List<PictureBox> boxes, PictureBox p)
        {
            tempColor = changeTurn(counter);
            List<PictureBox> tempDownRight = new List<PictureBox>();
            extractValues(p.Name.First(), p.Name.Last());
            while (p.Tag.Equals("green") && !boxes.ElementAt(tileValue).Tag.Equals(changeTurn(counter)))
            {
                tileValue = tileValue + 9;

                if (tileValue >= 64 || boxes.ElementAt(tileValue).Tag.Equals("green") || boxes.ElementAt(tileValue).Tag.Equals(changeTurn(counter)))
                {
                    break;
                }
                tempDownRight.Add(boxes.ElementAt(tileValue));
            }
            paint(tempColor, tempDownRight, boxes, p);
        }

        private void checkDownLeft(List<PictureBox> boxes, PictureBox p)
        {
            tempColor = changeTurn(counter);
            List<PictureBox> tempDownLeft = new List<PictureBox>();
            extractValues(p.Name.First(), p.Name.Last());
            while (p.Tag.Equals("green") && !boxes.ElementAt(tileValue).Tag.Equals(changeTurn(counter)))
            {
                tileValue = tileValue + 7;

                if (tileValue >= 64 || boxes.ElementAt(tileValue).Tag.Equals("green") || boxes.ElementAt(tileValue).Tag.Equals(changeTurn(counter)))
                {
                    break;
                }
                tempDownLeft.Add(boxes.ElementAt(tileValue));
            }
            paint(tempColor, tempDownLeft, boxes, p);
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

