using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Othello.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using  Othello.Objects;

namespace Othello.Model

{
    public class GameLogic
    {
        private int tileValueX;
        private int tileValueY;
        private SaveBoard saveBoard = new SaveBoard();
        private string playerTurn;
        private int counter;
        private int moveScore;
        private int legalMoveCounter = 0;
        private int playerTurnInt = 1;
        private int notPlayerTurnInt = 2;
        private string gameScore = "";
        private int[,] paintArray = new int[8, 8];
        private int[,] paintArrayTemp;
        private List<int[,]> paintList = new List<int[,]>();
        private Boolean move;
        private Board board;

        public GameLogic(Board b)
        { 
            Counter = 0;
            board = b;
            PaintArrayTemp = new int[8, 8];
        }

        public void doLogic()
        {
            paint(PlayerTurn);
            paintList.Clear();
            resetPaintArray(PaintArray);
            updateBoardArray();
            storeBoardInXml();
            board.BoardArray = saveBoard.restoreSavedGame();
            Counter = saveBoard.Counter;
            move = false;
            changeTurn(Counter);
        }

        public void countScore()
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
                tempImage = board.White;

            }
            else
            {
                tempImage = board.Black;
            }
            for (int i = 0; i < paintList.Count; i++)
            {
                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        if (PaintArray[x, y] == 1)
                        {
                            move = true;
                            board.BoardPictureBox[x, y].Tag = player;
                            board.BoardPictureBox[x, y].Image = tempImage;

                        }
                    }
                }
            }
            if (move)
            {
                Counter++;
                board.BoardPictureBox[TileValueX, TileValueY].Tag = player;
                board.BoardPictureBox[TileValueX, TileValueY].Image = tempImage;
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
                    if (board.BoardPictureBox[x, y].Name.Equals("d4") || board.BoardPictureBox[x, y].Name.Equals("e5"))
                    {
                        board.BoardPictureBox[x, y].Image = board.White;
                        board.BoardPictureBox[x, y].Tag = "white";
                    }
                    else if (board.BoardPictureBox[x, y].Name.Equals("d5") || board.BoardPictureBox[x, y].Name.Equals("e4"))
                    {
                        board.BoardPictureBox[x, y].Image = board.Black;
                        board.BoardPictureBox[x, y].Tag = "black";
                    }
                    else
                    {
                        board.BoardPictureBox[x, y].Image = board.Green;
                        board.BoardPictureBox[x, y].Tag = "green";
                    }
                }
            }
            updateBoardArray();
        }

        public void changeTurn(int counter)
        {
            if (counter % 2 == 0)
            {
                PlayerTurn = "white";
                PlayerTurnInt = board.WhiteMarker;
                NotPlayerTurnInt = board.BlackMarker;
            }
            else
            {
                PlayerTurn = "black";
                PlayerTurnInt = board.BlackMarker;
                NotPlayerTurnInt = board.WhiteMarker;
            }
        }

        public void extractValues(char f, char l)
        {
            TileValueX = (int)Char.GetNumericValue(l);
            TileValueX--;
            switch (f)
            {
                case 'a':
                    TileValueY = 0;
                    break;
                case 'b':
                    TileValueY = 1;
                    break;
                case 'c':
                    TileValueY = 2;
                    break;
                case 'd':
                    TileValueY = 3;
                    break;
                case 'e':
                    TileValueY = 4;
                    break;
                case 'f':
                    TileValueY = 5;
                    break;
                case 'g':
                    TileValueY = 6;
                    break;
                case 'h':
                    TileValueY = 7;
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

        public Boolean legalMove(int xValue, int yValue)
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (PaintArrayTemp[x, y] == 2)
                    {
                        LegalMoveCounter++;
                        copyTempArrayToPaintArray(xValue, yValue);
                        return true;
                    }
                }
            }
            resetPaintArray(PaintArrayTemp);
            return false;
        }

        private void updateBoardArray()
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (board.BoardPictureBox[x, y].Tag.Equals("green"))
                    {
                        board.BoardArray[x, y] = board.GreenMarker;
                    }
                    else if (board.BoardPictureBox[x, y].Tag.Equals("white"))
                    {
                        board.BoardArray[x, y] = board.WhiteMarker;
                    }
                    else if (board.BoardPictureBox[x, y].Tag.Equals("black"))
                    {
                        board.BoardArray[x, y] = board.BlackMarker;
                    }
                }
            }
        }

     /*   private Boolean checkIfGameOver()
        {
            int gameOverCounter = 0;
            for (int i = 0; i < 1; i++)
            {
                changeTurn(i);
                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        if (checkAllDirections(x, y))
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
        */
        private void storeBoardInXml()
        {
            saveBoard.storeBoard(board.BoardArray, counter);
        }

        public void restoreSavedGame()
        {
            board.BoardArray = saveBoard.restoreSavedGame();
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (board.BoardArray[x, y] == board.WhiteMarker)
                    {
                        board.BoardPictureBox[x, y].Image = board.White;
                        board.BoardPictureBox[x, y].Tag = "white";
                    }
                    else if (board.BoardArray[x, y] == board.BlackMarker)
                    {
                        board.BoardPictureBox[x, y].Image = board.Black;
                        board.BoardPictureBox[x, y].Tag = "black";
                    }
                    else
                    {
                        board.BoardPictureBox[x, y].Image = board.Green;
                        board.BoardPictureBox[x, y].Tag = "green";
                    }
                }
            }
            Counter = saveBoard.Counter;
        }

        private string currentScore()
        {
            string score = "";
            int blackScore = 0;
            int whiteScore = 0; ;
            for(int x = 0; x < 8;x++)
            {
                for(int y= 0; y < 8; y++)
                {
                    if (board.BoardArray[x, y] == board.BlackMarker)
                    {
                        blackScore++;
                    }
                    else if(board.BoardArray[x, y] == board.WhiteMarker)
                    {
                        whiteScore++;
                    }
                }
            }
            score = ("White score: " + whiteScore + " " + "Black score: " + blackScore);
            Action<string> onNewScore = GameScore;
            if(onNewScore  != null)
            {
                onNewScore(score);
            }
            return score;
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

        public int LegalMoveCounter
        {
            get
            {
                return legalMoveCounter;
            }

            set
            {
                legalMoveCounter = value;
            }
        }

        public int TileValueX
        {
            get
            {
                return tileValueX;
            }

            set
            {
                tileValueX = value;
            }
        }

        public int TileValueY
        {
            get
            {
                return tileValueY;
            }

            set
            {
                tileValueY = value;
            }
        }

        public int NotPlayerTurnInt
        {
            get
            {
                return notPlayerTurnInt;
            }

            set
            {
                notPlayerTurnInt = value;
            }
        }

        public string PlayerTurn
        {
            get
            {
                return playerTurn;
            }

            set
            {
                playerTurn = value;
            }
        }

        public Action<string> GameScore { get; set; }
    }
}

