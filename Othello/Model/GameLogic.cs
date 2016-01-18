using System;
using Othello.Linq;
using Othello.Objects;
using System.Windows.Forms;

namespace Othello.Model

{
    public class GameLogic
    {
        private SaveBoard saveBoard;
        private Board board;
        private string playerTurn;
        private int counter;
        private int legalMoveCounter = 0;
        private int playerTurnInt = 0;
        private int notPlayerTurnInt = 2;
        private int[,] currentRoundTilesToChange;
        private int[,] currentCheckTilesToChange;
        private int yourTurn;
        private int blackScore;
        private int whiteScore;
        private string currentScore;
        private Boolean[] gameOverCheck;
        private Boolean gameOver;
        private int switcher;

        public GameLogic(Board b, SaveBoard s)
        {
            Counter = 0;
            board = b;
            saveBoard = s;
            GameOverCheck= new Boolean[]{false,false };
            gameOver = false;
            CurrentRoundTilesToChange = new int[8, 8];
            CurrentCheckTilesToChange = new int[8, 8];
        }

        public void doLogic(int[] tileClicked)
        {
            flipTiles(tileClicked);
            CurrentRoundTilesToChange = new int[8, 8];
            storeBoardInXml();
            restoreSavedGame();
            CurrentScore = calculateCurrentScore();
            changeTurn(Counter);
        }

        public void boardArrayChanged(int[,] newArray)
        {
            board.BoardArray = newArray;
        }

        private Boolean flipTiles(int[] tileClicked)
        {
            bool move = false;

            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    if (CurrentRoundTilesToChange[row, column] == 1)
                    {
                        move = true;
                        board.BoardArray[row, column] = playerTurnInt;
                    }
                }
            }
            if (move)
            {
                Counter++;
                board.BoardArray[tileClicked[0], tileClicked[1]] = playerTurnInt;
            }
            return true;
        }

        public void changeTurn(int counter)
        {   
            if (counter % 2 == 0)
            {
                PlayerTurn = "white";
                PlayerTurnInt = board.WhiteMarker;
                NotPlayerTurnInt = board.BlackMarker;
                Switcher++;
            }
            else
            {
                PlayerTurn = "black";
                PlayerTurnInt = board.BlackMarker;
                NotPlayerTurnInt = board.WhiteMarker;
                Switcher++;
            }   
        }

        private void copyCheckedTilesToRound(int selectedRow, int selectedColumn)
        {
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    if (CurrentCheckTilesToChange[row, column] == 1)
                    {
                        CurrentRoundTilesToChange[row, column] = 1;
                    }
                }
            }
            currentCheckTilesToChange = new int[8, 8];
        }

        public Boolean legalMove(int selectedRow, int selectedColumn)
        {
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    if (CurrentCheckTilesToChange[row, column] == 2)
                    {
                        LegalMoveCounter++;
                        copyCheckedTilesToRound(selectedRow, selectedColumn);
                        return true;
                    }
                }
            }
            CurrentCheckTilesToChange = new int[8, 8];
            return false;
        }

        public void gameIsOver()
        {
            board.BoardArray = new int[8, 8];
            GameOver = true;
            GameOver = false;
        }

        private void storeBoardInXml()
        {
            saveBoard.storeBoard(board.BoardArray, counter);
        }

        public void restoreSavedGame()
        {
            board.BoardArray = saveBoard.restoreSavedGame();
            Counter = saveBoard.Counter;
            CurrentScore = calculateCurrentScore();
        }

        public string calculateCurrentScore()
        {
            blackScore = 0;
            whiteScore = 0;
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    if (board.BoardArray[row, column] == board.BlackMarker)
                    {
                        blackScore++;
                    }
                    else if (board.BoardArray[row, column] == board.WhiteMarker)
                    {
                        whiteScore++;
                    }
                }
            }


            string score = ("White score: " + whiteScore + " " + "Black score: " + blackScore);
            return score;
        }

        public string winner()
        {
            if (whiteScore < blackScore)
            {
                return ("Black is the winner!");
            }
            else if (whiteScore > blackScore)
            {
                return ("White is the winner!");
            }
            else
            {
                return ("Draw!");
            }
        }

        private void roundTimer_tick(object sender, EventArgs e)
        {
            Switcher++;
        }

        public Action<int> onTurnChange
        {
            get;
            set;
        }

        public Action<Boolean> onGameOverChange
        {
            get;
            set;
        }

        public Action<string> onScoreChange
        {
            get;
            set;
        }

        public Action<int> onSwitcherChange
        {
            get;
            set;
        }

        public int Switcher
        {
            get
            {
                return switcher;
            }

            set
            {
                    switcher = value;
                    Action<int> localOnChange = onSwitcherChange;
                    if (localOnChange != null)
                    {
                        localOnChange(value);
                    }
            }
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

        public int[,] CurrentCheckTilesToChange
        {
            get
            {
                return currentCheckTilesToChange;
            }

            set
            {
                currentCheckTilesToChange = value;
            }
        }

        public int[,] CurrentRoundTilesToChange
        {
            get
            {
                return currentRoundTilesToChange;
            }

            set
            {
                currentRoundTilesToChange = value;
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

        public bool[] GameOverCheck
        {
            get
            {
                return gameOverCheck;
            }

            set
            {
                gameOverCheck = value;
            }
        }

        public bool GameOver
        {
            get
            {
                return gameOver;
            }

            set
            {
                if (gameOver != value)
                {
                    gameOver = value;
                    Action<Boolean> localOnChange = onGameOverChange;
                    if (localOnChange != null)
                    {
                        localOnChange(value);
                    }
                }
            }
        }
    }
}

