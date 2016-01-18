using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Othello.Objects;

namespace Othello.Model
{
    public class AI
    {
        private Board board;
        private GameLogic gameLogic;
        private GameRules gameRules;
        private List<int[]> coordinates;
        private List<int[,]> listOfCurrentCheckTilesToChange;
        private int score;
        private int[] aiPressedTile;
        private int[,] currentCheckTilesToChange;
        private Timer roundTimer;
        private int[] tileClicked;

        public AI(GameRules gr, Board b, GameLogic g)
        {
            InitTimer();
            board = b;
            tileClicked = new int[2];
            aiPressedTile = new int[2];
            currentCheckTilesToChange = new int[8, 8];
            listOfCurrentCheckTilesToChange = new List<int[,]>();
            coordinates = new List<int[]>();
            score = 0;
            gameRules = gr;
            gameLogic = g;
        }

        public void aiTurn()
        {
            score = 0;
            getAllGreenTiles();
            compareScore();
            gameLogic.CurrentRoundTilesToChange = new int[8,8];
            gameRules.makeMove(tileClicked);
        }

        public void getAllGreenTiles()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    if (board.BoardArray[row, column] == 0)
                    {
                        if (gameRules.checkAllDirections(row, column))
                        {
                            int[] tempCoords = new int[2];
                            tempCoords[0] = row;
                            tempCoords[1] = column;
                            coordinates.Add(tempCoords);
                            copyArray();
                            storeCheckedTiles();
                        }
                    }
                }
            }
        }

        private void copyArray()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int y = 0; y < 8; y++)
                {
                    currentCheckTilesToChange[row, y] = gameLogic.CurrentCheckTilesToChange[row, y];
                }
            }
        }

        public void storeCheckedTiles()
        {
            int[,] temp = new int[8, 8];
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    if (currentCheckTilesToChange[row, column] == 1)
                    {
                        temp[row, column] = 1;
                    }
                    else
                    {
                        temp[row, column] = 0;
                    }
                }
            }
            listOfCurrentCheckTilesToChange.Add(temp);
        }

        public void compareScore()
        {
            int tempScore = 0;
            int[,] tempArray = new int[8, 8];
            int[] tempCoordinates = new int[2];
            int tempRow = 0;
            int tempColumn = 0;
            for (int i = 0; i < listOfCurrentCheckTilesToChange.Count; i++)
            {
                tempCoordinates = coordinates[i];
                tempArray = listOfCurrentCheckTilesToChange[i];
                for (int row = 0; row < 8; row++)
                {
                    for (int column = 0; column < 8; column++)
                    {

                        if (tempArray[row, column] == 1)
                        {
                            tempScore++;
                        }
                    }
                }
                if (tempScore >= score)
                {
                    score = tempScore;
                    tempRow = tempCoordinates[0];
                    tempColumn = tempCoordinates[1];
                }
            }
                tileClicked[0] = tempRow;
                tileClicked[1] = tempColumn;
        }

        public void switcherChanged(int switcher)
        {
            if (gameLogic.PlayerTurnInt == gameLogic.YourTurn)
            {
                roundTimer.Start();
            }
        }

        private void roundTimer_tick(object sender, EventArgs e)
        {
            aiTurn();
            roundTimer.Stop();
        }
        
        public void InitTimer()
        {
            roundTimer = new Timer();
            roundTimer.Interval = 1000;
            roundTimer.Tick += new EventHandler(roundTimer_tick);
        }
    }
}
