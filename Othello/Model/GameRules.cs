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
        private int counter = 0;
        private int moveScore;

        private int legalMoveCounter = 0;

        private int playerTurnInt = 1;
        private int notPlayerTurnInt = 2;

        private int blackMarker = 2;
        private int whiteMarker = 1;
        private int greenMarker = 0;

        private int[,] boardArray;
        private int[,] paintArray = new int[8, 8];
        private int[,] paintArrayTemp;
        private List<int[,]> paintList = new List<int[,]>();
        private Boolean move;
        private PictureBox[,] board = new PictureBox[8, 8];

        public GameRules()
        {
            BoardArray = new int[8, 8];
            PaintArrayTemp = new int[8, 8];
            green = Image.FromFile(@"..\\..\\Resources\\Images\\NoMarker.png");
            white = Image.FromFile(@"..\\..\\Resources\\Images\\whiteMarker.png");
            black = Image.FromFile(@"..\\..\\Resources\\Images\\BlackMarker.png");
        }

        public void makeMove(PictureBox p)
        {
            if (!p.Name.Equals(""))
            {
                extractValues(p.Name.First(), p.Name.Last());
                checkAllDirections(tileValueX, tileValueY);
                paint(playerTurn);
                paintList.Clear();
                resetPaintArray(PaintArray);
                updateBoardArray();
                move = false;
                changeTurn(Counter);
                if (checkIfGameOver())
                {
                    Console.WriteLine("GameOver");
                }
            }
        }
        public Boolean checkAllDirections(int tileValueX, int tileValueY)
        {
            legalMoveCounter = 0;
            changeTurn(Counter);
            MoveScore = 0;
            checkLeft(tileValueX, tileValueY);
            checkRight(tileValueX, tileValueY);
            checkUp(tileValueX, tileValueY);
            checkDown(tileValueX, tileValueY);
            checkUpRight(tileValueX, tileValueY);
            checkUpLeft(tileValueX, tileValueY);
            checkDownRight(tileValueX, tileValueY);
            checkDownLeft(tileValueX, tileValueY);
            countScore();
            if (legalMoveCounter > 0)
            {
                return true;
            }

            return false;



        }

        private void countScore()
        {
            MoveScore = 0;
            for (int i = 0; i < paintList.Count; i++)
            {
                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        if (PaintArray[x, y] == 1)
                        {
                            MoveScore++;
                        }
                    }
                }
            }
        }

        private Boolean paint(String player)
        {
            Image tempImage;
            if (player == "white")
            {
                tempImage = white;

            }
            else
            {
                tempImage = black;
            }
            for (int i = 0; i < paintList.Count; i++)
            {
                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        if (PaintArray[x, y] == 1)
                        {
                            Console.WriteLine(Board[x, y].Name);
                            Console.WriteLine(x);
                            Console.WriteLine(y);
                            move = true;
                            Board[x, y].Tag = player;
                            Board[x, y].Image = tempImage;

                        }
                    }
                }
            }
            if (move)
            {
                Counter++;
                Board[tileValueX, tileValueY].Tag = player;
                Board[tileValueX, tileValueY].Image = tempImage;
            }

            return true;
        }

        public void resetPaintArray(int[,] paintArrayTemp)
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
                    else if (board[x, y].Name.Equals("d5") || board[x, y].Name.Equals("e4"))
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
            if (counter % 2 == 0)
            {
                playerTurn = "white";
                //   notPlayerTurn = "black";
                PlayerTurnInt = WhiteMarker;
                notPlayerTurnInt = blackMarker;
            }
            else
            {
                playerTurn = "black";
                //   notPlayerTurn = "white";
                PlayerTurnInt = blackMarker;
                notPlayerTurnInt = WhiteMarker;
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
                    if (PaintArrayTemp[x, y] == 1)
                    {
                        PaintArray[x, y] = 1;
                    }
                }
            }
            paintList.Add(PaintArray);
            resetPaintArray(paintArrayTemp);
        }

        private Boolean legalMove()
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (PaintArrayTemp[x, y] == 2)
                    {
                        legalMoveCounter++;
                        return true;
                    }
                }
            }
            return false;
        }

        private void checkLeft(int xValue, int yValue)
        {
            int c = 0;
            if (BoardArray[xValue, yValue] == 0)
            {
                yValue--;
                {
                    while (yValue > 0 && BoardArray[xValue, yValue] == notPlayerTurnInt)
                    {
                        PaintArrayTemp[xValue, yValue] = 1;
                        if (BoardArray[xValue, yValue - 1] == PlayerTurnInt)
                        {
                            c++;
                            if (c == 2)
                            {
                                break;
                            }
                            else
                            {
                                PaintArrayTemp[xValue, yValue - 1] = 2;
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
                    resetPaintArray(PaintArrayTemp);
                }
            }
        }

        private void checkRight(int xValue, int yValue)
        {
            int c = 0;
            if (BoardArray[xValue, yValue] == 0)
            {
                yValue++;
                {
                    while (yValue < 7 && BoardArray[xValue, yValue] == notPlayerTurnInt)
                    {
                        PaintArrayTemp[xValue, yValue] = 1;
                        if (BoardArray[xValue, yValue + 1] == PlayerTurnInt)
                        {
                            c++;
                            if (c == 2)
                            {
                                break;
                            }
                            else
                            {
                                PaintArrayTemp[xValue, yValue + 1] = 2;
                            }
                        }
                        yValue++;
                    }
                }
                if (legalMove())
                {
                    copyTempArrayToPaintArray(xValue, yValue);
                    Console.WriteLine("right");
                }
                else
                {
                    resetPaintArray(PaintArrayTemp);
                }
            }
        }

        private void checkUp(int xValue, int yValue)
        {
            int c = 0;
            if (BoardArray[xValue, yValue] == 0)
            {
                xValue--;
                {
                    while (xValue > 0 && BoardArray[xValue, yValue] == notPlayerTurnInt)
                    {
                        PaintArrayTemp[xValue, yValue] = 1;
                        if (BoardArray[xValue - 1, yValue] == PlayerTurnInt)
                        {
                            c++;
                            if (c == 2)
                            {
                                break;
                            }
                            else
                            {
                                PaintArrayTemp[xValue - 1, yValue] = 2;
                            }
                        }
                        xValue--;
                    }
                }
                if (legalMove())
                {
                    copyTempArrayToPaintArray(xValue, yValue);
                    Console.WriteLine("up");
                }
                else
                {
                    resetPaintArray(PaintArrayTemp);
                }
            }
        }

        private void checkDown(int xValue, int yValue)
        {
            int c = 0;
            if (BoardArray[xValue, yValue] == 0)
            {
                xValue++;
                {
                    while (xValue < 7 && BoardArray[xValue, yValue] == notPlayerTurnInt)
                    {
                        PaintArrayTemp[xValue, yValue] = 1;
                        if (BoardArray[xValue + 1, yValue] == PlayerTurnInt)
                        {
                            c++;
                            if (c == 2)
                            {
                                break;
                            }
                            else
                            {
                                PaintArrayTemp[xValue + 1, yValue] = 2;
                            }
                        }
                        xValue++;
                    }
                }
                if (legalMove())
                {
                    copyTempArrayToPaintArray(xValue, yValue);
                    Console.WriteLine("down");
                }
                else
                {
                    resetPaintArray(PaintArrayTemp);
                }
            }
        }

        private void checkUpRight(int xValue, int yValue)
        {
            int c = 0;
            if (BoardArray[xValue, yValue] == 0)
            {
                yValue++;
                xValue--;
                {
                    while (xValue > 0 && yValue < 7 && BoardArray[xValue, yValue] == notPlayerTurnInt)
                    {
                        PaintArrayTemp[xValue, yValue] = 1;
                        if (BoardArray[xValue - 1, yValue + 1] == PlayerTurnInt)
                        {
                            c++;
                            if (c == 2)
                            {
                                break;
                            }
                            else
                            {
                                PaintArrayTemp[xValue - 1, yValue + 1] = 2;
                            }
                        }
                        yValue++;
                        xValue--;
                    }
                }
                if (legalMove())
                {
                    copyTempArrayToPaintArray(xValue, yValue);
                    Console.WriteLine("upRight");
                }
                else
                {
                    resetPaintArray(PaintArrayTemp);
                }
            }
        }

        private void checkUpLeft(int xValue, int yValue)
        {
            int c = 0;
            if (BoardArray[xValue, yValue] == 0)
            {
                yValue--;
                xValue--;
                {
                    while (xValue > 0 && yValue > 0 && BoardArray[xValue, yValue] == notPlayerTurnInt)
                    {
                        PaintArrayTemp[xValue, yValue] = 1;
                        if (BoardArray[xValue - 1, yValue - 1] == PlayerTurnInt)
                        {
                            c++;
                            if (c == 2)
                            {
                                break;
                            }
                            else
                            {
                                PaintArrayTemp[xValue - 1, yValue - 1] = 2;
                            }
                        }
                        yValue--;
                        xValue--;
                    }
                }
                if (legalMove())
                {
                    copyTempArrayToPaintArray(xValue, yValue);
                    Console.WriteLine("upLeft");
                }
                else
                {
                    resetPaintArray(PaintArrayTemp);
                }
            }
        }

        private void checkDownRight(int xValue, int yValue)
        {
            int c = 0;
            if (BoardArray[xValue, yValue] == 0)
            {
                yValue++;
                xValue++;
                {
                    while (xValue < 7 && yValue < 7 && BoardArray[xValue, yValue] == notPlayerTurnInt)
                    {
                        PaintArrayTemp[xValue, yValue] = 1;
                        if (BoardArray[xValue + 1, yValue + 1] == PlayerTurnInt)
                        {
                            c++;
                            if (c == 2)
                            {
                                break;
                            }
                            else
                            {
                                PaintArrayTemp[xValue + 1, yValue + 1] = 2;
                            }
                        }
                        yValue++;
                        xValue++;
                    }
                }
                if (legalMove())
                {
                    copyTempArrayToPaintArray(xValue, yValue);
                    Console.WriteLine("downRight");
                }
                else
                {
                    resetPaintArray(PaintArrayTemp);
                }
            }
        }

        private void checkDownLeft(int xValue, int yValue)
        {
            int c = 0;
            if (BoardArray[xValue, yValue] == 0)
            {
                yValue--;
                xValue++;
                {
                    while (xValue < 7 && yValue > 0 && BoardArray[xValue, yValue] == notPlayerTurnInt)
                    {
                        PaintArrayTemp[xValue, yValue] = 1;
                        if (BoardArray[xValue + 1, yValue - 1] == PlayerTurnInt)
                        {
                            c++;
                            if (c == 2)
                            {
                                break;
                            }
                            else
                            {
                                PaintArrayTemp[xValue + 1, yValue - 1] = 2;
                            }
                        }
                        yValue--;
                        xValue++;
                    }
                }
                if (legalMove())
                {
                    copyTempArrayToPaintArray(xValue, yValue);
                    Console.WriteLine("DownLeft");
                }
                else
                {
                    resetPaintArray(PaintArrayTemp);
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
                        BoardArray[x, y] = greenMarker;
                    }
                    else if (Board[x, y].Tag.Equals("white"))
                    {
                        BoardArray[x, y] = WhiteMarker;
                    }
                    else if (Board[x, y].Tag.Equals("black"))
                    {
                        BoardArray[x, y] = blackMarker;
                    }
                    Console.Write(BoardArray[x, y]);
                }
                Console.WriteLine();
            }

        }

        private Boolean checkIfGameOver()
        {
            int gameOverCounter = 0;
            for (int i = 0; i < 1; i++)
            {
                changeTurn(i);
                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        if (!checkAllDirections(x, y))
                        {
                            gameOverCounter++;
                        }
                    }
                }
            }
            paintList.Clear();
            resetPaintArray(PaintArray);
            resetPaintArray(PaintArrayTemp);
            if (gameOverCounter == 2)
            {

                return true;
            }
            return false;
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

        public int[,] BoardArray
        {
            get
            {
                return boardArray;
            }

            set
            {
                boardArray = value;
            }
        }

        public int[,] PaintArray
        {
            get
            {
                return paintArray;
            }

            set
            {
                paintArray = value;
            }
        }

        public int MoveScore
        {
            get
            {
                return moveScore;
            }

            set
            {
                moveScore = value;
            }
        }

        public int PlayerTurnInt
        {
            get
            {
                return playerTurnInt;
            }

            set
            {
                playerTurnInt = value;
            }
        }

        public int[,] PaintArrayTemp
        {
            get
            {
                return paintArrayTemp;
            }

            set
            {
                paintArrayTemp = value;
            }
        }

        public int Counter
        {
            get
            {
                return counter;
            }

            set
            {
                counter = value;
            }
        }

        public int WhiteMarker
        {
            get
            {
                return whiteMarker;
            }

            set
            {
                whiteMarker = value;
            }
        }
    }
}

