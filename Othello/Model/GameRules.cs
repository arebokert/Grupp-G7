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

            counter++;
            green = g;
            green.Tag = "green";
            black = b;
            black.Tag = "black";
            white = w;
            white.Tag = "white";
            // extractValues(p.Name.First(), p.Name.Last());
            checkLeft(boxes, p);
            checkRight(boxes, p);
            checkUp(boxes, p);
            checkUpRight(boxes, p);
            checkUpLeft(boxes, p);
            checkDown(boxes, p);
            checkDownRight(boxes, p);
            checkDownLeft(boxes, p);
            paint(tempColor, temp, boxes, p);
            //  paint();
 
        }
        private void paint(string tempColor, List<PictureBox> temp, List<PictureBox> boxes, PictureBox p)
        {
            if (temp.Any())
            {
                temp.Insert(0, p);
                if (!temp.First().Tag.Equals(boxes.ElementAt(tileValue).Tag))
                {
                    switch (changeTurn(counter))
                    {
                        case "white":
                            foreach (PictureBox pb in temp)
                            {
                                pb.Tag = "white";
                                pb.Image = white;
                              
                            }
                            break;
                        case "black":
                            foreach (PictureBox pb in temp)
                            {
                                pb.Tag = "black";
                                pb.Image = black;
                               
                            }
                            break;
                    }
                }
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
        private Boolean checkDiagonal(List<PictureBox> boxes, PictureBox p)
        {

            //(8*y)-(8-x)-10 LEFT UP
            //(8*y)-(8-x)-6 RIGHT UP
            //(8*y)-(8-x)+10 RIGHT DOWN
            //(8*y)-(8-x)+6 LEFT DOWN

            return true;
        }


        private void checkLeft(List<PictureBox> boxes, PictureBox p)
        {
            tempColor = changeTurn(counter);
            extractValues(p.Name.First(), p.Name.Last());
            Console.WriteLine(tileValue);
            List<PictureBox> temp = new List<PictureBox>();
            while (p.Tag.Equals("green") && !boxes.ElementAt(tileValue).Tag.Equals(changeTurn(counter)) && p.Name.Last().Equals(boxes.ElementAt(tileValue).Name.Last()))
            {
                tileValue--;
                if (tileValue < 0 || boxes.ElementAt(tileValue).Tag.Equals("green") || boxes.ElementAt(tileValue).Tag.Equals(changeTurn(counter)))
                {
                    break;
                }
                temp.Add(boxes.ElementAt(tileValue));
            }
            paint(tempColor, temp, boxes, p);
        }

        private void checkRight(List<PictureBox> boxes, PictureBox p)
        {
            string tempColor = changeTurn(counter);
            extractValues(p.Name.First(), p.Name.Last());
            List<PictureBox> temp = new List<PictureBox>();
            while (p.Tag.Equals("green") && !boxes.ElementAt(tileValue).Tag.Equals(changeTurn(counter)) && p.Name.Last().Equals(boxes.ElementAt(tileValue).Name.Last()))
            {
                tileValue++;
                if (tileValue >= 64 || boxes.ElementAt(tileValue).Tag.Equals("green") || boxes.ElementAt(tileValue).Tag.Equals(changeTurn(counter)))
                {
                    break;
                }
                temp.Add(boxes.ElementAt(tileValue));
            }
            paint(tempColor, temp, boxes, p);

        }
        private void checkUp(List<PictureBox> boxes, PictureBox p)
        {
            string tempColor = changeTurn(counter);
            extractValues(p.Name.First(), p.Name.Last());
            List<PictureBox> temp = new List<PictureBox>();
            while (p.Tag.Equals("green") && !boxes.ElementAt(tileValue).Tag.Equals(changeTurn(counter)) && (p.Name.First().Equals(boxes.ElementAt(tileValue).Name.First())))
            {
                tileValue = tileValue - 8;
                if (tileValue < 8 || boxes.ElementAt(tileValue).Tag.Equals("green") ||  boxes.ElementAt(tileValue).Tag.Equals(changeTurn(counter)))
                {
                    break;
                }
                temp.Add(boxes.ElementAt(tileValue));
            }
           paint(tempColor, temp, boxes, p);
        }


        private void checkUpRight(List<PictureBox> boxes, PictureBox p)
        {
            string tempColor = changeTurn(counter);
            extractValues(p.Name.First(), p.Name.Last());
            List<PictureBox> temp = new List<PictureBox>();
            while (p.Tag.Equals("green") && !boxes.ElementAt(tileValue).Tag.Equals(changeTurn(counter)))
            {
                tileValue = tileValue - 7;
                if (tileValue < 8 || boxes.ElementAt(tileValue).Tag.Equals("green") || boxes.ElementAt(tileValue).Tag.Equals(changeTurn(counter)))
                {
                    break;
                }
                temp.Add(boxes.ElementAt(tileValue));
            }
           paint(tempColor, temp, boxes, p);
        }
        private void checkUpLeft(List<PictureBox> boxes, PictureBox p)
        {
            string tempColor = changeTurn(counter);
            extractValues(p.Name.First(), p.Name.Last());
            List<PictureBox> temp = new List<PictureBox>();
            while (p.Tag.Equals("green") && !boxes.ElementAt(tileValue).Tag.Equals(changeTurn(counter)))
            {
                tileValue = tileValue - 9;
                if (tileValue < 8 || boxes.ElementAt(tileValue).Tag.Equals("green") || boxes.ElementAt(tileValue).Tag.Equals(changeTurn(counter)))
                {
                    break;
                }
                temp.Add(boxes.ElementAt(tileValue));
            }
            paint(tempColor, temp, boxes, p);
        }



        private void checkDown(List<PictureBox> boxes, PictureBox p)
        {
            string tempColor = changeTurn(counter);
            extractValues(p.Name.First(), p.Name.Last());
            List<PictureBox> temp = new List<PictureBox>();
            while (p.Tag.Equals("green") && !boxes.ElementAt(tileValue).Tag.Equals(changeTurn(counter)) && (p.Name.First().Equals(boxes.ElementAt(tileValue).Name.First())))
            {
                tileValue = tileValue + 8;

                if (tileValue >= 64 || boxes.ElementAt(tileValue).Tag.Equals("green") || boxes.ElementAt(tileValue).Tag.Equals(changeTurn(counter)))
                {
                    break;
                }
                temp.Add(boxes.ElementAt(tileValue));
            }
           paint(tempColor, temp, boxes, p);
        }
        private void checkDownRight(List<PictureBox> boxes, PictureBox p)
        {
            string tempColor = changeTurn(counter);
            extractValues(p.Name.First(), p.Name.Last());
            List<PictureBox> temp = new List<PictureBox>();
            while (p.Tag.Equals("green") && !boxes.ElementAt(tileValue).Tag.Equals(changeTurn(counter)))
            {
                tileValue = tileValue + 9;

                if (tileValue >= 64 || boxes.ElementAt(tileValue).Tag.Equals("green") || boxes.ElementAt(tileValue).Tag.Equals(changeTurn(counter)))
                {
                    break;
                }
                temp.Add(boxes.ElementAt(tileValue));
            }
           paint(tempColor, temp, boxes, p);
        }

        private void checkDownLeft(List<PictureBox> boxes, PictureBox p)
        {
            string tempColor = changeTurn(counter);
            extractValues(p.Name.First(), p.Name.Last());
            List<PictureBox> temp = new List<PictureBox>();
            while (p.Tag.Equals("green") && !boxes.ElementAt(tileValue).Tag.Equals(changeTurn(counter)))
            {
                tileValue = tileValue + 7;

                if (tileValue >= 64 || boxes.ElementAt(tileValue).Tag.Equals("green") || boxes.ElementAt(tileValue).Tag.Equals(changeTurn(counter)))
                {
                    break;
                }
                temp.Add(boxes.ElementAt(tileValue));
            }
            paint(tempColor, temp, boxes, p);
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

