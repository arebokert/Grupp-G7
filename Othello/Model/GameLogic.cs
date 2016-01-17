using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Othello.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Othello.Objects;

namespace Othello.Model

{
    public class GameLogic
    {
        private int tileValueX;
        private int tileValueY;
        private SaveBoard saveBoard;
        private string playerTurn;
        private int counter;
        private int legalMoveCounter = 0;
        private int playerTurnInt = 0;
        private int notPlayerTurnInt = 2;
        private int[,] paintArray = new int[8, 8];
        private int[,] paintArrayTemp;
        private int yourTurn;
        private string currentScore;
        private List<int[,]> paintList = new List<int[,]>();
        private Board board;

        public GameLogic(Board b, SaveBoard s)
        {
            Counter = 0;
            board = b;
            PaintArrayTemp = new int[8, 8];
            saveBoard = s;
        }

        public void doLogic(int[] tileClicked)
        {
            paint(tileClicked);
            paintList.Clear();
            resetPaintArray(PaintArray);
            storeBoardInXml();
            restoreSavedGame();
            CurrentScore = calcCurrentScore();
            changeTurn(Counter);
        }

        public void boardArrayChanged(int[,] newArray)
        {
            board.BoardArray = newArray;
        } 

        private Boolean paint(int[] tileClicked)
        {
            bool move = false;

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (PaintArray[x, y] == 1)
                    {
                        move = true;
                        board.BoardArray[x, y]= playerTurnInt;
                    }
                }
            }
            if (move)
            {
                Counter++;
                board.BoardArray[tileClicked[0], tileClicked[1]]=playerTurnInt;
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

        private void storeBoardInXml()
        {
            saveBoard.storeBoard(board.BoardArray, counter);
        }

        public void restoreSavedGame()
        {
            board.BoardArray = saveBoard.restoreSavedGame();         
            Counter = saveBoard.Counter;
            CurrentScore = calcCurrentScore();
        }

        public string calcCurrentScore()
        {
            int blackScore = 0;
            int whiteScore = 0; ;
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (board.BoardArray[x, y] == board.BlackMarker)
                    {
                        blackScore++;
                    }
                    else if (board.BoardArray[x, y] == board.WhiteMarker)
                    {
                        whiteScore++;
                    }
                }
            }
           string score = ("White score: " + whiteScore + " " + "Black score: " + blackScore);
           return score;
        }

        public Action<int> onTurnChange
        {
            get;
            set;
        }

        public Action<string> onScoreChange
        {
            get;
            set;
        }

        public string CurrentScore
        {
            get
            {
                return currentScore;
            }

            set
            {
                if (currentScore != value)
                {
                    currentScore = value;
                    Action<string> localOnChange = onScoreChange;
                    if (localOnChange != null)
                    {
                        localOnChange(value);
                    }
                }
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

        public int PlayerTurnInt
        {
            get
            {
                return playerTurnInt;
            }

            set
            {
                if (playerTurnInt != value)
                {
                    playerTurnInt = value;
                    Action<int> localOnChange = onTurnChange;
                    if (localOnChange != null)
                    {
                        localOnChange(value);
                    }
                }
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

        public int YourTurn
        {
            get
            {
                return yourTurn;
            }

            set
            {
                yourTurn = value;
            }
        }
    }
}

