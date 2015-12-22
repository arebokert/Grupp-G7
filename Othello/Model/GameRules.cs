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
        private int tileValueX;
        private int tileValueY;

        private Image green;
        private Image white;
        private Image black;

        private string playerTurn;
        private string notPlayerTurn;
        private int counter = 1;

        private int playerTurnInt;
        private int notPlayerTurnInt = 0;
        private int[,] boardArray;
        private int[,] paintArray = new int[8, 8];
        private int[,] paintArrayTemp;
        private List<int> test = new List<int>();
        private List<int[,]> paintList = new List<int[,]>();
        private int[][] paintA = new int[8][];
        private Boolean move;

        private PictureBox[,] board = new PictureBox[8, 8];
        private Boolean aiIsCalculating = false;
        private AI ai;

        public GameRules(AI a)
        {
            ai = a;
            boardArray = new int[8, 8];
            paintArrayTemp = new int[8, 8];
            green = Image.FromFile(@"..\\..\\Resources\\Images\\NoMarker.png");
            white = Image.FromFile(@"..\\..\\Resources\\Images\\whiteMarker.png");
            black = Image.FromFile(@"..\\..\\Resources\\Images\\BlackMarker.png");
        }


        public void makeMove(PictureBox p)
        {
            //resetPaintArray();
            changeTurn(counter);

            if (!p.Name.Equals(""))
            {
                extractValues(p.Name.First(), p.Name.Last());
                checkAllDirections();
                paint();
                paintList.Clear();
                resetPaintArray(paintArray);
                updateBoardArray();
                counter++;
                move = false;
            }

        }
        public void checkAllDirections()
        {

            checkLeft(tileValueX, tileValueY);
            checkRight(tileValueX, tileValueY);

              checkUp(tileValueX, tileValueY);
               checkDown(tileValueX, tileValueY);
               checkUpRight(tileValueX, tileValueY);
                checkUpLeft(tileValueX, tileValueY);
                checkDownRight(tileValueX, tileValueY);
                checkDownLeft(tileValueX, tileValueY);
        }


        private Boolean paint()
        {
            switch (playerTurn)
            {
                case "white":
                    for (int i = 0; i < paintList.Count; i++)
                    {
                        for (int x = 0; x < 8; x++)
                        {
                            for (int y = 0; y < 8; y++)
                            {
                                if (paintArray[x, y] == 1)
                                {
                                    Console.WriteLine(Board[x, y].Name);
                                    Console.WriteLine(x);
                                    Console.WriteLine(y);
                                    move = true;
                                    Board[x, y].Tag = "white";
                                    Board[x, y].Image = white;
                                }
                            }
                        }
                    }
                    if (move)
                    {
                        Board[tileValueX, tileValueY].Tag = "white";
                        Board[tileValueX, tileValueY].Image = white;
                    }
                    break;

                case "black":
                    for (int i = 0; i < paintList.Count; i++)
                    {
                        for (int x = 0; x < 8; x++)
                        {
                            for (int y = 0; y < 8; y++)
                            {
                                if (paintArray[x, y] == 1)
                                {
                                    move = true;
                                    Board[x, y].Tag = "black";
                                    Board[x, y].Image = black;
                                }
                            }
                        }

                    }
                    if (move)
                    {
                        Board[tileValueX, tileValueY].Tag = "black";
                        Board[tileValueX, tileValueY].Image = black;
                        move = false;
                    }

                    break;
            }

            return true;
        }

        private void resetPaintArray(int[,] paintArrayTemp)
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    paintArrayTemp[x, y] = 0;
                }
            }
        }
        public void initialLoad()
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (Board[x, y].Name.Equals("d4") || board[x, y].Name.Equals("e5"))
                    {
                        Board[x, y].Image = white;
                        Board[x, y].Tag = "white";
                    }
                    else if (board[x, y].Name.Equals("d5") || board[x, y].Name.Equals("e4") || board[x, y].Name.Equals("f4"))
                    {
                        Board[x, y].Image = black;
                        Board[x, y].Tag = "black";
                    }
                    else
                    {
                        Board[x, y].Image = green;
                        Board[x, y].Tag = "green";
                    }
                }
            }
            updateBoardArray();
        }

        private void changeTurn(int counter)
        {
            if (counter % 2 == 1)
            {
                playerTurn = "white";
                notPlayerTurn = "black";
                playerTurnInt = 1;
                notPlayerTurnInt = 2;
            }
            else
            {
                playerTurn = "black";
                notPlayerTurn = "white";
                playerTurnInt = 2;
                notPlayerTurnInt = 1;
            }
        }

        private void extractValues(char f, char l)
        {
            tileValueX = (int)Char.GetNumericValue(l);
            tileValueX--;
            switch (f)
            {
                case 'a':
                    tileValueY = 0;
                    break;
                case 'b':
                    tileValueY = 1;
                    break;
                case 'c':
                    tileValueY = 2;
                    break;
                case 'd':
                    tileValueY = 3;
                    break;
                case 'e':
                    tileValueY = 4;
                    break;
                case 'f':
                    tileValueY = 5;
                    break;
                case 'g':
                    tileValueY = 6;
                    break;
                case 'h':
                    tileValueY = 7;
                    break;
            }
        }

        private void copyTempArrayToPaintArray(int xValue, int yValue)
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (paintArrayTemp[x, y] == 1)
                    {
                        paintArray[x, y] = 1;
                    }
                }
            }
            paintList.Add(paintArray);
            resetPaintArray(paintArrayTemp);
        }

        private Boolean legalMove()
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (paintArrayTemp[x, y] == 2)
                    {
                        return true;
                    }
                }
            }
            return false; ;
        }

        private void checkLeft(int xValue, int yValue)
        {
            int c = 0;
            if (boardArray[xValue, yValue] == 0)
            {
                yValue--;
                {
                    while (yValue > 0 && boardArray[xValue, yValue] == notPlayerTurnInt)
                    {
                        paintArrayTemp[xValue, yValue] = 1;
                        if (boardArray[xValue, yValue - 1] == playerTurnInt)
                        {
                            c++;
                            if (c == 2)
                            {
                                break;
                            }
                            else
                            {
                                paintArrayTemp[xValue, yValue - 1] = 2;
                            }
                        }
                        yValue--;
                    }
                }
                if (legalMove())
                {
                    copyTempArrayToPaintArray(xValue, yValue);
                }
                else
                {
                    resetPaintArray(paintArrayTemp);
                }
            }
        }

        private void checkRight(int xValue, int yValue)
        {
            int c = 0;
            if (boardArray[xValue, yValue] == 0)
            {
                yValue++;
                {
                    while (yValue < 7 && boardArray[xValue, yValue] == notPlayerTurnInt)
                    {
                        paintArrayTemp[xValue, yValue] = 1;
                        test.Add(xValue);
                        test.Add(yValue);
                        if (boardArray[xValue, yValue + 1] == playerTurnInt)
                        {
                            c++;
                            if (c == 2)
                            {
                                break;
                            }
                            else
                            {
                                paintArrayTemp[xValue, yValue + 1] = 2;
                            }
                        }
                        yValue++;
                    }
                }
                if (legalMove())
                {
                    copyTempArrayToPaintArray(xValue, yValue);
                }
                else
                {
                    resetPaintArray(paintArrayTemp);
                }
            }
        }



        private void checkUp(int xValue, int yValue)
        {
            int c = 0;
            if (boardArray[xValue, yValue] == 0)
            {
                xValue--;
                {
                    while (xValue > 0 && boardArray[xValue, yValue] == notPlayerTurnInt)
                    {
                        paintArrayTemp[xValue, yValue] = 1;
                        if (boardArray[xValue - 1, yValue] == playerTurnInt)
                        {
                            c++;
                            if (c == 2)
                            {
                                break;
                            }
                            else
                            {
                                paintArrayTemp[xValue - 1, yValue] = 2;
                            }
                        }
                        xValue--;
                    }
                }
                if (legalMove())
                {
                    copyTempArrayToPaintArray(xValue, yValue);
                }
                else
                {
                    resetPaintArray(paintArrayTemp);
                }
            }
        }


        private void checkDown(int xValue, int yValue)
        {
            int c = 0;
            if (boardArray[xValue, yValue] == 0)
            {
                xValue++;
                {
                    while (xValue < 7 && boardArray[xValue, yValue] == notPlayerTurnInt)
                    {
                        paintArrayTemp[xValue, yValue] = 1;
                        if (boardArray[xValue + 1, yValue] == playerTurnInt)
                        {
                            c++;
                            if (c == 2)
                            {
                                break;
                            }
                            else
                            {
                                paintArrayTemp[xValue + 1, yValue] = 2;
                            }
                        }
                        xValue++;
                    }
                }
                if (legalMove())
                {
                    copyTempArrayToPaintArray(xValue, yValue);
                }
                else
                {
                    resetPaintArray(paintArrayTemp);
                }
            }
        }

        private void checkUpRight(int xValue, int yValue)
        {
            int c = 0;
            if (boardArray[xValue, yValue] == 0)
            {
                yValue++;
                xValue--;
                {
                    while (xValue > 0 && yValue < 7 && boardArray[xValue, yValue] == notPlayerTurnInt)
                    {
                        paintArrayTemp[xValue, yValue] = 1;
                        if (boardArray[xValue - 1, yValue + 1] == playerTurnInt)
                        {
                            c++;
                            if (c == 2)
                            {
                                break;
                            }
                            else
                            {
                                paintArrayTemp[xValue - 1, yValue + 1] = 2;
                            }
                        }
                        yValue++;
                        xValue--;
                    }
                }
                if (legalMove())
                {
                    copyTempArrayToPaintArray(xValue, yValue);
                }
                else
                {
                    resetPaintArray(paintArrayTemp);
                }
            }
        }

        private void checkUpLeft(int xValue, int yValue)
        {
            int c = 0;
            if (boardArray[xValue, yValue] == 0)
            {
                yValue--;
                xValue--;
                {
                    while (xValue > 0 && yValue > 0 && boardArray[xValue, yValue] == notPlayerTurnInt)
                    {
                        paintArrayTemp[xValue, yValue] = 1;
                        if (boardArray[xValue - 1, yValue - 1] == playerTurnInt)
                        {
                            c++;
                            if (c == 2)
                            {
                                break;
                            }
                            else
                            {
                                paintArrayTemp[xValue - 1, yValue - 1] = 2;
                            }
                        }
                        yValue--;
                        xValue--;
                    }
                }
                if (legalMove())
                {
                    copyTempArrayToPaintArray(xValue, yValue);
                }
                else
                {
                    resetPaintArray(paintArrayTemp);
                }
            }
        }

        private void checkDownRight(int xValue, int yValue)
        {
            int c = 0;
            if (boardArray[xValue, yValue] == 0)
            {
                yValue++;
                xValue++;
                {
                    while (xValue < 7 && yValue < 7 && boardArray[xValue, yValue] == notPlayerTurnInt)
                    {
                        paintArrayTemp[xValue, yValue] = 1;
                        if (boardArray[xValue + 1, yValue + 1] == playerTurnInt)
                        {
                            c++;
                            if (c == 2)
                            {
                                break;
                            }
                            else
                            {
                                paintArrayTemp[xValue + 1, yValue + 1] = 2;
                            }
                        }
                        yValue++;
                        xValue++;
                    }
                }
                if (legalMove())
                {
                    copyTempArrayToPaintArray(xValue, yValue);
                }
                else
                {
                    resetPaintArray(paintArrayTemp);
                }
            }
        }

        private void checkDownLeft(int xValue, int yValue)
        {
            int c = 0;
            if (boardArray[xValue, yValue] == 0)
            {
                yValue--;
                xValue++;
                {
                    while (xValue < 7 && yValue > 0 && boardArray[xValue, yValue] == notPlayerTurnInt)
                    {
                        paintArrayTemp[xValue, yValue] = 1;
                        if (boardArray[xValue + 1, yValue - 1] == playerTurnInt)
                        {
                            c++;
                            if (c == 2)
                            {
                                break;
                            }
                            else
                            {
                                paintArrayTemp[xValue + 1, yValue - 1] = 2;
                            }
                        }
                        yValue--;
                        xValue++;
                    }
                }
                if (legalMove())
                {
                    copyTempArrayToPaintArray(xValue, yValue);
                }
                else
                {
                    resetPaintArray(paintArrayTemp);
                }
            }
        }
        private void updateBoardArray()
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (Board[x, y].Tag.Equals("green"))
                    {
                        boardArray[x, y] = 0;
                    }
                    else if (Board[x, y].Tag.Equals("white"))
                    {
                        boardArray[x, y] = 1;
                    }
                    else if (Board[x, y].Tag.Equals("black"))
                    {
                        boardArray[x, y] = 2;
                    }
                    Console.Write(boardArray[x, y]);
                }
                Console.WriteLine();
            }
        }

        public PictureBox[,] Board
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

        public bool AiIsCalculating
        {
            get
            {
                return aiIsCalculating;
            }

            set
            {
                aiIsCalculating = value;
            }
        }
    }
}

