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

        private Image green;
        private Image white;
        private Image black;

        private List<int> storedTiles = new List<int>();
        private string playerTurn;
        private string notPlayerTurn;
        private int counter = 1;

        Form boardForm = new Form();

        private Boolean setPressed = false;
        private int playerTurnInt = 0;
        private int[] t = new int[64];
        private List<PictureBox> temp = new List<PictureBox>();
        private List<PictureBox> board;

        public GameRules()
        {
            green = Image.FromFile(@"..\\..\\Resources\\Images\\NoMarker.png");
            white = Image.FromFile(@"..\\..\\Resources\\Images\\whiteMarker.png");
            black = Image.FromFile(@"..\\..\\Resources\\Images\\BlackMarker.png");
            Allowed = false;

        }

        public void initialLoad()
        {
            foreach (PictureBox pb in board)
            {
                if (pb.Name.Equals("d4") || pb.Name.Equals("e5"))
                {
                    pb.Image = white;
                    pb.Tag = "white";
                }
                else if (pb.Name.Contains("d5") || pb.Name.Equals("e4"))
                {
                    pb.Image = black;
                    pb.Tag = "black";
                }
                else
                {
                    pb.Image = green;
                    pb.Tag = "green";
                }

            }
        }

        private void changeTurn(int counter)
        {
            if (counter % 2 == 1)
            {
                playerTurn = "white";
                notPlayerTurn = "black";
                playerTurnInt = 1;
            }
            else
            {
                playerTurn = "black";
                notPlayerTurn = "white";
                playerTurnInt = 2;
            }
        }


        public void checkIfAllowed(PictureBox p)
        {
            extractValues(p.Name.First(), p.Name.Last());
            changeTurn(counter);
            checkLeft(p, tileValue);
            checkRight(p, tileValue);
            checkUp(p, tileValue);
            checkUpRight(p, tileValue);
            checkUpLeft(p, tileValue);
            checkDown(p, tileValue);
            checkDownRight(p, tileValue);
            checkDownLeft(p, tileValue);
            if (setPressed)
            {
                setPressedTileColor(p);
                counter++;
            }
            //temp.Clear();
        }
        private Boolean paint()
        {
            Console.WriteLine(temp.Any() + " LIST NOT EMPTY");
            // Console.WriteLine(!temp.First().Tag.Equals(boxes.ElementAt(tileValue).Tag));

            Console.WriteLine("1");
            switch (playerTurn)
            {

                case "white":
                    Console.WriteLine("2");
                    foreach (PictureBox pb in temp)
                    {
                        //    Console.WriteLine(pb.Name);
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
            setPressed = true;
            temp.Clear();
            return true;
        }

        private void setPressedTileColor(PictureBox pressed)
        {
            if (playerTurn.Equals("black"))
            {
                pressed.Image = black;
                pressed.Tag = "black";
            }
            else if (playerTurn.Equals("white"))
            {

                pressed.Image = white;
                pressed.Tag = "white";
            }
            setPressed = false;

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

        private void checkLeft(PictureBox p, int tileValue)
        {
            tileValue--;
            if (p.Tag.Equals("green"))
            {
                while (tileValue >= 0 && board.ElementAt(tileValue).Tag.Equals(notPlayerTurn) &&
                       board.ElementAt(tileValue).Name.Last().Equals(p.Name.Last()))
                {
                    temp.Add(board.ElementAt(tileValue));
                    tileValue--;
                }
            }

            if (temp.Any() && board.ElementAt(tileValue).Tag.Equals(playerTurn))
            {
                paint();
            }
            else
            {
                temp.Clear();
            }
        }

        private void checkRight(PictureBox p, int tileValue)
        {
            tileValue++;
            while (tileValue <= 64 && p.Tag.Equals("green") &&
                  board.ElementAt(tileValue).Tag.Equals(notPlayerTurn) &&
                  board.ElementAt(tileValue).Name.Last().Equals(p.Name.Last()))
            {

                temp.Add(board.ElementAt(tileValue));
                tileValue++;

            }

            if (temp.Any() && board.ElementAt(tileValue).Tag.Equals(playerTurn))
            {
                paint();
            }
            else
            {
                temp.Clear();
            }

        }

        private void checkUp(PictureBox p, int tileValue)
        {
            tileValue -= 8;
            if (p.Tag.Equals("green"))
            {
                while (tileValue >= 8 && p.Tag.Equals("green") && board.ElementAt(tileValue).Tag.Equals(notPlayerTurn))
                {

                    temp.Add(board.ElementAt(tileValue));
                    tileValue -= 8;
                }

                if (temp.Any() && board.ElementAt(tileValue).Tag.Equals(playerTurn))
                {
                    setPressed = true;
                    paint();
                }
                else
                {
                    temp.Clear();
                }
            }
        }


        private void checkUpRight(PictureBox p, int tileValue)
        {
            tileValue -= 7;
            if (p.Tag.Equals("green"))
            {
                while (tileValue >= 7 && p.Tag.Equals("green") && board.ElementAt(tileValue).Tag.Equals(notPlayerTurn))
                {

                    temp.Add(board.ElementAt(tileValue));
                    tileValue -= 7;
                }

                if (temp.Any() && board.ElementAt(tileValue).Tag.Equals(playerTurn))
                {
                    setPressed = true;
                    paint();
                }
                else
                {
                    temp.Clear();
                }
            }
        }


        private void checkUpLeft(PictureBox p, int tileValue)
        {
            tileValue -= 9;
            if (p.Tag.Equals("green"))
            {
                while (tileValue >= 9 && p.Tag.Equals("green") && board.ElementAt(tileValue).Tag.Equals(notPlayerTurn))
                {
                    temp.Add(board.ElementAt(tileValue));
                    tileValue -= 9;
                }
                if (temp.Any() && board.ElementAt(tileValue).Tag.Equals(playerTurn))
                {
                    setPressed = true;
                    paint();
                }
                else
                {
                    temp.Clear();
                }
            }
        }


        private void checkDown(PictureBox p, int tileValue)
        {
            tileValue += 8;
            if (p.Tag.Equals("green"))
            {
                while (tileValue <= 56 && p.Tag.Equals("green") &&
                  board.ElementAt(tileValue).Tag.Equals(notPlayerTurn))
                {

                    temp.Add(board.ElementAt(tileValue));
                    tileValue += 8;
                }
                if (temp.Any() && board.ElementAt(tileValue).Tag.Equals(playerTurn))
                {
                    setPressed = true;
                    paint();
                }
                else
                {
                    temp.Clear();
                }
            }
        }


        private void checkDownRight(PictureBox p, int tileValue)
        {
            tileValue += 9;
            if (p.Tag.Equals("green"))
            {
                while (tileValue <= 55 && board.ElementAt(tileValue).Tag.Equals(notPlayerTurn))
                {
                    temp.Add(board.ElementAt(tileValue));
                    tileValue += 9;
                }
                if (temp.Any() && board.ElementAt(tileValue).Tag.Equals(playerTurn))
                {
                    setPressed = true;
                    paint();
                }
                else
                {
                    temp.Clear();
                }
            }
        }


        private void checkDownLeft(PictureBox p, int tileValue)
        {
            tileValue += 7;
            if (p.Tag.Equals("green"))
            {
                while (tileValue <= 49 && board.ElementAt(tileValue).Tag.Equals(notPlayerTurn))
                {
                    temp.Add(board.ElementAt(tileValue));
                    tileValue += 7;
                }
                if (temp.Any() && board.ElementAt(tileValue).Tag.Equals(playerTurn))
                {
                    setPressed = true;
                    paint();
                }
                else
                {
                    temp.Clear();
                }
            }

        }
        /*   public void updateBoard(List<PictureBox> temp)
       {
           int i = 0;
           Console.WriteLine("HJE");
           foreach(PictureBox pb in boxes)
           {
               if (pb.Tag.Equals("green"))
               {
                   Board[i] = 0;
               }
               else if (pb.Tag.Equals("white")){
                   Board[i] = 1;
               }
               else if (pb.Tag.Equals("black")){
                   Board[i] = 2;
               }
               i++;
               Console.WriteLine(board[i]);
           }
       }
       */
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

        public List<PictureBox> Board
        {
            get
            {
                return board;
            }

            set
            {
                board = value;
            }
        }
    }
}

